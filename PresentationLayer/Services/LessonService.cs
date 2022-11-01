using BusinessLayer;
using DataLayer.Entities;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    public class LessonService
    {
        private DataManager _dataManager;
        private DisciplineService _disciplineService;
        private ScoreService _scoreService;
        private TypeLessonService _typeLessonService;
        private UsersService _usersService;

        public LessonService(DataManager dataManager)
        {
            this._dataManager = dataManager;
            _disciplineService = new DisciplineService(_dataManager);
            _scoreService = new ScoreService(_dataManager);
            _typeLessonService = new TypeLessonService(_dataManager);
            _usersService = new UsersService(_dataManager);
        }

      //Список всех уроков
      public List<LessonsModel> GetLessonModelList()
        {
            var _dbData = _dataManager.Lessons.GetAllLessons(true);
            List<LessonsModel> _modelList = new List<LessonsModel>();

            foreach(var item in _dbData)
            {
                _modelList.Add(LessonModelDBToViewById(item.Id));
            }

            return _modelList;
        }

        //Все уроки по дисциплине
        public List<LessonsModel> GetLessonModelByDiscipline(int idDiscipline)
        {
            var _dbData = _dataManager.Lessons.GetAllLessons(true);
            List<LessonsModel> _modelList = new List<LessonsModel>();

            foreach (var item in _dbData)
            {
                if (item.IdDiscipline.Id == idDiscipline)
                {
                    _modelList.Add(LessonModelDBToViewById(item.Id));
                }
            }
            return _modelList;
        }

        public List<LessonsModel> GetLessonModelByDisciplineTime(int idDiscipline,string start, string end)
        {
            var _dbData = _dataManager.Lessons.GetAllLessons(true);
            List<LessonsModel> _modelList = new List<LessonsModel>();

            var cultureInfo = new CultureInfo("ru-RU");
            var dateTimeStart = DateTime.Parse(start, cultureInfo);
            var dateTimeEnd = DateTime.Parse(end, cultureInfo);

            foreach (var item in _dbData)
            {
                if (item.IdDiscipline.Id == idDiscipline)
                {
                    if (item.Date >= dateTimeStart && item.Date <= dateTimeEnd)
                    {
                        _modelList.Add(LessonModelDBToViewById(item.Id));
                    }
                    
                }
            }
            return _modelList;
        }


        //Список всех лекции по дисциплине
        public List<LessonsModel> GetLessonModelByDisciplineLection(int idDiscipline)
        {
            var _dbData = _dataManager.Lessons.GetAllLessons(true);
            List<LessonsModel> _modelListByDiscipline = new List<LessonsModel>();
            List<LessonsModel> _tempList = new List<LessonsModel>();
            List<LessonsModel> _modelListByDisciplineLection = new List<LessonsModel>();
            foreach (var item in _dbData)
            {
                if (item.IdDiscipline.Id == idDiscipline)
                {
                    _modelListByDiscipline.Add(LessonModelDBToViewById(item.Id));
                }
                _tempList = _modelListByDiscipline.FindAll(x => x.Lesson.IdTypeLesson.NameType == "Лекции");
                
            }


            return _tempList;
        }

        public List<LessonsModel> GetLessonModelByDisciplineLectionTime(int idDiscipline,string start, string end)
        {
            var _dbData = _dataManager.Lessons.GetAllLessons(true);
            List<LessonsModel> _modelListByDiscipline = new List<LessonsModel>();
            List<LessonsModel> _tempList = new List<LessonsModel>();
            List<LessonsModel> _modelListByDisciplineLection = new List<LessonsModel>();


            var cultureInfo = new CultureInfo("ru-RU");
            var dateTimeStart = DateTime.Parse(start, cultureInfo);
            var dateTimeEnd = DateTime.Parse(end, cultureInfo);


            foreach (var item in _dbData)
            {
                if (item.Date >= dateTimeStart && item.Date <= dateTimeEnd)
                {
                    if (item.IdDiscipline.Id == idDiscipline)
                    {
                        _modelListByDiscipline.Add(LessonModelDBToViewById(item.Id));
                    }
                }
                _tempList = _modelListByDiscipline.FindAll(x => x.Lesson.IdTypeLesson.NameType == "Лекции");

            }


            return _tempList;
        }


        //Список всех всех практик по дисциплине
        public List<LessonsModel> GetLessonModelByDisciplinePractics(int idDiscipline)
        {
            var _dbData = _dataManager.Lessons.GetAllLessons(true);
            List<LessonsModel> _modelListByDiscipline = new List<LessonsModel>();
            List<LessonsModel> _modelListByDisciplinePractics = new List<LessonsModel>();

            foreach (var item in _dbData)
            {
                if (item.IdDiscipline.Id == idDiscipline)
                {
                    _modelListByDiscipline.Add(LessonModelDBToViewById(item.Id));
                }
                _modelListByDisciplinePractics = _modelListByDiscipline.FindAll(x => x.Lesson.IdTypeLesson.NameType == "Практика");
            }
            return _modelListByDisciplinePractics;
        }

        public List<LessonsModel> GetLessonModelByDisciplinePracticsTime(int idDiscipline, string start, string end)
        {
            var _dbData = _dataManager.Lessons.GetAllLessons(true);
            List<LessonsModel> _modelListByDiscipline = new List<LessonsModel>();
            List<LessonsModel> _modelListByDisciplinePractics = new List<LessonsModel>();

            var cultureInfo = new CultureInfo("ru-RU");
            var dateTimeStart = DateTime.Parse(start, cultureInfo);
            var dateTimeEnd = DateTime.Parse(end, cultureInfo);

            foreach (var item in _dbData)
            {
                if (item.Date >= dateTimeStart && item.Date <= dateTimeEnd)
                {
                    if (item.IdDiscipline.Id == idDiscipline)
                    {
                        _modelListByDiscipline.Add(LessonModelDBToViewById(item.Id));
                    }
                }
                _modelListByDisciplinePractics = _modelListByDiscipline.FindAll(x => x.Lesson.IdTypeLesson.NameType == "Практика");
            }
            return _modelListByDisciplinePractics;
        }



        //Список всех всех лаб по дисциплине
        public List<LessonsModel> GetLessonModelByDisciplineLab(int idDiscipline)
        {
            var _dbData = _dataManager.Lessons.GetAllLessons(true);
            List<LessonsModel> _modelListByDiscipline = new List<LessonsModel>();
            List<LessonsModel> _modelListByDisciplineLabs = new List<LessonsModel>();
            foreach (var item in _dbData)
            {
                if (item.IdDiscipline.Id == idDiscipline)
                {
                    _modelListByDiscipline.Add(LessonModelDBToViewById(item.Id));
                }
                _modelListByDisciplineLabs =_modelListByDiscipline.FindAll(x => x.Lesson.IdTypeLesson.NameType == "Лабораторные работы");
            }
            return _modelListByDisciplineLabs;
        }

        public List<LessonsModel> GetLessonModelByDisciplineLabTime(int idDiscipline, string start, string end)
        {
            var _dbData = _dataManager.Lessons.GetAllLessons(true);
            List<LessonsModel> _modelListByDiscipline = new List<LessonsModel>();
            List<LessonsModel> _modelListByDisciplineLabs = new List<LessonsModel>();

            var cultureInfo = new CultureInfo("ru-RU");
            var dateTimeStart = DateTime.Parse(start, cultureInfo);
            var dateTimeEnd = DateTime.Parse(end, cultureInfo);

            foreach (var item in _dbData)
            {
                if (item.Date >= dateTimeStart && item.Date <= dateTimeEnd)
                {
                    if (item.IdDiscipline.Id == idDiscipline)
                    {
                        _modelListByDiscipline.Add(LessonModelDBToViewById(item.Id));
                    }
                }
                _modelListByDisciplineLabs = _modelListByDiscipline.FindAll(x => x.Lesson.IdTypeLesson.NameType == "Лабораторные работы");
            }
            return _modelListByDisciplineLabs;
        }



        //Переброс из модели во вьюху по айди
        public LessonsModel LessonModelDBToViewById(int idLesson)
        {
            var _dataDb = _dataManager.Lessons.GetLessonsById(idLesson,true);

            DisciplineModel _disciplineModels = new DisciplineModel();
            List<ScoreModel> _scoreModels = new List<ScoreModel>();
            TypeLessonModel _typeLessonModel = new TypeLessonModel();
            _disciplineModels = _disciplineService.DisciplineModelDBToViewById(_dataDb.IdDiscipline.Id);
            _typeLessonModel = _typeLessonService.TypeLessonModelDBToViewById(_dataDb.IdTypeLesson.Id);

            foreach(var item in _dataDb.IdScore)
            {
                _scoreModels.Add(_scoreService.ScoreModelDBToViewById(item.Id));

            }

            //_scoreModels = _scoreService.ScoreModelDBToViewById(_dataDb.IdScore.Id);



            return new LessonsModel()
            {
                Lesson = _dataDb,
                Discipline = _disciplineModels,
                Score = _scoreModels,
                TypeLesson = _typeLessonModel,
            };
        }
        
      



        // метод для проверки есть ли лекции в уроках по дисциплине
        public bool LessonModelIsLection(int idDiscipline)
        {
            var _dbData = _dataManager.Lessons.GetAllLessons(true);
            List<LessonsModel> _modelList = new List<LessonsModel>();

            foreach (var item in _dbData)
            {
                if (item.IdDiscipline.Id == idDiscipline)
                {
                    _modelList.Add(LessonModelDBToViewById(item.Id));
                }
            }
            if(_modelList.Any(x => x.Lesson.IdTypeLesson.NameType == "Лекции"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // метод для проверки есть ли практика в уроках по дисциплине

        public bool LessonModelIsPractics(int idDiscipline)
        {
            var _dbData = _dataManager.Lessons.GetAllLessons(true);
            List<LessonsModel> _modelList = new List<LessonsModel>();

            foreach (var item in _dbData)
            {
                if (item.IdDiscipline.Id == idDiscipline)
                {
                    _modelList.Add(LessonModelDBToViewById(item.Id));
                }
            }
            if (_modelList.Any(x => x.Lesson.IdTypeLesson.NameType == "Практика"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // метод для проверки есть ли лабораторные работы в уроках по дисциплине

        public bool LessonModelIsLabs(int idDiscipline)
        {
            var _dbData = _dataManager.Lessons.GetAllLessons(true);
            List<LessonsModel> _modelList = new List<LessonsModel>();

            foreach (var item in _dbData)
            {
                if (item.IdDiscipline.Id == idDiscipline)
                {
                    _modelList.Add(LessonModelDBToViewById(item.Id));
                }
            }
            if (_modelList.Any(x => x.Lesson.IdTypeLesson.NameType == "Лабораторные работы"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Общая статисtика для лекции
        public double GetStatisticsLessonByLection(int idDiscipline)
        {
            List<LessonsModel> _listLection = GetLessonModelByDisciplineLection(idDiscipline);
            double statistic = 0;
            int j = 0;

            foreach (var i in _listLection)
            {
                foreach(var item in i.Score)
                {
                    j++;
                    if (item.Score.Evalution == null || item.Score.Evalution == "")
                    {
                        statistic = statistic + 0;
                    }
                    else
                    {
                        statistic = statistic + Convert.ToDouble(item.Score.Evalution);
                    }
                }
            }

            return statistic/j;
        }

        public double GetStatisticsLessonByLectionTime(int idDiscipline, string start, string end)
        {
            List<LessonsModel> _listLection = GetLessonModelByDisciplineLectionTime(idDiscipline, start, end);
            if (_listLection.Count() == 0)
            {
                return 0;
            }
            double statistic = 0;
            int j = 0;

            foreach (var i in _listLection)
            {
             
                foreach (var item in i.Score)
                {
                    j++;
                    if (item.Score.Evalution == null)
                    {
                        statistic = statistic + 0;
                    }
                    else
                    {
                        statistic = statistic + Convert.ToDouble(item.Score.Evalution);
                    }
                }
            }

            return statistic / j;
        }

        //Общая статисtика для лекции
        public double GetStatisticsLessonByPractics(int idDiscipline)
        {
            List<LessonsModel> _listPractics = GetLessonModelByDisciplinePractics(idDiscipline);
            double statistic = 0;
            int j = 0;

            foreach (var i in _listPractics)
            {
             
                foreach (var item in i.Score)
                {
                    j++;
                    if (item.Score.Evalution == null || item.Score.Evalution == "")
                    {
                        statistic = statistic + 0;
                    }
                    else
                    {
                        statistic = statistic + Convert.ToDouble(item.Score.Evalution);
                    }
                }
            }

            return statistic / j;
        }

        public double GetStatisticsLessonByPracticsTime(int idDiscipline, string start, string end)
        {
            List<LessonsModel> _listPractics = GetLessonModelByDisciplinePracticsTime(idDiscipline, start, end);

            if (_listPractics.Count() == 0)
            {
                return 0;
            }

            double statistic = 0;
            int j = 0;

            foreach (var i in _listPractics)
            {
            
                foreach (var item in i.Score)
                {
                    j++;
                    if (item.Score.Evalution == null)
                    {
                        statistic = statistic + 0;
                    }
                    else
                    {
                        statistic = statistic + Convert.ToDouble(item.Score.Evalution);
                    }
                }
            }

            return statistic / j;
        }

        //Общая статисtика для лаб
        public double GetStatisticsLessonByLabs(int idDiscipline)
        {
            List<LessonsModel> _listLab = GetLessonModelByDisciplineLab(idDiscipline);
            double statistic = 0;
            int j = 0;

            foreach (var i in _listLab)
            {
                
                foreach (var item in i.Score)
                {
                    j++;
                    if (item.Score.Evalution == null || item.Score.Evalution == "")
                    {
                        statistic = statistic + 0;
                    }
                    else
                    {
                        statistic = statistic + Convert.ToDouble(item.Score.Evalution);
                    }
                }
            }

            return statistic / j;
        }

        public double GetStatisticsLessonByLabsTime(int idDiscipline, string start, string end)
        {
            List<LessonsModel> _listLab = GetLessonModelByDisciplineLabTime(idDiscipline, start, end);

            if (_listLab.Count() == 0)
            {
                return 0;
            }

            double statistic = 0;
            int j = 0;

            foreach (var i in _listLab)
            {
                
                foreach (var item in i.Score)
                {
                    j++;
                    if (item.Score.Evalution == null)
                    {
                        statistic = statistic + 0;
                    }
                    else
                    {
                        statistic = statistic + Convert.ToDouble(item.Score.Evalution);
                    }
                }
            }

            return statistic / j;
        }




        public double GetStatisticsLessonsByAllTypeLesson(int idDiscipline)
        {
            double res = GetStatisticsLessonByPractics(idDiscipline) + GetStatisticsLessonByLabs(idDiscipline) + GetStatisticsLessonByLection(idDiscipline);
            return res;
        }

        public double GetStatisticsLessonsByAllTypeLessonTime(int idDiscipline, string start, string end)
        {
            double res = GetStatisticsLessonByPracticsTime(idDiscipline, start, end) + GetStatisticsLessonByLabsTime(idDiscipline, start, end) + GetStatisticsLessonByLectionTime(idDiscipline, start, end);
            return res;
        }


        public double GetStatisticByPos(int idGroup,int idDiscipline)
        {
            List<LessonsModel> _listPos = GetLessonModelByDiscipline(idDiscipline);
            List<UsersModel> _listStudent = new List<UsersModel>();
            _listStudent = _usersService.GetStudentListByGroup(idGroup);

            if (_listPos.Count() == 0)
            {
                return 0;
            }

            double statistic = 0;
            int j = 0;
            int countN = 0;
            double precent = 100;
            foreach (var i in _listPos)
            {
                j++;
                foreach (var item in i.Score)
                {
                    if (item.Score.Attendance == true)
                    {
                        countN++;
                    }
                }
            }
            double temp = precent / (j * _listStudent.Count());
            statistic = temp * countN;

            return statistic;
        }


        public double GetStatisticByPosTime(int idGroup,int idDiscipline, string start, string end)
        {
            List<LessonsModel> _listPos = GetLessonModelByDisciplineTime(idDiscipline, start, end);
            List<UsersModel> _listStudent = new List<UsersModel>();
            _listStudent = _usersService.GetStudentListByGroup(idGroup);
            double statistic = 0;
            int j = 0;
            int countN = 0;
            double precent = 100;
            foreach (var i in _listPos)
            {
                j++;
                foreach (var item in i.Score)
                {
                    if (item.Score.Attendance == true)
                    {
                        countN++;
                    }
                }
            }
            double temp = precent / (j * _listStudent.Count());
            statistic = temp * countN;

            return statistic;
        }

        public void DeleteLessonsByGroup(int idGroup)
        {
            var _temp = _dataManager.Lessons.GetAllLessonsNotAsNoTracking(true);
            foreach (var item in _temp)
            {
                if (item.IdDiscipline == null)
                {
                    continue;
                }
                if (item.IdDiscipline.IdGroup.Id == idGroup)
                {
                    _dataManager.Lessons.DeleteLesson(item);
                }
            }
        }

        public void DeleteLessonsByIdDiscipline(int idDicipline)
        {
            var _temp = _dataManager.Lessons.GetAllLessonsNotAsNoTracking(true);
            foreach (var item in _temp)
            {
                if (item.IdDiscipline == null)
                {
                    continue;
                }
                if (item.IdDiscipline.Id == idDicipline)
                {
                    _dataManager.Lessons.DeleteLesson(item);
                }
            }
        }

        public void CreateLessonsLection(string NameLesson, DateTime dateTime, string NameDiscipline)
        {
            Lesson lesson = new();
            lesson.NameLesson = NameLesson;
            lesson.Date = dateTime;
            lesson.IdDiscipline = _dataManager.Disciplines.GetDisciplineByName(NameDiscipline);
            lesson.IdTypeLesson = _dataManager.TypeLesson.GetTypeLessonByName("Лекции");
            _dataManager.Lessons.CreateLesson(lesson);
        }
        public void CreateLessonsPractics(string NameLesson, DateTime dateTime, string NameDiscipline)
        {
            Lesson lesson = new();
            lesson.NameLesson = NameLesson;
            lesson.Date = dateTime;
            lesson.IdDiscipline = _dataManager.Disciplines.GetDisciplineByName(NameDiscipline);
            lesson.IdTypeLesson = _dataManager.TypeLesson.GetTypeLessonByName("Практика");
            _dataManager.Lessons.CreateLesson(lesson);

        }
        public void CreateLessonsLabs(string NameLesson, DateTime dateTime, string NameDiscipline)
        {
            Lesson lesson = new();
            lesson.NameLesson = NameLesson;
            lesson.Date = dateTime;
            lesson.IdDiscipline = _dataManager.Disciplines.GetDisciplineByName(NameDiscipline);
            lesson.IdTypeLesson = _dataManager.TypeLesson.GetTypeLessonByName("Лабораторные работы");
            _dataManager.Lessons.CreateLesson(lesson);

        }

        public LessonsModel GetLessonById(int id)
        {
            Lesson _lesson = _dataManager.Lessons.GetLessonsById(id, true);
            return LessonModelDBToViewById(_lesson.Id);
        }

        public LessonsModel GetLessonByFullField(string name, string nameDiscipline, string typeLesson, DateTime date)
        {
            var _allLesson = _dataManager.Lessons.GetAllLessons(true);

            var _temp = _allLesson.Where(x => x.NameLesson == name && x.IdDiscipline.Name == nameDiscipline &&
             x.IdTypeLesson.NameType == typeLesson && x.Date == date).FirstOrDefault();
            return LessonModelDBToViewById(_temp.Id);
        }

    }
}
