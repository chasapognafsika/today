using System.Threading.Tasks;
using Clients.Domain.Models;
using Clients.Domain.Services;
using Clients.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Clients.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IClientService _clientService;
        private readonly IQueryClientsService _queryClientsService;

        public ClientsController(ILogger<ClientsController> logger,
                                 IClientService clientService,
                                 IQueryClientsService queryClientsService)
        {
            _logger = logger;
            _clientService = clientService;
            _queryClientsService = queryClientsService;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClients()
        {
            //var test = await _queryClientsService.QueryClientsAsync();
            //var list = test.


            //return test.GetEnumerator(x => ItemToDTO(x))
            //    .ToListAsync();

        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDTO>> GetClient(int id)
        {
            _logger.LogInformation(LogEvents.GetItem, "Getting item {Id}", id);
            var client = await _clientService.GetClientAsync(id);

            if (client == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Get({Id}) NOT FOUND", id);
                return NotFound();
            }

            return ClientToDTO(client);

        }

        // POST: api/Clients
        [HttpPost]
        public async Task<ActionResult<ClientDTO>> CreateClient(ClientDTO client)
        {
            _logger.LogInformation(LogEvents.InsertItem, "Creating new client");
            client.id = await _clientService.AddClientAsync(client);

            return CreatedAtAction(nameof(GetClient), 
                new { id = client.id },
                ClientToDTO(client));
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, ClientDTO client)
        {
            _logger.LogInformation(LogEvents.UpdateItem, "Updating client {Id}", id);
            if (id != client.id)
            {
                _logger.LogWarning(LogEvents.UpdateItemNotFound, "Get({Id}) NOT FOUND", id);
                return BadRequest();
            }
            await _clientService.UpdateClientAsync(client);
            return NoContent();
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientService.DeleteClientAsync(id);

            return NoContent();
        }

        private static ClientDTO ClientToDTO(IClientModel client) =>
            new ClientDTO
            {
                id = client.id,
                firstName = client.firstName,
                lastName = client.lastName,
                email = client.email,
                gender = client.gender,
                ipAddress = client.ipAddress,
                isDeleted = client.isDeleted,
            };
    }

}
