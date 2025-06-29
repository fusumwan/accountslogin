using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient; // Use this for MySQL.Data
using Microsoft.Data.SqlClient; // Corrected namespace for MSSQL.Data
using System;

namespace accountslogin.Data
{
    public enum DbType
    {
        MSSQL,
        MySQL
    }

    public static class DatabaseConnectionTester
    {
        public static bool TestConnectionString(string? connectionString, ILogger logger, DbType dbType)
        {
            try
            {
                if (dbType == DbType.MySQL)
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open(); // Try to open the MySQL connection
                        logger.LogInformation("MySQL database connection successful.");
                        return true;
                    }
                }
                else if (dbType == DbType.MSSQL)
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open(); // Try to open the MSSQL connection
                        logger.LogInformation("MSSQL database connection successful.");
                        return true;
                    }
                }
                else
                {
                    logger.LogError("Unsupported database type.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"{dbType} database connection failed: {ex.Message}");
                return false;
            }
        }
    }
}
