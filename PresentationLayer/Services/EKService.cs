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
    public class EKService
    {

        private DataManager _dataManager;
        private UsersService _usersService;
        private DisciplineService _disciplineService;

        public EKService(DataManager dataManager)
        {
            this._dataManager = dataManager;
            _usersService = new UsersService(_dataManager);
            _disciplineService = new DisciplineService(_dataManager);
        }

        public void CreateEk(DateTime date, int idStudent, int idDisciplie)
        {
            EK ek = new();
            ek.EKScore = 0;
            ek.Date = date;
            ek.IdStudent = _dataManager.Users.GetUserById(idStudent);
            ek.IdDiscipline = _dataManager.Disciplines.GetDisciplineById(idDisciplie);

            _dataManager.Ek.CreateEK(ek);
        }

        public EKModel EkModelDBToViewById(int idEk)
        {
            var _dataDb = _dataManager.Ek.GetEkById(idEk, true);
            UsersModel usersModel = new UsersModel();
            DisciplineModel discipline = new DisciplineModel();
            usersModel = _usersService.UsersModelDBToViewById(_dataDb.IdStudent.Id);
            discipline = _disciplineService.DisciplineModelDBToViewById(_dataDb.IdDiscipline.Id);
            return new EKModel()
            {
                ek = _dataDb,
                Discipline = discipline,
                Users = usersModel,
            };

        }

        



        public List<EKModel> GetAllEKByDate(DateTime date)
        {
            string newDate = date.ToString("MM.yyyy");
            var list = _dataManager.Ek.GetAllEk(true);
            List<EKModel> sessionScore = new List<EKModel>();
            foreach (var item in list)
            {
                if (item.Date.ToString("MM.yyyy") == newDate)
                {
                    sessionScore.Add(EkModelDBToViewById(item.Id));
                }
            }
            return sessionScore;
        }

        public List<EKModel> GetAllEKByDiscipline(int idDiscpline)
        {
            var _allListEK = _dataManager.Ek.GetAllEk(true);
            List<EKModel> model = new();
            foreach (var item in _allListEK)
            {
                if (item.IdDiscipline.Id == idDiscpline)
                {
                    model.Add(EkModelDBToViewById(item.Id));
                }
            }
            return model;

        }


        public List<EKModel> GetAllEKByDisciplineByDate(int idDiscpline, DateTime date)
        {
            var _allListEK = GetAllEKByDate(date);
            List<EKModel> model = new();
            foreach (var item in _allListEK)
            {
                if (item.Discipline.Discipline.Id == idDiscpline)
                {
                    model.Add(item);
                }
            }
            return model;

        }

        public List<EKModel> GetAllEKByDisciplineByDateForStudent(int idDiscpline, DateTime date, int idStudent)
        {
            var _allListEK = GetAllEKByDate(date);
            List<EKModel> model = new();
            foreach (var item in _allListEK)
            {
                if (item.Discipline.Discipline.Id == idDiscpline)
                {
                    if (item.ek.IdStudent.Id == idStudent)
                    {
                        model.Add(item);
                    }
                }
            }
            return model;

        }


        public void EditEkScore(int idEk, int EkScore)
        {
            var ekId = _dataManager.Ek.GetEkById(idEk);
            ekId.EKScore = EkScore;
            _dataManager.Ek.EditEk(ekId);
        }
       
        public void DeleteEkScoreByIdStudent(int idStudent)
        {

            List<EK> _dataDb = _dataManager.Ek.GetAllScoreByIdStudent(idStudent);
            _dataManager.Ek.DeleteScories(_dataDb);
        }
    }
}
