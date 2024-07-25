using ServerSide.Entity;

namespace ServerSide.Services.Interfaces
{
    public interface IFixedMealService : IGenericService<FixedMeal>
    {
        public Task<string> RolloutFinalMeal(string message);
    }
}
