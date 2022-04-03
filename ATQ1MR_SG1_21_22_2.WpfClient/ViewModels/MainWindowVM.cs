using ATQ1MR_SG1_21_22_2.WpfClient.BL.Interfaces;
using ATQ1MR_SG1_21_22_2.WpfClient.Models;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ATQ1MR_SG1_21_22_2.WpfClient.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        private ProcessorModel currentProcessor;

        public ProcessorModel CurrentProcessor
        {
            get { return currentProcessor; }
            set { Set(ref currentProcessor, value); }
        }
        public ObservableCollection<ProcessorModel> Processors { get; private set; }

        public ICommand AddCommand { get; set; }
        public ICommand ModifyCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ViewCommand { get; set; }
        public ICommand LoadCommand { get; set; }

        readonly IProcessorHandlerService handlerService;

        public MainWindowVM(IProcessorHandlerService handlerService)
        {
            this.handlerService = handlerService;
            Processors = new ObservableCollection<ProcessorModel>();

            if (IsInDesignMode)
            {
                Processors.Add(new ProcessorModel(1, "AM4", "Ryzen 5 3600", 3.6, 4.2, 6, 12, 110000, 2));
                var icpu = new ProcessorModel(2, "LGA-1151(300)", "Core i9-9900K", 3.6, 5.0, 8, 16, 134000, 1);
                Processors.Add(icpu);
                Processors.Add(new ProcessorModel(3, "AM4", "Ryzen 5 5600X", 3.7, 4.6, 6, 12, 123000, 2));
                CurrentProcessor = icpu;
            }

            LoadCommand = new RelayCommand(() =>
            {
                var processors = this.handlerService.GetAll();
                Processors.Clear();

                foreach (var processor in processors)
                {
                    Processors.Add(processor);
                }
            });

            AddCommand = new RelayCommand(() => this.handlerService.AddProcessor(Processors));
            ModifyCommand = new RelayCommand(() => this.handlerService.ModifyProcessor(Processors, CurrentProcessor));
            DeleteCommand = new RelayCommand(() => this.handlerService.DeleteProcessor(Processors, CurrentProcessor));
            ViewCommand = new RelayCommand(() => this.handlerService.ViewProcessor(CurrentProcessor));

        }
        public MainWindowVM() : this(IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<IProcessorHandlerService>())
        {

        }
    }
}
