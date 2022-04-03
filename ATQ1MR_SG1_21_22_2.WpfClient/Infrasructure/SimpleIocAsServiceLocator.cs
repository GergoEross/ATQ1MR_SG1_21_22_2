using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace ATQ1MR_SG1_21_22_2.WpfClient.Infrasructure
{
    public class SimpleIocAsServiceLocator : SimpleIoc, IServiceLocator
    {
        public static SimpleIocAsServiceLocator Instance { get; private set; } = new SimpleIocAsServiceLocator();
    }
}
