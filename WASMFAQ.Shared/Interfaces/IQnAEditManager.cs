using WASMFAQv2.Shared.Models;

namespace WASMFAQv2.Shared.Interfaces
{
    public interface IQnAEditManager
    {
        Task<bool> CreateQnAAsync(int qnaSetId, Func<Task> reloadData);
        Task<bool> DeleteQnAAsync(int qnaId, Func<Task> reloadData);
        Task<bool> EditQnAAsync(QnA qna, int qnaSetId, Func<Task> reloadData);
    }
}
