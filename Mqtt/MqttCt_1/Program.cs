// See https://aka.ms/new-console-template for more information
using MqttCt_1;

Console.WriteLine("Hello, World!");

MqttClientService mqttClientService = new MqttClientService();
mqttClientService.MqttClientStart();

//Console.WriteLine("Client_1==11111===");

Console.ReadLine();

mqttClientService.Publish("I'm  client_1  hello  ....");



Console.WriteLine("Client_1==22222===");

Console.ReadLine();