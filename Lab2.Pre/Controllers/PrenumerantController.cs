using Lab2.Pre.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Pre.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PrenumerantController : ControllerBase
    {
        [HttpGet(Name = "SelectPrenLista")]
        public List<Pren> GetPrenLista()
        {
            List<Pren> plist = new List<Pren>();
            PrenMetoder pm = new PrenMetoder();
            string error = "";

            plist = pm.SelectPrenLista(out error);
            return plist;
        }

        [HttpGet("id", Name = "SelectPrenumerant")]
        public Pren SelectPrenumerant(int id)
        {
            Pren p = new Pren();
            PrenMetoder pm = new PrenMetoder();
            p = pm.SelectPrenumerant(out string error, id);
            return p;
        }

        [HttpPut("id", Name = "UpdatePrenumerant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public string UpdatePrenumerant(int id, [FromBody] Pren prenumerant)
        {
            PrenMetoder pm = new PrenMetoder();
            string errormsg = "";
            int i = pm.UpdatePrenumerant(out errormsg, prenumerant);
            if (errormsg == null)
            {
                return i.ToString();
            }
            else
            {
                return errormsg;
            }
        }
    }
}
