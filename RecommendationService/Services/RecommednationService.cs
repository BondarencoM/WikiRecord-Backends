﻿using Microsoft.EntityFrameworkCore;
using RecommendationService.Models;
using RecommendationService.Models.Exceptions;
using RecommendationService.Models.Recommendations;
using RecommendationService.Models.User;
using RecommendationService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace RecommendationService.Services;

public class RecommednationService : IRecommendationService
{

    private readonly DatabaseContext db;

    private readonly IPrincipal _principal;

    private string PrincipalUsername => _principal?.Identity?.Name;

    public RecommednationService(
        DatabaseContext db,
        IPrincipal principal = null)
    {
        this.db = db;
        _principal = principal;
    }

    public async Task<Recommendation> Add(CreateRecommedationInputModel input)
    {

        var alreadyExists = await db.Recommendations.AsQueryable()
                                    .Where(r => r.InterestId == input.InterestId)
                                    .Where(r => r.PersonaId == input.PersonaId)
                                    .SingleOrDefaultAsync();
        if(alreadyExists != null)
        {
            throw new EntityAlreadyExistsException<Recommendation>(alreadyExists);
        }

        if (this.PrincipalUsername is null) throw new Exception("401 or something");

        var recommendation = new Recommendation()
        {
            InterestId = input.InterestId,
            PersonaId = input.PersonaId,
            Context = input.Context,
            AddedBy = PrincipalUsername,
            CreatedAt = DateTime.Now,
        };

        var fromDb = db.Recommendations.Add(recommendation);

        await db.SaveChangesAsync();

        return fromDb.Entity;
    }

    public async Task<List<Recommendation>> All()
    {
        return await db.Recommendations.AsQueryable().ToListAsync();
    }

    public async Task<Recommendation> Find(long id)
    {
        return await db.Recommendations
                            .Include(r => r.Interest)
                            .Include(r => r.Persona)
                            .Where(r => r.Id == id)
                            .SingleOrDefaultAsync();
    }

    public async Task Update(long id, UpdateRecommendationInputModel update)
    {

        var commend = await db.Recommendations.FindAsync(id);

        if(update.Context != null) commend.Context = update.Context;

        await db.SaveChangesAsync();
    }

    public Task CleanseUser(UserIdentifierIM user) =>
        db.Recommendations
            .Where(r => r.AddedBy == user.Username)
            .ExecuteUpdateAsync(comment => comment.SetProperty(p => p.AddedBy, "[removed]"));

    public async Task<PersonalDataModel> GetDownloadableUserData(UserIdentifierIM user)
    {
        var data = await db.Recommendations
            .Where(c => c.AddedBy == user.Username)
            .ToListAsync();

        return new PersonalDataModel()
        {
            JsonData = data,
            Name = "addedRecommendations",
        };
    }
}
