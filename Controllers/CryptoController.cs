using Microsoft.AspNetCore.Mvc;
using DoggyApi.Services;
using DoggyApi.Models;

namespace DoggyApi.Controllers;

[ApiController]
[Route("")]
public class CryptoController : ControllerBase
{
    private readonly ICryptoService _cryptoService;

    public CryptoController(ICryptoService cryptoService)
    {
        _cryptoService = cryptoService;
    }

    [HttpPost("encrypt")]
    public IActionResult Encrypt([FromBody] EncryptRequest request)
    {
        var result = _cryptoService.Encrypt(request.Text, request.Shift);
        return Ok(result);
    }

 [HttpPost("decrypt")]
public IActionResult Decrypt([FromBody] EncryptRequest request)
{
    if (request == null)
        return BadRequest("Request body is missing.");

    var result = _cryptoService.Decrypt(request.Text, request.Shift);
    return Ok(result);

}
}
