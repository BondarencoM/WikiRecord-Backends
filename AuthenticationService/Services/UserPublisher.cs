﻿using AuthenticationService.Data.Messages;
using AuthenticationService.Services.Interfaces;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuthenticationService.Services
{
    public sealed class RabbitmqUserPublisher : IUserPublisher, IDisposable
    {
        private readonly IConnection connection;
        private readonly ILogger<RabbitmqUserPublisher> logger;
        private readonly IModel channel;

        public RabbitmqUserPublisher(IConnection connection, ILogger<RabbitmqUserPublisher> logger)
        {
            this.connection = connection;
            this.logger = logger;
            this.channel = connection.CreateModel();
        }

        public void Dispose()
        {
            this.channel.Dispose();
        }

        public Task Created(UserIdentifierMessage user)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(user));
            channel.BasicPublish(exchange: "users",
                                 routingKey: $"users.new",
                                 basicProperties: null,
                                 body: body);
            return Task.CompletedTask;
        }

        public Task Deleted(UserIdentifierMessage user)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(user));
            channel.BasicPublish(exchange: "users",
                                 routingKey: $"users.deleted",
                                 basicProperties: null,
                                 body: body);
            return Task.CompletedTask;
        }

        public Task DataRequested(UserIdentifierMessage user)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(user));
            channel.BasicPublish(exchange: "users",
                                 routingKey: $"users.dataRequested",
                                 basicProperties: null,
                                 body: body);
            return Task.CompletedTask;
        }
    }
}