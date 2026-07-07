using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OilfieldManager.Domain.Entities;
using OilfieldManager.Infrastructure.Data;

namespace OilfieldManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetsController : ControllerBase
{
    private readonly OilfieldDbContext _context;

    public AssetsController(OilfieldDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
    {
        return await _context.Assets.Include(a => a.CurrentWell).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Asset>> GetAsset(Guid id)
    {
        var asset = await _context.Assets.Include(a => a.CurrentWell).FirstOrDefaultAsync(a => a.Id == id);

        if (asset == null)
        {
            return NotFound();
        }

        return asset;
    }

    [HttpPost]
    public async Task<ActionResult<Asset>> CreateAsset(Asset asset)
    {
        _context.Assets.Add(asset);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAsset), new { id = asset.Id }, asset);
    }
}