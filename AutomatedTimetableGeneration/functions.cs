using AutomatedTimetableGeneration.Classes;
using AutomatedTimetableGeneration.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using WebApplication5.Models;


namespace AutomatedTimetableGeneration
{
    public class functions
    {
       

        public List<Assignement> Generate_lect(ref List<GroupModel> GrpsAfterLecs)
        {

            List<GroupModel> GrpList = new List<GroupModel>();
            CollegeDatabaseEntities10 _db = new CollegeDatabaseEntities10();
            List<Variables> variables = new List<Variables>();
            //   List<CSPModel> CSP = new List<CSPModel>();
            //  List<GroupModel> GrpList = new List<GroupModel>();

            var grps = _db.Groups.ToList();
            for (int i = 0; i < grps.Count; i++)
            {
                GrpList.Add(new GroupModel(grps[i].ID, grps[i].Name, grps[i].AcadmicYear_id));
            }




            var list = (from
                         course in _db.Courses
                        where course.Doctor_Available_Time.Any()
                        select new { course, AcademicYear_id = course.AcademicYear.ID, availTime = _db.Doctor_Available_Time.Where(x => x.Course_id == course.ID).ToList() }).ToList();




            foreach (var item in list)
            {


                var grpid = (from x in _db.Groups
                             where x.AcadmicYear_id == item.AcademicYear_id
                             //where x.Department_id == null
                             // where x.Name == item.course.Department.Name
                             select x).ToList();

                // hna lw el sna 1 , 2 , 3  ha3ml course L kol group  , lw sana rab3a fa ha3ml course l kol group f nfs el dept ;
                if (grpid[0].Department_id == null)
                {

                    var rooms = (from room in _db.Rooms
                                 join rType in _db.RoomTypes on room.RoomType_id equals rType.ID
                                 where (rType.Name == "Hall")
                                 where (room.Capacity >= (item.course.AcademicYear.StudentsCount / item.course.AcademicYear.GroupCount))
                                 select room).ToList();

                    var doctors = _db.LinkDoctorCourses.Where(x => x.Course_id == item.course.ID).Select(x => x.Doctor_id).ToList();

                    foreach (var item2 in grpid)
                    {
                        variables.Add(new Variables(rooms, item2.ID, item.course, item.availTime, doctors));
                    }
                }
                else
                {
                    var depts = _db.LinkCourseDepts.Where(x => x.Course_id == item.course.ID).ToList();
                    foreach (var item2 in depts)
                    {
                        var grpsForSameDept = grpid.Where(x => x.Department_id == item2.Department_id).ToList();



                        var deptCapacity = grpsForSameDept[0].Department.Capacity;
                        var rooms = (from room in _db.Rooms
                                     join rType in _db.RoomTypes on room.RoomType_id equals rType.ID
                                     where (rType.Name == "Hall")
                                     where (room.Capacity >= (deptCapacity / grps.Count))
                                     select room).ToList();
                        var doctors = _db.LinkDoctorCourses.Where(x => x.Course_id == item.course.ID).Select(x => x.Doctor_id).ToList();

                        foreach (var item3 in grpsForSameDept)
                        {
                            variables.Add(new Variables(rooms, item3.ID, item.course, item.availTime, doctors));
                        }
                    }

                }

            }


            List<Assignement> finalresult = new List<Assignement>();
            finalresult = Recursive_Backtracking_Search(new List<Assignement>(),variables,ref GrpList);
            GrpsAfterLecs = GrpList;
            return finalresult;


        }

        private static List<Assignement> Recursive_Backtracking_Search(List<Assignement> assignment, List<Variables> variables,ref List<GroupModel> GrpList)

