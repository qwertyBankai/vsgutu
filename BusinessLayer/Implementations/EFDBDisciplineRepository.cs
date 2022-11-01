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
    public class EFDBDisciplineRepository : IDisciplineRepository
    {
        private EFDBContext context;

        public EFDBDisciplineRepository(EFDBContext context)
        {
            this.context = context;
        }
        public void CreateDiscipline(Discipline discipline)
        {
            context.Add(discipline);
            context.SaveChanges();
        }

        public void DeleteDiscipline(Discipline discipline)
        {
            context.Discipline.Remove(discipline);
            context.SaveChanges();
        }

        public void DeleteDisciplines(IEnumerable<Discipline> discipline)
        {
            foreach (var item in discipline)
            {
                context.Discipline.Remove(item);
            }
            context.SaveChanges();
        }

        public void Edit(Discipline discipline)
        {
            context.Entry(discipline).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Discipline> GetAllDiscipline(bool includes = false)
        {
            if (includes)
            {
                return context.Set<Discipline>().Include(x => x.IdGroup).AsNoTracking().ToList();
            }
            else
            {
                return context.Discipline.ToList();
            }
        }


        public IEnumerable<Discipline> GetAllDisciplineByName(string name, bool includes = false)
        {
            if (includes)
            {
                return context.Set<Discipline>().Where(x => x.Name == name).Include(x => x.IdGroup).AsNoTracking().ToList();
            }
            else
            {
                return context.Discipline.Where(x=>x.Name == name).ToList();
            }
        }

        public Discipline GetDisciplineById(int id, bool includes = false)
        {
            if (includes)
            {
                return context.Set<Discipline>().Include(x => x.IdGroup).AsNoTracking().FirstOrDefault(x => x.Id == id);
            }
            else
            {
                return context.Discipline.FirstOrDefault(x => x.Id == id);
            }
        }


        //????

        public Discipline GetDisciplineByName(string name, bool includes = false)
        {
            if (includes)
            {
                return context.Set<Discipline>().Include(x => x.IdGroup).AsNoTracking().FirstOrDefault(x => x.Name == name);
            }
            else
            {
                return context.Discipline.FirstOrDefault(x => x.Name == name);
            }
        }


        public List<Discipline> GetDisciplineByGroup(int idGroup)
        {
            return context.Set<Discipline>().Include(x => x.IdGroup).Include(x=> x.IdTeacher).Where(x=>x.IdGroup.Id == idGroup).ToList();
        }


       /* public List<Discipline> GetAllDisciplineForStudent(int idStudent) 
        {

            return context.Set<Discipline>().Where(x => x. == name).Include(x => x.IdGroup).AsNoTracking().ToList();

        }

        public List<Discipline> GetAllDisciplineForTeacher(string name, bool includes = false)
        {

        }*/

        public IEnumerable<Discipline> GetAllDisciplineById(int id, bool includes = false)
        {
            if (includes)
            {
                return context.Set<Discipline>().Where(x => x.Id == id).Include(x => x.IdGroup).Include(x => x.IdTeacher).ToList();
            }
            else
            {
                return context.Discipline.Where(x => x.Id == id).ToList();
            }
        }
    }
}
