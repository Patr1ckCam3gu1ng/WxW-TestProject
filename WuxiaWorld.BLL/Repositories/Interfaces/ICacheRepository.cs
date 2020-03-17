namespace WuxiaWorld.BLL.Repositories.Interfaces {

    using System.Threading.Tasks;

    using Microsoft.Extensions.Primitives;

    public interface ICacheRepository {

        object GetCache(string keyValue);


        Task<object> CreateAsync(string keyValue, object data, IChangeToken cancellationToken);


        Task RemoveAsync(string keyValue);
    }

}