        {

            CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();
            if (assignment.Count == variables.Count)
                return assignment;
            var unSelectedVar = Select_UnAssigned_Course(assignment, variables);
            var allDeptsForCourse = db.LinkCourseDepts.Where(x => x.Course_id == unSelectedVar.course.ID).ToList();
            List<Group> allgroups = new List<Group>();
           
            if (allDeptsForCourse.Count > 1)
            {

                foreach (var dept in allDeptsForCourse)
                {
                    var grpsfordept = db.Groups.Where(x => x.Department_id == dept.Department_id).ToList();
                    foreach (var item in grpsfordept)
                    {
                        allgroups.Add(item);
                    }
                }
            }
            //   var domain_Values_for_unselctedVar = Domain.Where(x => x.RoomId == unSelectedVar).ToList();
            for (int i = 0; i < unSelectedVar.Rooms.Count; i++)
            {
                for (int j = 0; j < unSelectedVar.DocAvailTime.Count; j++)
                {


                    if (allDeptsForCourse.Count > 1)
                    {
                        if (isConsistentForDepts(unSelectedVar.Rooms[i].ID, unSelectedVar.DocAvailTime[j], assignment, GrpList, allgroups, unSelectedVar.Doc_ids))

                        {
                            foreach (var item in allgroups)
                            {
                                assignment.Add(new Assignement(unSelectedVar.course.Name, item.ID, unSelectedVar.course.AcademicYear_id
                              , unSelectedVar.DocAvailTime[j].DayOfWeek, unSelectedVar.DocAvailTime[j].StartHour,
                              unSelectedVar.DocAvailTime[j].StartHour + unSelectedVar.course.Hours,
                              unSelectedVar.Rooms[i].ID, unSelectedVar.course.ID, unSelectedVar.Doc_ids));

                            }
                            var result = Recursive_Backtracking_Search(assignment, variables, ref GrpList);

                            if (result[0].CourseName != "Failure")
                            { return result; }
                            else
                            {
                                foreach (var item in allgroups)
                                {
                                    assignment.Remove(assignment.Where(x => x.Course_id == unSelectedVar.course.ID && x.GrpId == item.ID).Single());
                                    GrpList.Where(x => x.GrpId == item.ID).Single().Week[unSelectedVar.DocAvailTime[j].DayOfWeek - 1].Removeinterval(unSelectedVar.course.ID);
                                }

                            }
                        }

                    }
                    else
                    {
                        if (isConsistent(unSelectedVar.Rooms[i].ID, unSelectedVar.DocAvailTime[j], assignment, GrpList.Where(x => x.GrpId == unSelectedVar.GroupId).Single(), unSelectedVar.Doc_ids))
                        {
                            assignment.Add(new Assignement(unSelectedVar.course.Name, unSelectedVar.GroupId, unSelectedVar.course.AcademicYear_id
                                , unSelectedVar.DocAvailTime[j].DayOfWeek, unSelectedVar.DocAvailTime[j].StartHour,
                                unSelectedVar.DocAvailTime[j].StartHour + unSelectedVar.course.Hours,
                                unSelectedVar.Rooms[i].ID, unSelectedVar.course.ID, unSelectedVar.Doc_ids));

                            

                            var result = Recursive_Backtracking_Search(assignment, variables, ref GrpList);

                            if (result[0].CourseName != "Failure")
                            { return result; }
                            else
                            {
                                assignment.Remove(assignment.Where(x => x.Course_id == unSelectedVar.course.ID && x.GrpId == unSelectedVar.GroupId).Single());
                                GrpList.Where(x => x.GrpId == unSelectedVar.GroupId).Single().Week[unSelectedVar.DocAvailTime[j].DayOfWeek - 1].Removeinterval(unSelectedVar.course.ID);
                            }
                        }
                    }
                }
            }



            return new List<Assignement> { new Assignement("Failure", 2, 2, 2, 2, 2, 2, 46, unSelectedVar.Doc_ids) };
        }

