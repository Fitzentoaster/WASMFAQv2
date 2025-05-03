using WASMFAQv2.Shared.Models;
using WASMFAQv2.Shared.Interfaces;
using System.Net.Http.Json;

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
            return await _httpClient.GetFromJsonAsync<List<QnASet>>("api/faq/qnasets");
        }
        public async Task<QnASet> GetQnASetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<QnASet>($"api/faq/qnasets/{id}");
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
            return await _httpClient.GetFromJsonAsync<List<QnA>>($"api/faq/qnasets/{id}/questions");
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
    }
}
