using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogics.MallBusiness;
using BusinessLogics.StandBusiness;
using BusinessLogics.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MallService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MallController : ControllerBase
    {
        private readonly IMallBusiness _mallBusiness;
        private readonly IStandBusiness _standBusiness;
        public MallController(IMallBusiness mallBusiness, IStandBusiness standBusiness)
        {
            _mallBusiness = mallBusiness;
            _standBusiness = standBusiness;
        }

        // GET: api/<MallController>
        [HttpGet("MallOpenedStatus")]
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
        [HttpGet("MallOpenCloseDuration")]
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

        // POST api/<MallController>
        [HttpPost]
        public void CreateStand([FromBody] Stand stand)
        {

        }

        // GET api/<MallController>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return "value";
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
