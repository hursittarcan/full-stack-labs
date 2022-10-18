using DevOps.Api.Models;
using DevOps.AppLogic;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class TeamsController : ControllerBase
{
    public TeamsController(ITeamService teamService, ITeamRepository teamRepository, IMapper mapper)
    {
    }

    [HttpGet]
    public Task<IActionResult> GetAll()
    {
        throw new NotImplementedException();
    }

    [HttpPost("{id}/assemble")]
    public Task<IActionResult> AssembleTeam(Guid id, TeamAssembleInputModel model)
    {
        throw new NotImplementedException();
    }
}