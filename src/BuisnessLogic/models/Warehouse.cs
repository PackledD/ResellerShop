namespace BuisnessLogic.models
{
    public class Warehouse
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public Warehouse(int id, string address)
        {
            this.id = id;
            this.address = address;
        }

        public override string ToString()
        {
            return string.Format("{0}, '{1}'", id, address);
        }
    }
}
