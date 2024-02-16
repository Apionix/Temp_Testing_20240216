namespace Application2.Interfaces {

    /// <summary>
    /// Interface representing the full user
    /// </summary>
    public interface IAdvancedUser : IBasicUser {

        /// <summary>
        /// The user's surname
        /// </summary>
        string Surname { get; set; }

        /// <summary>
        /// How old the user is
        /// </summary>
        int? Age { get; set; }
    }
}
