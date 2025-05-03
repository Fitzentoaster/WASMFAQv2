using WASMFAQv2.Server.Contexts;
using WASMFAQv2.Shared.Models;
using WASMFAQv2.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                throw new Exception($"QnASet with id {id} not found");
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
                throw new Exception($"QnASet with id {qnaSet.QnASetId} not found");
            }
            
            _context.Entry(existingQnASet).CurrentValues.SetValues(qnaSet);

            return await SaveChangesAsync();
        }
        public async Task<bool> DeleteQnASetAsync(int id)
        {
            var qnaSet = await _context.QnASets
                .Include(q => q.QnAs)
                .FirstOrDefaultAsync(q => q.QnASetId == id);

            if (qnaSet == null)
            {
                throw new Exception($"QnASet with id {id} not found");
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
                throw new Exception($"QnAs with QnASetId {id} not found");
            }
            return await qnas;
        }

        public async Task<bool> DeleteQnAAsync(int id)
        {
            var qna = await _context.QnAs
                .FirstOrDefaultAsync(q => q.QnaId == id);
            if (qna == null)
            {
                throw new Exception($"QnA with id {id} not found");
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
                throw new Exception($"QnASet with id {qna.QnASetId} not found");
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
                throw new Exception($"QnA with id {qna.QnaId} not found");
            }
            _context.Entry(existingQnA).CurrentValues.SetValues(qna);
            return await SaveChangesAsync();
        }
    }
}
