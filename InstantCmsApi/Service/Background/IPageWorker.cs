using System.Threading;
using System.Threading.Tasks;

namespace InstantCmsApi.Service.Background;

public interface IPageWorker
{
    Task DoWork(CancellationToken cancellationToken);
}
