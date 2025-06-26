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
            // Инициализация тестовыми данными
            _diagnoses = new List<Diagnosis>
            {
                new Diagnosis { Id = 1, Code = "J06.9", Name = "Острая инфекция верхних дыхательных путей неуточненная" },
                new Diagnosis { Id = 2, Code = "I10", Name = "Эссенциальная (первичная) гипертензия" },
                new Diagnosis { Id = 3, Code = "E11.9", Name = "Сахарный диабет 2 типа без осложнений" }
            };

            _doctors = new List<Doctor>
            {
                new Doctor { Id = 1, LastName = "Иванов", FirstName = "Иван", MiddleName = "Иванович", Specialty = "Терапевт", Experience = 10 },
                new Doctor { Id = 2, LastName = "Петрова", FirstName = "Мария", MiddleName = "Сергеевна", Specialty = "Кардиолог", Experience = 15 },
                new Doctor { Id = 3, LastName = "Сидоров", FirstName = "Алексей", MiddleName = "Владимирович", Specialty = "Эндокринолог", Experience = 8 }
            };

            _patients = new List<Patient>
            {
                new Patient { Id = 1, LastName = "Кузнецов", FirstName = "Дмитрий", MiddleName = "Александрович", BirthDate = new DateTime(1985, 5, 12), Address = "ул. Ленина, 15" },
                new Patient { Id = 2, LastName = "Смирнова", FirstName = "Ольга", MiddleName = "Игоревна", BirthDate = new DateTime(1978, 11, 3), Address = "пр. Победы, 42" },
                new Patient { Id = 3, LastName = "Васильев", FirstName = "Петр", MiddleName = "Сергеевич", BirthDate = new DateTime(1992, 7, 25), Address = "ул. Мира, 7" }
            };

            _visits = new List<Visit>
            {
                new Visit { Id = 1, VisitDate = DateTime.Now.AddDays(-5), DoctorId = 1, PatientId = 1, DiagnosisId = 1 },
                new Visit { Id = 2, VisitDate = DateTime.Now.AddDays(-3), DoctorId = 2, PatientId = 2, DiagnosisId = 2 },
                new Visit { Id = 3, VisitDate = DateTime.Now.AddDays(-1), DoctorId = 3, PatientId = 3, DiagnosisId = 3 },
                new Visit { Id = 4, VisitDate = DateTime.Now.AddDays(-2), DoctorId = 1, PatientId = 2, DiagnosisId = 1 }
            };

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