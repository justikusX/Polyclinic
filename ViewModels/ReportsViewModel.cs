using Polyclinic.Models;
using Polyclinic.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Polyclinic.ViewModels
{
    public class ReportsViewModel : ViewModelBase
    {
        private readonly IDatabaseService _databaseService;

        public ObservableCollection<DoctorVisitCount> DoctorVisitCounts { get; set; }
        public ObservableCollection<DiagnosisCount> DiagnosisCounts { get; set; }

        public ReportsViewModel(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
            GenerateReports();
        }

        public void GenerateReports()
        {
            // Количество визитов к врачам
            var visitsByDoctor = _databaseService.GetVisits()
                .GroupBy(v => v.Doctor)
                .Select(g => new DoctorVisitCount
                {
                    DoctorName = g.Key.FullName,
                    Specialty = g.Key.Specialty,
                    VisitCount = g.Count()
                });

            DoctorVisitCounts = new ObservableCollection<DoctorVisitCount>(visitsByDoctor);

            // Количество случаев заболевания по каждому диагнозу
            var visitsByDiagnosis = _databaseService.GetVisits()
                .GroupBy(v => v.Diagnosis)
                .Select(g => new DiagnosisCount
                {
                    DiagnosisName = g.Key.Name,
                    DiagnosisCode = g.Key.Code,
                    Count = g.Count()
                });

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