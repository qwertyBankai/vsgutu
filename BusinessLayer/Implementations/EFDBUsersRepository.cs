using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations
{
    public class EFDBUsersRepository : IUsersRepository
    {
        private EFDBContext context;

        public EFDBUsersRepository(EFDBContext context)
        {
            this.context = context;
        }

        public void CreateUser(List<Users> newUsers)
        {
            foreach(var user in newUsers)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();
        }


        public void DeleteUser(Users user)
        {
            
            context.Users.Remove(user);
            context.SaveChanges();
        }



        public IEnumerable<Users> GetAllUsers(bool includes = false)
        {
            if (includes)
            {
               return context.Set<Users>().Include(x => x.IdRole).AsNoTracking().Include(x => x.IdGroup).AsNoTracking().ToList();
            }
            else
            {
                return context.Users.ToList();
            }
        }

        public IEnumerable<Users> GetAllUsersByName(string name,bool includes = false)
        {
            if (includes)
            {
                return context.Set<Users>().Where(x => x.Fio == name).Include(x => x.IdRole).AsNoTracking().Include(x => x.IdGroup).AsNoTracking().ToList();
            }
            else
            {
                return context.Users.Where(x => x.Fio == name).ToList();
            }
        }

        public Users GetUsersByName(string name, bool includes)
        {
            if (includes)
            {
               return context.Set<Users>().Include(x => x.IdRole).AsNoTracking().Include(x => x.IdGroup).AsNoTracking().FirstOrDefault(x => x.Fio == name);
            }
            else
            {
                return context.Users.FirstOrDefault(x => x.Fio == name);
            }
        }


        public Users GetUsersByRole(string role, bool includes)
        {
            if (includes)
            {
                return context.Set<Users>().Include(x => x.IdRole).AsNoTracking().Include(x => x.IdGroup).AsNoTracking().FirstOrDefault(x => x.IdRole.Role == role);
            }
            else
            {
                return context.Users.FirstOrDefault(x => x.IdRole.Role == role);
            }
        }


        public Users GetUserById(int userId, bool includes=false)
        {
            if (includes)
            {
                return context.Set<Users>().Include(x => x.IdRole)
                    .Include(x => x.IdGroup).FirstOrDefault(x => x.Id == userId);
            }
            else
            {
                return context.Users.FirstOrDefault(x => x.Id == userId);
            }
        }
        public Users GetUsersByLoginAndPassword(string login, string password)
        {
            return context.Set<Users>().Include(x => x.IdRole)
                    .Include(x => x.IdGroup).FirstOrDefault(x => x.Login == login && x.Password == password);
        }
        public bool CheckUsers(string login, string password)
        {
            return context.Set<Users>().Any(x => x.Login == login && x.Password == password);
        }
        public Users GetUserByIdNotFull(int userId)
        {
            return context.Set<Users>().Include(x => x.IdRole).FirstOrDefault(x => x.Id == userId);
        }


    }
}
