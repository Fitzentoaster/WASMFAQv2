using WASMFAQv2.Shared.Models;
using WASMFAQv2.Shared.Interfaces;

namespace WASMFAQv2.Client.Services
{
    public class SortManager : ISortManager
    {
        private readonly IFAQApiService _faqApiService;

        public SortManager(IFAQApiService faqApiService)
        {
            _faqApiService = faqApiService;
        }

        public async Task MoveQnASetUpAsync(QnASet qnaSet, List<QnASet> allSets)
        {
            var ordered = allSets.OrderBy(q => q.SortOrder).ToList();
            var index = ordered.FindIndex(q => q.QnASetId == qnaSet.QnASetId);
            if (index <= 0) return;

            var above = ordered[index - 1];

            SwapSortOrder(qnaSet, above);

            await _faqApiService.UpdateQnASetAsync(qnaSet);
            await _faqApiService.UpdateQnASetAsync(above);
            await _faqApiService.NormalizeQnASetSortOrderAsync();
        }

        public async Task MoveQnASetDownAsync(QnASet qnaSet, List<QnASet> allSets)
        {
            var ordered = allSets.OrderBy(q => q.SortOrder).ToList();
            var index = ordered.FindIndex(q => q.QnASetId == qnaSet.QnASetId);
            if (index < 0 || index >= ordered.Count - 1) return;

            var below = ordered[index + 1];

            SwapSortOrder(qnaSet, below);

            await _faqApiService.UpdateQnASetAsync(qnaSet);
            await _faqApiService.UpdateQnASetAsync(below);
            await _faqApiService.NormalizeQnASetSortOrderAsync();
        }

        public async Task MoveQnAUpAsync(QnA qna, QnASet qnaSet)
        {
            if (qnaSet?.QnAs == null) return;
            var ordered = qnaSet.QnAs.OrderBy(q => q.SortOrder).ToList();
            var index = ordered.FindIndex(q => q.QnaId == qna.QnaId);
            if (index <= 0) return;

            var above = ordered[index - 1];

            SwapSortOrder(qna, above);

            await _faqApiService.UpdateQnAAsync(qna);
            await _faqApiService.UpdateQnAAsync(above);
            await _faqApiService.NormalizeQnASortOrderAsync();
        }

        public async Task MoveQnADownAsync(QnA qna, QnASet qnaSet)
        {
            if (qnaSet?.QnAs == null) return;
            var ordered = qnaSet.QnAs.OrderBy(q => q.SortOrder).ToList();
            var index = ordered.FindIndex(q => q.QnaId == qna.QnaId);
            if (index < 0 || index >= ordered.Count - 1) return;

            var below = ordered[index + 1];

            SwapSortOrder(qna, below);

            await _faqApiService.UpdateQnAAsync(qna);
            await _faqApiService.UpdateQnAAsync(below);
            await _faqApiService.NormalizeQnASortOrderAsync();
        }

        private void SwapSortOrder(dynamic a, dynamic b)
        {
            int temp = a.SortOrder;
            a.SortOrder = b.SortOrder;
            b.SortOrder = temp;
        }
    }

}
