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
    public class EFDBScoreRepository : IScoreRepository
    {
        private EFDBContext context;

        public EFDBScoreRepository(EFDBContext context)
        {
            this.context = context;
        }

        public void CreateScore(Score newScore)
        {
            context.Score.Add(newScore);
            context.SaveChanges();
        }

        public void DeleteScore(Score score)
        {
            context.Score.Remove(score);
            context.SaveChanges();
        }

        public void DeleteScories(List<Score> score)
        {
            foreach (var item in score)
            {
                context.Score.Remove(item);
            }
            context.SaveChanges();
        }

        public List<Score> GetAllScoreByIdStudent(int idStudent)
        {
            return context.Set<Score>().Include(x => x.IdStudent).Where(x => x.IdStudent.Id == idStudent).ToList();
        }

        public void EditScore(Score score)
        {
            context.Entry(score).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Score> GetAllScore() => context.Score.ToList();

        public Score GetScore(int id, bool includes = false)
        {
            if (includes)
            {
                return context.Set<Score>().Include(x => x.IdStudent).AsNoTracking().FirstOrDefault(x => x.Id == id);
            }
            else
            {
                return context.Score.FirstOrDefault(x => x.Id == id);
            }
        }
    }
}
