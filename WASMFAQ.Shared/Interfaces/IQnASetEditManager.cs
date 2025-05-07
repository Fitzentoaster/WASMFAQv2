using WASMFAQv2.Shared.Models;

namespace WASMFAQv2.Shared.Interfaces
{
    public interface IQnASetEditManager
    {
        Task<bool> CreateQnASetAsync(Func<Task> reloadData, Func<int, Task> scrollToQnASet);
        Task<bool> EditQnASetAsync(QnASet qnaSet, Func<Task> reloadData, Func<int, Task> scrollToQnASet);
        Task<bool> DeleteQnASetAsync(int qnaSetId, List<QnASet> qnaSets, Func<Task> reloadData, Func<int, Task> scrollToQnASet);
    }
}