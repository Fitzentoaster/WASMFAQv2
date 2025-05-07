using MudBlazor;
using WASMFAQv2.Client.Components;
using WASMFAQv2.Shared.Interfaces;
using WASMFAQv2.Shared.Models;

public class QnASetEditManager : IQnASetEditManager
{
    private readonly IFAQApiService _faqApiService;
    private readonly IDialogService _dialogService;

    public QnASetEditManager(IFAQApiService faqApiService, IDialogService dialogService)
    {
        _faqApiService = faqApiService;
        _dialogService = dialogService;
    }

    public async Task<bool> CreateQnASetAsync(Func<Task> reloadData, Func<int, Task> scrollToQnASet)
    {
        var parameters = new DialogParameters
        {
            { "QnASet", new QnASet() }
        };

        var options = new DialogOptions { BackgroundClass = "bg-blur" };

        var dialog = await _dialogService.ShowAsync<QnASetModal>("New QnA Set", parameters, options);
        var result = await dialog.Result;

        if (result is not null && !result.Canceled && result.Data is QnASet qnaSet)
        {
            await _faqApiService.AddQnASetAsync(qnaSet);
            await _faqApiService.NormalizeQnASetSortOrderAsync();
            await reloadData();
            await scrollToQnASet(qnaSet.QnASetId);
            return true;
        }
        return false;
    }

    public async Task<bool> EditQnASetAsync(QnASet qnaSet, Func<Task> reloadData, Func<int, Task> scrollToQnASet)
    {
        var parameters = new DialogParameters
        {
            { "qnaSet", qnaSet }
        };

        var options = new DialogOptions { BackgroundClass = "bg-blur" };

        var dialog = await _dialogService.ShowAsync<QnASetEditModal>("Edit QnA Set", parameters, options);
        var result = await dialog.Result;

        if (result is not null && !result.Canceled && result.Data is QnASet updated)
        {
            updated.QnASetId = qnaSet.QnASetId;
            updated.SortOrder = qnaSet.SortOrder;

            await _faqApiService.UpdateQnASetAsync(updated);
            await reloadData();
            await scrollToQnASet(updated.QnASetId);
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteQnASetAsync(
        int qnaSetId,
        List<QnASet> qnaSets,
        Func<Task> reloadData,
        Func<int, Task> scrollToQnASet)
    {
        var options = new DialogOptions { BackgroundClass = "bg-blur" };

        var dialog = await _dialogService.ShowAsync<ConfirmDeleteModal>("Confirm Delete", options);
        var result = await dialog.Result;

        if (result is not null && !result.Canceled)
        {
            var qnaSet = qnaSets.FirstOrDefault(q => q.QnASetId == qnaSetId);

            await _faqApiService.DeleteQnASetAsync(qnaSetId);
            await reloadData();

            return true;
        }
        return false;
    }
}
