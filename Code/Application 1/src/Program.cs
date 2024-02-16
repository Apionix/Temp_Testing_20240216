using ConsoleApplication1.Library;

// Log that the application has started
Console.WriteLine("Application started");
Console.WriteLine("Checking all notifications for the past two hours until " + DateTime.Now);

// Database connection used in the application
string databaseConnection = @"root@127.0.0.1:3306";

// Evaluate notifications
var library = new NotificationLibrary(databaseConnection, DateTime.Now);
library.EvaluateNotifications();

// Log that the application is shutting down
Console.WriteLine("Application ended");