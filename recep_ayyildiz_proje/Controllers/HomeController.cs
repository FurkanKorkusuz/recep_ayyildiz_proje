using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using recep_ayyildiz.DataAccess;
using recep_ayyildiz.Entities;
using recep_ayyildiz_proje.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace recep_ayyildiz_proje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            PersonelDB dB = new PersonelDB();
            List<PersonalLogViewDto> personalLogs = dB.GetPersonalLog(new PersonalLog());
            if(personalLogs==null)
                personalLogs= new List<PersonalLogViewDto>();
            return View(personalLogs);
        }


        // https://localhost:44332/home/addlog?personalid=1&state=1
        // http://recep.furkankorkusuz.com/home/addlog?personalid=1&state=0
        public IActionResult AddLog(int personalid, byte state)
        {
            string ww = "";
            PersonalLog personalLog = new PersonalLog{
                PersonalID= personalid,
                State=state
            };
            PersonelDB dB = new PersonelDB();
            dB.AddLog(personalLog);
            return View("Index");
        }

        public async Task<IActionResult> Test()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://recep.furkankorkusuz.com/api/PersonalControl/addlog?personalid=1&state=0");
            

            if (response.IsSuccessStatusCode)
            {
                var sss =  response.Content;
            }

            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
