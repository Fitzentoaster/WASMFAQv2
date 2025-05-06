using WASMFAQv2.Shared.Models;
using WASMFAQv2.Shared.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace WASMFAQv2.Client.Services
{
    public class FAQApiService : IFAQApiService
    {
        private readonly HttpClient _httpClient;
        public FAQApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<QnASet>> GetQnASetsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<QnASet>>("api/faq/qnasets") ?? new List<QnASet>();
        }
        public async Task<QnASet> GetQnASetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<QnASet>($"api/faq/qnasets/{id}") ?? new QnASet();
        }
        public async Task<bool> AddQnASetAsync(QnASet qnaSet)
        {
            var response = await _httpClient.PostAsJsonAsync("api/faq/qnasets", qnaSet);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateQnASetAsync(QnASet qnaSet)
        {
            var response = await _httpClient.PutAsJsonAsync("api/faq/qnasets", qnaSet);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteQnASetAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/faq/qnasets/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<List<QnA>> GetQuestionsByQnASetIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<QnA>>($"api/faq/qnasets/{id}/questions") ?? new List<QnA>();
        }
        public async Task<bool> DeleteQnAAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/faq/qnasets/questions/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> AddQnAAsync(QnA qna)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/faq/qnasets/{qna.QnaId}/questions", qna);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> SaveChanges()
        {
            var response = await _httpClient.PostAsync("api/faq/qnasets/savechanges", null);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateQnAAsync(QnA qna)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/faq/qnasets/questions/{qna.QnaId}", qna);
            return response.IsSuccessStatusCode;
        }
        public async Task<FAQ> GetFAQAsync()
        {
            return await _httpClient.GetFromJsonAsync<FAQ>("api/faq/faq") ?? new FAQ();
        }
        public async Task<bool> NormalizeQnASetSortOrderAsync()
        {
            var response = await _httpClient.PostAsync("api/faq/qnasets/normalize-sort-order", null);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> NormalizeQnASortOrderAsync()
        {
            var response = await _httpClient.PostAsync("api/faq/qnasets/questions/normalize-sort-order", null);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateFAQAsync(FAQ faq)
        {
            var response = await _httpClient.PutAsJsonAsync("api/faq/faq", faq);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> CreateFAQAsync(FAQ faq)
        {
            var response = await _httpClient.PostAsJsonAsync("api/faq/faq", faq);
            return response.IsSuccessStatusCode;
        }
    }
}
