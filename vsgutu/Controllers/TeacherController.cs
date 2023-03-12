using BusinessLayer;
using DataLayer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer;
using PresentationLayer.Models;
using PresentationLayer.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace vsgutu.Controllers
{
    [Authorize(Policy = "Teacher")]

    public class TeacherController : Controller
    {

        private DataManager _dataManager;
        private ServiceManager _serviceManager;
        IWebHostEnvironment _appEnvironment;
        public TeacherController(DataManager dataManager, IWebHostEnvironment appEnvironment)
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
            return RedirectToAction("Index", "Teacher", new { Id });
        }


        public IActionResult Index(int Id, string disciplineList = "All")
        {
            if (String.IsNullOrEmpty(disciplineList) || disciplineList.Equals("All") || disciplineList.Equals("Все") )
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


        public IActionResult NewJournalPageFirst()
        {
            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            TempData["popupMessage_emptyFields"] = false;
            TempData["popupMessageHours"] = false;
            TempData["popupMessage_isInt"] = false;
            TempData["popupMessage_checkGroup"] = false;

            return View(model);
        }

        [HttpPost]
        public IActionResult NewJournalPageFirst(string NameDiscipline, string countSession, DateTime yearsStart,
            DateTime yearsEnd, string allCountHours, string LectionCountHours, string PracticsCountHours,
            string LabCountHours, string GroupsName, string formAttestation, bool availabilityOfCoursework, string block)
        {
            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            int _temp;
            int countSessionInt =0;
            int allCountHoursInt =0 ;
            int LectionCountHoursInt =0;
            int PracticsCountHoursInt=0;
            int LabCountHoursInt=0;
            int BlockInt = 0;
            if (int.TryParse(countSession, out _temp) && int.TryParse(allCountHours, out _temp) && int.TryParse(LectionCountHours, out _temp) &&
                int.TryParse(PracticsCountHours, out _temp) && int.TryParse(LabCountHours, out _temp) && int.TryParse(block, out _temp))
            {
                countSessionInt = Convert.ToInt32(countSession);
                allCountHoursInt = Convert.ToInt32(allCountHours);
                LectionCountHoursInt = Convert.ToInt32(LectionCountHours);
                PracticsCountHoursInt = Convert.ToInt32(PracticsCountHours);
                LabCountHoursInt = Convert.ToInt32(LabCountHours);
                BlockInt = Convert.ToInt32(block);
            }
            else
            {
                TempData["popupMessage_isInt"] = true;

                TempData["popupMessage_emptyFields"] = false;
                TempData["popupMessageHours"] = false;
                TempData["popupMessage_checkGroup"] = false;


                return View(model);
            }
            //Проверки для полей форма(валидация полей)
            if( String.IsNullOrEmpty(NameDiscipline) || countSessionInt == 0 || yearsStart == default(DateTime) || yearsEnd == default(DateTime) ||
                allCountHoursInt == 0 || String.IsNullOrEmpty(GroupsName) || String.IsNullOrEmpty(formAttestation))
            {
                TempData["popupMessage_emptyFields"] = true;

                TempData["popupMessage_isInt"] = false;
                TempData["popupMessageHours"] = false;
                TempData["popupMessage_checkGroup"] = false;

                return View(model);
            }
            else if (countSessionInt <= 0 || BlockInt <= 0 || allCountHoursInt <= 0 || LectionCountHoursInt < 0 || PracticsCountHoursInt < 0 || LabCountHoursInt < 0)
            {
                TempData["popupMessage_isInt"] = true;

                TempData["popupMessage_emptyFields"] = false;
                TempData["popupMessageHours"] = false;
                TempData["popupMessage_checkGroup"] = false;


                return View(model);
            }
            else if (allCountHoursInt != (LectionCountHoursInt + PracticsCountHoursInt + LabCountHoursInt) )
            {
                TempData["popupMessageHours"] = true;

                TempData["popupMessage_emptyFields"] = false;
                TempData["popupMessage_isInt"] = false;
                TempData["popupMessage_checkGroup"] = false;


                return View(model);
            }
            else if ( !(_serviceManager.Groups.CheckGroup(GroupsName)) )
            {
                TempData["popupMessage_checkGroup"] = true;

                TempData["popupMessage_emptyFields"] = false;
                TempData["popupMessage_isInt"] = false;
                TempData["popupMessageHours"] = false;


                return View(model);
            }
            else
            {
                TempData["popupMessage_emptyFields"] = false;
                TempData["popupMessage_isInt"] = false;
                TempData["popupMessageHours"] = false;
                TempData["popupMessage_checkGroup"] = false;
                //создание дисциплины
                int zetCount = (allCountHoursInt * 90) / 60;  
                _serviceManager.Disciplines.CreateDiscipline(NameDiscipline, yearsStart, yearsEnd, GroupsName,
                formAttestation, availabilityOfCoursework, zetCount, BlockInt, globalIdUser.IdUser);

                //Создание ежемесячного контроля
                List<DateTime> allMonth = new();
                for (DateTime date = yearsStart; date <= yearsEnd; date = date.AddMonths(1))
                {
                    allMonth.Add(date);
                }
                var objGroup = _serviceManager.Groups.GetGroupByName(GroupsName);
                var objDiscipline = _serviceManager.Disciplines.GetDisciplineByName(NameDiscipline);
                var _listStudentByGroup = _serviceManager.Users.GetStudentListByGroup(objGroup.Groups.Id);
                foreach (var item in allMonth)
                {
                    foreach (var subItem in _listStudentByGroup)
                    {
                        _serviceManager.EK.CreateEk(item, subItem.Users.Id, objDiscipline.Discipline.Id);
                    }
                }


                return RedirectToAction("NewJournalPageSecond", "Teacher",
                new
                {
                    allCountHoursInt,
                    LectionCountHoursInt,
                    PracticsCountHoursInt,
                    LabCountHoursInt,
                    NameDiscipline,
                    GroupsName,
                    countSessionInt,
                    model
                });
            }
        }
        public IActionResult NewJournalPageSecond(int allCountHoursInt,
            int LectionCountHoursInt, int PracticsCountHoursInt, int LabCountHoursInt, string NameDiscipline, string GroupsName, int countSessionInt, bool msg = false)
        {
            

            TempData["fieldIsEmpty"] = msg;
            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            model.allCountHours = allCountHoursInt;
            model.LectionCountHours = LectionCountHoursInt;
            model.PracticsCountHours = PracticsCountHoursInt;
            model.LabCountHours = LabCountHoursInt;
            model.NameDiscipline = NameDiscipline;
            model.GroupsName = GroupsName;
            model.CountSession = countSessionInt;
            return View(model);
        }

        [HttpPost]

        public IActionResult NewJournalPageSecond(IFormCollection formFields, string NameDiscipline, string GroupsName,
            int allCountHoursInt, int LectionCountHoursInt, 
            int PracticsCountHoursInt, int LabCountHoursInt, int countSessionInt)
        {
            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            //валидация полей
            int _temp = 0;
            for (int i = 1; i <= countSessionInt; i++)
            {
                string date = formFields["SessionDate_" + i.ToString()];
                if (String.IsNullOrEmpty(date))
                {
                    TempData["fieldIsEmpty"] = true;
                    _temp = 1;
                }
            }
            for (int i = 1; i <= LectionCountHoursInt; i++)
            {
                string date = formFields["lectionDate_" + i.ToString()];
                string name = formFields["lectionName_" + i.ToString()];
                if (String.IsNullOrEmpty(date) || String.IsNullOrEmpty(name))
                {
                    TempData["fieldIsEmpty"] = true;
                    _temp = 1;
                }
            }
            for (int i = 1; i <= PracticsCountHoursInt; i++)
            {
                string date = formFields["practicsDate_" + i.ToString()];
                string name = formFields["practicsName_" + i.ToString()];
                if (String.IsNullOrEmpty(date) || String.IsNullOrEmpty(name))
                {
                    TempData["fieldIsEmpty"] = true;
                    _temp = 1;
                }
            }
            for (int i = 1; i <= LabCountHoursInt; i++)
            {
                string date = formFields["LabDate_" + i.ToString()];
                string name = formFields["LabName_" + i.ToString()];
                if (String.IsNullOrEmpty(date) || String.IsNullOrEmpty(name))
                {
                    TempData["fieldIsEmpty"] = true;
                    _temp = 1;
                }

            }


            if (_temp == 1)
            {
                bool msg = true;
                return RedirectToAction("NewJournalPageSecond", "Teacher", new
                {
                    allCountHoursInt,
                    LectionCountHoursInt,
                    PracticsCountHoursInt,
                    LabCountHoursInt,
                    NameDiscipline,
                    GroupsName,
                    countSessionInt,
                    msg
                }) ;

            }
            else
            {
                // создание сессии лекций практик лаб 
                if (countSessionInt > 0)
                {
                    for (int i = 1; i <= countSessionInt; i++)
                    {
                        string date = formFields["SessionDate_" + i.ToString()];
                        var discipline = _serviceManager.Disciplines.GetDisciplineByName(NameDiscipline);
                        _serviceManager.Session.CreateSessionScoreForCreateInDiscipline(discipline.Discipline.Id, DateTime.Parse(date), discipline.Groups.Groups.Name);
                    }
                }


                if (LectionCountHoursInt > 0)
                {
                    for (int i = 1; i <= LectionCountHoursInt; i++)
                    {
                        string date = formFields["lectionDate_" + i.ToString()];
                        string name = formFields["lectionName_" + i.ToString()];

                        _serviceManager.Lessons.CreateLessonsLection(name, DateTime.Parse(date), NameDiscipline);
                        //Здесь создаем оценки под каждого студента со значением null чтобы потом просто редактировать их по нужде?????
                        var lesson = _serviceManager.Lessons.GetLessonByFullField(name, NameDiscipline, "Лекции", DateTime.Parse(date));
                        GroupsModel group = _serviceManager.Groups.GetGroupByName(GroupsName);
                        List<UsersModel> studentList = _serviceManager.Users.GetStudentListByGroup(group.Groups.Id);
                        foreach (var item in studentList)
                        {
                            _serviceManager.Scories.CreateScoreForPageSecond(item.Users.Id, lesson.Lesson.Id);
                        }


                    }
                }

                if (PracticsCountHoursInt > 0)
                {
                    for (int i = 1; i <= PracticsCountHoursInt; i++)
                    {
                        string date = formFields["practicsDate_" + i.ToString()];
                        string name = formFields["practicsName_" + i.ToString()];

                        _serviceManager.Lessons.CreateLessonsPractics(name, DateTime.Parse(date), NameDiscipline);
                        //Здесь создаем оценки под каждого студента со значением null чтобы потом просто редактировать их по нужде?????
                        var lesson = _serviceManager.Lessons.GetLessonByFullField(name, NameDiscipline, "Практика", DateTime.Parse(date));
                        GroupsModel group = _serviceManager.Groups.GetGroupByName(GroupsName);
                        List<UsersModel> studentList = _serviceManager.Users.GetStudentListByGroup(group.Groups.Id);
                        foreach (var item in studentList)
                        {
                            _serviceManager.Scories.CreateScoreForPageSecond(item.Users.Id, lesson.Lesson.Id);
                        }

                    }

                }
                if (LabCountHoursInt > 0)
                {
                    for (int i = 1; i <= LabCountHoursInt; i++)
                    {
                        string date = formFields["LabDate_" + i.ToString()];
                        string name = formFields["LabName_" + i.ToString()];

                        _serviceManager.Lessons.CreateLessonsLabs(name, DateTime.Parse(date), NameDiscipline);
                        //Здесь создаем оценки под каждого студента со значением null чтобы потом просто редактировать их по нужде?????
                        var lesson = _serviceManager.Lessons.GetLessonByFullField(name, NameDiscipline, "Лабораторные работы", DateTime.Parse(date));
                        GroupsModel group = _serviceManager.Groups.GetGroupByName(GroupsName);
                        List<UsersModel> studentList = _serviceManager.Users.GetStudentListByGroup(group.Groups.Id);
                        foreach (var item in studentList)
                        {
                            _serviceManager.Scories.CreateScoreForPageSecond(item.Users.Id, lesson.Lesson.Id);
                        }

                    }
                }
                return RedirectToAction("Index", "Teacher");
            }
           

        }

        public IActionResult Journal(int idDiscipline)
        {
            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            model.DisciplineById = _serviceManager.Disciplines.GetDisciplineById(idDiscipline);
            //проверки на существование типов занятий
            model.lessonModelLection = _serviceManager.Lessons.LessonModelIsLection(model.DisciplineById.Discipline.Id);
            model.lessonModelPractics = _serviceManager.Lessons.LessonModelIsPractics(model.DisciplineById.Discipline.Id);
            model.lessonModelLabs = _serviceManager.Lessons.LessonModelIsLabs(model.DisciplineById.Discipline.Id);
            //Вывод занятий
            model.ListLection = _serviceManager.Lessons.GetLessonModelByDisciplineLection(model.DisciplineById.Discipline.Id);
            model.ListPractics = _serviceManager.Lessons.GetLessonModelByDisciplinePractics(model.DisciplineById.Discipline.Id);
            model.ListLabs = _serviceManager.Lessons.GetLessonModelByDisciplineLab(model.DisciplineById.Discipline.Id);
            //Список студентов
            model.ListStudent = _serviceManager.Users.GetStudentListByGroup(model.DisciplineById.Discipline.IdGroup.Id);
            //Статистика
            model.StatisticLection = _serviceManager.Lessons.GetStatisticsLessonByLection(model.DisciplineById.Discipline.Id);
            model.StatisticPractic = _serviceManager.Lessons.GetStatisticsLessonByPractics(model.DisciplineById.Discipline.Id);
            model.StatisticLab = _serviceManager.Lessons.GetStatisticsLessonByLabs(model.DisciplineById.Discipline.Id);
            model.StatisticAllTypeLesson = _serviceManager.Lessons.GetStatisticsLessonsByAllTypeLesson(model.DisciplineById.Discipline.Id);
            return View(model);

        }
        public IActionResult EditLesson(int idLesson, int idDiscipline)
        {
            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            model.Lesson = _serviceManager.Lessons.GetLessonById(idLesson);
            model.Discipline = _serviceManager.Disciplines.GetDisciplineById(idDiscipline);
            model.StudensList = _serviceManager.Users.GetStudentListByGroup(model.Discipline.Discipline.IdGroup.Id);
            model.CountScore = _serviceManager.Lessons.GetLessonById(idLesson).Score.Count();
            return View(model);
        }


        [HttpPost]
        public IActionResult EditLessonEvalution(IFormCollection formFields, int countScore, int LessonId)
        {
            var _temp = _serviceManager.Lessons.GetLessonById(LessonId);
            var idLesson = _temp.Lesson.Id;
            var idDiscipline = _temp.Discipline.Discipline.Id;
            for (int i = 1; i <= countScore; i++)
            {
                string fieldScore = formFields["field_" + i.ToString()];
                string fieldIdScore = formFields["fieldId_" + i.ToString()];
                _serviceManager.Scories.EditScoreEvalution(Convert.ToInt32(fieldIdScore), fieldScore);

            }

            return RedirectToAction("EditLesson", "Teacher", new { idLesson, idDiscipline });
        }

        [HttpPost]
        public IActionResult EditLessonAttendance(IFormCollection formFields, int countScore, int LessonId)
        {
            dynamic model = new ExpandoObject();
            var _temp = _serviceManager.Lessons.GetLessonById(LessonId);
            var idLesson = _temp.Lesson.Id;
            var idDiscipline = _temp.Discipline.Discipline.Id;
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            for (int i = 1; i <= countScore; i++)
            {
                string fieldScore = formFields["fieldAtt_" + i.ToString()];
                bool Att;
                if (fieldScore.Equals("Н") || fieldScore.Equals("н"))
                {
                    Att = true;
                }
                else
                {
                    Att = false;
                }
                string fieldIdScore = formFields["fieldId_" + i.ToString()];
                _serviceManager.Scories.EditScoreAttendace(Convert.ToInt32(fieldIdScore), Att);

            }

            return RedirectToAction("EditLesson", "Teacher", new { idLesson, idDiscipline });
        }

        public IActionResult DisciplineJournals(string disciplineList = "All")
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

        public IActionResult Session(int idDiscipline, string date, bool msgIsInt = false, bool mgEval = false)
        {
            TempData["isInt"] = msgIsInt;
            TempData["evalutionCheck"] = mgEval;

            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            model.Discipline = _serviceManager.Disciplines.GetDisciplineById(idDiscipline);
            model.Date = date;
            model.ListSessionScoreByDate = _serviceManager.Session.GetAllSessionScoreByDisciplineByDate(idDiscipline, date);
            model.CountScores = _serviceManager.Session.GetAllSessionScoreByDisciplineByDate(idDiscipline, date).Count();
            return View(model);
        }
        [HttpPost]
        public IActionResult Session(IFormCollection formFields, int countScores, int idDiscipline, string date)
        {
            TempData["isInt"] = false;
            TempData["evalutionCheck"] = false;
            for (int i =1;i<= countScores;++i)
            {
                int _check;
                string fieldIdScore = formFields["idSessionScore_" + i.ToString()];
                string fieldSessionScore;

                if (formFields["sessionScore_" + i.ToString()].Equals(""))
                {
                    fieldSessionScore = "0";
                }
                else
                {
                    fieldSessionScore = formFields["sessionScore_" + i.ToString()];
                }
                if (int.TryParse(fieldSessionScore, out _check))
                {
                    if (!(fieldSessionScore.Equals("0")))
                    {
                        if (!(formFields["sessionScore_" + i.ToString()].Equals("5")) && !(formFields["sessionScore_" + i.ToString()].Equals("4")) &&
                        !(formFields["sessionScore_" + i.ToString()].Equals("3")) && !(formFields["sessionScore_" + i.ToString()].Equals("2")))
                        {
                            TempData["evalutionCheck"] = true;
                            break;
                        }
                    }
                }
                if (int.TryParse(fieldSessionScore, out _check))
                {
                    TempData["isInt"] = false;
                    _serviceManager.Session.EditSessionScore(Convert.ToInt32(fieldIdScore), Convert.ToInt32(fieldSessionScore));
                }
                else
                {
                    TempData["isInt"] = true;
                    break;
                }
            }
            if ((bool)TempData["isInt"] == true)
            {
                bool msgIsInt = true;
                bool mgEval = false;
                return RedirectToAction("Session", "Teacher", new { idDiscipline, date, msgIsInt, mgEval });
            }
            else if ((bool)TempData["evalutionCheck"] == true)
            {
                bool msgIsInt = false;
                bool mgEval = true;
                return RedirectToAction("Session", "Teacher", new { idDiscipline, date, msgIsInt, mgEval });
            }
            else
            {
                return RedirectToAction("Sessions", "Teacher", new { idDiscipline});
            }
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
            
            IEnumerable<IdDate> idDates = _serviceManager.EK.GetAllEKByDiscipline(idDiscipline).Select(x => new IdDate { Id = x.Discipline.Discipline.Id,  Date = x.ek.Date  }).Distinct().ToList();
            model.ListEK = idDates;
            return View(model);
        }

        public IActionResult Ek(string dateTime, int idDiscipline, bool msgIsInt = false, bool mgEval = false)
        {
            
            TempData["isInt"] = msgIsInt;
            TempData["evalutionCheck"] = mgEval;
            DateTime newDate = DateTime.Parse(dateTime);
            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            model.Discipline = _serviceManager.Disciplines.GetDisciplineById(idDiscipline);
            model.Date = dateTime;
            model.ListEkByDate = _serviceManager.EK.GetAllEKByDisciplineByDate(idDiscipline, newDate);
            model.CountEKs = _serviceManager.EK.GetAllEKByDisciplineByDate(idDiscipline, newDate).Count();
            return View(model);
        }
        [HttpPost]
        public IActionResult Ek(IFormCollection formFields, int countScores, int idDiscipline, string dateTime)
        {

            TempData["isInt"] = false;
            TempData["evalutionCheck"] = false;
            for (int i = 1; i <= countScores; ++i)
            {
                dynamic model = new ExpandoObject();
                model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
                int _check;
                string fieldIdScore = formFields["idEK_" + i.ToString()];
                string fieldEkScore;

                if (formFields["EkScore_" + i.ToString()] == "")
                {
                    fieldEkScore = "0";
                }
                else
                {
                    fieldEkScore = formFields["EkScore_" + i.ToString()];
                }
                if (int.TryParse(fieldEkScore, out _check))
                {
                    if (!(fieldEkScore.Equals("0")))
                    {
                        if (!(formFields["EkScore_" + i.ToString()].Equals("5")) && !(formFields["EkScore_" + i.ToString()].Equals("4")) &&
                        !(formFields["EkScore_" + i.ToString()].Equals("3")) && !(formFields["EkScore_" + i.ToString()].Equals("2")))
                        {
                            TempData["evalutionCheck"] = true;
                            break;
                        }
                    }
                }
                if (int.TryParse(fieldEkScore, out _check))
                {
                    TempData["isInt"] = false;
                    _serviceManager.EK.EditEkScore(Convert.ToInt32(fieldIdScore), Convert.ToInt32(fieldEkScore));
                }
                else
                {
                    TempData["isInt"] = true;
                    break;
                }
            }
            if ((bool)TempData["isInt"] == true)
            {
                bool msgIsInt = true;
                bool mgEval = false;
                return RedirectToAction("Ek", "Teacher", new { idDiscipline, dateTime, msgIsInt, mgEval });
            }
            else if ((bool)TempData["evalutionCheck"] == true)
            {
                bool msgIsInt = false;
                bool mgEval = true;
                return RedirectToAction("Ek", "Teacher", new { idDiscipline, dateTime, msgIsInt, mgEval });
            }
            else
            {
                return RedirectToAction("Eks", "Teacher", new { idDiscipline });
            }

            
        }

        public IActionResult DeleteDiscipline(string disciplineList = "All")
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
        public IActionResult DeleteDisciplines(int idDiscipline,int idGroup)
        {
            dynamic model = new ExpandoObject();
            model.name = _serviceManager.Users.GetUserById(globalIdUser.IdUser);
            var _temp = _serviceManager.Users.GetStudentListByGroup(idGroup);
            foreach (var item in _temp)
            {
                _serviceManager.Scories.DeleteScoreByIdStudent(item.Users.Id);
                _serviceManager.EK.DeleteEkScoreByIdStudent(item.Users.Id);
                _serviceManager.Session.DeleteSessionScoreByIdStudent(item.Users.Id);
            }
            _serviceManager.Lessons.DeleteLessonsByIdDiscipline(idDiscipline);
            _serviceManager.Disciplines.DeleteDisciplineById(idDiscipline);

            return RedirectToAction("DeleteDiscipline", "Teacher");
        }
    }

}
