using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;
using ServerSide.Services.Interfaces;

namespace ServerSide.Services
{
    public class DiscardItemService : GenericService<DiscardItem>, IDiscardItemService
    {
        private readonly IDiscardItemRepository _discardItemRepository;
        public DiscardItemService(IDiscardItemRepository discardItemRepository) : base(discardItemRepository)
        {
            _discardItemRepository = discardItemRepository;
        }


    }
}
