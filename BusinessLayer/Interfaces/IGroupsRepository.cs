using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IGroupsRepository
    {
        public void CreateGroup(Groups group);
        public void DeleteGroup(Groups groups);
        public IEnumerable<Groups> GetAllGroups();
        public Groups GetGroupsById(int id);
        public Groups GetGroupsByName(string name);
        public bool CheckGroup(string name);
        public IEnumerable<Groups> GetAllGroupsByName(string name);
    }
}
