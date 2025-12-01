using Polyclinic.Models;
using Polyclinic.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace Polyclinic.ViewModels
{
    public class DoctorsViewModel : ViewModelBase
    {
        private readonly DatabaseService _databaseService;

        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<string> Specialties { get; set; }

        private Doctor _selectedDoctor;
        public Doctor SelectedDoctor
        {
            get => _selectedDoctor;
            set
            {
                _selectedDoctor = value;
                OnPropertyChanged();
            }
        }

        private string _selectedSpecialty;
        public string SelectedSpecialty
        {
            get => _selectedSpecialty;
            set
            {
                _selectedSpecialty = value;
                OnPropertyChanged();
                FilterDoctorsBySpecialty();
            }
        }

        public DoctorsViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            Doctors = new ObservableCollection<Doctor>(_databaseService.GetDoctors());
            Specialties = new ObservableCollection<string>(_databaseService.GetSpecialties());
        }

        private void FilterDoctorsBySpecialty()
        {
            if (string.IsNullOrEmpty(SelectedSpecialty))
            {
                Doctors = new ObservableCollection<Doctor>(_databaseService.GetDoctors());
            }
            else
            {
                Doctors = new ObservableCollection<Doctor>(
                    _databaseService.GetDoctors().Where(d => d.Specialty == SelectedSpecialty));
            }
            OnPropertyChanged(nameof(Doctors));
        }


        public ObservableCollection<Doctor> ChitateliList
        {
            get { return Doctors; }
            set
            {
                if (Doctors != value)
                {
                    Doctors = value;
                    OnPropertyChanged(nameof(Doctors));
                }
            }
        }
        public DoctorsViewModel()
        {
            chitateliService = new ChitateliService();
            Load();
        }
    }


}