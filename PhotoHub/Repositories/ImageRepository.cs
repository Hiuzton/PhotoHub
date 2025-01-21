using PhotoHub.Data;
using PhotoHub.Models.DBObjects;
using PhotoHub.Repositories.Interfaces;

namespace PhotoHub.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Image image)
        {
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task<Image> GetByIdAsync(Guid id)
        {
            return await _context.Images.FindAsync(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var image = await GetByIdAsync(id);
            if (image != null)
            {
                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
            }
        }
    }
}
