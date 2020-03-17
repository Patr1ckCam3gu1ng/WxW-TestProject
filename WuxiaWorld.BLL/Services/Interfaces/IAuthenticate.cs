namespace WuxiaWorld.BLL.Services.Interfaces {

    using DAL.Models;

    public interface IAuthenticate {

        bool Validate(LoginModel login);


        string GenerateToken(string loginUserName);
    }

}