using BusinessLogics.DTO;
using BusinessLogics.StandBusiness;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StandService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandController : ControllerBase
    {
        private readonly IStandBusiness _standBusiness;
        public StandController(IStandBusiness standBusiness)
        {
            _standBusiness = standBusiness;
        }

        // POST api/<StandController>
        [HttpPost("addStand")]
        public async Task<IActionResult> AddStand([FromBody] StandDTO standDTO)
        {
            try
            {
                DataAcess.DataModels.Stand stand = await _standBusiness.AddStand(standDTO);
                if (stand != null)
                    return Created($"Stand with Id: {stand.Id} has been added.", stand.Id);
                else
                    return Problem($"Stand with Id: {stand.Id} could not be added.", null, 500);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        // Delete api/<StandController>
        [HttpDelete("deleteStand/{standId}")]
        public async Task<IActionResult> DeleteStand(string standId)
        {
            try
            {
                await _standBusiness.DeleteStand(standId);
                return Ok($"Stand with Id: {standId} has been deleted.");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        // Delete api/<StandController>
        [HttpPost("deleteStands")]
        public async Task<IActionResult> DeleteStandBulk([FromBody] List<string> Ids)
        {
            try
            {
                await _standBusiness.DeleteStandBulk(Ids);
                return Ok($"Stand with Ids: {string.Join(",", Ids)} have been deleted.");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        // Delete api/<StandController>
        [HttpDelete("deleteStandByproduct/{productId}")]
        public async Task<IActionResult> DeleteStandProductId(string productId)
        {
            try
            {
                await _standBusiness.DeleteStandProductId(productId);
                return Ok($"Stand with Id: {productId} has been deleted.");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        // Get api/<StandController>
        [HttpGet("getStand/{standId}")]
        public async Task<IActionResult> GetStand(string standId)
        {
            try
            {
                DataAcess.DataModels.Stand stand = await _standBusiness.GetStand(standId);
                if (stand != null)
                    return Ok(stand);
                else
                    return Problem($"Could not find stand with Id {standId}", null, 404);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        [HttpGet("getAllStands")]
        public async Task<IActionResult> GetStands()
        {
            try
            {
                var stands = await _standBusiness.GetStands();
                return Ok(stands);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        // Get api/<StandController>
        [HttpGet("getStandByProduct/{productId}")]
        public async Task<IActionResult> GetStandByProductId(string productId)
        {
            try
            {
                var stands = await _standBusiness.GetStandsProductId(productId);
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
                await _standBusiness.UpdateStand(standDTO);
                return Ok($"Stand with Id: {standDTO.Id} has been updated.");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        // Put api/<StandController>
        [HttpPut("updateStandBulk")]
        public async Task<IActionResult> UpdateStandBulk([FromBody] List<StandDTO> standDTOs)
        {
            try
            {
                await _standBusiness.UpdateStandBulk(standDTOs);
                return Ok($"Stand with Id: { string.Join(',', standDTOs.Select(i=>i.Id)) } has been updated.");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

    }
}
