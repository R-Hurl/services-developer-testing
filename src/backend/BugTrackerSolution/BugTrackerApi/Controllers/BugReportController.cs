using BugTrackerApi.Models;
using BugTrackerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BugTrackerApi.Controllers;

public class BugReportController : ControllerBase
{
    private readonly ISystemTime _systemTime;

    public BugReportController(ISystemTime systemTime)
    {
        _systemTime = systemTime;
    }

    [Authorize]
    [HttpPost("/catalog/excel/bugs")]
    public async Task<ActionResult> AddABugReport([FromBody] BugReportCreateRequest request)
    {
        {
            var name = User.GetName();
            var response = new BugReportCreateResponse
            {
                Id = "excel-goes-boom",
                Issue = request,
                Software = "Excel",
                Status = IssueStatus.InTriage,
                User = name,
                Created = _systemTime.GetCurrent()
            };
            return StatusCode(201, response);
        }
    }
}

public static class ControllerAuthExtensions
{
    public static string GetName(this ClaimsPrincipal claims)
    {
        return claims?.Identity?.Name ?? throw new ApplicationException("Something is really wrong with Auth");
    }
}