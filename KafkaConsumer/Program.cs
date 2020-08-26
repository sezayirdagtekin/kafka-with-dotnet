using System;

using System.Collections.Generic;
using Confluent.Kafka;
using System.IO;
using System.Threading;

namespace KafkaConsumer
{
    class Program
    {
        static string KAFKA_SERVER = "127.0.0.1:9092";
        static string TOPIC = "weblog";
        static string GROUP_ID = "testgroup";

        static void Main(string[] args)
        {
            Console.WriteLine("Kafka Consumer...!");
            Console.WriteLine("address:" + KAFKA_SERVER);
            Console.WriteLine("topicName:" + TOPIC);


            var config = new ConsumerConfig
            {
                BootstrapServers = KAFKA_SERVER,
                GroupId = GROUP_ID,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                try
                {

                consumer.Subscribe(TOPIC);

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    cts.Cancel();
                };
                while(true){
                          var cr = consumer.Consume(cts.Token);
                            Console.WriteLine("Topic:"+ cr.Topic);
                            Console.WriteLine("Partition:"+ cr.Partition);
                            Console.WriteLine("Message key:"+ cr.Message.Key);  
                             Console.WriteLine("Message Value:"+ cr.Message.Value);                          
                            Thread.Sleep(500);
                }
                     
            
                    }
                        
                catch (Exception e)
                {
                    Console.WriteLine("Hata olustu" + e);
                }

            }

        }

    }
}
