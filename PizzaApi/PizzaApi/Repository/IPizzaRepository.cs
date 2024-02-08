using Microsoft.AspNetCore.Mvc;
using PizzaApi.Data;

namespace PizzaApi.Repository
{
    public interface IPizzaRepository
    {
        Task<List<Pizza>> GetAllPizzasAsync();

        Task<Pizza> GetPizzaAsync([FromRoute] int pizzaid);

        Task<int> AddPizzaAsync(Pizza x);

        Task<Pizza> UpdatePizzaAsync(int pizzaId, Pizza _pizza);
        Task DeletePizzaAsync(int pizzaId);
    }
}
