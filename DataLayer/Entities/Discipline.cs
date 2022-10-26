using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Discipline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime yearsStart { get; set; }
        public DateTime yearsEnd { get; set; }
        public int block { get; set; }
        public int zet { get; set; }
        public string formAttestation { get; set; }
        public bool availabilityOfCoursework { get; set; }
        public Groups IdGroup { get; set; }
        public Users IdTeacher { get; set; }

    }
}
