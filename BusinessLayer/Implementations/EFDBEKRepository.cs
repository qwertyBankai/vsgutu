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
    public class EFDBEKRepository : IEKRepository
    {
        private EFDBContext context;

        public EFDBEKRepository(EFDBContext context)
        {
            this.context = context;
        }

        public void CreateEK(EK eK)
        {
            context.EKs.Add(eK);
            context.SaveChanges();
        }
        public void DeleteScories(List<EK> score)
        {
            foreach (var item in score)
            {
                context.EKs.Remove(item);
            }
            context.SaveChanges();
        }
        public void EditEk(EK eK)
        {
            context.Entry(eK).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public List<EK> GetAllScoreByIdStudent(int idStudent)
        {
            return context.Set<EK>().Include(x => x.IdDiscipline).Include(x => x.IdStudent).Where(x => x.IdStudent.Id == idStudent).ToList();
        }


        public IEnumerable<EK> GetAllEk(bool includes = false)
        {
            if (includes)
            {
                return context.Set<EK>().Include(x => x.IdDiscipline).Include(x => x.IdStudent).AsNoTracking().ToList();
            }
            else
            {
                return context.Set<EK>().ToList();
            }
        }

        public EK GetEkById(int id, bool includes=false)
        {
            if (includes)
            {
                return context.Set<EK>().Include(x => x.IdDiscipline).Include(x => x.IdStudent).AsNoTracking().FirstOrDefault(x => x.Id == id);
            }
            else
            {
                return context.Set<EK>().FirstOrDefault(x => x.Id == id);

            }
        }
    }
}
