using System;
using System.Collections.Generic;
using System.Net;
using Confluent.Kafka;


namespace KafkaProducer
{
    class Program
    {
        private const string KAFKA_SERVER = "localhost:9092";

        static void Main(string[] args)
        {
            Console.WriteLine("KafkaProducer is  starting");
            sending();

        }


        private static void sending()
        {
            const string topic = "topic1";
            Console.WriteLine("sending...");
            var config = new ProducerConfig() { BootstrapServers = KAFKA_SERVER };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    int i = 1;
                    while (i <= 5)
                    {
                        String text = "test msg  # " + i;
                        DeliveryResult<Null, string> result = producer.ProduceAsync(topic, new Message<Null, string> { Value = text }).GetAwaiter().GetResult();
                        Console.WriteLine($"Delivered to '{result.TopicPartitionOffset}'");
                        i++;
                    }
               Console.WriteLine("Complete...");
                }
                catch (ProduceException<Null, string> err)
                {
                    Console.WriteLine("Hata olustu" + err);
                }

            }

        }

    }
}
