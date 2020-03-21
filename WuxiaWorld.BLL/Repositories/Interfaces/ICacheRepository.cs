namespace WuxiaWorld.BLL.Repositories.Interfaces {

    using System.Threading;
    using System.Threading.Tasks;

    using DAL.Entities;

    using Microsoft.Extensions.Primitives;

    public interface ICacheRepository {

        object GetCache(string keyValue);


        Task<object> CreateAsync(string keyValue, object data, IChangeToken cancellationToken);


        Task RemoveAsync(string keyValue);


        Task UpsertAsync(string apiEndpointKeyValue, int newId, object newRecord, CancellationToken ctToken);
    }

}