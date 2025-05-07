using WASMFAQv2.Shared.Models;

namespace WASMFAQv2.Shared.Interfaces
{
    public interface ISortManager
    {
        Task MoveQnASetUpAsync(QnASet qnaSet, List<QnASet> allSets);
        Task MoveQnASetDownAsync(QnASet qnaSet, List<QnASet> allSets);
        Task MoveQnAUpAsync(QnA qna, QnASet qnaSet);
        Task MoveQnADownAsync(QnA qna, QnASet qnaSet);

    }
}
