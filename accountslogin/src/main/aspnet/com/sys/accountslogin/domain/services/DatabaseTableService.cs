namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services
{
    public abstract class DatabaseTableService
    {
        public static DatabaseTableService Interface()
        {
            return new DatabaseTableServiceImpl(); // Assuming DatabaseTableServiceImpl is implemented
        }

        public abstract string GetTableFieldType(string dbType, string tablename);
    }

}
