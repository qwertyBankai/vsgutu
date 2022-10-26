using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IEKRepository
    {
        public void CreateEK(EK eK);
        public void EditEk(EK eK);
        public EK GetEkById(int id, bool includes = false);
        public IEnumerable<EK> GetAllEk(bool includes=false);
        public void DeleteScories(List<EK> score);
        public List<EK> GetAllScoreByIdStudent(int idStudent);
    }
}
