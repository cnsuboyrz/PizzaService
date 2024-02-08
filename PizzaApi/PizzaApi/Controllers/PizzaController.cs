using Microsoft.AspNetCore.Mvc;
using PizzaApi.Repository;
using PizzaApi.Data;
using PizzaApi.RabbitMQ;

namespace PizzaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaRepository Pizzarepository;
        private readonly IPizzaRepository Pizzarepository2;
        private readonly IRabitMQProducer rabitMQProducer;
        public PizzaController(IPizzaRepository pizzarepository, IPizzaRepository pizzarepository2, IRabitMQProducer _rabitMQProducer)
        {
            Pizzarepository = pizzarepository;
            Pizzarepository2 = pizzarepository2;
            rabitMQProducer = _rabitMQProducer;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPizzas()
        {
            var results = await Pizzarepository.GetAllPizzasAsync();

            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPizzaById([FromRoute] int id)
        {
            var result = await Pizzarepository.GetPizzaAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddNewPizza([FromBody] Pizza product)
        {
            rabitMQProducer.SendProductMessage(product);

            var id = await Pizzarepository.AddPizzaAsync(product);


            return CreatedAtAction(nameof(GetPizzaById), new { id = id, controller = "pizza" }, id);
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePizza([FromBody] Pizza product, [FromRoute] int id)
        {
            await Pizzarepository.UpdatePizzaAsync(id, product);
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizza([FromRoute] int id)
        {
            await Pizzarepository.DeletePizzaAsync(id);
            return Ok();

        }

    }
}
