using GalaSoft.MvvmLight;

namespace ATQ1MR_SG1_21_22_2.WpfClient.Models
{
    public class ProcessorModel : ObservableObject
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { Set(ref id, value); }
        }
        private string socket;

        public string Socket
        {
            get { return socket; }
            set { Set(ref socket, value); }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { Set(ref name, value); }
        }
        private double baseClock;

        public double BaseClock
        {
            get { return baseClock; }
            set { Set(ref baseClock, value); }
        }
        private double boostClock;

        public double BoostClock
        {
            get { return boostClock; }
            set { Set(ref boostClock, value); }
        }
        private int cores;

        public int Cores
        {
            get { return cores; }
            set { Set(ref cores, value); }
        }
        private int threads;

        public int Threads
        {
            get { return threads; }
            set { Set(ref threads, value); }
        }
        private int price;

        public int Price
        {
            get { return price; }
            set { Set(ref price, value); }
        }
        private int brandId;

        public int BrandId
        {
            get { return brandId; }
            set { Set(ref brandId, value); }
        }
        public ProcessorModel()
        {

        }
        public ProcessorModel(int id, string socket, string name, double baseClock, double boostClock, int cores, int threads, int price, int brandId)
        {
            this.id = id;
            this.socket = socket;
            this.name = name;
            this.baseClock = baseClock;
            this.boostClock = boostClock;
            this.cores = cores;
            this.threads = threads;
            this.price = price;
            this.brandId = brandId;
        }
        public ProcessorModel(ProcessorModel other)
        {
            id = other.Id;
            socket = other.Socket;
            name = other.Name;
            baseClock = other.BaseClock;
            boostClock = other.BoostClock;
            cores = other.Cores;
            threads = other.Threads;
            price = other.Price;
            brandId = other.BrandId;
        }
    }
}
