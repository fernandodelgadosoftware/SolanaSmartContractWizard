using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using WSL_SolanaSmartContractWizard.Services;

namespace WSL_SolanaSmartContractWizard.ViewModels
{
    public class RustCargoViewModel : INotifyPropertyChanged
    {
        private bool _isRustInstalled;
        private string _rustOutput;

        private bool _isCargoInstalled;
        private string _cargoOutput;

        private bool _isRustupInstalled;
        private string _rustupOutput;

        public bool IsRustInstalled
        {
            get => _isRustInstalled;
            set { _isRustInstalled = value; OnPropertyChanged(nameof(IsRustInstalled)); }
        }

        public string RustOutput
        {
            get => _rustOutput;
            set { _rustOutput = value; OnPropertyChanged(nameof(RustOutput)); }
        }

        public bool IsCargoInstalled
        {
            get => _isCargoInstalled;
            set { _isCargoInstalled = value; OnPropertyChanged(nameof(IsCargoInstalled)); }
        }

        public string CargoOutput
        {
            get => _cargoOutput;
            set { _cargoOutput = value; OnPropertyChanged(nameof(CargoOutput)); }
        }

        public bool IsRustupInstalled
        {
            get => _isRustupInstalled;
            set { _isRustupInstalled = value; OnPropertyChanged(nameof(IsRustupInstalled)); }
        }

        public string RustupOutput
        {
            get => _rustupOutput;
            set { _rustupOutput = value; OnPropertyChanged(nameof(RustupOutput)); }
        }

        public void CheckDependencies()
        {
            var (rustInstalled, rustOutput) = DependencyCheckService.CheckRust();
            IsRustInstalled = rustInstalled;
            RustOutput = rustOutput;

            var (cargoInstalled, cargoOutput) = DependencyCheckService.CheckCargo();
            IsCargoInstalled = cargoInstalled;
            CargoOutput = cargoOutput;

            var (rustupInstalled, rustupOutput) = DependencyCheckService.CheckRustup();
            IsRustupInstalled = rustupInstalled;
            RustupOutput = rustupOutput;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

