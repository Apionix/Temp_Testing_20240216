namespace Application2.Interfaces {

    /// <summary>
    /// Interface representing optional user information
    /// </summary>
    public interface IOptionalUser : IAdvancedUser {

        /// <summary>
        /// How tall the user is
        /// </summary>
        int? Height { get; set; }

        /// <summary>
        /// How much the user weighs
        /// </summary>
        int? Weight { get; set; }
    }
}
