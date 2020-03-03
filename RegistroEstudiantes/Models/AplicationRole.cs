using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroEstudiantes.Models
{
    public class AplicationRole : IdentityRole
    {
        public AplicationRole() : base() { }

        public AplicationRole(string rolname, DateTime creationDate) : base(rolname) {

            this.CreationDate = CreationDate;


        }

        public AplicationRole(string rolename) : base(rolename) { }
        
        public DateTime CreationDate { get; set; }

    }
}
