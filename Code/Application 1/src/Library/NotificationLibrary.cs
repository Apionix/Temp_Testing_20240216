using ConsoleApplication1.Data;
using ConsoleApplication1.Models;

namespace ConsoleApplication1.Library {
    public class NotificationLibrary {

        /// <summary>
        /// Internal reference to the connection string used to interact with the database
        /// </summary>
        private string _connection { get; }

        /// <summary>
        /// Internal reference to the current time
        /// </summary>
        private DateTime _now { get; }

        /// <summary>
        /// Public referrence to the current time
        /// </summary>
        public DateTime Now => _now;

        public NotificationLibrary(string connection, DateTime? now = null) {
            _connection = connection;
            _now = (DateTime)now;
        }

        /// <summary>
        /// Evaluate all notifications for the past two hours of data
        /// </summary>
        public void EvaluateNotifications() {

            // Get all notifications
            var dal = new NotificationDataAccessLayer();
            var notifications = dal.GetAllEntries<NotificationConfiguration>("Notications");

            // Evaluate each notification individually to prevent them from interfering with one another and causing false positives
            foreach (var notification in notifications)
            {

                // Get the data linked to the notification
                var allDatapoints = new NotificationDataAccessLayer().GetAllEntries<Datapoint>("Datapoints");
                if (allDatapoints.Count > 0)
                {

                    // Get the data linked to the notification that should be filtered out and not evaluated
                    var excludedDataPoints = new NotificationDataAccessLayer().GetEntriesBeforeTimestamp<Datapoint>("Datapoints", DateTime.Now.AddHours(-2));
                    if (excludedDataPoints.Count > 0)
                    {

                        // Compile the list of data points that should be evaluated
                        var datapointsToCheck = new List<Datapoint>();
                        foreach (var datapoint in allDatapoints)
                        {
                            bool shouldBeEvaluated = false;
                            foreach (var excludedDataPoint in excludedDataPoints)
                            {
                                if (datapoint.Timestamp != excludedDataPoint.Timestamp)
                                {
                                    shouldBeEvaluated = true;
                                }
                            }

                            if (shouldBeEvaluated)
                            {
                                datapointsToCheck.Add(datapoint);
                            }
                        }

                        // Evaluate the relevant data and create the custom notification message if any data point exceeds a limit
                        if (datapointsToCheck.Count > 0)
                        {

                            // Evaluate each relevant data point for limit exceptions
                            for (int i = 0; i < datapointsToCheck.Count; i++)
                            {

                                // Use a variable to track whether any data point exceeded a limit to prevent spam messages from being created
                                bool exceedsLimit = false;

                                // Get the current data point and the limit corresponding to the current datapoint
                                var datapoint = datapointsToCheck[i];
                                var limit = notification.Limits[i];

                                // Check if the current data points exceeds the relevant limit
                                if (limit.Type == -1) // -1 - Lower limit
                                {
                                    if (limit.Value > datapoint.Value)
                                    {
                                        exceedsLimit = true;
                                    }
                                    else
                                    {
                                        exceedsLimit = false;
                                    }
                                }
                                else if (limit.Type == 0) // 0 - Upper and lower limit
                                {
                                    if (limit.Value > datapoint.Value && limit.Value < datapoint.Value)
                                    {
                                        exceedsLimit = true;
                                    }
                                    else
                                    {
                                        exceedsLimit = false;
                                    }
                                }
                                else if (limit.Type == 1) // 1 - Upper limit
                                {
                                    if (limit.Value < datapoint.Value)
                                    {
                                        exceedsLimit = true;
                                    }
                                    else
                                    {
                                        exceedsLimit = false;
                                    }
                                }

                                // Create the message once
                                if (exceedsLimit)
                                {
                                    var messageDal = new NotificationDataAccessLayer();
                                    messageDal.CreateEntryNotificationMessage("NotificationMessages", $"Limit exceeded at {Now}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
