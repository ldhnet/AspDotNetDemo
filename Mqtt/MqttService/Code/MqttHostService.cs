using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System;
using System.Text;
using System.Threading;

namespace MqttService.Code
{
    public class MqttHostService : IHostedService, IDisposable
    {
        public void Dispose()
        {

        }
        const string ServerClientId = "SERVER";
        public Task StartAsync(CancellationToken cancellationToken)
        {
            MqttServerOptionsBuilder optionsBuilder = new MqttServerOptionsBuilder();
            optionsBuilder.WithDefaultEndpoint();
            optionsBuilder.WithDefaultEndpointPort(10086); // 设置 服务端 端口号
            optionsBuilder.WithConnectionBacklog(1000); // 最大连接数
            MqttServerOptions options = optionsBuilder.Build();

            MqttService._mqttServer = new MqttFactory().CreateMqttServer(options);

            MqttService._mqttServer.ClientConnectedAsync += _mqttServer_ClientConnectedAsync; //客户端连接事件
            MqttService._mqttServer.ClientDisconnectedAsync += _mqttServer_ClientDisconnectedAsync; // 客户端关闭事件
            MqttService._mqttServer.ApplicationMessageNotConsumedAsync += _mqttServer_ApplicationMessageNotConsumedAsync; // 消息接收事件

            MqttService._mqttServer.ClientSubscribedTopicAsync += _mqttServer_ClientSubscribedTopicAsync; // 客户端订阅主题事件
            MqttService._mqttServer.ClientUnsubscribedTopicAsync += _mqttServer_ClientUnsubscribedTopicAsync; // 客户端取消订阅事件
            MqttService._mqttServer.StartedAsync += _mqttServer_StartedAsync; // 启动后事件
            MqttService._mqttServer.StoppedAsync += _mqttServer_StoppedAsync; // 关闭后事件
            MqttService._mqttServer.InterceptingPublishAsync += _mqttServer_InterceptingPublishAsync; // 消息接收事件
            MqttService._mqttServer.ValidatingConnectionAsync += _mqttServer_ValidatingConnectionAsync; // 用户名和密码验证有关

            MqttService._mqttServer.StartAsync();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 客户端订阅主题事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task _mqttServer_ClientSubscribedTopicAsync(ClientSubscribedTopicEventArgs arg)
        {
            Console.WriteLine($"ClientSubscribedTopicAsync：客户端ID=【{arg.ClientId}】订阅的主题=【{arg.TopicFilter}】 ");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 关闭后事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task _mqttServer_StoppedAsync(EventArgs arg)
        {
            Console.WriteLine($"StoppedAsync：MQTT服务已关闭……");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 用户名和密码验证有关
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task _mqttServer_ValidatingConnectionAsync(ValidatingConnectionEventArgs arg)
        {
            arg.ReasonCode = MqttConnectReasonCode.Success;
            if ((arg.Username ?? string.Empty) != "admin" || (arg.Password ?? String.Empty) != "123456")
            {
                arg.ReasonCode = MqttConnectReasonCode.Banned;
                Console.WriteLine($"ValidatingConnectionAsync：客户端ID=【{arg.ClientId}】用户名或密码验证错误 ");

            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 消息接收事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task _mqttServer_InterceptingPublishAsync(InterceptingPublishEventArgs arg)
        {
            if (string.Equals(arg.ClientId, ServerClientId))
            {
                return Task.CompletedTask;
            }

            Console.WriteLine($"InterceptingPublishAsync：客户端ID=【{arg.ClientId}】 Topic主题=【{arg.ApplicationMessage.Topic}】 消息=【{Encoding.UTF8.GetString(arg.ApplicationMessage.Payload)}】 qos等级=【{arg.ApplicationMessage.QualityOfServiceLevel}】");
            return Task.CompletedTask;

        }

        /// <summary>
        /// 启动后事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task _mqttServer_StartedAsync(EventArgs arg)
        {
            Console.WriteLine($"StartedAsync：MQTT服务已启动……");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 客户端取消订阅事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task _mqttServer_ClientUnsubscribedTopicAsync(ClientUnsubscribedTopicEventArgs arg)
        {
            Console.WriteLine($"ClientUnsubscribedTopicAsync：客户端ID=【{arg.ClientId}】已取消订阅的主题=【{arg.TopicFilter}】  ");
            return Task.CompletedTask;
        }

        private Task _mqttServer_ApplicationMessageNotConsumedAsync(ApplicationMessageNotConsumedEventArgs arg)
        {
            Console.WriteLine($"ApplicationMessageNotConsumedAsync：发送端ID=【{arg.SenderId}】 Topic主题=【{arg.ApplicationMessage.Topic}】 消息=【{Encoding.UTF8.GetString(arg.ApplicationMessage.Payload)}】 qos等级=【{arg.ApplicationMessage.QualityOfServiceLevel}】");
            return Task.CompletedTask;

        }

        /// <summary>
        /// 客户端断开时候触发
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private Task _mqttServer_ClientDisconnectedAsync(ClientDisconnectedEventArgs arg)
        {
            Console.WriteLine($"ClientDisconnectedAsync：客户端ID=【{arg.ClientId}】已断开, 地址=【{arg.Endpoint}】  ");
            return Task.CompletedTask;

        }

        /// <summary>
        /// 客户端连接时候触发
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task _mqttServer_ClientConnectedAsync(ClientConnectedEventArgs arg)
        {
            Console.WriteLine($"ClientConnectedAsync：客户端ID=【{arg.ClientId}】已连接, 用户名=【{arg.UserName}】地址=【{arg.Endpoint}】  ");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}