using ATQ1MR_SG1_21_22_2.WpfClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_SG1_21_22_2.WpfClient.BL.Interfaces
{
    public interface IProcessorEditorService
    {
        ProcessorModel EditProcessor(ProcessorModel processor);
    }
}
