using System.Collections.Immutable;
using Coyn.Plaid;
using Coyn.Transaction.Model;
using Going.Plaid.Accounts;
using Going.Plaid.Entity;
using Going.Plaid.Transactions;

namespace Coyn.Transaction.Service;

public class TransactionService: ITransactionService
{
    private readonly PlaidService _plaidService;
    
    public TransactionService(PlaidService plaidService)
    {
        _plaidService = plaidService;
    }

    /// <summary> The GetTransactionsAsync function retrieves transactions from Plaid for a given access token.</summary>
    /// <param name="accessToken"> The accessToken used for the specific institution.</param>
    /// <param name="cursor?"> The cursor value represents the last update requested. Providing it will cause the response to only return changes after this update.</param>
    /// 
    /// <returns> A transactionsresponse object.</returns>
    // ReSharper disable once InvalidXmlDocComment
    public async Task<TransactionInstitutionResponse> GetTransactionsAsync(string accessToken, string? cursor)
    {
        var transactionsSyncRequest = new TransactionsSyncRequest
        {
            Count = 100
        };
        if (cursor != null) transactionsSyncRequest.Cursor = cursor;
        transactionsSyncRequest.AccessToken = accessToken;
        var transactionsSyncResponse = await _plaidService.PlaidClient.TransactionsSyncAsync(transactionsSyncRequest);
        var transactions = transactionsSyncResponse.Added;
        return new TransactionInstitutionResponse
        {
            Cursor = transactionsSyncResponse.NextCursor,
            Transactions = transactions
        };
    }

    

    /// <summary> The GetTransactionsAsync function retrieves transactions for a given institution.</summary>
    ///
    /// <param name="transactionsRequest"> The transactions request.</param>
    ///
    /// <returns> A transactionsresponse object.</returns>
    public async Task<TransactionsResponse> GetTransactionsAsync(TransactionsRequest transactionsRequest)
    {
        var accountIds = transactionsRequest.Institutions.SelectMany(institution => institution.AccountIds).ToImmutableHashSet();

        var transactionInstitutionResponses = new List<TransactionInstitutionResponse>();

        foreach (var transactionInstitutionRequest in transactionsRequest.Institutions)
        {
            var institutionAccountIds = transactionInstitutionRequest.AccountIds.ToList();

            var transactionsResponse = await GetTransactionsAsync(transactionInstitutionRequest.AccessToken, transactionInstitutionRequest.Cursor);
            var filteredTransactions = transactionsResponse.Transactions.Where(transaction => accountIds.Contains(transaction.AccountId));
            
            var transactionArray = filteredTransactions as Going.Plaid.Entity.Transaction[] ?? filteredTransactions.ToArray();
            
            var accountsGetRequest = new AccountsGetRequest
            {
                AccessToken = transactionInstitutionRequest.AccessToken,
                Options = new AccountsGetRequestOptions
                {
                    AccountIds = institutionAccountIds
                }
            };
            var accountsGetResponse = await _plaidService.PlaidClient.AccountsGetAsync(accountsGetRequest);
            
            foreach (var institutionAccountId in institutionAccountIds)
            {
                var account = accountsGetResponse.Accounts.FirstOrDefault(account => account.AccountId.Equals(institutionAccountId));
                if (account == null) continue;
                var transactions = transactionArray.Where(transaction => transaction.AccountId.Equals(institutionAccountId));
                var transactionInstitutionResponse = new TransactionInstitutionResponse
                {
                    Cursor = transactionsResponse.Cursor,
                    Transactions = transactions,
                    Account = account
                };
                transactionInstitutionResponses.Add(transactionInstitutionResponse);
            }
            
        }

        return new TransactionsResponse
        {
            AccountTransactions = transactionInstitutionResponses
        };
    }
}