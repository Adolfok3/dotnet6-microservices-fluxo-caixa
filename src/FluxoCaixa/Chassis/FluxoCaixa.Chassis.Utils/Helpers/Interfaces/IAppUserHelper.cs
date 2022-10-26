using FluxoCaixa.Chassis.Utils.Common;

namespace FluxoCaixa.Chassis.Utils.Helpers.Interfaces
{
    public interface IAppUserHelper
    {
        Task<AppUser> GetUserFromAccessTokenasync(string accessToken);
    }
}
