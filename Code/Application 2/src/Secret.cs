namespace Application2 {

    /// <summary>
    /// Model used to represent a secret
    /// </summary>
    public class Secret {

        /// <summary>
        /// Some secret data
        /// </summary>
        public string? Data { get; set; }

        /// <summary>
        /// MySQL connection object
        /// </summary>
        public SqlAuthenticationM? MySQLConnection { get; set; } = new SqlAuthenticationM();
    }

    /// <summary>
    /// Authentication model for MySQL connections
    /// </summary>
    public class SqlAuthenticationM {

        /// <summary>
        /// The User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The Host URL
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// The port number
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Allow retrieval of RSA public keys when SSL is disabled.
        /// </summary>
        public bool AllowPublicKeyRetrieval { get; set; }

    }
}
