using Polyclinic.Models;
using Polyclinic.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Polyclinic.ViewModels
{
    public class ReportsViewModel : ViewModelBase
    {
        private readonly DatabaseService _databaseService;

        private ObservableCollection<DoctorVisitCount> _doctorVisitCounts;
        public ObservableCollection<DoctorVisitCount> DoctorVisitCounts
        {
            get => _doctorVisitCounts;
            set => SetProperty(ref _doctorVisitCounts, value);
        }

        private ObservableCollection<DiagnosisCount> _diagnosisCounts;
        public ObservableCollection<DiagnosisCount> DiagnosisCounts
        {
            get => _diagnosisCounts;
            set => SetProperty(ref _diagnosisCounts, value);
        }

        public ReportsViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            GenerateReports();
        }

        public void GenerateReports()
        {
            var visits = _databaseService.GetVisits();

            if (visits == null || !visits.Any())
            {
                DoctorVisitCounts = new ObservableCollection<DoctorVisitCount>();
                DiagnosisCounts = new ObservableCollection<DiagnosisCount>();
                return;
            }

            // Количество визитов к врачам
            var visitsByDoctor = visits
                .Where(v => v.Doctor != null)
                .GroupBy(v => v.Doctor)
                .Select(g => new DoctorVisitCount
                {
                    DoctorName = $"{g.Key.LastName} {g.Key.FirstName} {g.Key.Patronymic ?? ""}".Trim(),
                    Specialty = g.Key.Specialty,
                    VisitCount = g.Count()
                })
                .OrderByDescending(d => d.VisitCount);

            DoctorVisitCounts = new ObservableCollection<DoctorVisitCount>(visitsByDoctor);

            // Количество случаев заболевания по каждому диагнозу
            var visitsByDiagnosis = visits
                .Where(v => v.Diagnosis != null)
                .GroupBy(v => v.Diagnosis)
                .Select(g => new DiagnosisCount
                {
                    DiagnosisName = g.Key.Name,
                    DiagnosisCode = g.Key.Code,
                    Count = g.Count()
                })
                .OrderByDescending(d => d.Count);

            DiagnosisCounts = new ObservableCollection<DiagnosisCount>(visitsByDiagnosis);
        }
    }

    public class DoctorVisitCount
    {
        public string DoctorName { get; set; }
        public string Specialty { get; set; }
        public int VisitCount { get; set; }
    }

    public class DiagnosisCount
    {
        public string DiagnosisName { get; set; }
        public string DiagnosisCode { get; set; }
        public int Count { get; set; }
    }
}