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
        void CreateDiscipline(Discipline discipline);
        void DeleteDiscipline(Discipline discipline);
        public void DeleteDisciplines(List<Discipline> discipline);
        void Edit(Discipline discipline);
        IEnumerable<Discipline> GetAllDiscipline(bool includes = false);
        IEnumerable<Discipline> GetAllDisciplineByName(string name, bool includes = false);
        Discipline GetDisciplineById(int id, bool includes = false);
        Discipline GetDisciplineByName(string name, bool includes = false);
        public List<Discipline> GetDisciplineByGroup(int idGroup);
        /*public List<Discipline> GetDisciplineForStudent(int idStudent);
        public List<Discipline> GetDisciplineForTeacher(int idStudent);*/

    }
}
