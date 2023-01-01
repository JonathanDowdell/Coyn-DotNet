namespace Coyn.Transaction.Model;

public class TransactionsRequest
{
    public IEnumerable<TransactionInstitutionRequest> Institutions { get; set; }
}