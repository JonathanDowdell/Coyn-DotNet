using Coyn.Plaid;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coyn.Transaction.Controller;

[ApiController]
[Route("user"), Authorize]
public class TransactionController: Microsoft.AspNetCore.Mvc.Controller
{
   private readonly IPlaidService _plaidService;

   public TransactionController(IPlaidService plaidService)
   {
      _plaidService = plaidService;
   }

   public Task<ActionResult<IEnumerable<Going.Plaid.Entity.Transaction>>> GetTransactions()
   {
      throw new NotImplementedException();
   }
   
}