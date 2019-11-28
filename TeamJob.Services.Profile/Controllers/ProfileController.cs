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

        // GET api/profile/get/{id}
        [HttpGet("get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProfileDto>> GetProfile([FromRoute]Guid id, GetProfile InQuery)
        {
            var profile = await _queryDispatcher.QueryAsync(InQuery.Bind(x => x.Id, id));
            if (profile is null)
            {
                return NotFound();
            }

            return profile;
        }

        // GET api/profile/getall
        [HttpGet("getall")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProfileDto>>> GetAllProfiles([FromQuery]string role, GetProfiles InQuery)
        {
            var profile = await _queryDispatcher.QueryAsync(InQuery.Bind(x => x.Role, role));
            if (profile is null)
            {
                return NotFound();
            }

            return profile.ToList();
        }

        // DELETE api/profile/delete/{id}
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> DeleteProfile([FromRoute]Guid id, DeleteProfile InCommand)
        {
            await _commandDispatcher.SendAsync(InCommand.Bind(x => x.Id, id));

            return CreatedAtAction(nameof(DeleteProfile), InCommand.Id, new { id = InCommand.Id });
        }

        // POST api/profile/create
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateProfile([FromBody]CreateProfile InCommand)
        {
            await _commandDispatcher.SendAsync(InCommand);

            return CreatedAtAction(nameof(CreateProfile), InCommand.Id, new { id = InCommand.Id });
        }

        // POST api/profile/update
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> UpdateProfile([FromBody]UpdateProfile InCommand)
        {
            await _commandDispatcher.SendAsync(InCommand);

            return CreatedAtAction(nameof(UpdateProfile), InCommand.Id, new { id = InCommand.Id });
        }
    }
}