        private static bool isConsistent(int roomid, Doctor_Available_Time doctor_Available_Time, List<Assignement> assignment, GroupModel GrpTable, List<string> doc_ids)
        {
            CollegeDatabaseEntities10 _db = new CollegeDatabaseEntities10();
            //var s = _db.lin
            // var num = _db.doctor_Available_Time.Course_id;
            foreach (var item in assignment)
            {

                for (int i = 0; i < doc_ids.Count; i++)
                {
                    if ((doctor_Available_Time.DayOfWeek == item.Day) && ((doctor_Available_Time.StartHour >= item.StartHour && doctor_Available_Time.StartHour < item.EndHour)
                            || (doctor_Available_Time.StartHour + doctor_Available_Time.Course.Hours > item.StartHour
                            && doctor_Available_Time.StartHour + doctor_Available_Time.Course.Hours <= item.EndHour)
                            || (item.StartHour >= doctor_Available_Time.StartHour && item.StartHour < doctor_Available_Time.StartHour + doctor_Available_Time.Course.Hours)
                            || (item.EndHour > doctor_Available_Time.StartHour && item.EndHour <= doctor_Available_Time.StartHour + doctor_Available_Time.Course.Hours))
                            && (item.RoomId == roomid || item.Doc_ids.Contains(doc_ids[i]) || GrpTable.GrpId == item.GrpId))
                    {
                        return false;
                    }
                }



            }
            GrpTable.Week[doctor_Available_Time.DayOfWeek - 1].addinterval(doctor_Available_Time.Course.ID, roomid, doctor_Available_Time.StartHour,
                doctor_Available_Time.StartHour + doctor_Available_Time.Course.Hours);
            //CollegeDatabaseEntities2 _db = new CollegeDatabaseEntities2();

            //var adminConstraints = _db.AcademicYears.Where(x => x.ID == doctor_Available_Time.Course.AcademicYear_id).Single();
            if (GrpTable.FreeDaysCount() < 1)
            {
                GrpTable.Week[doctor_Available_Time.DayOfWeek - 1].Removeinterval(doctor_Available_Time.Course.ID);
                return false;
            }


            /*if (GrpTable.Week[doctor_Available_Time.DayOfWeek - 1].NumfHours > doctor_Available_Time.Course.AcademicYear.DayWorkHours)
            {
                GrpTable.Week[doctor_Available_Time.DayOfWeek - 1].Removeinterval(doctor_Available_Time.Course.ID);
                return false;
            }*/

            var TempSecHours = _db.Courses.OrderBy(x => x.SectionHours).Select(x => x.SectionHours).Where(x => x.HasValue).Take(1).Single();/* Select(x=>x.SectionHours).Take(1).Last();*/
            var TempLabHours = _db.Courses.OrderBy(x => x.LabHours).Select(x => x.LabHours).Where(x => x.HasValue).Take(1).Single();


            if (GrpTable.Week[doctor_Available_Time.DayOfWeek - 1].Gap > doctor_Available_Time.Course.AcademicYear.AllowedGapHours)
            {
                var GapAfterSecs = GrpTable.Week[doctor_Available_Time.DayOfWeek - 1].Gap % TempSecHours;
                var GapAfterLabs = GrpTable.Week[doctor_Available_Time.DayOfWeek - 1].Gap % TempLabHours;
                if (GapAfterSecs > doctor_Available_Time.Course.AcademicYear.AllowedGapHours && GapAfterLabs > doctor_Available_Time.Course.AcademicYear.AllowedGapHours)
                {
                    GrpTable.Week[doctor_Available_Time.DayOfWeek - 1].Removeinterval(doctor_Available_Time.Course.ID);
                    return false;
                }

            }
            return true;

        }

        private static Variables Select_UnAssigned_Course(List<Assignement> assignement, List<Variables> variables)
        {
            foreach (var item in variables)
            {
                if (assignement.Where(x => x.GrpId == item.GroupId && x.Course_id == item.course.ID).Select(x => x.CourseName).SingleOrDefault() == null)
                { return item; }
            }
            return null;
        }

