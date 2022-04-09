using DunAndBradstreetProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DunAndBradstreetProject.Controllers
{
    [ApiController]
    [Route("agentsOrders")]
    public class AgentsOrdersController : ControllerBase
    {
        // For the purpose of the excirses I'll write the connection string hardcoded, but of course 
        // in a real project the connection string will be saved in the configuration. 

        private IRepository _repository;
        public AgentsOrdersController()
        {
            _repository = new Repository("Data Source =.; Initial Catalog = DunAndBradstreet; Integrated Security = True");
        }

        [HttpGet("GetHighestAdvancedAmount/{year}")]
        public IActionResult GetHighestAdvancedAmount(int year)
        {
            try
            {
                string agentCode = _repository.GetAgentWithHighestAdvancedAmount(year);
                return Ok(agentCode);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetAgentsByOrderAmuont/{num}/{year}")] 
        public IActionResult GetAgentsByOrderAmuont(int num, int year)
        {
            
            try
            {
                List<Agent> agents = _repository.GetAgentsByOrderAmuont(num, year);
                return Ok(agents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetAgentsLatestOrder/{num}")]
        public IActionResult GetAgentsLatestOrder(int num, [FromQuery]List<string> agentsCodes)
        {
            try
            {
                List<Order> orders = _repository.GetAgentsLatestOrder(agentsCodes, num);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);                 
            }
        }

    };

       
    
}