using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Contracts
{
	public sealed class EventManager
	{
		public static void PublishEvent(string exchangeName, string routingKey, string queueName, object genericObject)
		{
			ConnectionFactory factory = new()
			{
				Uri = new Uri("amqp://guest:guest@localhost:5672"),
				ClientProvidedName = "RabbitMQ App"
			};

			IConnection connection = factory.CreateConnection();
			IModel channel = connection.CreateModel();

			channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
			channel.QueueDeclare(queueName, false, false, false, null);
			channel.QueueBind(queueName, exchangeName, routingKey, null);
			channel.BasicQos(0, 1, false);

			string serializedObject = JsonSerializer.Serialize(genericObject!);
			byte[] messageBodyBytes = Encoding.UTF8.GetBytes(serializedObject);

			channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);
			channel.Close();
			connection.Close();
		}

		//public object? ConsumeEvent(string exchangeName, string routingKey, string queueName, object genericObject)
		//{
		//	object? receivedObject;

		//	ConnectionFactory factory = new()
		//	{
		//		Uri = new Uri("amqp://guest:guest@localhost:5672"),
		//		ClientProvidedName = "RabbitMQ App"
		//	};

		//	IConnection connection = factory.CreateConnection();
		//	IModel channel = connection.CreateModel();

		//	channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
		//	channel.QueueDeclare(queueName, false, false, false, null);
		//	channel.QueueBind(queueName, exchangeName, routingKey, null);
		//	channel.BasicQos(0, 1, false);

		//	EventingBasicConsumer consumer = new(channel);

		//	consumer.Received += (sender, args) =>
		//	{
		//		byte[] body = args.Body.ToArray();
		//		string receivedObjectAsString = Encoding.UTF8.GetString(body);

		//		receivedObject = JsonSerializer.Deserialize<object>(receivedObjectAsString);

		//		channel.BasicAck(args.DeliveryTag, false);
		//	};

		//	string consumerTag = channel.BasicConsume(queueName, false, consumer);

		//	channel.BasicCancel(consumerTag);

		//	channel.Close();
		//	connection.Close();

		//	return receivedObject;
		//}
	}
}
