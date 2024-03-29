﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProfileService.Profiles;

public class ProfileService : IProfileService
{
    private readonly ILogger<ProfileService> logger;
    private readonly DatabaseContext db;

    public ProfileService(
        ILogger<ProfileService> logger,
        DatabaseContext db)
    {
        this.logger = logger;
        this.db = db;
    }

    public async Task<Profile> FindByUsername(string username) =>
        await db.Profiles
            .Where(p => p.Id == username)
            .Include(p => p.Comments.OrderByDescending(c => c.CreatedAt).Take(100))
            .FirstOrDefaultAsync()
            ?? throw new ProfileNotFoundException();

    public Task HandleAsyncEvent(object sender, BasicDeliverEventArgs args)
    {
        var message = Encoding.UTF8.GetString(args.Body.ToArray());
        return args.RoutingKey switch
        {
            "users.new" => AddProfile(),
            "users.deleted" => DeleteProfile(),
            _ => Default(),
        };

        async Task AddProfile()
        {
            var newProfile = JsonSerializer.Deserialize<ProfileIdentiferIM>(message) 
                ?? throw new InvalidOperationException($"Could not deserialize {typeof(ProfileIdentiferIM)} from {message}");

            var profile = new Profile(newProfile);

            this.db.Profiles.Add(profile);

            await this.db.SaveChangesAsync();
        }

        async Task DeleteProfile()
        {
            var toDelete = JsonSerializer.Deserialize<ProfileIdentiferIM>(message)
             ?? throw new InvalidOperationException($"Could not deserialize {typeof(ProfileIdentiferIM)} from {message}");

            try
            {
                await this.db.Profiles
                .Where(p => p.Id == toDelete.Username)
                .ExecuteDeleteAsync();
            }catch(Exception e)
            {
                logger.LogError(e, "Could not delete profile");
            }
        }

        Task Default()
        {
            this.logger.LogWarning("Could not handle Comment message" +
                                    $" with routing key {args.RoutingKey} and body {message}");
            return Task.CompletedTask;
        }
    }
}
