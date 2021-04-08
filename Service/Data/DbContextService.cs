using DataLayer;

namespace Service.Data
{
    public class DbContextService
    {
        private readonly StockContext _connection;
        private static DbContextService _instance;

        public DbContextService()
        {
            _connection = new StockContext();
        }

        public static DbContextService GetInstance()
        {
            if (null == _instance)
            {
                _instance = new DbContextService();
            }
            return _instance;
        }

        public StockContext GetConnection()
        {
            return _connection;
        }
    }
}
