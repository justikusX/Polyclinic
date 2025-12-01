using Polyclinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Polyclinic.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly List<Doctor> _doctors;
        private readonly List<Patient> _patients;
        private readonly List<Visit> _visits;
        private readonly List<Diagnosis> _diagnoses;

        public DatabaseService()
        {
           

            // Навигационные свойства
            foreach (var visit in _visits)
            {
                visit.Doctor = _doctors.First(d => d.Id == visit.DoctorId);
                visit.Patient = _patients.First(p => p.Id == visit.PatientId);
                visit.Diagnosis = _diagnoses.First(d => d.Id == visit.DiagnosisId);

                visit.Doctor.Visits.Add(visit);
                visit.Patient.Visits.Add(visit);
                visit.Diagnosis.Visits.Add(visit);
            }
        }

        public IEnumerable<Doctor> GetDoctors() => _doctors;

        public IEnumerable<string> GetSpecialties() => _doctors.Select(d => d.Specialty).Distinct();

        public IEnumerable<Patient> GetPatients() => _patients;

        public IEnumerable<Visit> GetVisits() => _visits;

        public IEnumerable<Diagnosis> GetDiagnoses() => _diagnoses;

        public void AddDoctor(Doctor doctor)
        {
            doctor.Id = _doctors.Max(d => d.Id) + 1;
            _doctors.Add(doctor);
        }

        public void AddPatient(Patient patient)
        {
            patient.Id = _patients.Max(p => p.Id) + 1;
            _patients.Add(patient);
        }

        public void AddVisit(Visit visit)
        {
            visit.Id = _visits.Max(v => v.Id) + 1;
            _visits.Add(visit);

            visit.Doctor = _doctors.First(d => d.Id == visit.DoctorId);
            visit.Patient = _patients.First(p => p.Id == visit.PatientId);
            visit.Diagnosis = _diagnoses.First(d => d.Id == visit.DiagnosisId);

            visit.Doctor.Visits.Add(visit);
            visit.Patient.Visits.Add(visit);
            visit.Diagnosis.Visits.Add(visit);
        }
    }
}