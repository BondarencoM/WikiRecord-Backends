﻿using RecommendationService.Models.Interests;
using System.Threading.Tasks;

namespace RecommendationService.Services.Interfaces;

public interface IInterestService: IRepository<Interest, CreateInterestInputModel, CreateInterestInputModel>
{
    public Task<Interest> GetOrCreate(CreateInterestInputModel input);

}
