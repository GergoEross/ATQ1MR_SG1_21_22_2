using ATQ1MR_SG1_21_22_2.WpfClient.BL.Interfaces;
using ATQ1MR_SG1_21_22_2.WpfClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_SG1_21_22_2.WpfClient
{
    public class ProcessorDisplayService : IProcessorDisplayService
    {
        public void Display(ProcessorModel processor)
        {
            var window = new ProcessorEditorWindow(processor, false);

            window.Show();
        }
    }
}
