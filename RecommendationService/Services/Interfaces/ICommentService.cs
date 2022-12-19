﻿using RabbitMQ.Client.Events;
using RecommendationService.Models.Comments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecommendationService.Services.Interfaces;

public interface ICommentService : IUserCleanseable, IRabbitEventHandler
{
    Task<List<Comment>> GetCommentsForRecommendation(long id, int limit, int skip);
}
