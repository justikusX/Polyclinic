using Polyclinic.Models;
using Polyclinic.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Polyclinic.ViewModels
{
    public class VisitsViewModel : ViewModelBase
    {
        private readonly IDatabaseService _databaseService;

        public ObservableCollection<Visit> Visits { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Diagnosis> Diagnoses { get; set; }

        private Visit _selectedVisit;
        public Visit SelectedVisit
        {
            get => _selectedVisit;
            set
            {
                _selectedVisit = value;
                OnPropertyChanged();
            }
        }

        private DateTime _startDate = DateTime.Now.AddMonths(-1);
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
                FilterVisits();
            }
        }

        private DateTime _endDate = DateTime.Now;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
                FilterVisits();
            }
        }

        public VisitsViewModel(IDatabaseService databaseService)
        {
            _databaseService = databaseService;

            Doctors = new ObservableCollection<Doctor>(_databaseService.GetDoctors());
            Patients = new ObservableCollection<Patient>(_databaseService.GetPatients());
            Diagnoses = new ObservableCollection<Diagnosis>(_databaseService.GetDiagnoses());

            FilterVisits();
        }

        private void FilterVisits()
        {
            Visits = new ObservableCollection<Visit>(
                _databaseService.GetVisits().Where(v => v.VisitDate >= StartDate && v.VisitDate <= EndDate));
            OnPropertyChanged(nameof(Visits));
        }
    }
}