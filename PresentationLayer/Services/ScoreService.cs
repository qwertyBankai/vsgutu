using BusinessLayer;
using DataLayer.Entities;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    public class ScoreService
    {
        private DataManager _dataManager;
        private UsersService _userService;

        public ScoreService(DataManager dataManager)
        {
            this._dataManager = dataManager;
            _userService = new UsersService(_dataManager);
        }

        public ScoreModel ScoreModelDBToViewById(int idScore)
        {
            var _dataDb = _dataManager.Scories.GetScore(idScore, true);

            UsersModel _usersModel = new UsersModel();

            _usersModel = _userService.UsersModelDBToViewById(_dataDb.IdStudent.Id);

            if (_dataDb.Attendance == true)
            {
                _dataDb.Evalution = null;
            }
            else
            {
                _dataDb.Evalution = _dataDb.Evalution;
            }


            return new ScoreModel()
            {
                Users = _usersModel,
                Score = _dataDb,
            };
        }

        public void DeleteScoreByIdStudent(int idStudent)
        {
            List<Score> _dataDb = _dataManager.Scories.GetAllScoreByIdStudent(idStudent);
            _dataManager.Scories.DeleteScories(_dataDb);
        }

        public void CreateScoreForPageSecond(int idStudent, int idLesson)
        {
            Score score = new();
            score.Evalution = null;
            score.IdStudent = _dataManager.Users.GetUserById(idStudent);
            score.Attendance = false;
            score.IdLesson = _dataManager.Lessons.GetLessonsById(idLesson);
            _dataManager.Scories.CreateScore(score);
        }

        public void EditScoreEvalution(int idScore, string Evalution)
        {
            var _temp = _dataManager.Scories.GetScore(idScore);
            _temp.Evalution = Evalution;
            _dataManager.Scories.EditScore(_temp);
        }

        public void EditScoreAttendace(int idScore, bool Attendance)
        {
            var _temp = _dataManager.Scories.GetScore(idScore);
            _temp.Attendance = Attendance;
            _dataManager.Scories.EditScore(_temp);
        }
    }
}
