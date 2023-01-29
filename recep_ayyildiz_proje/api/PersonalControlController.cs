using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using recep_ayyildiz.DataAccess;
using recep_ayyildiz.Entities;
using System;
using System.Net;

namespace recep_ayyildiz_proje.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalControlController : ControllerBase
    {
        // 

        // https://localhost:44332/api/PersonalControl/addlog?personalid=1&state=1
        // http://recep.furkankorkusuz.com/api/PersonalControl/addlog?personalid=1&state=0
        public IActionResult AddLog(int personalid, byte state)
        {
            string ww = "";
            PersonalLog personalLog = new PersonalLog
            {
                PersonalID = personalid,
                State = state
            };
            PersonelDB dB = new PersonelDB();
            dB.AddLog(personalLog);
            return Ok(ww);
        }

       
    }
}
