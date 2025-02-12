﻿using PhotoHub.Models.DBObjects;

namespace PhotoHub.Repositories.Interfaces
{
    public interface IBlogPostRepository
    {
        Task AddAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task DeleteAsync(Guid id);
        Task<BlogPost> GetByIdAsync(Guid id);
        Task UpdateAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllPostsByAuthorId(Guid id);
    }
}