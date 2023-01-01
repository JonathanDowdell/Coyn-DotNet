using Coyn.Transaction.Model;
using Going.Plaid;
using Going.Plaid.Transactions;

namespace Coyn.Transaction.Service;

public interface ITransactionService
{
    /// <summary> The GetTransactionsAsync function retrieves transactions from Plaid for a given access token.</summary>
    /// <param name="accessToken"> The accessToken used for the specific institution.</param>
    /// <param name="cursor"></param>
    /// <param name="cursor?"> The cursor value represents the last update requested. Providing it will cause the response to only return changes after this update.</param>
    /// 
    /// <returns> A transactionsresponse object.</returns>
    // ReSharper disable once InvalidXmlDocComment
    Task<TransactionInstitutionResponse> GetTransactionsAsync(string accessToken, string? cursor);
    
    Task<TransactionsResponse> GetTransactionsAsync(TransactionsRequest transactionsRequest);
}