namespace ConsoleApplication1.Models {

    /// <summary>
    /// Model used to represent a notification that should be evaluated
    /// </summary>
    public class NotificationConfiguration {

        /// <summary>
        /// Notification Id
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Name of the notification
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Message that should be sent when a limit is exceeded
        /// </summary>
        public string Message => $"[{Id}] {Name}: Limit exceeded";

        /// <summary>
        /// List of limits linked to the notification
        /// Limit Types:
        /// 0 - Upper limit
        /// 1 - Lower limit
        /// 2 - Upper and lower limit
        /// </summary>
        public List<NotificationLimit> Limits { get; set; }

        /// <summary>
        /// List of devices linked to the notification that should be evaluated
        /// </summary>
        public List<uint> DeviceIds { get; set; } = new List<uint>();

        /// <summary>
        /// Indicates whether the notification is enabled and should be evaluated
        /// </summary>
        public bool Enabled { get; set; }
    }
}
