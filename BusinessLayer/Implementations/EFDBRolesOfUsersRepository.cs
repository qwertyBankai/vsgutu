using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations
{
    public class EFDBRolesOfUsersRepository : IRolesOfUsersRepository
    {
        private EFDBContext context;
        public EFDBRolesOfUsersRepository(EFDBContext context)
        {
            this.context = context;
        }
        public void CreateRole(RolesOfUsers role)
        {
            context.Add(role);
            context.SaveChanges();
        }

        public void DeleteRole(RolesOfUsers role)
        {
            context.Remove(role);
            context.SaveChanges();
        }

        public RolesOfUsers GetRole(int id) => context.RolesOfUsers.FirstOrDefault(x => x.Id == id);
        public RolesOfUsers GetRoleByName(string name) => context.RolesOfUsers.FirstOrDefault(x => x.Role == name);

        public IEnumerable<RolesOfUsers> GetAllRoles() => context.RolesOfUsers.ToList();

    }
}
