using NewsFeedVn.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewsFeedVn.Controllers
{
    public class ServiceController : ApiController
    {
        // GET: api/StartGetUrl
        [Route("api/Service/StartDataDetail")]
        [HttpGet]
        public IHttpActionResult StartDataDetail()
        {
            bot2 bot2_serrvice = new bot2();
            try
            {
                bot2_serrvice.getDataDetail();
                return Ok();
            }
            catch (Exception ex)
            {
                return Exception(ex.Message);
            }

        }

        private IHttpActionResult Exception(string message)
        {
            throw new NotImplementedException();
        }
        [Route("api/Service/StartGetUrl")]
        [HttpGet]
        public IHttpActionResult StartGetUrl()
        {
            bot1 bot1_serrvice = new bot1();
            try
            {
                bot1_serrvice.getData();
                return Ok();
            }
            catch (Exception ex)
            {
                return Exception(ex.Message);
            }

        }
    }
}
