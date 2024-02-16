using MySql.Data.MySqlClient;
using System.Data;

namespace Application2.Middleware {
    public static class Extensions {

        /// <summary>
        /// Maps a secret from file.
        /// Expects the path to the secret to be in an env variable named SECRET_FILE
        /// </summary>
        /// <param name="variableName">
        ///   [Optional] Name used for additional secrets that need to be mapped
        /// </param>
        /// <typeparam name="T">The type of secret model</typeparam>
        public static IServiceCollection AddSecret<T>(this IServiceCollection service,
            string variableName = "") where T : class, new() =>

          // Add the secret object as a service
          GetSecret<T>(service, variableName);

        /// <summary>
        /// Try and map a secret from the following locations:
        /// 1. From a file in the environment
        /// 2. From the Secret Manager
        /// </summary>
        /// <typeparam name="T">Model to which the secret should be binded</typeparam>
        /// <param name="services">Service collection to extend</param>
        /// <param name="variableName">Name of the environmental variable that contains the secret name
        /// <returns>Service collection with mapped secret</returns>
        public static IServiceCollection GetSecret<T>(IServiceCollection services, string variableName) where T : class, new() {
            // Get the secret name
            string secretName = GetSecretNameFromEnvironment(variableName);

            return services.AddScoped(serviceProvider => GetSecretFromEnvironment<T>(secretName));
        }

        /// <summary>
        /// Read the contents of a secret file and map it to a desired model
        /// </summary>
        /// <param name="secretName">
        ///   Name of the secret to retrieve from the environment
        /// </param>
        /// <typeparam name="T">The type of secret model</typeparam>
        /// <returns>Model version of the secret</returns>
        public static T GetSecretFromEnvironment<T>(string secretName) where T : class, new() =>
            string.IsNullOrWhiteSpace(secretName) || !File.Exists(secretName)
              ? new T()
              : JsonSerialiser.Deserialize<T>(File.ReadAllText(secretName));

        /// <summary>
        /// Get the secret name from the environment
        /// <summary>
        /// <param name="variableName">Name of the variable that contains the secret name</param>
        /// <returns>Name of the secret</returns>
        public static string GetSecretNameFromEnvironment(string variableName) =>
            string.IsNullOrWhiteSpace(variableName) ?
                Environment.GetEnvironmentVariable("SECRET_NAME") :
                Environment.GetEnvironmentVariable(variableName);

        /// <summary>
        /// Adds a MySQL connection service
        /// </summary>
        /// <param name="services">The service</param>
        /// <param name="implementation">A function that will return a SQL credential model</param>
        /// <param name="allowUserVariables">Should the connection allow user-defined variables?</param>
        /// <typeparam name="T">The type of model for the SQL secrets</typeparam>
        /// <typeparam name="U">DAL that should be used to test database connectivity</typeparam>
        /// <typeparam name="V">Model response for the abovementioned DAL</typeparam>
        /// <returns>A MySqlConnection service</returns>
        public static IServiceCollection AddSQL<T>(this IServiceCollection services,
            Func<T, SqlAuthenticationM> implementation, bool allowUserVariables = false) =>

          services.AddScoped(service => implementation(service.GetRequiredService<T>()))
          .AddScoped<IDbConnection>(service =>
          {
              SqlAuthenticationM mySQL = service.GetRequiredService<SqlAuthenticationM>();
              return CreateMySQLConnection(mySQL, allowUserVariables: allowUserVariables);
          });

        /// <summary>
        /// Create a MySQL connection object with the specified settings received
        /// NB! This is required as a separate function to ensure integration tests can use a database
        /// </summary>
        /// <param name="settings">MySQL connection settings</param>
        /// <param name="database">Default schema name</param>
        /// <param name="allowUserVariables">Should the connection allow user-defined variables?</param>
        /// only used for development with insecure connections</param>
        /// <returns>MySQL database connection object</returns>
        public static MySqlConnection CreateMySQLConnection(SqlAuthenticationM settings,
            string database = "", bool allowUserVariables = false) {
            var stringBuilder = new MySqlConnectionStringBuilder
            {
                Server = settings.Host,
                UserID = settings.UserName,
                Password = settings.Password,
                Database = database,
                Port = (uint)settings.Port,
                AllowUserVariables = allowUserVariables,
                AllowPublicKeyRetrieval = settings.AllowPublicKeyRetrieval
            };

            return new MySqlConnection(stringBuilder.ConnectionString);
        }
    }
}
