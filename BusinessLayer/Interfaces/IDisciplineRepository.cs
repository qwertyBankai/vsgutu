using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IDisciplineRepository
    {
        public void CreateDiscipline(Discipline discipline);
        public void DeleteDiscipline(Discipline discipline);
        public void DeleteDisciplines(IEnumerable<Discipline> discipline);
        public  void Edit(Discipline discipline);
        public IEnumerable<Discipline> GetAllDiscipline(bool includes = false);
        public IEnumerable<Discipline> GetAllDisciplineById(int id, bool includes = false);
        public  IEnumerable<Discipline> GetAllDisciplineByName(string name, bool includes = false);
        public  Discipline GetDisciplineById(int id, bool includes = false);
        public Discipline GetDisciplineByName(string name, bool includes = false);
        public List<Discipline> GetDisciplineByGroup(int idGroup);
        /*public List<Discipline> GetDisciplineForStudent(int idStudent);
        public List<Discipline> GetDisciplineForTeacher(int idStudent);*/

    }
}
