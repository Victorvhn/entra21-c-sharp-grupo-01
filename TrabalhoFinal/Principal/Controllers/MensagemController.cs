using Newtonsoft.Json;
using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Principal.Controllers
{
    public class MensagemController : Controller
    {
        // GET: Mensagem
        [HttpPost]
        public async Task<ActionResult> Enviar(string mensagem, string id)
        {
            var options = new PusherOptions
            {
                Cluster = "us2",
                Encrypted = true
            };

            var pusher = new Pusher(
              "604379",
              "1ba12d6fac4afcac5b77",
              "34ff044888ac5d739453",
              options);

            var dataHora = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

            var result = await pusher.TriggerAsync(
              "my-channel",
              "my-event",
              new { message =  mensagem, dataHora, id });

            return Content(JsonConvert.SerializeObject(new { status = "ok", dataHora }));
        }
    }
}