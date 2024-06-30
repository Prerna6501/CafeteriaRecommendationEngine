using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;
using ServerSide.Services.Interfaces;

namespace ServerSide.Services
{
    public class FixedMealService : GenericService<FixedMeal>, IFixedMealService
    {
        private readonly IFixedMealRepository _fixedMealRepository;

        public FixedMealService(IFixedMealRepository fixedMealRepository) : base(fixedMealRepository)
        {
            _fixedMealRepository = fixedMealRepository;
        }
    }
}
