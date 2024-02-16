namespace Application2.Interfaces {

    /// <summary>
    /// Interface used to represent a basic user
    /// </summary>
    public interface IBasicUser {

        /// <summary>
        /// The user's id in the database
        /// </summary>
        uint Id { get; }

        /// <summary>
        /// The user's name
        /// </summary>
        string Name { get; set; }

    }
}
