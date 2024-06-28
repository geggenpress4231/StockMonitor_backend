using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.Interface
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAll();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment?> UpdateAsync(int id, Comment commentModel);
        Task<Comment?> DeleteAsync(int id);
    }
}
