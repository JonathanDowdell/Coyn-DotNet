using Coyn.User.Data;
using Going.Plaid;
using Going.Plaid.Entity;
using Going.Plaid.Item;
using Going.Plaid.Link;
using Going.Plaid.Transactions;
using Environment = Going.Plaid.Environment;

namespace Coyn.Plaid;

public class PlaidService: IPlaidService
{
    private readonly PlaidClient _plaidClient;
    
    public PlaidService(IConfiguration configuration)
    {
        _plaidClient = new PlaidClient(
            Environment.Sandbox,
            clientId: configuration.GetSection("AppSettings:ClientID").Value,
            secret: configuration.GetSection("AppSettings:SandboxKey").Value);
        Console.WriteLine("Init - PlaidService");
    }

    /// <summary> The CreateLinkToken function creates a link token for the user.</summary>
    ///
    /// <param name="userEntity"> The user entity</param>
    ///
    /// <returns> A linktokencreateresponse object.</returns>
    public async Task<LinkTokenCreateResponse> CreateLinkToken(UserEntity userEntity)
    {
        var linkTokenCreateRequest = new LinkTokenCreateRequest
        {
            User = new LinkTokenCreateRequestUser { EmailAddress = userEntity.Email, ClientUserId = userEntity.Id.ToString()},
            Products = new [] {  Products.Assets, Products.Auth },
            ClientName = "Coyn",
            CountryCodes = new [] { CountryCode.Us },
            Language = Language.English
        };
        var linkTokenCreateResponse = await _plaidClient.LinkTokenCreateAsync(linkTokenCreateRequest);
        return linkTokenCreateResponse;
    }

    /// <summary> The ExchangePublicToken function exchanges a public token for an access token.</summary>
    ///
    ///
    /// <returns> An itempublictokenexchangeresponse object.</returns>
    public async Task<ItemPublicTokenExchangeResponse> ExchangePublicToken()
    {
        var itemPublicTokenExchangeResponse = await _plaidClient.ItemPublicTokenExchangeAsync(new ItemPublicTokenExchangeRequest());
        return itemPublicTokenExchangeResponse;
    }

    /// <summary> The GetTransactions function retrieves transactions from Plaid for a given access token.</summary>
    ///
    /// <param name="accessToken"> The access token for the user</param>
    /// <param name="cursor?"> The cursor value represents the last update requested</param>
    ///
    /// <returns> An ienumerable of transaction objects.</returns>
    public async Task<IEnumerable<Going.Plaid.Entity.Transaction>> GetTransactions(string accessToken, string? cursor)
    {
        var transactionsSyncRequest = new TransactionsSyncRequest();
        if (cursor != null) transactionsSyncRequest.Cursor = cursor;
        transactionsSyncRequest.AccessToken = accessToken;
        var transactionsSyncResponse = await _plaidClient.TransactionsSyncAsync(transactionsSyncRequest);
        var transactions = transactionsSyncResponse.Added;
        return transactions;
    }
}