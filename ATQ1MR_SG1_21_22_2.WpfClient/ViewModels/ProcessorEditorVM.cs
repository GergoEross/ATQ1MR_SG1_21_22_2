using ATQ1MR_SG1_21_22_2.WpfClient.BL.Interfaces;
using ATQ1MR_SG1_21_22_2.WpfClient.Models;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_SG1_21_22_2.WpfClient.ViewModels
{
    public class ProcessorEditorVM : ViewModelBase
    {
        private ProcessorModel currentProcessor;

        public ProcessorModel CurrentProcessor
        {
            get { return currentProcessor; }
            set { Set(ref currentProcessor, value);
                SelectedBrand = AvailableBrands?.SingleOrDefault(x => x.Id == currentProcessor.BrandId);
            }
        }
        private PBrandModel selectedBrand;

        public PBrandModel SelectedBrand
        {
            get { return selectedBrand; }
            set { Set(ref selectedBrand, value);
                currentProcessor.BrandId = selectedBrand?.Id ?? 0;
            }
        }
        public IList<PBrandModel> AvailableBrands { get; private set; }
        private bool editEnabled;

        public bool EditEnabled
        {
            get { return editEnabled; }
            set { Set(ref editEnabled, value);
                RaisePropertyChanged(nameof(CancelButtonVisibility));
            }
        }
        public System.Windows.Visibility CancelButtonVisibility => EditEnabled ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;

        public ProcessorEditorVM(IProcessorHandlerService handlerService)
        {
            CurrentProcessor = new ProcessorModel();

            if (IsInDesignModeStatic)
            {
                AvailableBrands = new List<PBrandModel>()
                {
                    new PBrandModel(1, "Intel"),
                    new PBrandModel(2, "AMD")
                };

                SelectedBrand = AvailableBrands[1];
                CurrentProcessor.Name = "Ryzen 5 3600";
                CurrentProcessor.Socket = "AM4";
                CurrentProcessor.Cores = 6;
                CurrentProcessor.Threads = 12;
                CurrentProcessor.BaseClock = 3.6;
                CurrentProcessor.BoostClock = 4.2;
                CurrentProcessor.Price = 110000;
            }
            else
            {
                AvailableBrands = handlerService.GetAllBrands();
            }
        }
        public ProcessorEditorVM() : this(IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<IProcessorHandlerService>())
        {

        }
    }
}
