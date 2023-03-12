using BusinessLayer;
using ClosedXML.Excel;
using DataLayer.Entities;
using DataLayer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace vsgutu.Controllers
{
    [Authorize(Policy = "Administrator")]
    public class AdminController : Controller
    {

        private DataManager _dataManager;
        private ServiceManager _serviceManager;
        IWebHostEnvironment _appEnvironment;
        public AdminController(DataManager dataManager, IWebHostEnvironment appEnvironment)
        {
            _dataManager = dataManager;
            _serviceManager = new ServiceManager(_dataManager);
            _appEnvironment = appEnvironment;
        }

        public static class globalIdUser
        {
            public static int IdUser { get; set; }
        }
        public IActionResult Temp(int Id)
        {
            globalIdUser.IdUser = Id;
            return RedirectToAction("Index", "Admin", new { Id });
        }


        public IActionResult Index(int Id, string disciplineList = "All")
        {

            if (String.IsNullOrEmpty(disciplineList) || disciplineList.Equals("All") || disciplineList.Equals("Все"))
            {

                dynamic model = new ExpandoObject();
                model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
                model.listDiscipline = _serviceManager.Disciplines.GetDisciplineList();
                TempData["Emp"] = false;
                return View(model);
            }
            else
            {
                var user = _serviceManager.Users.GetUserById(Id);

                dynamic model = new ExpandoObject();
                model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);


                model.listDiscipline = _serviceManager.Disciplines.SearchJournal(disciplineList);
                if(_serviceManager.Disciplines.SearchJournal(disciplineList).Count() == 0)
                {
                    TempData["Emp"] = true;
                }
                else
                {
                    TempData["Emp"] = false;
                }
                return View(model);
            }
            
        }

        public IActionResult Journal(string name,string start = "", string end = "")
        {
            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);

            //вывод всех дисцисплин
            model.disciplineModel = _serviceManager.Disciplines.DisciplineModelDBToViewByName(name);
            //вывод всех уроков
            model.lessonModel = _serviceManager.Lessons.GetLessonModelByDiscipline(model.disciplineModel.Discipline.Id);
            //проверки на существование типов занятий
            model.lessonModelLection = _serviceManager.Lessons.LessonModelIsLection(model.disciplineModel.Discipline.Id);
            model.lessonModelPractics = _serviceManager.Lessons.LessonModelIsPractics(model.disciplineModel.Discipline.Id);
            model.lessonModelLabs = _serviceManager.Lessons.LessonModelIsLabs(model.disciplineModel.Discipline.Id);
            //Вывод занятий
            model.ListLection = _serviceManager.Lessons.GetLessonModelByDisciplineLection(model.disciplineModel.Discipline.Id);
            model.ListPractics = _serviceManager.Lessons.GetLessonModelByDisciplinePractics(model.disciplineModel.Discipline.Id);
            model.ListLabs = _serviceManager.Lessons.GetLessonModelByDisciplineLab(model.disciplineModel.Discipline.Id);
            //Список студентов
            model.ListStudent = _serviceManager.Users.GetStudentListByGroup(model.disciplineModel.Discipline.IdGroup.Id);
           

            //Статистика по времени
            if (!(String.IsNullOrWhiteSpace(start)) && !(String.IsNullOrWhiteSpace(end)))
            {
                model.StatisticLection = _serviceManager.Lessons.GetStatisticsLessonByLectionTime(model.disciplineModel.Discipline.Id, start, end);
                model.StatisticPractic = _serviceManager.Lessons.GetStatisticsLessonByPracticsTime(model.disciplineModel.Discipline.Id, start, end);
                model.StatisticLab = _serviceManager.Lessons.GetStatisticsLessonByLabsTime(model.disciplineModel.Discipline.Id, start, end);
                model.StatisticAllTypeLesson = _serviceManager.Lessons.GetStatisticsLessonsByAllTypeLessonTime(model.disciplineModel.Discipline.Id, start, end);
                model.StatisticPos = _serviceManager.Lessons.GetStatisticByPosTime(model.disciplineModel.Discipline.IdGroup.Id, model.disciplineModel.Discipline.Id, start, end);
            }

            return View(model);
        }

        public IActionResult CreateUsers()
        {
            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            return View(model);
        }
        public IActionResult CreateAdminUser()
        {
            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateAdminUser(IFormFile uploadAdmin)
        {
            string path = "/files/" + uploadAdmin.FileName;

            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                uploadAdmin.CopyTo(fileStream);
            }
            string _temp = _appEnvironment.WebRootPath + path;
            _serviceManager.Users.CreateAdmin(_temp);

            System.IO.File.Delete(_appEnvironment.WebRootPath + path);
            return RedirectToAction("CreateUsers", "Admin");
        }

        public IActionResult CreateTeacherUser()
        {
            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            return View();
        }

        [HttpPost]
        public IActionResult CreateTeacherUser(IFormFile uploadTeacher)
        {
            string paths = "/files/" + uploadTeacher.FileName;

            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + paths, FileMode.Create))
            {
                uploadTeacher.CopyTo(fileStream);
            }
            string _temp = _appEnvironment.WebRootPath + paths;
            _serviceManager.Users.CreateTeacher(_temp);

            System.IO.File.Delete(_appEnvironment.WebRootPath + paths);


            return RedirectToAction("CreateUsers", "Admin");
        }

        public IActionResult CreateStudentrUser(string groupList="All")
        {

            if (String.IsNullOrEmpty(groupList) || groupList.Equals("All") || groupList.Equals("Все"))
            {
                dynamic model = new ExpandoObject();
                model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
                model.listGroup = _serviceManager.Groups.GetGroupsList();
                TempData["Emp"] = false;
                return View(model);
            }
            else
            {
                dynamic model = new ExpandoObject();
                model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
                model.listGroup = _serviceManager.Groups.SearchGroupByName(groupList);
                if (_serviceManager.Groups.SearchGroupByName(groupList).Count() == 0)
                {
                    TempData["Emp"] = true;
                }
                else
                {
                    TempData["Emp"] = false;
                }
                return View(model);
            }
        }

        public IActionResult CreateStudentrUsers(string nameGroup)
        {
            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            model.nameGroups = _serviceManager.Groups.GroupsModelDBToViewByName(nameGroup);
            return View(model);

        }

        [HttpPost]

        public IActionResult CreateStudentrUsers(IFormFile uploadStudent, string nameGroup)
        {
            string paths = "/files/" + uploadStudent.FileName;

            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + paths, FileMode.Create))
            {
                uploadStudent.CopyTo(fileStream);
            }
            string _temp = _appEnvironment.WebRootPath + paths;
            _serviceManager.Users.CreateStudent(_temp, nameGroup);

            System.IO.File.Delete(_appEnvironment.WebRootPath + paths);

            return RedirectToAction("CreateUsers", "Admin");
        }



        public IActionResult DeleteUsers(string userList = "All")
        {
           

            if (String.IsNullOrEmpty(userList) || userList.Equals("All") || userList.Equals("Все"))
            {
                dynamic model = new ExpandoObject();
                model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
                model.listUsers = _serviceManager.Users.GetUsersList();
                TempData["Emp"] = false;
                return View(model);
            }
            else
            {

                dynamic model = new ExpandoObject();
                model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
                model.listUsers = _serviceManager.Users.GetUsersListByName(userList);
                if (_serviceManager.Users.GetUsersListByName(userList).Count() == 0)
                {
                    TempData["Emp"] = true;
                }
                else
                {
                    TempData["Emp"] = false;
                }
                return View(model);
            }
        }
        public IActionResult DeleteUsersMethod(int IdUser)
        {
            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            _serviceManager.Scories.DeleteScoreByIdStudent(IdUser);
            _serviceManager.Users.DeleteUsersById(IdUser);
            return RedirectToAction("DeleteUsers", "Admin");
        }

        public IActionResult Groups(string groupList="All", bool msgCheck = false)
        {

            TempData["CheckGroup"] = msgCheck;
            if (String.IsNullOrEmpty(groupList) || groupList.Equals("All") || groupList.Equals("Все"))
            {
                dynamic model = new ExpandoObject();
                model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
                model.listGroup = _serviceManager.Groups.GetGroupsList();
                TempData["Emp"] = false;
                return View(model);
            }
            else
            {
                dynamic model = new ExpandoObject();
                model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
                model.listGroup = _serviceManager.Groups.GetGroupsListByName(groupList);
                if (_serviceManager.Groups.GetGroupsListByName(groupList).Count() == 0)
                {
                    TempData["Emp"] = true;
                }
                else
                {
                    TempData["Emp"] = false;
                }
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult CreateGroup(string Name = "")
        {
            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            TempData["CheckGroup"] = false;
            if (_serviceManager.Groups.CheckGroup(Name))
            {
                string str = "All";
                bool msgCheck = true;
                return RedirectToAction("Groups", "Admin", new { str, msgCheck});

            }
            else
            {
                dynamic models = new ExpandoObject();
                models.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
                Groups modelGroup = new();
                modelGroup.Name = Name;
                GroupsModel groupsModel = new();
                groupsModel.Groups = modelGroup;
                _serviceManager.Groups.CreateGroup(groupsModel);
                string str = "All";
                bool msgCheck = false;
                return RedirectToAction("Groups", "Admin", new { str, msgCheck });
            }

        }

        public IActionResult DeleteGroup(int idGroup)
        {
            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            var _temp = _serviceManager.Users.GetStudentListByGroup(idGroup);
            foreach (var item in _temp)
            {
                _serviceManager.Scories.DeleteScoreByIdStudent(item.Users.Id);
                _serviceManager.EK.DeleteEkScoreByIdStudent(item.Users.Id);
                _serviceManager.Session.DeleteSessionScoreByIdStudent(item.Users.Id);
                _serviceManager.Users.DeleteUsersById(item.Users.Id);
            }
            _serviceManager.Lessons.DeleteLessonsByGroup(idGroup);
            _serviceManager.Disciplines.DeleteDisciplineByGroup(idGroup);

            _serviceManager.Groups.DeleteGroups(idGroup);
            return RedirectToAction("Groups", "Admin");
        }

        public IActionResult SessionsJournal(string disciplineList = "All")
        {
            if (String.IsNullOrEmpty(disciplineList) || disciplineList.Equals("All") || disciplineList.Equals("Все"))
            {
                dynamic model = new ExpandoObject();
                model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
                model.listDiscipline = _serviceManager.Disciplines.GetDisciplineList();
                TempData["Emp"] = false;
                return View(model);
            }
            else
            {
                dynamic model = new ExpandoObject();
                model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
                model.listDiscipline = _serviceManager.Disciplines.SearchJournal(disciplineList);
                if (_serviceManager.Disciplines.SearchJournal(disciplineList).Count() == 0)
                {
                    TempData["Emp"] = true;
                }
                else
                {
                    TempData["Emp"] = false;
                }
                return View(model);
            }
        }

        public IActionResult Sessions(int idDiscipline)
        {
            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            List<IdDate> idDates = _serviceManager.Session.GetAllSessionScoreByDiscipline(idDiscipline).Select(x => new IdDate { Id = x.Discipline.Discipline.Id, Date = x.SessionScore.Date }).Distinct().ToList();
            model.listsessionScore = idDates;
            return View(model);
        }

        public IActionResult Session(string date, int idDiscipline)
        {
            dynamic model = new ExpandoObject();
            model.dateSession = DateTime.Parse(date);
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            model.ListSessionScoreByDate = _serviceManager.Session.GetAllSessionScoreByDisciplineByDate(idDiscipline, date);
            model.CountScores = _serviceManager.Session.GetAllSessionScoreByDate(date).Count();
            return View(model);
        }

        public IActionResult EkJournals(string disciplineList = "All")
        {
            if (String.IsNullOrEmpty(disciplineList) || disciplineList.Equals("All") || disciplineList.Equals("Все"))
            {
                dynamic model = new ExpandoObject();
                model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
                model.listDiscipline = _serviceManager.Disciplines.GetDisciplineList();
                TempData["Emp"] = false;
                return View(model);
            }
            else
            {
                dynamic model = new ExpandoObject();
                model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
                model.listDiscipline = _serviceManager.Disciplines.SearchJournal(disciplineList);
                if (_serviceManager.Disciplines.SearchJournal(disciplineList).Count() == 0)
                {
                    TempData["Emp"] = true;
                }
                else
                {
                    TempData["Emp"] = false;
                }
                return View(model);
            }
        }

        public IActionResult Eks(int idDiscipline)
        {
            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            IEnumerable<IdDate> idDates = _serviceManager.EK.GetAllEKByDiscipline(idDiscipline).Select(x => new IdDate { Id = x.Discipline.Discipline.Id, Date = x.ek.Date }).Distinct().ToList();
            model.ListEK = idDates;
            return View(model);
        }

        public IActionResult Ek(string dateTime, int idDiscipline)
        {
            DateTime newDate = DateTime.Parse(dateTime);
            dynamic model = new ExpandoObject();
            model.dateEk = newDate;
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            model.ListEkByDate = _serviceManager.EK.GetAllEKByDisciplineByDate(idDiscipline, newDate);
            model.CountEKs = _serviceManager.EK.GetAllEKByDisciplineByDate(idDiscipline, newDate).Count();
            return View(model);
        }

        public IActionResult Report(int idGroup, int idDiscipline)
        {
            var group = _serviceManager.Users.GetStudentListByGroup(idGroup);
            using (XLWorkbook workbook = new XLWorkbook())
            {
                var workSheet = workbook.Worksheets.Add($"Группа {group.First().Group.Groups.Name}");
                workSheet.Cell(1, 1).Value = "ФИО";
                workSheet.Cell(1, 2).Value = "Средний балл";
                workSheet.Cell(1, 3).Value = "Пропусков %";


                workSheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                workSheet.Cell(1, 1).Style.Font.Bold = true;
                workSheet.Cell(1, 1).Style.Font.FontSize = 14;
                workSheet.Cell(1, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                workSheet.Cell(1, 2).Style.Font.Bold = true;
                workSheet.Cell(1, 2).Style.Font.FontSize = 14;
                workSheet.Cell(1, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                workSheet.Cell(1, 3).Style.Font.Bold = true;
                workSheet.Cell(1, 3).Style.Font.FontSize = 14;
                int count = 2;
                foreach (var item in group)
                {
                    workSheet.Cell(count, 1).Value = item.Users.Fio;
                    workSheet.Cell(count, 2).Value = _serviceManager.Users.GetStatisticByStudent(item.Users.Id, idDiscipline);
                    workSheet.Cell(count, 3).Value = _serviceManager.Users.GetStatisticByStudentAttendence(item.Users.Id, idDiscipline);
                    count++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();
                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"report_admin.xlsx"
                    };
                }
            }
        }
    }
}
