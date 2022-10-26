using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IScoreRepository
    {
        public void CreateScore(Score newScore);
        public void DeleteScore(Score score);
        public void DeleteScories(List<Score> score);
        public void EditScore(Score score);
        public IEnumerable<Score> GetAllScore();
        public Score GetScore(int id, bool includes=false);
        public List<Score> GetAllScoreByIdStudent(int idStudent);
    } 
}
