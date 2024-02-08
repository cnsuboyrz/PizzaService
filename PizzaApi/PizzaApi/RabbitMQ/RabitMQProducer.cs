using RabbitMQ.Client;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace PizzaApi.RabbitMQ
{
    public class RabitMQProducer : IRabitMQProducer
    {
        public void SendProductMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
           
            var connection = factory.CreateConnection();
            
            using
            var channel = connection.CreateModel();
            
            channel.QueueDeclare("pizza", exclusive: false);
           
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
           
            channel.BasicPublish(exchange: "", routingKey: "pizza", body: body);
        }
    }
}
