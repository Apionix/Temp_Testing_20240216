namespace Application2.Interfaces {

    /// <summary>
    /// Interface used to interact with the User objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUserProvider<T> where T : IBasicUser {

        /// <summary>
        /// Get a user by Id
        /// </summary>
        /// <param name="id">Entry Id</param>
        /// <returns>User</returns>
        T GetUser(uint id);

        /// <summary>
        /// Create a new user with specified information
        /// </summary>
        /// <param name="user">User entry to create</param>
        void CreateUser(T user);
    }
}
