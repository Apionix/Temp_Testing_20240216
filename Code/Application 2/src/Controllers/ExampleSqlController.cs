using Application2.DAL;
using Application2.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net.Mime;

namespace Application2.Controllers {

    /// <summary>
    /// Dummy controller with MySQL connections
    /// TODO: Remove if not needed
    /// </summary>
    [Route("example/sql")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ExampleSqlController : ControllerBase {

        /// <summary>
        /// User DAL used to retrieve the user entry
        /// </summary>
        private UserDAL _userDal { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="connection">Connection object</param>
        public ExampleSqlController(IDbConnection connection) =>
            _userDal = new UserDAL(connection);

        /// <summary>
        /// Find an entry with the corresponding id
        /// </summary>
        /// <param name="id">Entry id</param>
        /// <returns>User entry</returns>
        [HttpGet("{id}/basic", Name = "get-basic-user")]
        public ActionResult<UserM> GetBasicUserProfile([Required] uint id) {
            UserM entry = _userDal.GetUser(id);

            return entry != null && entry.Id == id
                ? entry
                : NotFound();
        }

        /// <summary>
        /// Create an entry in the database
        /// </summary>
        /// <param name="entry">Entry to create</param>
        /// <returns>Status code</returns>
        [HttpPost(Name = "create-user")]
        public ActionResult CreateUserProfile([Required][FromBody] UserM entry) {
            _userDal.CreateUser(entry);
            return Created("", null);
        }
    }
}
