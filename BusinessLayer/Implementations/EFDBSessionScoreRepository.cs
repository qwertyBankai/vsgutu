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
    public class EFDBSessionScoreRepository : ISessionScoreRepository
    {
        private EFDBContext context;

        public EFDBSessionScoreRepository(EFDBContext context)
        {
            this.context = context;
        }

        public void DeleteScories(List<SessionScore> score)
        {
            foreach (var item in score)
            {
                context.sessionScores.Remove(item);
            }
            context.SaveChanges();
        }

        public List<SessionScore> GetAllScoreByIdStudent(int idStudent)
        {
            return context.Set<SessionScore>().Include(x => x.IdDiscipline).Include(x => x.IdStudent).Where(x => x.IdStudent.Id == idStudent).ToList();
        }


        public void CreateSessionScore(SessionScore sessionScore)
        {
            context.sessionScores.Add(sessionScore);
            context.SaveChanges();
        }
        public void DeleteSessionScore(SessionScore sessionScore)
        {
            context.sessionScores.Remove(sessionScore);
            context.SaveChanges();
        }
        public IEnumerable<SessionScore> GetAllSessionScore(bool includes = false)
        {
           
                if (includes)
                {
                    return context.Set<SessionScore>().Include(x => x.IdDiscipline).Include(x=> x.IdStudent).AsNoTracking().ToList();
                }
                else
                {
                    return context.sessionScores.ToList();
                }
            
        }

        public SessionScore GetSessionScoreById(int idSessionScore, bool include = false)
        {
            if (include)
            {
                return context.Set<SessionScore>().Include(x => x.IdDiscipline).Include(x => x.IdStudent).AsNoTracking().FirstOrDefault(x => x.Id == idSessionScore);
            }
            else
            {
                return context.sessionScores.FirstOrDefault(x => x.Id == idSessionScore);
            }
        }
        public IEnumerable<SessionScore> GetSessionScoresByDate(DateTime date, bool includes = false)
        {
            if (includes)
            {
                return context.Set<SessionScore>().Where(x => DateTime.Compare(x.Date, date) == 0).Include(x => x.IdDiscipline).Include(x => x.IdStudent).ToList();
            }
            else
            {
                return context.Set<SessionScore>().Where(x => DateTime.Compare(x.Date, date) == 0).ToList();

            }
        }

        public void EditSessionScore(SessionScore sessionScore)
        {
            context.Entry(sessionScore).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

    }
}
