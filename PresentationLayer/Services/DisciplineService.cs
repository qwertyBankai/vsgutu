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
    public class DisciplineService
    {

        private DataManager _dataManager;
        private GroupsService _groupsService;
       /* private LessonService _lessonService;*/
        public DisciplineService(DataManager dataManager)
        {
            this._dataManager = dataManager;
            _groupsService = new GroupsService(dataManager);
            /*_lessonService = new LessonService(_dataManager);*/
        }


        public DisciplineModel DisciplineModelDBToViewById(int idDiscipline)
        {
            var _dataDb = _dataManager.Disciplines.GetDisciplineById(idDiscipline, true);

            GroupsModel _groupsModels = new GroupsModel();
            List<LessonsModel> _lessonModel = new List<LessonsModel>();
            _groupsModels = _groupsService.GroupsModelDBToViewById(_dataDb.IdGroup.Id);

            return new DisciplineModel()
            {
                Discipline = _dataDb,
                Groups = _groupsModels, 
            };
        }

        public DisciplineModel DisciplineModelDBToViewByName(string name)
        {
            var _dataDb = _dataManager.Disciplines.GetDisciplineByName(name, true);

            GroupsModel _groupsModels = new GroupsModel();
            _groupsModels = _groupsService.GroupsModelDBToViewById(_dataDb.IdGroup.Id);

            return new DisciplineModel()
            {
                Discipline = _dataDb,
                Groups = _groupsModels,
            };
        }

        /*public DisciplineModel DisciplineModelDBToViewByIdForStudent(int idDiscipline, int IdStudent)
        {
            var _dataDb = _dataManager.Disciplines.GetDisciplineById(idDiscipline, true);

            GroupsModel _groupsModels = new GroupsModel();
            _groupsModels = _groupsService.GroupsModelDBToViewById(_dataDb.IdGroup.Id);

            return new DisciplineModel()
            {
                Discipline = _dataDb,
                Groups = _groupsModels,
            };
        }*/



        //Вывод групп
        public List<DisciplineModel> GetDisciplineList()
        {
            var _dataDb = _dataManager.Disciplines.GetAllDiscipline(true);
            List<DisciplineModel> _modelList = new List<DisciplineModel>();
            foreach(var i in _dataDb)
            {
                _modelList.Add(DisciplineModelDBToViewById(i.Id));
            }
            return _modelList;
        }

        public List<DisciplineModel> SearchJournal(string name)
        {
            var _dataDB = _dataManager.Disciplines.GetAllDisciplineByName(name, true);
            List<DisciplineModel> _modelList = new List<DisciplineModel>();
            foreach(var item in _dataDB)
            {
                _modelList.Add(DisciplineModelDBToViewByName(item.Name));
            }

            return _modelList;
        }
        public List<DisciplineModel> SearchJournalForStudent(string name, int idGroup)
        {
           // var _dataDB = _dataManager.Disciplines.GetAllDisciplineByName(name, true);
            var _dataTemp = _dataManager.Disciplines.GetDisciplineByGroup(idGroup);
            List<DisciplineModel> _modelList = new List<DisciplineModel>();
            foreach (var item in _dataTemp)
            {
                if (item.Name.Equals(name))
                {
                    _modelList.Add(DisciplineModelDBToViewById(item.Id));
                }
            }

            return _modelList;
        }



        public void DeleteDisciplineByGroup(int idGroup)
        {
            List<Discipline> _tempDiscipline = _dataManager.Disciplines.GetDisciplineByGroup(idGroup);
            _dataManager.Disciplines.DeleteDisciplines(_tempDiscipline);
        }
        public void DeleteDisciplineById(int idDiscipline)
        {
            IEnumerable<Discipline> _tempDiscipline = _dataManager.Disciplines.GetAllDisciplineById(idDiscipline, true);
            _dataManager.Disciplines.DeleteDisciplines(_tempDiscipline);
        }

        public void CreateDiscipline(string Name, DateTime yearsStart, DateTime yearsEnd, string GroupsName,
            string formAttestation, bool availabilityOfCoursework, int zet, int block, int idTeacher)
        {
            Discipline discipline = new();
            discipline.Name = Name;
            discipline.yearsStart = yearsStart;
            discipline.yearsEnd = yearsEnd;
            discipline.IdGroup = _dataManager.Groups.GetGroupsByName(GroupsName);
            discipline.formAttestation = formAttestation;
            discipline.availabilityOfCoursework = availabilityOfCoursework;
            discipline.IdTeacher = _dataManager.Users.GetUserById(idTeacher);
            discipline.zet = zet;
            discipline.block = block;
            _dataManager.Disciplines.CreateDiscipline(discipline);

        }

        public DisciplineModel GetDisciplineById(int id)
        {
            var _temp = _dataManager.Disciplines.GetDisciplineById(id, true);
            return DisciplineModelDBToViewById(_temp.Id);
        }
        public DisciplineModel GetDisciplineByName(string name)
        {
            var _temp = _dataManager.Disciplines.GetDisciplineByName(name, true);
            return DisciplineModelDBToViewById(_temp.Id);
        }

        public List<DisciplineModel> GetDisciplieForStudent(int idStudent)
        {
            var _temp = _dataManager.Users.GetUserById(idStudent,true);
            var _temp2 = _dataManager.Disciplines.GetDisciplineByGroup(_temp.IdGroup.Id);
            List<DisciplineModel> model = new();
            foreach(var item in _temp2)
            {
                model.Add(DisciplineModelDBToViewById(item.Id));
            }
            return model;
        }
    }
}
