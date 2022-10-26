using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ISessionScoreRepository
    {
        public void CreateSessionScore(SessionScore sessionScore);
        public void DeleteSessionScore(SessionScore sessionScore);
        public void EditSessionScore(SessionScore sessionScore);
        public SessionScore GetSessionScoreById(int idSessionScore,bool includes =false);
        public IEnumerable<SessionScore> GetAllSessionScore(bool includes = false);
        public IEnumerable<SessionScore> GetSessionScoresByDate(DateTime date, bool includes = false);

        public void DeleteScories(List<SessionScore> score);
        public List<SessionScore> GetAllScoreByIdStudent(int idStudent);
    }
}
