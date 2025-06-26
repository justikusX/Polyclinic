using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polyclinic.Models;

namespace Polyclinic.Models
{
    public class Diagnosis
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public List<Visit> Visits { get; set; } = new List<Visit>();
    }
}