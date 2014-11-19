using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImgGroup.OnlineStore.Contracts
{
    public interface IShoppingSessionRepository
    {
        Task<ShoppingSession> RetrieveShopperSessionAsync(string shopperId, CancellationToken ct);
        Task SaveShopperSessionAsync(ShoppingSession session, CancellationToken ct);
    }
}
