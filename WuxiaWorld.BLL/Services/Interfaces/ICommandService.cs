namespace WuxiaWorld.BLL.Services.Interfaces {

    using System.Collections.Generic;

    using DAL.Models;

    public interface ICommandService {

        List<CommandModel> Get();
    }

}