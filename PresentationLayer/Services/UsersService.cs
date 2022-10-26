using BusinessLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Helpers;

namespace PresentationLayer.Services
{
    public class UsersService
    {

        private DataManager _dataManager;
        private GroupsService _groupsService;
        private RolesOfUsersService _roleService;
        public UsersService(DataManager dataManager)
        {
            this._dataManager = dataManager;
            _groupsService = new GroupsService(dataManager);
            _roleService = new RolesOfUsersService(dataManager);
        }
        public UsersModel UsersModelDBToViewById(int userId)
        {
            var _DbData = _dataManager.Users.GetUserById(userId, true);

            RolesOfUsersModel _roleModel = new RolesOfUsersModel();
            GroupsModel _gropsModel = new GroupsModel();
            _gropsModel = _groupsService.GroupsModelDBToViewById(_DbData.IdGroup.Id);

            _roleModel = _roleService.GetRolesById(_DbData.IdRole.Id);

            return new UsersModel()
            {
                Users = _DbData,
                Group = _gropsModel,
                Role = _roleModel,
            };
        }

        public UsersModel UsersModelDBToViewByIdNotFull(int userId)
        {
            var _DbData = _dataManager.Users.GetUserById(userId, true);

            RolesOfUsersModel _roleModel = new RolesOfUsersModel();

            _roleModel = _roleService.GetRolesById(_DbData.IdRole.Id);

            return new UsersModel()
            {
                Users = _DbData,
                Group = null,
                Role = _roleModel,
            };
        }


        public UsersModel UsersModelDBToViewByIdForFullList(int userId)
        {
            var _DbData = _dataManager.Users.GetUserById(userId, true);
            RolesOfUsersModel _roleModel = new RolesOfUsersModel();
            _roleModel = _roleService.GetRolesById(_DbData.IdRole.Id);
            return new UsersModel()
            {
                Users = _DbData,
                Role = _roleModel,
            };
        }
        public List<UsersModel> GetUsersList()
        {
            var _dbData = _dataManager.Users.GetAllUsers(true);
            List<UsersModel> _modelList = new List<UsersModel>();
            foreach (var i in _dbData)
            {
                _modelList.Add(UsersModelDBToViewByIdForFullList(i.Id));

            }
            return _modelList;
        }

        public List<UsersModel> GetStudentListByGroup(int idGroup)
        {
            var _dbData = _dataManager.Users.GetAllUsers(true);
            List<UsersModel> _modelList = new List<UsersModel>();
            foreach(var i in _dbData)
            {
                if (i.IdRole.Role == "Студент")
                {
                    if (i.IdGroup.Id == idGroup)
                    {
                        _modelList.Add(UsersModelDBToViewById(i.Id));
                    }
                }
                
            }
            return _modelList;
        }

        public List<UsersModel> GetUsersListByName(string name)
        {
            var _dbData = _dataManager.Users.GetAllUsersByName(name ,true);
            List<UsersModel> _modelList = new List<UsersModel>();
            foreach (var i in _dbData)
            {
                _modelList.Add(UsersModelDBToViewByIdForFullList(i.Id));

            }
            return _modelList;
        }

        public void CreateAdmin(string path)
        {
            IEnumerable<string> fileFromAdmin = File.ReadLines(path);
            List<Users> newUsers = new();
            int i = fileFromAdmin.Count();
            string[] _temp = new string[i];

            foreach (var item in fileFromAdmin)
            {
                Users users = new(); 
                _temp = item.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string Login = _temp[0];
                string Password = _temp[1];
                string Fio = _temp[2];
                Fio += " ";
                Fio += _temp[3];
                Fio += " ";
                Fio += _temp[4];
                users.Login = Login;
                users.Password = PasswordEnc.HashPassword(Password);
                users.Fio = Fio;
                users.IdRole = _dataManager.RolesOfUsers.GetRoleByName("Администратор");
                newUsers.Add(users);
                
            }

            _dataManager.Users.CreateUser(newUsers);

        }



        public void CreateTeacher(string path)
        {
            IEnumerable<string> fileFromAdmin = File.ReadLines(path);
            List<Users> newUsers = new();
            int i = fileFromAdmin.Count();
            string[] _temp = new string[i];

            foreach (var item in fileFromAdmin)
            {
                Users users = new();
                _temp = item.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string Login = _temp[0];
                string Password = _temp[1];
                string Fio = _temp[2];
                Fio += " ";
                Fio += _temp[3];
                Fio += " ";
                Fio += _temp[4];
                users.Login = Login;
                users.Password = PasswordEnc.HashPassword(Password);
                users.Fio = Fio;
                users.IdRole = _dataManager.RolesOfUsers.GetRoleByName("Преподователь");
                newUsers.Add(users);

            }
            _dataManager.Users.CreateUser(newUsers);

        }

        public void CreateStudent(string path, string nameGroup)
        {
            IEnumerable<string> fileFromAdmin = File.ReadLines(path);
            List<Users> newUsers = new();
            int i = fileFromAdmin.Count();
            string[] _temp = new string[i];

            foreach (var item in fileFromAdmin)
            {
                Users users = new();
                _temp = item.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string Login = _temp[0];
                string Password = _temp[1];
                string Fio = _temp[2];
                Fio += " ";
                Fio += _temp[3];
                Fio += " ";
                Fio += _temp[4];
                users.Login = Login;
                users.Password = PasswordEnc.HashPassword(Password);
                users.Fio = Fio;
                users.IdRole = _dataManager.RolesOfUsers.GetRoleByName("Студент");
                users.IdGroup = _dataManager.Groups.GetGroupsByName(nameGroup);
                newUsers.Add(users);

            }
            _dataManager.Users.CreateUser(newUsers);

        }

        public void DeleteUsersById(int IdUser)
        {

            var _dataDbUsersList = _dataManager.Users.GetUserById(IdUser,true);
            _dataManager.Users.DeleteUser(_dataDbUsersList);
        }

        public UsersModel SearchUserByLoginAndPassword(string login, string password)
        {
            var _temp = _dataManager.Users.GetUsersByLoginAndPassword(login, password);
            return UsersModelDBToViewByIdNotFull(_temp.Id);
        }
        public bool CheckUsers(string login, string password)
        {
            bool _temp = _dataManager.Users.CheckUsers(login, password);
            if ( _temp == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public UsersModel GetUserById(int id)
        {
            var _temp = _dataManager.Users.GetUserByIdNotFull(id);
            return UsersModelDBToViewByIdNotFull(_temp.Id);
        }
        public UsersModel GetUserByIdForStudent(int id)
        {
            var _temp = _dataManager.Users.GetUserById(id, true);
            return UsersModelDBToViewById(_temp.Id);
        }
    }
}
