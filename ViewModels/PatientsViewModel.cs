using Polyclinic.Models;
using Polyclinic.Services;
using System.Collections.ObjectModel;

namespace Polyclinic.ViewModels
{
    public class PatientsViewModel : ViewModelBase
    {
        private readonly DatabaseService _databaseService;

        public ObservableCollection<Patient> Patients { get; set; }

        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                _selectedPatient = value;
                OnPropertyChanged();
            }
        }

        public PatientsViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            Patients = new ObservableCollection<Patient>(_databaseService.GetPatients());
        }
    }
}
