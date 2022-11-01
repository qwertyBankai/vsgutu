using BusinessLayer;
using DataLayer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace vsgutu.Controllers
{
    [Authorize(Policy = "Student")]
    public class StudentController : Controller
    {
        private DataManager _dataManager;
        private ServiceManager _serviceManager;
        IWebHostEnvironment _appEnvironment;
        public StudentController(DataManager dataManager, IWebHostEnvironment appEnvironment)
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
            return RedirectToAction("Index", "Student", new { Id });
        }


            
        public IActionResult Index(int Id, string disciplineList = "All")
        {

            if (String.IsNullOrEmpty(disciplineList) || disciplineList.Equals("All") || disciplineList.Equals("Все"))
            {
                
                dynamic model = new ExpandoObject();
                model.listDiscipline = _serviceManager.Disciplines.GetDisciplieForStudent(globalIdUser.IdUser);
                model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
                TempData["Emp"] = false;
                return View(model);
            }
            else
            {
                var user = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
                dynamic model = new ExpandoObject();
                model.name = user;
                model.listDiscipline = _serviceManager.Disciplines.SearchJournalForStudent(disciplineList, user.Group.Groups.Id);
                if (_serviceManager.Disciplines.SearchJournalForStudent(disciplineList, user.Group.Groups.Id).Count() == 0)
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
       
        public IActionResult Journal(string name)
        {
            dynamic model = new ExpandoObject();

            var user = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            model.name = user;


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
            var _temp = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            List<UsersModel> usersList = new();
            usersList.Add(_temp);
            model.ListStudent = usersList;

            return View(model);
        }


        public IActionResult SessionsJournal(string disciplineList = "All")
        {
            if (String.IsNullOrEmpty(disciplineList) || disciplineList.Equals("All") || disciplineList.Equals("Все"))
            {
                dynamic model = new ExpandoObject();
                model.listDiscipline = _serviceManager.Disciplines.GetDisciplieForStudent(globalIdUser.IdUser);
                model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
                TempData["Emp"] = false;
                return View(model);
            }
            else
            {
                var user = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
                dynamic model = new ExpandoObject();
                model.name = user;
                model.listDiscipline = _serviceManager.Disciplines.SearchJournalForStudent(disciplineList, user.Group.Groups.Id);
                if (_serviceManager.Disciplines.SearchJournalForStudent(disciplineList, user.Group.Groups.Id).Count() == 0)
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
            model.ListSessionScoreByDate = _serviceManager.Session.GetAllSessionScoreByDisciplineByDateForStudent(idDiscipline, date, globalIdUser.IdUser);
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
            model.ListEkByDate = _serviceManager.EK.GetAllEKByDisciplineByDateForStudent(idDiscipline, newDate, globalIdUser.IdUser);
            model.CountEKs = _serviceManager.EK.GetAllEKByDisciplineByDate(idDiscipline, newDate).Count();
            return View(model);
        }



    }

    
}