        private static bool isConsistentForDepts(int roomid, Doctor_Available_Time doctor_Available_Time, List<Assignement> assignment, List<GroupModel> GrpTable, List<Group> allGrps, List<string> doc_ids)
        {
            CollegeDatabaseEntities10 _db = new CollegeDatabaseEntities10();
            //var s = _db.lin
            // var num = _db.doctor_Available_Time.Course_id;
            foreach (var item in assignment)
            {
                /*if ((doctor_Available_Time.DayOfWeek == item.Day) && (doctor_Available_Time.StartHour >= item.StartHour || doctor_Available_Time.StartHour <= item.EndHour) && item.RoomId == roomid)
                {
                    return false;

                }*/
                for (int j = 0; j < allGrps.Count; j++)
                {
                    for (int i = 0; i < doc_ids.Count; i++)
                    {
                        if ((doctor_Available_Time.DayOfWeek == item.Day) && ((doctor_Available_Time.StartHour >= item.StartHour && doctor_Available_Time.StartHour < item.EndHour)
                            || (doctor_Available_Time.StartHour + doctor_Available_Time.Course.Hours > item.StartHour &&
                            doctor_Available_Time.StartHour + doctor_Available_Time.Course.Hours <= item.EndHour)
                            || (item.StartHour >= doctor_Available_Time.StartHour && item.StartHour < doctor_Available_Time.StartHour + doctor_Available_Time.Course.Hours)
                            || (item.EndHour > doctor_Available_Time.StartHour && item.EndHour <= doctor_Available_Time.StartHour + doctor_Available_Time.Course.Hours))
                            && (item.RoomId == roomid || item.Doc_ids.Contains(doc_ids[i]) || allGrps[j].ID == item.GrpId))
                        {
                            return false;
                        }
                    }
                }
            }

            List<GroupModel> grps = new List<GroupModel>();
            for (int i = 0; i < allGrps.Count; i++)
            {
                GrpTable.Where(x => x.GrpId == allGrps[i].ID).Single().Week[doctor_Available_Time.DayOfWeek - 1].addinterval(doctor_Available_Time.Course.ID, roomid,
                    doctor_Available_Time.StartHour, doctor_Available_Time.StartHour + doctor_Available_Time.Course.Hours);

                grps.Add(GrpTable.Where(x => x.GrpId == allGrps[i].ID).Single());
                //               
            }
          
            for (int j = 0; j < grps.Count; j++)
            {


               
                if (grps[j].FreeDaysCount() < 1)
                {

                    foreach (var item in grps)
                    {
                        GrpTable.Where(x => x.GrpId == item.GrpId).Single().Week[doctor_Available_Time.DayOfWeek - 1].Removeinterval(doctor_Available_Time.Course.ID);
                    }
                    return false;
                }


                if (grps[j].Week[doctor_Available_Time.DayOfWeek - 1].NumfHours > doctor_Available_Time.Course.AcademicYear.DayWorkHours)
                {
                    foreach (var item in grps)
                    {
                        GrpTable.Where(x => x.GrpId == item.GrpId).Single().Week[doctor_Available_Time.DayOfWeek - 1].Removeinterval(doctor_Available_Time.Course.ID);
                    }
                    return false;
                }

                var TempSecHours = _db.Courses.OrderBy(x => x.SectionHours).Select(x => x.SectionHours).Take(1).Single();/* Select(x=>x.SectionHours).Take(1).Last();*/
                var TempLabHours = _db.Courses.OrderBy(x => x.LabHours).Select(x => x.LabHours).Take(1).Single();


                if (grps[j].Week[doctor_Available_Time.DayOfWeek - 1].Gap > doctor_Available_Time.Course.AcademicYear.AllowedGapHours)
                {
                    var GapAfterSecs = grps[j].Week[doctor_Available_Time.DayOfWeek - 1].Gap % TempSecHours;
                    var GapAfterLabs = grps[j].Week[doctor_Available_Time.DayOfWeek - 1].Gap % TempLabHours;
                    if (GapAfterSecs > doctor_Available_Time.Course.AcademicYear.AllowedGapHours && GapAfterLabs > doctor_Available_Time.Course.AcademicYear.AllowedGapHours)
                    {
                        foreach (var item in grps)
                        {
                            GrpTable.Where(x => x.GrpId == item.GrpId).Single().Week[doctor_Available_Time.DayOfWeek - 1].Removeinterval(doctor_Available_Time.Course.ID);

                            
                        }
                       
                        return false;
                    }

                }
            }
            return true;

        }

        /// <summary>
        /// //sectionnnnnnnnnnnn
        /// </summary>
        /// <param name="sortedTAList"></param>
        /// <param name="courseList"></param>
        /// <param name="taForCourse"></param>
        ///

