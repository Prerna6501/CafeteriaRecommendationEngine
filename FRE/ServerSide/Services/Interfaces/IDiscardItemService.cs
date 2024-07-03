using Common.Models;
using ServerSide.Entity;

namespace ServerSide.Services.Interfaces
{
    public interface IDiscardItemService : IGenericService<DiscardItem>
    {
        public Task<List<DiscardItemModel>> GetDiscardItemList();
    }
}
