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
    public class SessionScoreService
    {
        private DataManager _dataManager;
        private UsersService _usersService;
        private DisciplineService _disciplineService;

        public SessionScoreService(DataManager dataManager)
        {
            this._dataManager = dataManager;
            _usersService = new UsersService(_dataManager);
            _disciplineService = new DisciplineService(_dataManager);
        }
        public void CreateSessionScoreForCreateInDiscipline(int idDiscipline, DateTime date, string nameGroup)
        {
            var groups = _dataManager.Groups.GetGroupsByName(nameGroup);
            var listStudent = _usersService.GetStudentListByGroup(groups.Id);

            foreach(var item in listStudent)
            {
                SessionScore sessionScore = new();
                sessionScore.IdDiscipline = _dataManager.Disciplines.GetDisciplineById(idDiscipline);
                sessionScore.Date = date;
                sessionScore.ScoreSession = 0;
                sessionScore.IdStudent = _dataManager.Users.GetUserById(item.Users.Id);
                _dataManager.SessionScore.CreateSessionScore(sessionScore);
            }
            
        }


        public SessionScoreModel SessionScoreModelDBToViewById(int idSessionScore)
        {
            var _dataDb = _dataManager.SessionScore.GetSessionScoreById(idSessionScore, true);
            UsersModel usersModel = new UsersModel();
            DisciplineModel discipline = new DisciplineModel();
            usersModel = _usersService.UsersModelDBToViewById(_dataDb.IdStudent.Id);
            discipline = _disciplineService.DisciplineModelDBToViewById(_dataDb.IdDiscipline.Id);
            return new SessionScoreModel()
            {
                SessionScore = _dataDb,
                Discipline = discipline,
                Users = usersModel,
            };

        }


        public List<SessionScoreModel> GetAllSessionScoreByDiscipline(int idDiscpline)
        {
            var listSessionScore = _dataManager.SessionScore.GetAllSessionScore(true);
            List<SessionScoreModel> sessionScore = new List<SessionScoreModel>();
            foreach(var item in listSessionScore)
            {
                if(item.IdDiscipline.Id == idDiscpline)
                {
                    sessionScore.Add(SessionScoreModelDBToViewById(item.Id));
                }
            }
            return sessionScore;
        }

        public List<SessionScoreModel> GetAllSessionScoreByDisciplineByDate(int idDiscpline, string date)
        {
            var listSessionScore = GetAllSessionScoreByDate(date);
            List<SessionScoreModel> sessionScore = new List<SessionScoreModel>();
            foreach (var item in listSessionScore)
            {
                if (item.Discipline.Discipline.Id == idDiscpline)
                {
                    sessionScore.Add(item);
                }
            }
            return sessionScore;
        }

        public List<SessionScoreModel> GetAllSessionScoreByDisciplineByDateForStudent(int idDiscpline, string date, int idStudent)
        {
            var listSessionScore = GetAllSessionScoreByDate(date);
            List<SessionScoreModel> sessionScore = new List<SessionScoreModel>();
            foreach (var item in listSessionScore)
            {
                if (item.Discipline.Discipline.Id == idDiscpline)
                {
                    if (item.Users.Users.Id == idStudent)
                    {
                        sessionScore.Add(item);
                    }
                }
            }
            return sessionScore;
        }



        public List<SessionScoreModel> GetAllSessionScoreByDate(string date)
        {
            DateTime newDate = DateTime.Parse(date);
            //string newDate = date.ToString("MM.dd.yyyy");
            var list = _dataManager.SessionScore.GetAllSessionScore(true);
            List<SessionScoreModel> sessionScore = new List<SessionScoreModel>();
            foreach(var item in list)
            {
                if(item.Date.ToString("MM.dd.yyyy") == newDate.ToString("MM.dd.yyyy"))
                {
                    sessionScore.Add(SessionScoreModelDBToViewById(item.Id));
                }
            }
            return sessionScore;
        }

        public void EditSessionScore(int idSessionScore, int SessionScore)
        {
            var sessionScoreById = _dataManager.SessionScore.GetSessionScoreById(idSessionScore);
            sessionScoreById.ScoreSession = SessionScore;
            _dataManager.SessionScore.EditSessionScore(sessionScoreById);
        }

        public void DeleteSessionScoreByIdStudent(int idStudent)
        {

            List<SessionScore> _dataDb = _dataManager.SessionScore.GetAllScoreByIdStudent(idStudent);
            _dataManager.SessionScore.DeleteScories(_dataDb);
        }

    }
}
