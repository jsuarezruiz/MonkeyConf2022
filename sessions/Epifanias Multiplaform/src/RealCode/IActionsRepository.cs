using System.Threading.Tasks;

namespace Features.Offline
{
    public interface IActionsRepository
    {
        Task<bool> ThereAreActionsToSendAsync();
    }
}