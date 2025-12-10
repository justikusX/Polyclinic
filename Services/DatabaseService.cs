using Microsoft.EntityFrameworkCore;
using Polyclinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Polyclinic.Services
{
    public class DatabaseService
    {
        private readonly PolyclinicContext _context;

        public DatabaseService()
        {
            _context = new PolyclinicContext();
        }

        // Доктора
        public List<Doctor> GetDoctors()
        {
            try
            {
                return _context.Doctors
                    .OrderBy(d => d.LastName)
                    .ThenBy(d => d.FirstName)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading doctors: {ex.Message}");
                return new List<Doctor>();
            }
        }

        // Пациенты
        public List<Patient> GetPatients()
        {
            try
            {
                return _context.Patients
                    .OrderBy(p => p.LastName)
                    .ThenBy(p => p.FirstName)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading patients: {ex.Message}");
                return new List<Patient>();
            }
        }

        // Диагнозы
        public List<Diagnosis> GetDiagnoses()
        {
            try
            {
                return _context.Diagnoses
                    .OrderBy(d => d.Code)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading diagnoses: {ex.Message}");
                return new List<Diagnosis>();
            }
        }

        // Визиты с загрузкой связанных данных
        public List<Visit> GetVisits()
        {
            try
            {
                return _context.Visits
                    .Include(v => v.Doctor)
                    .Include(v => v.Patient)
                    .Include(v => v.Diagnosis)
                    .OrderByDescending(v => v.VisitDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading visits: {ex.Message}");
                return new List<Visit>();
            }
        }

        // Специальности докторов
        public List<string> GetSpecialties()
        {
            try
            {
                return _context.Doctors
                    .Select(d => d.Specialty)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading specialties: {ex.Message}");
                return new List<string>();
            }
        }

        // Сохранение изменений
        public bool SaveChanges()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message}");
                return false;
            }
        }
    }
}