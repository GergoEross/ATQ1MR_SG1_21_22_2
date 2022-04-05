using ATQ1MR_SG1_21_22_2.WpfClient.BL.Interfaces;
using ATQ1MR_SG1_21_22_2.WpfClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_SG1_21_22_2.WpfClient
{
    public class ProcessorEditorViaWindowService : IProcessorEditorService
    {
        public ProcessorModel EditProcessor(ProcessorModel processor)
        {
            var window = new ProcessorEditorWindow(processor);

            if (window.ShowDialog() == true)
            {
                return window.Processor;
            }
            return null;
        }
    }
}
