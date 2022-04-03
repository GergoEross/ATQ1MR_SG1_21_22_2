using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_SG1_21_22_2.WpfClient.Models
{
    public class PBrandModel : ObservableObject
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { Set(ref id, value); }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { Set(ref name, value); }
        }
        public PBrandModel()
        {

        }
        public PBrandModel(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
