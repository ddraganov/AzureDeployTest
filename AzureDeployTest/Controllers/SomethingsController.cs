using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AzureDeployTest.Models;

namespace AzureDeployTest.Controllers
{
    public class SomethingsController : ApiController
    {
        [HttpPost]
        public void Create(Something something)
        {
            // TODO
        }

        [HttpGet]
        public void GetAll()
        {
            // TODO
        }
    }
}
