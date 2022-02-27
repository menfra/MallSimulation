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
        public MallController(IMallBusiness mallBusiness)
        {
            _mallBusiness = mallBusiness;
        }

        // GET: api/<MallController>
        [HttpGet("mallOpenedStatus")]
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
        [HttpGet("mallOpenCloseDuration")]
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

       
        // POST api/<StandController>
        [HttpPost("addStand")]
        public async Task<IActionResult> AddStand([FromBody] StandDTO standDTO)
        {
            try
            {
                DataAcess.DataModels.Stand stand = await _mallBusiness.AddStand(standDTO);
                return Created($"Stand with Id: {stand.Id} has been added.", stand);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // Get api/<StandController>
        [HttpGet("getStandId/{Id}")]
        public async Task<IActionResult> GetStand(string Id)
        {
            try
            {
                DataAcess.DataModels.Stand stand = await _mallBusiness.GetStand(Id);
                return Ok(stand);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStands()
        {
            try
            {
                var stands = await _mallBusiness.GetStands();
                return Ok(stands);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        // Put api/<StandController>
        [HttpPut("updateStand")]
        public async Task<IActionResult> UpdateStand([FromBody] StandDTO standDTO)
        {
            try
            {
                await _mallBusiness.UpdateStand(standDTO);
                return Ok($"Stand with Id: {standDTO.Id} has been updated.");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        // Delete api/<StandController>
        [HttpDelete("deleteStandId/{Id}")]
        public async Task<IActionResult> DeleteStand(string Id)
        {
            try
            {
                await _mallBusiness.DeleteStand(Id);
                return Ok($"Stand with Id: {Id} has been deleted.");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }
    }
}
