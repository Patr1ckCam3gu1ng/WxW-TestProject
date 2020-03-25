namespace WuxiaWorld.BLL.Services.Interfaces {

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INovelGenreService {

        Task<bool> Assign(int novelId, List<int> genreIds);


        Task UnAssign(int novelId, int genreId);
    }

}