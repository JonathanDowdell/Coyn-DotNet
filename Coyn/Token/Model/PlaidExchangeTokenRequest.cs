namespace Coyn.Token.Model;

public class PlaidExchangeTokenRequest
{
    public PlaidExchangeTokenRequest(string publicToken)
    {
        PublicToken = publicToken;
    }

    public string PublicToken { get; set; }
}