using AutoMapper;
using DevOps.Api.Models;
using DevOps.AppLogic;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class TeamsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITeamService _service;
    private readonly ITeamRepository _repository;

    public TeamsController(ITeamService teamService, ITeamRepository teamRepository, IMapper mapper)
    {
        _service = teamService;
        _repository = teamRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var teams = await _repository.GetAllAsync();
        List<TeamDetailModel> teamsList = new List<TeamDetailModel>();
        foreach (var team in teamsList)
        {
            teamsList.Add(_mapper.Map<TeamDetailModel>(team));
        }
        return Ok(teamsList);
    }

    [HttpPost("{id}/assemble")]
    public async Task<IActionResult> AssembleTeam(Guid id, TeamAssembleInputModel model)
    {
        var team = await _repository.GetByIdAsync(id);
        if (team == null)
        {
            return NotFound();
        }
        await _service.AssembleDevelopersAsyncFor(team, model.RequiredNumberOfDevelopers);
        return Ok();
    }
}