using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using testAPI.Models;
using Stripe;
using Stripe.net;


namespace testAPI.Controllers
{
    public class DisputesController : ApiController
    {
        private readonly DisputeDb DB = new DisputeDb();

        [HttpGet]
        public IHttpActionResult GetDisputeByClientId(int id)
        {
           
            var disputes = DB.DisputesTable.Where(d => d.Client.Id == id);
            if (disputes.Any() == false)
            {
                return NotFound();
            }
            return Ok(disputes);
        }

        [HttpPost]
        public IHttpActionResult AddDisputeToClientId([FromBody]viewmodels.DisputeModel model) {

            if (!ModelState.IsValid)
                return BadRequest();
            var client = DB.ClientsTable.Where(x => x.Id == model.ClientId).FirstOrDefault();
            if (client != null)
            {
                var dispute = new Models.Dispute();
                dispute.Amount = model.Amount;
                dispute.ClientId = model.ClientId;
                dispute.IGet = model.IGet;
                dispute.Latitude = model.Latitude;
                dispute.Longitude = model.Longitude;
                dispute.Reason = model.Reason;

                DB.DisputesTable.Add(dispute);
                DB.SaveChanges();
                return Ok();
            }
       
           
            return BadRequest();
        }
        [HttpPost]

        public IHttpActionResult DisputeHandle([FromBody]viewmodels.DisputeModel model)
        {
            
            StripeConfiguration.SetApiKey("sk_test_6GXM4G0b5JiHSxrv8PYkGP40");

           
            // Get the payment token submitted by the form:
            var token = model.Token; // Using ASP.NET MVC
            var options = new StripeChargeCreateOptions
            {
                Amount = 999,
                Currency = "usd",
                Description = "Example charge",
                SourceTokenOrExistingSourceId = token,
            };
            var service = new StripeChargeService();
            StripeCharge charge = service.Create(options);

            return Ok();

        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteDispute([FromBody]RemoveDisputeViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var findDispute = await DB.DisputesTable.SingleOrDefaultAsync(x => x.ClientId == model.ClientId && x.Id == model.DisputeId);

            if (findDispute != null)
            {
                DB.DisputesTable.Remove(findDispute);
                await DB.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult >UpdateDispute([FromBody]UpdateDisputeViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var findDispute = await DB.DisputesTable.SingleOrDefaultAsync(x =>x.Id==model.Id);

            if (findDispute != null)
            {
                findDispute.Amount = model.Amount;
                findDispute.Reason = model.Reason;
                findDispute.Latitude = model.Latitude;
                findDispute.Longitude = model.Longitude;
                findDispute.IGet = model.IGet;

                DB.Entry(findDispute).State = EntityState.Modified;
               await DB.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();    
        }


    }

}
