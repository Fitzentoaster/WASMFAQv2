using WASMFAQv2.Shared.Models;

namespace WASMFAQv2.Shared.Interfaces
{
    public interface IFAQApiService
    {
        Task<List<QnASet>> GetQnASetsAsync();
        Task<QnASet> GetQnASetByIdAsync(int id);
        Task<bool> AddQnASetAsync(QnASet qnaSet);
        Task<bool> UpdateQnASetAsync(QnASet qnaSet);
        Task<bool> DeleteQnASetAsync(int id);
        Task<List<QnA>> GetQuestionsByQnASetIdAsync(int id);
        Task<bool> DeleteQnAAsync(int id);
        Task<bool> AddQnAAsync(QnA qna);
        Task<bool> SaveChanges();
        Task<bool> UpdateQnAAsync(QnA qna);
        Task<FAQ> GetFAQAsync();
        Task<bool> NormalizeQnASetSortOrderAsync();
        Task<bool> NormalizeQnASortOrderAsync();
    }
}
