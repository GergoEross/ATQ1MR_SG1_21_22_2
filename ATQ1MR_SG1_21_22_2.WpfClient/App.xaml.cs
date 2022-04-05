using ATQ1MR_SG1_21_22_2.WpfClient.BL.Implementation;
using ATQ1MR_SG1_21_22_2.WpfClient.BL.Interfaces;
using ATQ1MR_SG1_21_22_2.WpfClient.Infrasructure;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ATQ1MR_SG1_21_22_2.WpfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIocAsServiceLocator.Instance);
            SimpleIocAsServiceLocator.Instance.Register<IProcessorHandlerService, ProcessorHandlerService>();
            SimpleIocAsServiceLocator.Instance.Register<IProcessorEditorService, ProcessorEditorViaWindowService>();
            SimpleIocAsServiceLocator.Instance.Register<IProcessorDisplayService, ProcessorDisplayService>();
            SimpleIocAsServiceLocator.Instance.Register(() => Messenger.Default);
        }
    }
}
