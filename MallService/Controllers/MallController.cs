using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAcess.DataModels;
using DataAcess.Interfaces;
using DataAcess.DataServices;
using MallService.MallBusinessLayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MallService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MallController : ControllerBase
    {
        private IMallBusiness _mallBusiness;
        public MallController(IMallBusiness mallBusiness)
        {
            _mallBusiness = mallBusiness;
        }

        // GET: api/<MallController>
        [HttpGet("Get Mall Opened Status")]
        public IActionResult GetMallOpenedStatus()
        {
            try
            {
                var mall = _mallBusiness.GetMallOpenedStatus();
                return Ok(mall.OpenedState.ToString());
            }
            catch (Exception)
            {

                return NotFound("The requested service could not be found");
            }
           
        }

        // GET: api/<MallController>
        [HttpGet("Get Mall Open and Close Duration")]
        public IActionResult GetMallOpenCloseDuration()
        {
            try
            {
                var mall = _mallBusiness.GetMallOpenCloseDuration();
                if (mall.OpenClosedDuration > 0)
                    return Ok(mall.OpenClosedDuration.ToString());
                else
                    return Problem("There Seem to be an invalid input for the mall open and close duration.");
            }
            catch (Exception)
            {

                return NotFound("There Seem to be an invalid input for the mall open and close duration.");
            }

        }

        // GET api/<MallController>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return "value";
        }

        // POST api/<MallController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MallController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MallController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
