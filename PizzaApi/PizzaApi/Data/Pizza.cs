using System.ComponentModel;

namespace PizzaApi.Data
{
    public class Pizza
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsGlutenFree { get; set; }

        public double Price { get; set; }

        [DefaultValue(false)]
        public bool IsWritten { get; set; } = false;
    }
}
