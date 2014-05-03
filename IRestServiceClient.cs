namespace Portable.ServiceConsumer
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRestServiceClient<T> where T : class
    {
        Task<T> Get(string id);

        Task<IList<T>> GetAll();

        Task<IList<T>> GetAll(string serviceParameter);

        Task<T> Post(T dto);

        Task<T> Put(T dto);
    }
}