        private static List<SectionsVariables> SectionIntialize()
        {
            CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();
            List<SectionsVariables> SectionVar = new List<SectionsVariables>();
           

            var list = (from
                         course in db.Courses
                       where( course.Doctor_Available_Time.Any()&&course.AcademicYear_id==2) 
                       select new { course }).ToList();




            foreach (var crs in list)
            {
                if (crs.course.HaveLab|| crs.course.HaveSection)
                {
                    var grps = crs.course.AcademicYear.Groups.ToList();

                    foreach (var grp in grps)
                    {
                        var sections = db.Sections.Where(x => x.Grp_id == grp.ID).ToList();
                        for (int i = 0; i < sections.Count; i++)
                        {
                            if (crs.course.HaveLab)
                                SectionVar.Add(new SectionsVariables(sections[i].ID, crs.course.ID, grp.ID, crs.course.HaveLab, false,crs.course.LabHours.Value,0));
                            if (crs.course.HaveSection)
                                SectionVar.Add(new SectionsVariables(sections[i].ID, crs.course.ID, grp.ID, false, crs.course.HaveSection,0, crs.course.SectionHours.Value));

                        }
                    }

                }

            }
            return SectionVar;
        }

        private static List<SectionTimeTable> SectionTimeTableIntialize(List<GroupModel> GrpList)
        {
            CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();
            var sections = db.Sections.ToList();
            List<SectionTimeTable> list = new List<SectionTimeTable>();
            foreach (var item in sections)
            {
                list.Add(new SectionTimeTable(item.Grp_id.Value,item.Year_id,item.ID));
             
                var grp_timetable = GrpList.Where(x => x.GrpId == item.Grp_id).Single();
                for (int i = 0; i < grp_timetable.Week.Count(); i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        if (grp_timetable.Week[i].Slots[j] == true)
                            list.Last().Week[i].Slots[j] = true;
                        else
                            list.Last().Week[i].Slots[j] = false;
                        //list.Last().Week[i].Slots = grp_timetable.Week[i].Slots;
                    }
                    list.Last().Week[i].GetNoHours();
                }
               
            }
            return list;

        }
        private static List<AvailTimeforgroup> avilabletimegrpinit(List<GroupModel> GrpList)
        {
            List<AvailTimeforgroup> AvailForGrp = new List<AvailTimeforgroup>();
            foreach (var item in GrpList)
            {
                AvailForGrp.Add(new AvailTimeforgroup(item.GrpId));
                foreach (var item2 in item.Week)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        if (!item2.Slots[i])
                            AvailForGrp.Last().Week[item2.Id].AvailableTimes.Add(i);
                    }
                }
            }
            return AvailForGrp;
        }
        private static List<TaTimetable> Mo3eeeeed()
        {
            CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();
            var roles = db.AspNetRoles.Where(x => x.Name == "TA").ToList();
            var mo3ed = roles[0].AspNetUsers.ToList();
            List<TaTimetable> talist = new List<TaTimetable>();
            foreach (var item in mo3ed)
            {
                var crsformo3edd = db.LinkDoctorCourses.Where(x => x.Doctor_id==item.Id).ToList();
                List<KeyValuePair<int, int?>> hosam = new List<KeyValuePair<int, int?>>();
                foreach (var itemo in crsformo3edd)
                {
                    hosam.Add(new KeyValuePair<int, int?>(itemo.Course_id, itemo.hours));
                }
                talist.Add(new TaTimetable(item.Id, hosam));

            }
            
            return talist;
        }
        private static List<RoomsTimeTables> Roominit(List<GroupModel>GrpList)
        {

            CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();
            List<RoomsTimeTables> rom = new List<RoomsTimeTables>();
            // List<RoomDay> romd = new List<RoomDay>();
            var rooms = db.Rooms.ToList();
            foreach (var item in rooms)
            {
                rom.Add(new RoomsTimeTables(item.ID,item.RoomType_id));

            }
            foreach (var item in GrpList)
            {
                foreach (var item2 in item.Week)
                {
                    foreach (var item3 in item2.CourseStartEnd)
                    {

                        var ind = rom.IndexOf(rom.Where(x => x.Roomid == item3.Value.RoomId).Single());
                        rom[ind].Week[item2.Id].ReserveInterval(item3.Value.Start-8, item3.Value.End-1-8);
                    }
                }
            }
            return rom;
        }
        private static SectionsVariables Select_UnAssigned_Section(List<SectionAssignement> assignement, List<SectionsVariables> variables)
        {
            foreach (var item in variables)
            {
                if (assignement.Where(x => x.CourseId == item.CourseId && x.SectionId == item.SectionId&&x.IsLab==item.HaveLab&&x.IsSection==item.HaveSections).SingleOrDefault() == null)
                { return item; }
            }
            return null;
        }

        private static List<SectionAssignement> Recursive_Backtracking_Search_Sections(List<SectionAssignement> assignment, List<SectionsVariables> variables,
            ref List<SectionTimeTable> SecTimeTable, List<AvailTimeforgroup> availTimeGrp, List<TaTimetable> taTimetable, ref List<RoomsTimeTables> roomsTimeTable)
        {

            if (assignment.Count == variables.Count)
                return assignment;

            SectionsVariables UnSection = Select_UnAssigned_Section(assignment, variables);
            int noofhours = 0;
            if (UnSection.HaveLab)
                noofhours = UnSection.LabHours;
            else
                noofhours = UnSection.SectionHours;
           
                var grpAvailTimeTable = availTimeGrp.Where(x => x.Groupid == UnSection.GroupId).Single();
                int START = 0;
                int dayNumber = 0;
                int startSlot = 0;
                int EndSlot = 0;
                for (int i = 0; i < grpAvailTimeTable.Week.Length; i++)
                {
                    if (grpAvailTimeTable.Week[i].AvailableTimes.Count >=noofhours)
                    {
                        int count = 1;
                        for (int j = START + 1; j < grpAvailTimeTable.Week[i].AvailableTimes.Count; j++)
                        {

                            if (grpAvailTimeTable.Week[i].AvailableTimes[j] - grpAvailTimeTable.Week[i].AvailableTimes[j - 1] == 1)
                            {
                                count++;
                            }
                            else
                            {
                                count = 1;
                            }
                            if (count == noofhours)
                            {
                                EndSlot = grpAvailTimeTable.Week[i].AvailableTimes[j];
                                startSlot = EndSlot - count + 1;
                                dayNumber = i;
                                int roomid = -3;
                                List<string> tasids = new List<string>();

                                if (isConsistentForSection(startSlot, EndSlot, dayNumber, UnSection.SectionId,ref SecTimeTable,
                                    taTimetable, UnSection,ref roomsTimeTable, ref roomid, ref tasids))
                                {
                                    assignment.Add(new SectionAssignement(UnSection, startSlot, dayNumber, roomid, tasids,UnSection.HaveLab,UnSection.HaveSections));
                                    var result = Recursive_Backtracking_Search_Sections(assignment, variables,ref SecTimeTable, availTimeGrp, taTimetable,ref  roomsTimeTable);
                                     if(result[0].RoomId!=-22)
                                    {
                                        return result;
                                    }
                                     else
                                    {
                                        var removedass=assignment.Where(x => x.CourseId == UnSection.CourseId && x.SectionId == UnSection.SectionId &&
                                        x.IsLab == UnSection.HaveLab && x.IsSection == UnSection.HaveSections).Single();
                                        
                                        int ind = SecTimeTable.IndexOf(SecTimeTable.Where(x => x.Sectionid == UnSection.SectionId).Single());
                                        SecTimeTable[ind].Week[dayNumber].Removeslots(startSlot, EndSlot);
                                        int indofroom = roomsTimeTable.IndexOf(roomsTimeTable.Where(x => x.Roomid == removedass.RoomId).Single());
                                        roomsTimeTable[indofroom].Week[dayNumber].Remove(startSlot, EndSlot);
                                    List<string> removedtas = new List<string>();
                                    removedtas = removedass.Tasids;
                                    for (int n = 0; n < removedtas.Count; n++)
                                    {
                                        var indofta = taTimetable.IndexOf(taTimetable.Where(x => x.Id == removedtas[n]).Single());
                                        var h = taTimetable[indofta].Course_hours.IndexOf(taTimetable[indofta].Course_hours.Where(x => x.Key == UnSection.CourseId).Single());
                                        KeyValuePair<int, int?> tempPair = new KeyValuePair<int, int?>(taTimetable[indofta].Course_hours[h].Key,
                                            taTimetable[indofta].Course_hours[h].Value + EndSlot - startSlot + 1);
                                        taTimetable[indofta].Week[dayNumber].Removeinterval(startSlot, EndSlot);
                                        taTimetable[indofta].Course_hours.RemoveAt(h);
                                        taTimetable[indofta].Course_hours.Add(tempPair);
                                    }
                                    assignment.Remove(assignment.Where(x => x.CourseId == UnSection.CourseId && x.SectionId == UnSection.SectionId &&
                                        x.IsLab == UnSection.HaveLab && x.IsSection == UnSection.HaveSections).Single());
                                        count = 1;
                                    }
                                }
                                else
                                {
                                    count = 1;
                                }
                            }

                        }
                    }
                }
                
           
            List<string> failurestate = new List<string>();
            assignment.Add(new SectionAssignement(UnSection,-1, -1, -1, failurestate,false,false));
            //return new List<SectionAssignement> { new SectionAssignement(UnSection, -22, -22, -22, failurestate,false,false) };
            return Recursive_Backtracking_Search_Sections(assignment, variables, ref SecTimeTable, availTimeGrp, taTimetable,ref roomsTimeTable);
        }

        private static bool isConsistentForSection(int start, int end, int day, int SecId,ref List<SectionTimeTable> allSecsTable, List<TaTimetable> TaTable,
            SectionsVariables UNsection, ref List<RoomsTimeTables> rooms, ref int roomid,ref List<string> tasids)
        {
            CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();
            int indexrom = 0;
            int indexsec = allSecsTable.IndexOf(allSecsTable.Where(x => x.Sectionid == SecId).Single());
            bool islab = false;
            if (UNsection.HaveLab)
                islab = true;
            else
                islab = false;
            int roomId = findRoom(start, end, day, rooms,ref indexrom,islab);
            if (roomId == -1)
                return false;
            roomid = roomId;
            for (int i = start; i <= end; i++)
            {
                if (allSecsTable[indexsec].Week[day].Slots[i] == true)
                {
                    return false;
                }
            }
            //TA
            var tas = db.LinkDoctorCourses.Where(x => x.Course_id == UNsection.CourseId && x.AspNetUser.type == "Ta").ToList();
            List<TaTimetable> tasforcrs = new List<TaTimetable>();

            foreach (var item in tas)
            {
                TaTimetable query = TaTable.Where(x => x.Id == item.Doctor_id).SingleOrDefault();
                tasforcrs.Add(query);
            }
            List<string> ta_assigned = new List<string>();
            int count = 0;
            int noftas = 1;
            /*if (UNsection.HaveLab)
                noftas = 2;
            else
                noftas++;*/

            for (int i = 0; i < tasforcrs.Count; i++)
            {
                var tatimetable = tasforcrs[i].Course_hours.Where(x => x.Key == UNsection.CourseId).Single();
                if (tatimetable.Value >= (end - start + 1))
                {
                    bool f = true;
                    for (int o = start; o <= end; o++)
                    {
                        if (tasforcrs[i].Week[day].Slots[o] == true)
                        {
                            f = false;
                            break;
                        }
                    }
                    if (f)
                    {
                        var allowedhourss = db.Groups.Where(x => x.ID == UNsection.GroupId).Select(x => x.AcademicYear).Single();
                        var ind = TaTable.IndexOf(TaTable.Where(x => x.Id == tasforcrs[i].Id).Single());
                        TaTable[ind].Week[day].addinterval(start, end);
                        /*if ((TaTable[ind].Week[day].Gap > allowedhourss.AllowedGapHours || TaTable[ind].Week[day].NumfHours > allowedhourss.DayWorkHours) || TaTable[ind].FreeDaysCount() < 1)
                        {
                            TaTable[ind].Week[day].Removeinterval(start, end);

                        }
                        else
                        {*/

                        var h = TaTable[ind].Course_hours.IndexOf(TaTable[ind].Course_hours.Where(x => x.Key == UNsection.CourseId).Single());
                        KeyValuePair<int, int?> temppair = new KeyValuePair<int, int?>(TaTable[ind].Course_hours[h].Key, TaTable[ind].Course_hours[h].Value - (end - start + 1));

                        TaTable[ind].Course_hours.RemoveAt(h);
                        TaTable[ind].Course_hours.Add(temppair);
                        ta_assigned.Add(tasforcrs[i].Id);
                        count++;
                        //}
                    }
                }
                if (count == noftas)
                    break;
            }

            if (count < noftas)
            {
                for (int g = 0; g < count; g++)
                {
                    var ind = TaTable.IndexOf(TaTable.Where(x => x.Id == ta_assigned[g]).Single());
                    var h = TaTable[ind].Course_hours.IndexOf(TaTable[ind].Course_hours.Where(x => x.Key == UNsection.CourseId).Single());
                    KeyValuePair<int, int?> temppair = new KeyValuePair<int, int?>(TaTable[ind].Course_hours[h].Key, TaTable[ind].Course_hours[h].Value + end - start + 1);
                    TaTable[ind].Week[day].Removeinterval(start, end);
                    TaTable[ind].Course_hours.RemoveAt(h);
                    TaTable[ind].Course_hours.Add(temppair);
                }
                ta_assigned.Clear();
                return false;
            }
            allSecsTable[indexsec].Week[day].Addslots(start, end);
            rooms[indexrom].Week[day].ReserveInterval(start, end);
            //var allowedHours = db.Groups.Where(x => x.ID == UNsection.GroupId).Select(x => x.AcademicYear).Single();
            /*if (allSecsTable[indexsec].Week[day].Gap > allowedHours.AllowedGapHours || allSecsTable[indexsec].Week[day].NumfHours > allowedHours.DayWorkHours)
            {
                allSecsTable[indexsec].Week[day].Removeslots(start, end);
                return false;
            }*/
            /*if (allSecsTable[indexsec].FreeDaysCount() < 1)
            {
                allSecsTable[indexsec].Week[day].Removeslots(start, end);
               
                return false;

            }*/
           tasids = ta_assigned;
            return true;
        }

        private static int findRoom(int start, int end, int day, List<RoomsTimeTables> rooms,ref int indexofrom,bool islab)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if(islab&&rooms[i].RoomTypeid!=3)
                {
                   
                        continue;
                }
                bool falg = true;
                for (int j = start; j <= end; j++)
                {
                    if (rooms[i].Week[day].Slots[j] == true)
                    {
                        falg = false;
                        break;
                    }
                }
                if (falg)
                {
                    indexofrom = i;
                    return rooms[i].Roomid;
                   
                }
            }
            return -1;
        }


        public List<SectionAssignement> generatesecs(List<GroupModel>GrpList)
        {


            List<SectionsVariables> secVariables = new List<SectionsVariables>();             //= SectionIntialize();

            List<SectionTimeTable> secTimetable = new List<SectionTimeTable>();           //= SectionTimeTableIntialize();

            List<AvailTimeforgroup> Availtimeforgrp = new List<AvailTimeforgroup>();       //= avilabletimegrpinit();

            List<TaTimetable> Tastimetables = new List<TaTimetable>();
               secVariables = SectionIntialize();
            secTimetable = SectionTimeTableIntialize(GrpList);
            Availtimeforgrp = avilabletimegrpinit(GrpList);
            List<RoomsTimeTables> RoomsTimetables = new List<RoomsTimeTables>();
            Tastimetables = Mo3eeeeed();
            RoomsTimetables = Roominit(GrpList);

            List<SectionAssignement> res = new List<SectionAssignement>();
            res = Recursive_Backtracking_Search_Sections(new List<SectionAssignement>(), secVariables,ref secTimetable, Availtimeforgrp, Tastimetables, ref RoomsTimetables);
            return res;
        }

    }
}