using Polyclinic.Models;
using Polyclinic.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Polyclinic.ViewModels
{
    public class VisitsViewModel : ViewModelBase
    {
        private readonly DatabaseService _databaseService;

        private ObservableCollection<Visit> _visits;
        public ObservableCollection<Visit> Visits
        {
            get => _visits;
            set => SetProperty(ref _visits, value);
        }

        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Diagnosis> Diagnoses { get; set; }

        private Visit _selectedVisit;
        public Visit SelectedVisit
        {
            get => _selectedVisit;
            set => SetProperty(ref _selectedVisit, value);
        }

        // Используем DateTime для фильтрации
        private DateTime _startDate = DateTime.Now.AddMonths(-1);
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (SetProperty(ref _startDate, value))
                {
                    FilterVisits();
                }
            }
        }

        private DateTime _endDate = DateTime.Now;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (SetProperty(ref _endDate, value))
                {
                    FilterVisits();
                }
            }
        }

        public VisitsViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            LoadData();
        }

        public void LoadData()
        {
            Doctors = new ObservableCollection<Doctor>(_databaseService.GetDoctors());
            Patients = new ObservableCollection<Patient>(_databaseService.GetPatients());
            Diagnoses = new ObservableCollection<Diagnosis>(_databaseService.GetDiagnoses());

            OnPropertyChanged(nameof(Doctors));
            OnPropertyChanged(nameof(Patients));
            OnPropertyChanged(nameof(Diagnoses));

            FilterVisits();
        }

        private void FilterVisits()
        {
            var allVisits = _databaseService.GetVisits();

            if (allVisits == null)
            {
                Visits = new ObservableCollection<Visit>();
                return;
            }

            try
            {
                // Преобразуем DateTime в DateOnly для сравнения
                DateOnly startDateOnly = DateOnly.FromDateTime(StartDate);
                DateOnly endDateOnly = DateOnly.FromDateTime(EndDate);

                var filteredVisits = allVisits
                    .Where(v => v.VisitDate >= startDateOnly && v.VisitDate <= endDateOnly)
                    .OrderByDescending(v => v.VisitDate)
                    .ToList();

                Visits = new ObservableCollection<Visit>(filteredVisits);
            }
            catch (Exception ex)
            {
                // В случае ошибки показываем все записи
                Visits = new ObservableCollection<Visit>(allVisits.OrderByDescending(v => v.VisitDate));
            }
        }
    }
}