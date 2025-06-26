using Polyclinic.Models;
using System.Collections.Generic;

namespace Polyclinic.Services
{
    public interface IDatabaseService
    {
        IEnumerable<Doctor> GetDoctors();
        IEnumerable<string> GetSpecialties();
        IEnumerable<Patient> GetPatients();
        IEnumerable<Visit> GetVisits();
        IEnumerable<Diagnosis> GetDiagnoses();

        void AddDoctor(Doctor doctor);
        void AddPatient(Patient patient);
        void AddVisit(Visit visit);
    }
}