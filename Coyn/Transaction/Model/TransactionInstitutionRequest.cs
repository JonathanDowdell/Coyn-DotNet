namespace Coyn.Transaction.Model;

public class TransactionInstitutionRequest
{
    public string AccessToken { get; set; }
    public IEnumerable<string> AccountIds { get; set; }
    public string? Cursor { get; set; }
}