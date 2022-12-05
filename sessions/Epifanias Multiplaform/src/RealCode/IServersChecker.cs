using System.Threading.Tasks;

namespace Features.Offline
{
    public interface IServersChecker
    {
        Task<bool> AreServersAvailableAsync();
    }
}