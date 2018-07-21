using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using testAPI.Models;
using testAPI.viewmodels;
using System.Data.Entity;

namespace testAPI.Controllers
{
   
   
    public class ClientsController : ApiController
    {
        private readonly DisputeDb DB = new DisputeDb();
        [HttpGet]
        public IHttpActionResult GetClientById(int id)
        {
            var client = DB.ClientsTable.FirstOrDefault(c => c.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }


        [HttpDelete]

        public async Task<IHttpActionResult> DeleteClient([FromBody]RemoveClient model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var findClient = await DB.ClientsTable.SingleOrDefaultAsync(x => x.Id == model.ClientId);

            if (findClient != null)
            {
                DB.ClientsTable.Remove(findClient);
                await DB.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetAllClients()
        {

            var clients = DB.ClientsTable.ToList();
            if (clients == null)
            {
                return NotFound();
            }
            return Ok(clients);
        }

        [HttpPost]
        public IHttpActionResult AddClient([FromBody]ClientModel model)
        {
            DateTime bd = DateTime.Parse(model.BirthDate);
            var client = new Client();
            client.FirstName = model.FirstName;
            client.LastName = model.LastName;
            client.BirthDate = bd;
            try
            {
                DB.ClientsTable.Add(client);
                DB.SaveChanges();
                return Ok(client);
            }
            catch (Exception)
            {
                return BadRequest();
            }


        }
    }
}
