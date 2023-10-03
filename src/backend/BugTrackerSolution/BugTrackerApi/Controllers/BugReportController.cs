using BugTrackerApi.Models;
using BugTrackerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BugTrackerApi.Controllers;

public class BugReportController : ControllerBase
{
    private readonly BugReportManager _bugReportManager;

    public BugReportController(BugReportManager bugReportManager)
    {
        _bugReportManager = bugReportManager;
    }

    [Authorize]
    [HttpPost("/catalog/{software}/bugs")]
    public async Task<ActionResult<BugReportCreateResponse>> AddABugReport([FromBody] BugReportCreateRequest request, [FromRoute] string software)
    {
        var slugGenerator = new SlugUtils.SlugGenerator();
        var user = User.GetName();
        var response = await _bugReportManager.CreateBugReportAsync(user, software, request);

        return response.Match<ActionResult>(
            report => StatusCode(201, report),
            _ => NotFound("That software is not supported")
        );
    }
}

public static class ControllerAuthExtensions
{
    public static string GetName(this ClaimsPrincipal claims)
    {
        return claims?.Identity?.Name ?? throw new ApplicationException("Something is really wrong with Auth");
    }
}