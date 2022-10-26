using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class UsersModel
    {
        public Users Users { get; set; }
        public RolesOfUsersModel Role { get; set; }
        public GroupsModel Group { get; set; }
    }
}
