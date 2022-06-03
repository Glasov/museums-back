using WebApplication2.Data.Models;

namespace WebApplication2.Data
{
    public class DataSource
    {
        private static DataSource instance;

        private DataSource() { }

        public static DataSource GetInstance()
        {
            if (instance == null)
            {
                instance = new DataSource();
            }

            return instance;
        }

        public List<Exhibition> _exhibitions = new List<Exhibition>();
        public List<ExhibitionItem> _exhibitionItems = new List<ExhibitionItem>();
        public List<Order> _orders = new List<Order>();
        public List<Museum> _museums = new List<Museum>();
        public List<User> _users = new List<User>();
    }
}
