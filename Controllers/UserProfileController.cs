using Microsoft.AspNetCore.Mvc;
using Gifter.Repositories;
using Gifter.Models;

namespace Gifter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        // GET: api/UserProfile
        [HttpGet]
        public ActionResult GetAll()
        {
            var userProfiles = _userProfileRepository.GetAll();
            return Ok(userProfiles);
        }

        // GET api/UserProfile/5
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var userProfile = _userProfileRepository.GetById(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            return Ok(userProfile);
        }

        // GET api/UserProfile/5/posts
        [HttpGet("{id}/posts")]
        public ActionResult GetPostsByUserId(int id)
        {
            var userProfile = _userProfileRepository.GetByIdWithPosts(id);
            if (userProfile == null)
            {
                return NotFound();
            }

            if (userProfile.Posts == null)
            {
                return NotFound("No posts found for this user");
            }

            return Ok(userProfile.Posts);
        }


        // POST api/UserProfile
        [HttpPost]
        public ActionResult Create(UserProfile userProfile)
        {
            _userProfileRepository.Add(userProfile);
            return CreatedAtAction(nameof(GetById), new { id = userProfile.Id }, userProfile);
        }

        // PUT api/UserProfile/5
        [HttpPut("{id}")]
        public ActionResult Update(int id, UserProfile userProfile)
        {
            if (id != userProfile.Id)
            {
                return BadRequest();
            }

            _userProfileRepository.Update(userProfile);
            return NoContent();
        }

        // DELETE api/UserProfile/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _userProfileRepository.Delete(id);
            return NoContent();
        }
    }
}