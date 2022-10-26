using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IRolesOfUsersRepository
    {
        void CreateRole(RolesOfUsers role);
        void DeleteRole(RolesOfUsers role);
        RolesOfUsers GetRole(int id);
        RolesOfUsers GetRoleByName(string name);

        IEnumerable<RolesOfUsers> GetAllRoles();
    }
}
