using Microsoft.AspNetCore.Mvc;
using REST.Models;
using REST.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DCandidatesController : ControllerBase
    {
        private readonly DCandidateRepository _repo;

        public DCandidatesController(DCandidateRepository repo)
        {
            _repo = repo;
        }

        // GET: api/DCandidates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DCandidate>>> GetCandidates()
        {
            return await _repo.GetAllAsync();
        }

        // GET: api/DCandidates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DCandidate>> GetCandidate(int id)
        {
            var candidate = await _repo.GetByIdAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }
            return candidate;
        }

        // POST: api/DCandidates
        [HttpPost]
        public async Task<ActionResult<DCandidate>> PostCandidate(DCandidate candidate)
        {
            await _repo.AddAsync(candidate);
            return CreatedAtAction(nameof(GetCandidate), new { id = candidate.id }, candidate);
        }

        // PUT: api/DCandidates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidate(int id, DCandidate candidate)
        {
            if (id != candidate.id)
            {
                return BadRequest();
            }

            await _repo.UpdateAsync(id, candidate);

            return NoContent();
        }

        // DELETE: api/DCandidates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }

}
