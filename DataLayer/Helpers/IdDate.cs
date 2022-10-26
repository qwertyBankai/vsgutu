using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Helpers
{
    public class IdDate
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        //Compare

        public override bool Equals(object obj)
        {
            return this.Id == ((IdDate)obj).Id && this.Date == ((IdDate)obj).Date;
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ this.Date.GetHashCode();
        }
    }
}
