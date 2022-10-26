using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUsersRepository
    {

        //NOT METHOD EDIT
        public void CreateUser(List<Users> newUsers);
        public void DeleteUser(Users user);
        public IEnumerable<Users> GetAllUsers(bool includes=false);

        public Users GetUserById(int userId, bool includes=false);
        public Users GetUserByIdNotFull(int userId);
        public Users GetUsersByName(string name, bool includes=false);
        public Users GetUsersByRole(string role, bool includes=false);
        public Users GetUsersByLoginAndPassword(string login, string password);
        public bool CheckUsers(string login, string password);
        public IEnumerable<Users> GetAllUsersByName(string name, bool includes = false);


    }
}
