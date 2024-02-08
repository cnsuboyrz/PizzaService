using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApi.Data;

namespace PizzaApi.Repository
{
    public class PizzaRepository : IPizzaRepository
    {

        private readonly PizzaContext context;

        public PizzaRepository(PizzaContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<List<Pizza>> GetAllPizzasAsync()
        {
            var pizzas = await context.Pizzas.Select(x => new Pizza()
            {
                Id = x.Id,
                Name = x.Name,
                IsGlutenFree = x.IsGlutenFree,
                Price = x.Price,
                IsWritten = x.IsWritten,    

            }).ToListAsync();

            return pizzas;
        }

        [HttpGet("{id}")]
        public async Task<Pizza> GetPizzaAsync([FromRoute] int pizzaid)
        {
            var pizza = await context.Pizzas.Where(x => x.Id == pizzaid).Select(x => new Pizza()
            {
                Id = x.Id,
                Name = x.Name,
                IsGlutenFree = x.IsGlutenFree,
                Price = x.Price,
                IsWritten = x.IsWritten,

            }).FirstOrDefaultAsync();

            return pizza;

        }



        [HttpPost]
        public async Task<int> AddPizzaAsync(Pizza x)
        {
            var record = new Pizza()
            {
                Id = x.Id,
                Name = x.Name,
                IsGlutenFree = x.IsGlutenFree,
                Price = x.Price,
                IsWritten=false,
            };
            context.Add(record);
            await context.SaveChangesAsync();
            return record.Id;

        }

        [HttpPut("{id}")]
        public async Task<Pizza> UpdatePizzaAsync(int pizzaId, Pizza _pizza)
        {
            var pizza = await context.Pizzas.FindAsync(pizzaId);
            if (pizza != null)
            {
                pizza.Name = _pizza.Name;
                pizza.IsGlutenFree=_pizza.IsGlutenFree;
                pizza.Price=_pizza.Price;
                pizza.IsWritten = false;
                await context.SaveChangesAsync();
            }

            return pizza;

        }

        [HttpDelete]
        public async Task DeletePizzaAsync(int pizzaId)
        {
            var pizza = new Pizza() { Id = pizzaId };
            context.Remove(pizza);

            await context.SaveChangesAsync();
           

        }
    }
}
