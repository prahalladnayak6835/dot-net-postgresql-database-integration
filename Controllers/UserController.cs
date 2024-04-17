using Microsoft.AspNetCore.Mvc;
using Models; // Assuming this namespace contains the User model
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Data;

namespace UserCrudApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly ILogger<UsersController> Logger;

        // Constructor to initialize the controller with required dependencies
        public UsersController(ILogger<UsersController> logger, UserContext context)
        {
            _context = context;
            Logger = logger;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            // Returns all users from the database
            Logger.LogInformation("Fetching all users...");
            return await _context.Users.ToListAsync();
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            // Returns the user with the specified ID
            Logger.LogInformation($"Fetching user with ID: {id}");
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                Logger.LogInformation($"User with ID: {id} not found.");
                return NotFound();
            }

            Logger.LogInformation($"User with ID: {id} found.");
            return user;
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            // Adds a new user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            Logger.LogInformation("User has been created.");
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT api/users/2
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] User user)
        {
            // Updates an existing user's name and email
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;

                _context.SaveChanges();
                Logger.LogInformation("User updated: {UserId}, Name: {UserName}, Email: {UserEmail}", id, user.Name, user.Email);
            }
            return existingUser;
        }

        // DELETE api/users/2
        [HttpDelete("{id}")]
        public String Delete(int id)
        {
            // Deletes a user with the specified ID
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                Logger.LogInformation("User has been deleted.");
            }
            return "User has been deleted.";
        }

        // Dummy endpoint to test the endpoint url
        [HttpGet("test")]
        public string Test()
        {
            return "Hello World!";
        }
    }
}
