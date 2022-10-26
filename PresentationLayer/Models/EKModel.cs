using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class EKModel
    {
        public EK ek { get; set; }
        public DisciplineModel Discipline { get; set; }
        public UsersModel Users { get; set; }
    }
}
