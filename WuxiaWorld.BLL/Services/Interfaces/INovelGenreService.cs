namespace WuxiaWorld.BLL.Services.Interfaces {

    using System.Collections.Generic;

    public interface INovelGenreService {

        void Assign(int novelId, List<int> inputGenreIds);
    }

}