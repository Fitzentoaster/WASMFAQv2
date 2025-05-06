using WASMFAQv2.Server.Contexts;
using WASMFAQv2.Shared.Models;
using WASMFAQv2.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using WASMFAQv2.Resources;

namespace WASMFAQv2.Server.Repository
{
    public class QnASetRepository : IQnASetRepository
    {
        private readonly FAQContext _context;
        public QnASetRepository(FAQContext context)
        {
            _context = context;
        }
        public async Task<List<QnASet>> GetQnASetsAsync()
        {
            return await _context.QnASets.ToListAsync();
        }
        public async Task<QnASet> GetQnASetByIdAsync(int id)
        {
            var retValue = await _context.QnASets
                .Include(q => q.QnAs)
                .FirstOrDefaultAsync(q => q.QnASetId == id);

            if (retValue == null)
            {
                throw new Exception(AppStrings.QnASetNotFound);
            }

            return retValue;
        }

        public async Task<bool> AddQnASetAsync(QnASet qnaSet)
        {
            _context.QnASets.Add(qnaSet);
            return await SaveChangesAsync();
        }
        public async Task<bool> UpdateQnASetAsync(QnASet qnaSet)
        {
            var existingQnASet = await _context.QnASets
                .Include(q => q.QnAs)
                .FirstOrDefaultAsync(q => q.QnASetId == qnaSet.QnASetId);
            if (existingQnASet == null)
            {
                throw new Exception(AppStrings.QnASetNotFound);
            }
            if (qnaSet.Name != null) existingQnASet.Name = qnaSet.Name;
            if (qnaSet.Description != null) existingQnASet.Description = qnaSet.Description;
            if (qnaSet.SortOrder != 0) existingQnASet.SortOrder = qnaSet.SortOrder;

            return await SaveChangesAsync();
        }
        public async Task<bool> DeleteQnASetAsync(int id)
        {
            var qnaSet = await _context.QnASets
                .Include(q => q.QnAs)
                .FirstOrDefaultAsync(q => q.QnASetId == id);

            if (qnaSet == null)
            {
                throw new Exception(AppStrings.QnASetNotFound);
            }

            _context.QnASets.Remove(qnaSet);
            return await SaveChangesAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync().ContinueWith(task => task.Result > 0);
        }

        public async Task<List<QnA>> GetQuestionsByQnASetIdAsync(int id)
        {
            var qnas = _context.QnAs
                .Where(q => q.QnASetId == id)
                .ToListAsync();
            if (qnas == null)
            {
                throw new Exception(AppStrings.QnASetNotFound);
            }
            return await qnas;
        }

        public async Task<bool> DeleteQnAAsync(int id)
        {
            var qna = await _context.QnAs
                .FirstOrDefaultAsync(q => q.QnaId == id);
            if (qna == null)
            {
                throw new Exception(AppStrings.QnANotFound);
            }
            _context.QnAs.Remove(qna);
            return await SaveChangesAsync();
        }
        public async Task<bool> AddQnAAsync(QnA qna)
        {
            var qnaSet = await _context.QnASets
                .Include(q => q.QnAs)
                .FirstOrDefaultAsync(q => q.QnASetId == qna.QnASetId);

            if (qnaSet == null)
            {
                throw new Exception(AppStrings.QnASetNotFound);
            }

            if (qnaSet.QnAs == null)
            {
                qnaSet.QnAs = new List<QnA>();
            }

            qnaSet.QnAs.Add(qna);
            return await SaveChangesAsync();
        }
        public async Task<bool> UpdateQnAAsync(QnA qna)
        {
            var existingQnA = await _context.QnAs
                .FirstOrDefaultAsync(q => q.QnaId == qna.QnaId);
            if (existingQnA == null)
            {
                throw new Exception(AppStrings.QnANotFound);
            }
            _context.Entry(existingQnA).CurrentValues.SetValues(qna);
            return await SaveChangesAsync();
        }
        public async Task<FAQ> GetFAQAsync()
        {
            var faq = await _context.FAQs
                .FirstOrDefaultAsync();
            if (faq == null)
            {
                faq = new FAQ();
                faq.Title = AppStrings.FAQDefaultTitle;
                faq.Description = AppStrings.FAQDefaultDescription;
            }
            return faq;
        }
        public async Task<bool> NormalizeQnASetSortOrderAsync()
        {
            var qnaSets = await _context.QnASets
                .OrderBy(q => q.SortOrder)
                .ToListAsync();
            for (int i = 0; i < qnaSets.Count; i++)
            {
                qnaSets[i].SortOrder = i;
            }
            _context.QnASets.UpdateRange(qnaSets);
            return await SaveChangesAsync();
        }
        public async Task<bool> NormalizeQnASortOrderAsync()
        {
            var sets = await _context.QnASets
                .Include(s => s.QnAs)
                .ToListAsync();

            foreach (var set in sets)
            {
                var qnas = (set.QnAs ?? Enumerable.Empty<QnA>()).OrderBy(q => q.SortOrder).ToList();
                for (int i = 0; i < qnas.Count; i++)
                {
                    qnas[i].SortOrder = i;
                }
                _context.QnAs.UpdateRange(qnas);
            }

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateFAQAsync(FAQ faq)
        {
            var existingFAQ = await _context.FAQs
                .FirstOrDefaultAsync();
            if (existingFAQ == null)
            {
                throw new Exception(AppStrings.FAQNotFound);
            }
            _context.Entry(existingFAQ).CurrentValues.SetValues(faq);
            return await SaveChangesAsync();
        }
        public async Task<bool> CreateFAQAsync(FAQ faq)
        {
            var existingFAQ = await _context.FAQs
                .FirstOrDefaultAsync();
            if (existingFAQ != null)
            {
                throw new Exception(AppStrings.FAQAlreadyExists);
            }
            _context.FAQs.Add(faq);
            return await SaveChangesAsync();
        }
    }
}
