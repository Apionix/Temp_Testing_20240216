namespace ConsoleApplication1.Models {

    /// <summary>
    /// Model used to represent a limit linked to a notification
    /// </summary>
    public class NotificationLimit {

        /// <summary>
        /// Limit value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Type
        /// Limit Types:
        /// 1 - Upper limit
        /// 2 - Lower limit
        /// 3 - Upper and lower limit
        /// </summary>
        public int Type { get; set; }
    }
}
