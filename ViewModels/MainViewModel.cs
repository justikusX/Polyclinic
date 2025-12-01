using Polyclinic.Models;
using Polyclinic.Services;
using Polyclinic.ViewModels;
using System.Collections.ObjectModel;

namespace Polyclinic.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DatabaseService _databaseService;

        public DoctorsViewModel DoctorsVM { get; }
        public PatientsViewModel PatientsVM { get; }
        public VisitsViewModel VisitsVM { get; }
        public ReportsViewModel ReportsVM { get; }

        public MainViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;

            DoctorsVM = new DoctorsViewModel(databaseService);
            PatientsVM = new PatientsViewModel(databaseService);
            VisitsVM = new VisitsViewModel(databaseService);
            ReportsVM = new ReportsViewModel(databaseService);
        }
    }
}