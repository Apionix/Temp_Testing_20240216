using Application2.Interfaces;

namespace Application2.Models {
    public class UserM : IAdvancedUser, IOptionalUser {

        public uint Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? Age { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }

        public UserM() { }
    }
}
