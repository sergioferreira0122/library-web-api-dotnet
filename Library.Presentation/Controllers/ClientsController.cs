using Library.Application;
using Library.Application.Features.Clients;
using Library.Application.Features.Clients.Commands;
using Library.Application.Features.Clients.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ISender _sender;

        public ClientsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetClientById(
            int clientId,
            CancellationToken cancellationToken)
        {
            var response = await _sender.Send(new GetClientByIdQuery() { Id = clientId }, cancellationToken);

            if (response == null) return StatusCode(404, ClientErrors.ClientNotFound);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients(CancellationToken cancellationToken)
        {
            var response = await _sender.Send(new GetAllClientsQuery(), cancellationToken);

            if (response.Count == 0)
                return StatusCode(204);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(
            [FromBody] CreateClientCommand createClientCommand,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(createClientCommand, cancellationToken);

            if (result.IsSuccess) return StatusCode(201);

            return HandleClientErrors(result);
        }

        [HttpPut("{clientId}")]
        public async Task<IActionResult> UpdateClient(
            int clientId,
            [FromBody] UpdateClientCommand updateClientCommand,
            CancellationToken cancellationToken)
        {
            if (clientId != updateClientCommand.Id)
                return StatusCode(400, ClientErrors.UpdateClientNotSameIdFromBodyAndParameter);

            var result = await _sender.Send(updateClientCommand, cancellationToken);

            if (result.IsSuccess) return StatusCode(201);

            return HandleClientErrors(result);
        }

        [HttpDelete("{clientId}")]
        public async Task<IActionResult> DeleteClient(
            int clientId,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new DeleteClientCommand(clientId), cancellationToken);

            if (result.IsSuccess) return StatusCode(204);

            return HandleClientErrors(result);
        }

        private IActionResult HandleClientErrors(Result result)
        {
            var resultError = result.Error;

            if (resultError.Equals(ClientErrors.ClientNotFound)) return StatusCode(404, result);
            if (resultError.Equals(ClientErrors.PhoneNumberContainLetters)) return StatusCode(400, result);

            throw new Exception($"Unexpected error: {resultError}");
        }
    }
}