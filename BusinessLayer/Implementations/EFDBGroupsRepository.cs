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
    public class EFDBGroupsRepository : IGroupsRepository
    {
        private EFDBContext context;

        public EFDBGroupsRepository(EFDBContext context)
        {
            this.context = context;
        }
        public void CreateGroup(Groups group)
        {
            context.Add(group);
            context.SaveChanges();
        }

        public void DeleteGroup(Groups groups)
        {
            context.Groups.Remove(groups);
            context.SaveChanges();
        }

        public IEnumerable<Groups> GetAllGroups() => context.Groups.ToList();
        public IEnumerable<Groups> GetAllGroupsByName(string name) 
        {
          
            return context.Groups.Where(x => x.Name == name).ToList();
            
        }


        public Groups GetGroupsById(int id) => context.Groups.FirstOrDefault(x => x.Id == id);

        public Groups GetGroupsByName(string name) => context.Groups.FirstOrDefault(x => x.Name == name);

        public bool CheckGroup(string name)
        {
            return context.Groups.Any(x => x.Name == name);
        }

    }
}
