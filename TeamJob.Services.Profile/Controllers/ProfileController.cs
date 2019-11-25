using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Convey.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamJob.Services.Profile.Commands;
using TeamJob.Services.Profile.DTO;
using TeamJob.Services.Profile.Queries;

namespace TeamJob.Services.Profile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher   _queryDispatcher;

        public ProfileController(ICommandDispatcher InCommandDispatcher,
                                 IQueryDispatcher   InQueryDispatcher)
        {
            _commandDispatcher = InCommandDispatcher;
            _queryDispatcher   = InQueryDispatcher;
        }

        // GET api/profile/
        [HttpGet("{profileId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProfileDto>> GetProfile([FromRoute]Guid profileId, GetProfile InQuery)
        {
            var profile = await _queryDispatcher.QueryAsync(InQuery.Bind(x => x.ProfileId, profileId));
            if (profile is null)
            {
                return NotFound();
            }

            return profile;
        }

        // GET api/profile/all
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProfileDto>>> GetAllProfiles([FromBody] GetProfiles InQuery)
        {
            var profile = await _queryDispatcher.QueryAsync(InQuery);
            if (profile is null)
            {
                return NotFound();
            }

            return profile.ToList();
        }

        // DELETE api/profile/
        [HttpDelete("{profileId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> DeleteProfile([FromRoute]Guid profileId, DeleteProfile InCommand)
        {
            await _commandDispatcher.SendAsync(InCommand.Bind(x => x.ProfileId, profileId));

            return CreatedAtAction(nameof(DeleteProfile), InCommand.ProfileId, new { profileId = InCommand.ProfileId });
        }

        // POST api/profile/create
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateProfile([FromBody]CreateProfile InCommand)
        {
            await _commandDispatcher.SendAsync(InCommand);

            return CreatedAtAction(nameof(CreateProfile), InCommand.ProfileId, new { profileId = InCommand.ProfileId });
        }

        // POST api/profile/
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> UpdateProfile([FromBody]UpdateProfile InCommand)
        {
            await _commandDispatcher.SendAsync(InCommand);

            return CreatedAtAction(nameof(UpdateProfile), InCommand.ProfileId, new { profileId = InCommand.ProfileId });
        }
    }
}
