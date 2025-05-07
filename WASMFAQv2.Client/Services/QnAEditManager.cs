using MudBlazor;
using WASMFAQv2.Client.Components;
using WASMFAQv2.Shared.Interfaces;
using WASMFAQv2.Shared.Models;

public class QnAEditManager : IQnAEditManager
{
    private readonly IFAQApiService _faqApiService;
    private readonly IDialogService _dialogService;

    public QnAEditManager(IFAQApiService faqApiService, IDialogService dialogService)
    {
        _faqApiService = faqApiService;
        _dialogService = dialogService;
    }

    public async Task<bool> CreateQnAAsync(int qnaSetId, Func<Task> reloadData)
    {
        var parameters = new DialogParameters
        {
            { "QnASetId", qnaSetId },
            { "qna", new QnA() }
        };

        var options = new DialogOptions
        {
            BackgroundClass = "bg-blur",
            MaxWidth = MaxWidth.Large,
            FullWidth = true
        };

        var dialog = await _dialogService.ShowAsync<QnAModal>("New Q&A", parameters, options);
        var result = await dialog.Result;

        if (result is not null && !result.Canceled && result.Data is QnA qna)
        {
            qna.QnASetId = qnaSetId;
            qna.SortOrder = (await _faqApiService.GetQuestionsByQnASetIdAsync(qnaSetId)).Count + 1;

            await _faqApiService.AddQnAAsync(qna);
            await _faqApiService.NormalizeQnASortOrderAsync();
            await reloadData();
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteQnAAsync(int qnaId, Func<Task> reloadData)
    {
        var options = new DialogOptions { BackgroundClass = "bg-blur" };
        var dialog = await _dialogService.ShowAsync<ConfirmDeleteModal>("Confirm Delete", options);
        var result = await dialog.Result;

        if (result is not null && !result.Canceled)
        {
            await _faqApiService.DeleteQnAAsync(qnaId);
            await reloadData();
            return true;
        }
        return false;
    }

    public async Task<bool> EditQnAAsync(QnA qna, int qnaSetId, Func<Task> reloadData)
    {
        var parameters = new DialogParameters
        {
            { "qna", qna },
            { "QnASetId", qnaSetId }
        };

        var options = new DialogOptions
        {
            BackgroundClass = "bg-blur",
            MaxWidth = MaxWidth.Large,
            FullWidth = true
        };

        var dialog = await _dialogService.ShowAsync<QnAEditModal>("Edit Q&A", parameters, options);
        var result = await dialog.Result;

        if (result is not null && !result.Canceled && result.Data is QnA updated)
        {
            updated.QnASetId = qnaSetId;
            updated.SortOrder = qna.SortOrder;

            await _faqApiService.UpdateQnAAsync(updated);
            await reloadData();
            return true;
        }
        return false;
    }
}
