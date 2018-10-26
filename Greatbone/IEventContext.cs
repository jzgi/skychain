using System.Threading.Tasks;

namespace Greatbone
{
    public interface IEventContext
    {
        string RefShard { get; }

        string RefName { get; }

        string Query { get; set; }

        Task<byte[]> PollAsync();

        Task<M> PollAsync<M>() where M : class, ISource;

        Task<D> PollObjectAsync<D>(byte proj = 0x0f) where D : IData, new();

        Task<D[]> PollArrayAsync<D>(byte proj = 0x0f) where D : IData, new();
    }
}