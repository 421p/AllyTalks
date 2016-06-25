namespace AllyTalksClient.Model {
    public class User {
        public User()
        {
        }

        public User(string login, string name, string picture)
        {
            Login = login;
            Nickname = name;
            Picture = picture;
        }

        public string Login { get; set; }
        public string Nickname { get; set; }
        public string Picture { get; set; }
    }
}