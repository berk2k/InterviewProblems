using Microsoft.AspNetCore.Mvc;

namespace UserApi
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class UserRepository
    {
        private List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com", BirthDate = new DateTime(1990, 1, 1) },
            new User { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", BirthDate = new DateTime(1985, 5, 15) }
        };

        public List<User> GetAllUsers(){ 
            
            return _users; 
        }

        public User AddUser(User user)
        {
            try
            {
                user.Id = _users.Max(x => x.Id) + 1;
                _users.Add(user);
                return user;

            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return null;
            }


        }

        public User GetById(int id)
        {
            try
            {
                var user = _users.FirstOrDefault(x => x.Id == id);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {id} not found.");
                }

                return user;
            }
            catch (KeyNotFoundException knfEx)
            {
                
                Console.WriteLine(knfEx.Message);
                return null; 
            }


        }
        public User UpdateUser(int id, User user) { 
            var currentUser = GetById(id);

            if (currentUser != null)
            {
                currentUser.Name = user.Name;
                currentUser.Email = user.Email;
                currentUser.BirthDate = user.BirthDate;
                return currentUser;
            }
            else
            {
                Console.WriteLine("error");
                return null;
            }
        }

        public void DeleteUser(int id) {
        
            var user = GetById(id);
            if (user != null) {
                _users.Remove(user);
            }
        }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _users;

        public UserController(UserRepository users)
        {
            _users = users;
        }

        [HttpPost]
        public ActionResult<User> Create([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User data is null"); 
                }

                var createdUser = _users.AddUser(user);

                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");  
            }

        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            try
            {
                var user = _users.GetById(id);
                if (user == null)
                {
                    return BadRequest("User data is null");
                }

                return Ok(user);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                var users = _users.GetAllUsers();
                return Ok(users);  
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                _users.DeleteUser(id);
                

                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User data is null");  // 400 Bad Request
                }

                var updatedUser = _users.UpdateUser(id, user);
                if (updatedUser == null)
                {
                    return NotFound($"User with ID {id} not found.");  // 404 Not Found
                }

                return Ok(updatedUser);  // 200 OK
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "Internal server error");  // 500 Internal Server Error
            }
        }



    }
}   
