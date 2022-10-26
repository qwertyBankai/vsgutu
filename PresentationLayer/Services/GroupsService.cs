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
    public class GroupsService
    {
        private DataManager _dataManager;
        public GroupsService(DataManager dataManager)
        {
            this._dataManager = dataManager;
        }

        //фильтрация групп

        //создание групп
        public void CreateGroup(GroupsModel createGroup)
        {
            Groups _groupsModel = new();
            _groupsModel.Name = createGroup.Groups.Name;
            _dataManager.Groups.CreateGroup(_groupsModel);
            /*return GroupsModelDBToViewById(_groupsModel.Id);*/
        }


        //Удаление групп

        public void DeleteGroups(int idGroup)
        {
            var group = _dataManager.Groups.GetGroupsById(idGroup);

            _dataManager.Groups.DeleteGroup(group);

        }

        //Вывод групп
        public List<GroupsModel> GetGroupsList()
        {
            var _dataDB = _dataManager.Groups.GetAllGroups();
            List<GroupsModel> _modelList = new List<GroupsModel>();
            foreach(var i in _dataDB)
            {
                _modelList.Add(GroupsModelDBToViewById(i.Id));
            }
            return _modelList;
        }


        public List<GroupsModel> GetGroupsListByName(string name)
        {
            var _dataDB = _dataManager.Groups.GetAllGroupsByName(name);
            List<GroupsModel> _modelList = new List<GroupsModel>();
            foreach (var i in _dataDB)
            {
                _modelList.Add(GroupsModelDBToViewById(i.Id));
            }
            return _modelList;
        }

        //??
        public GroupsModel GroupsModelDBToViewById(int idGroups)
        {
            var _model = new GroupsModel()
            {
                Groups = _dataManager.Groups.GetGroupsById(idGroups),

            };

            return _model;
        }

        public GroupsModel GroupsModelDBToViewByName(string name)
        {
            var _model = new GroupsModel()
            {
                Groups = _dataManager.Groups.GetGroupsByName(name),

            };

            return _model;
        }

        public List<GroupsModel> SearchGroupByName(string name)
        {
            var _dataDB = _dataManager.Groups.GetAllGroupsByName(name);
            List<GroupsModel> _modelList = new List<GroupsModel>();
            foreach (var item in _dataDB)
            {
                _modelList.Add(GroupsModelDBToViewByName(item.Name));
            }

            return _modelList;
        }

        public GroupsModel GetGroupByName(string name)
        {
            var _dataDB = _dataManager.Groups.GetGroupsByName(name);
            return GroupsModelDBToViewById(_dataDB.Id);
        }

        public bool CheckGroup(string name)
        {
            if (_dataManager.Groups.CheckGroup(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
