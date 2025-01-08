using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WSL_SolanaSmartContractWizard.Services;

namespace WSL_SolanaSmartContractWizard.ViewModels
{
    public class EnvironmentSetupViewModel : INotifyPropertyChanged
    {
        private bool _isWSLInstalled;
        private string _wslOutput;
        private bool _isRustInstalled;
        private string _rustOutput;
        private bool _isSolanaCLIInstalled;
        private string _solanaOutput;
        private bool _isNodeInstalled;
        private string _nodeOutput;

        public bool IsWSLInstalled
        {
            get => _isWSLInstalled;
            set { _isWSLInstalled = value; OnPropertyChanged(nameof(IsWSLInstalled)); }
        }

        public string WSLOutput
        {
            get => _wslOutput;
            set { _wslOutput = value; OnPropertyChanged(nameof(WSLOutput)); }
        }

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

        public bool IsSolanaCLIInstalled
        {
            get => _isSolanaCLIInstalled;
            set { _isSolanaCLIInstalled = value; OnPropertyChanged(nameof(IsSolanaCLIInstalled)); }
        }

        public string SolanaOutput
        {
            get => _solanaOutput;
            set { _solanaOutput = value; OnPropertyChanged(nameof(SolanaOutput)); }
        }

        public bool IsNodeInstalled
        {
            get => _isNodeInstalled;
            set { _isNodeInstalled = value; OnPropertyChanged(nameof(IsNodeInstalled)); }
        }

        public string NodeOutput
        {
            get => _nodeOutput;
            set { _nodeOutput = value; OnPropertyChanged(nameof(NodeOutput)); }
        }

        public void CheckDependencies()
        {
            var (wslInstalled, wslOutput) = DependencyCheckService.CheckWSL();
            IsWSLInstalled = wslInstalled;
            WSLOutput = wslOutput;

            var (rustInstalled, rustOutput) = DependencyCheckService.CheckRust();
            IsRustInstalled = rustInstalled;
            RustOutput = rustOutput;

            var (solanaInstalled, solanaOutput) = DependencyCheckService.CheckSolanaCLI();
            IsSolanaCLIInstalled = solanaInstalled;
            SolanaOutput = solanaOutput;

            var (nodeInstalled, nodeOutput) = DependencyCheckService.CheckNode();
            IsNodeInstalled = nodeInstalled;
            NodeOutput = nodeOutput;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
