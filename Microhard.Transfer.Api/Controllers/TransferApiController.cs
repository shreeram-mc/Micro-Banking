using System.Collections.Generic;
using Microhard.Transfer.Domain.Models;
using MicroHard.Transfer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microhard.Transfer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferApiController : ControllerBase
    {

        private readonly ITransferService _transferService;

        public TransferApiController(ITransferService transferService)
        {
            _transferService = transferService;
        }


        // GET api/banking
        [HttpGet]
        public ActionResult<IEnumerable<TransferLog>> Get()
        {
            return Ok(_transferService.GetTransferLogs());
        }


        //[HttpPost]
        //public IActionResult Post([FromBody]AccountTransfer accountTransfer)
        //{
        //    _accountService.TransferFunds(accountTransfer);

        //    return Ok(accountTransfer);
        //}
    }
}