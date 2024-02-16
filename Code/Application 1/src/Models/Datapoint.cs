namespace ConsoleApplication1.Models {

    /// <summary>
    /// Model used to represent a data point that was captured by a specific device
    /// </summary>
    public class Datapoint {

        /// <summary>
        /// Id for the data point
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Device that captured the data point
        /// </summary>
        public uint DeviceId { get; set; }

        /// <summary>
        /// The measured value for the given timestamp
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// The timestamp for which this entry was captured
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
