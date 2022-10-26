using BusinessLayer;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    public class RolesOfUsersService
    {
        private DataManager _dataManager;

        public RolesOfUsersService(DataManager dataManager)
        {
            this._dataManager = dataManager;
        }

        //Вывод ролей
        public List<RolesOfUsersModel> GetRolesList()
        {
            var _dataDB = _dataManager.RolesOfUsers.GetAllRoles();
            List<RolesOfUsersModel> allRoles = new List<RolesOfUsersModel>();
            foreach(var item in _dataDB)
            {
                allRoles.Add(GetRolesById(item.Id));
            }
            return allRoles;
        }

        public RolesOfUsersModel GetRolesById(int id)
        {
            var _dataDB = _dataManager.RolesOfUsers.GetRole(id);
            return new RolesOfUsersModel()
            {
                Role = _dataDB
            };
        }

        public RolesOfUsersModel GetRolesByName(string name)
        {
            var _dataDB = _dataManager.RolesOfUsers.GetRoleByName(name);
            return new RolesOfUsersModel()
            {
                Role = _dataDB
            };
        }

    }

}
