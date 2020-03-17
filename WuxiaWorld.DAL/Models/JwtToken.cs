namespace WuxiaWorld.DAL.Models {

    public class JwtToken {
        public int AddMinutes { get; set; }
        public string Key { get; set; }
        public string Issuer { get; set; }
    }

}