using PixelDrift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PixelDrift.Controllers
{
    [Route("api/[controller]")]
    public class User_loginController : ApiController
    {
        // GET: api/User_login
  [HttpGet]
        public List<User_login> Get()
        {
            User_loginDBRep dbRep = new User_loginDBRep();


            return dbRep.GetUser_Logins();
        }

        // GET: api/User_login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User_login
        public void Post([FromBody]string value)
        {

        }

        // PUT: api/User_login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User_login/5
        public void Delete(int id)
        {
        }
    }
}
