using Polyclinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyclinic.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Specialty { get; set; }
        public int Experience { get; set; }

        public string FullName => $"{LastName} {FirstName} {MiddleName}";

        public List<Visit> Visits { get; set; } = new List<Visit>();
    }
}
