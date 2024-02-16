using MySql.Data.MySqlClient;
using System.Data;

namespace ConsoleApplication1.Data {

    /// <summary>
    /// Class used to interact with the database
    /// </summary>
    public class NotificationDataAccessLayer {

        /// <summary>
        /// Connection used to communicate with the database
        /// </summary>
        private IDbConnection _connection { get; }

        /// <summary>
        /// Data access layer class used to interact with the database
        /// </summary>
        /// <param name="connectionstring">Connection string used to establish a connection to the database (Required)</param>
        public NotificationDataAccessLayer(string connectionstring = null) {
            _connection = new MySqlConnection(connectionstring);
        }

        /// <summary>
        /// Get all entries from the specified table name
        /// </summary>
        /// <typeparam name="T">Return type</typeparam>
        /// <param name="tableName">Desired table</param>
        /// <returns>List of entries in the desired data type</returns>
        public List<T> GetAllEntries<T>(string tableName) {
            // The function uses _connection to execute the equivalent of the query below
            // SELECT * FROM tableName
            return new List<T>();
        }

        /// <summary>
        /// Gets all entries from the specified table with a date propertye prior to a specific time
        /// </summary>
        /// <typeparam name="T">Return type</typeparam>
        /// <param name="tableName">Desired table</param>
        /// <param name="timestamp">Timestamp that represents the latest applicable point (Inclusive)</param>
        /// <returns></returns>
        public List<T> GetEntriesBeforeTimestamp<T>(string tableName, DateTime timestamp) {
            // The function uses _connection to execute the equivalent of the query below
            // SELECT * FROM tableName WHERE Timestamp < timestamp
            return new List<T>();
        }

        /// <summary>
        /// Create a new entry in the desired table with a specific message
        /// </summary>
        /// <param name="tableName">Desired table</param>
        /// <param name="message">Message entry to created in the table</param>
        public void CreateEntryNotificationMessage(string tableName, string message) {
            // The function uses _connection to execute the equivalent of the query below
            // INSERT INTO tableName (Message) VALUES ('message')
            // Creates a message in the desired table
        }
    }
}
