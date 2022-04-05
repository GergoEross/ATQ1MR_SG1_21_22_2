using ATQ1MR_SG1_21_22_2.WpfClient.Models;
using ATQ1MR_SG1_21_22_2.WpfClient.ViewModels;
using System.Windows;

namespace ATQ1MR_SG1_21_22_2.WpfClient
{
    /// <summary>
    /// Interaction logic for ProcessorEditorWindow.xaml
    /// </summary>
    public partial class ProcessorEditorWindow : Window
    {
        public ProcessorModel Processor { get; set; }
        bool enableEdit;
        public ProcessorEditorWindow(ProcessorModel processor = null, bool enableEdit = true)
        {
            InitializeComponent();
            Processor = processor == null ? new ProcessorModel() : new ProcessorModel(processor);
            this.enableEdit = enableEdit;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            var vm = (ProcessorEditorVM)Resources["VM"];
            vm.CurrentProcessor = Processor;
            vm.EditEnabled = enableEdit;
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            if (enableEdit)
            {
                DialogResult = true;
            }
            else
            {
                Close();
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            if (enableEdit)
            {
                DialogResult = false;
            }
            else
            {
                Close();
            }
        }
    }
}
