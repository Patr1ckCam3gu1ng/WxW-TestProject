namespace WuxiaWorld.BLL.Repositories.Interfaces {

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INovelGenreRepository {

        Task<bool> Assign(int novelId, List<int> genreIds);
    }

}