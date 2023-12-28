using Async.Application.Common.Identity.Model;
using Async.Application.Common.Identity.Service;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Async.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController(IMapper mapper, IAuthAggregationService authAggregationService)  : ControllerBase
{
    [HttpPost("sign-up")]
    public async ValueTask<IActionResult> SignUp([FromBody] SignUpDetails signUpDetails, CancellationToken cancellationToken)
    {
        var result = await authAggregationService.SignUpAsync(signUpDetails, cancellationToken);
        return result ? Ok() : BadRequest();
    }
}

