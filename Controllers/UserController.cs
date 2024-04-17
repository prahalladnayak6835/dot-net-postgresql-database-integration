using Microsoft.AspNetCore.Mvc;
using Models; // Assuming this namespace contains the User model
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Data;

namespace UserCrudApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly ILogger<UsersController> Logger;

        public UsersController(ILogger<UsersController> logger, UserContext context)
        {
            _context = context;
            Logger = logger;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                // Log information about the operation
                Logger.LogInformation("Fetching all users...");
                // Return all users from the database
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log an error if an exception occurs
                Logger.LogError($"An error occurred while fetching users: {ex.Message}");
                // Return a 500 Internal Server Error response with a generic error message
                return StatusCode(500, "An error occurred while fetching users.");
            }
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                // Log information about the operation
                Logger.LogInformation($"Fetching user with ID: {id}");
                // Find and return the user with the specified ID
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    // Log a message if the user is not found
                    Logger.LogInformation($"User with ID: {id} not found.");
                    // Return a 404 Not Found response
                    return NotFound();
                }

                // Log a message if the user is found
                Logger.LogInformation($"User with ID: {id} found.");
                // Return the user
                return user;
            }
            catch (Exception ex)
            {
                // Log an error if an exception occurs
                Logger.LogError($"An error occurred while fetching user with ID {id}: {ex.Message}");
                // Return a 500 Internal Server Error response with a generic error message
                return StatusCode(500, $"An error occurred while fetching user with ID {id}.");
            }
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                // Add the new user to the database
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Log a message indicating success
                Logger.LogInformation("User has been created.");
                // Return a 201 Created response with the created user
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                // Log an error if an exception occurs
                Logger.LogError($"An error occurred while creating user: {ex.Message}");
                // Return a 500 Internal Server Error response with a generic error message
                return StatusCode(500, "An error occurred while creating user.");
            }
        }

        // PUT api/users/2
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] User user)
        {
            try
            {
                // Find the existing user in the database
                var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);
                if (existingUser != null)
                {
                    // Update the existing user's name and email
                    existingUser.Name = user.Name;
                    existingUser.Email = user.Email;

                    // Save the changes to the database
                    _context.SaveChanges();
                    // Log a message indicating success
                    Logger.LogInformation("User updated: {UserId}, Name: {UserName}, Email: {UserEmail}", id, user.Name, user.Email);
                }
                // Return the existing user
                return existingUser;
            }
            catch (Exception ex)
            {
                // Log an error if an exception occurs
                Logger.LogError($"An error occurred while updating user with ID {id}: {ex.Message}");
                // Return a 500 Internal Server Error response with a generic error message
                return StatusCode(500, $"An error occurred while updating user with ID {id}.");
            }
        }

        // DELETE api/users/2
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                // Find the user in the database
                var user = _context.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    // Remove the user from the database
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                    // Log a message indicating success
                    Logger.LogInformation("User has been deleted.");
                }
                // Return a success message
                return "User has been deleted.";
            }
            catch (Exception ex)
            {
                // Log an error if an exception occurs
                Logger.LogError($"An error occurred while deleting user with ID {id}: {ex.Message}");
                // Return a 500 Internal Server Error response with a generic error message
                return StatusCode(500, $"An error occurred while deleting user with ID {id}.");
            }
        }

        // Dummy endpoint to test the endpoint url
        [HttpGet("test")]
        public string Test()
        {
            return "Hello World!";
        }
    }
}
