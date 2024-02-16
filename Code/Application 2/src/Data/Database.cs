namespace Application2.Data {
    public static class Database<T> {
        public static List<T> UserTable { get; set; } = new List<T>();
    }
}
