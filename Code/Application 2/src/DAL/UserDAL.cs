using Application2.Data;
using Application2.Interfaces;
using Application2.Models;
using System.Data;

namespace Application2.DAL {
    public class UserDAL : IUserProvider<UserM> {

        /// <summary>
        /// Used to establish the connection to the actual database
        /// </summary>
        private IDbConnection _connection { get; }

        /// <summary>
        /// Default constructor
        /// <param name="connection">Sql credentials connection</param>
        /// </summary>
        public UserDAL(IDbConnection connection) =>
            _connection = connection;

        /// <summary>
        /// Get a user by Id
        /// </summary>
        /// <param name="id">Entry Id</param>
        /// <returns>User</returns>
        public UserM GetUser(uint id) => Database<UserM>.UserTable.FirstOrDefault(v => v.Id == id);

        /// <summary>
        /// Create a new user with specified information
        /// </summary>
        /// <param name="user">User entry to create</param>
        public void CreateUser(UserM user) => Database<UserM>.UserTable.Add(user);
    }
}
