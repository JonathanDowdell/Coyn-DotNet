using Coyn.Token.Model;
using Coyn.Transaction.Model;
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
    public readonly PlaidClient PlaidClient;
    

    /// <summary> The PlaidService function is used to create a PlaidClient object and return it.</summary>
    ///
    /// <param name="configuration"> Configuration</param>
    ///
    /// <returns> A plaidclient object.</returns>
    public PlaidService(IConfiguration configuration)
    {
        var clientId = configuration.GetSection("AppSettings:ClientID").Value;
        var secret = configuration.GetSection("AppSettings:SandboxKey").Value;
        var environment = configuration.GetSection("AppSettings:Environment").Value;
        
        if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(secret))
        {
            throw new System.Exception("Plaid Client ID and/or Secret are required");
        }

        if (string.IsNullOrEmpty(environment))
        {
            throw new System.Exception("Plaid Environment is required");
        }

        PlaidClient = new PlaidClient
        (
            environment switch
            {
                "sandbox" => Environment.Sandbox,
                "development" => Environment.Development,
                "production" => Environment.Production,
                _ => throw new System.Exception("Invalid Plaid environment")
            },
            clientId,
            secret
        );
    }

    /// <summary> The CreateLinkToken function creates a link token for the user.</summary>
    ///
    /// <param name="userEntity"> The user entity</param>
    ///
    /// <returns> A linktokencreateresponse object.</returns>
    public async Task<LinkTokenCreateResponse> CreateLinkTokenAsync(UserEntity userEntity)
    {
        var linkTokenCreateRequest = new LinkTokenCreateRequest
        {
            User = new LinkTokenCreateRequestUser { EmailAddress = userEntity.Email, ClientUserId = userEntity.Id.ToString()},
            Products = new [] {  Products.Assets, Products.Auth },
            ClientName = "Coyn",
            CountryCodes = new [] { CountryCode.Us },
            Language = Language.English
        };
        var linkTokenCreateResponse = await PlaidClient.LinkTokenCreateAsync(linkTokenCreateRequest);
        return linkTokenCreateResponse;
    }

    /// <summary> The ExchangePublicToken function exchanges a public token for an access token.</summary>
    /// <param name="plaidExchangeTokenRequest"></param>
    /// <returns> An itempublictokenexchangeresponse object.</returns>
    public async Task<ItemPublicTokenExchangeResponse> ExchangePublicTokenAsync(
        PlaidExchangeTokenRequest plaidExchangeTokenRequest)
    {
        var itemPublicTokenExchangeRequest = new ItemPublicTokenExchangeRequest
        {
            PublicToken = plaidExchangeTokenRequest.PublicToken
        };
        var itemPublicTokenExchangeResponse = await PlaidClient.ItemPublicTokenExchangeAsync(itemPublicTokenExchangeRequest);
        return itemPublicTokenExchangeResponse;
    }

    /// <summary> The GetTransactionsAsync function retrieves transactions from Plaid for a given access token.</summary>
    /// 
    /// <param name="accessToken"> The access token for the user</param>
    /// <param name="cursor?"> ///     the access token to use for the request.
    /// </param>
    /// <returns> A transactionsresponse object that contains an array of transaction objects and a cursor string.</returns>
    // ReSharper disable once InvalidXmlDocComment
    public async Task<TransactionsResponse> GetTransactionsAsync(string accessToken, string? cursor)
    {
        throw new NotImplementedException();
    }
}