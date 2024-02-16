using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace Application2.Controllers {


    /// <summary>
    /// Dummy controller
    /// </summary>
    [Route("example")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ExampleController : ControllerBase {

        /// <summary>
        /// Instance data for dummy secret
        /// </summary>
        private string? _topSecretString { get; }

        /// <summary>
        /// Default constructor with injected secret
        /// </summary>
        /// <param name="secret">injected secret</param>
        public ExampleController(Secret secret) => _topSecretString = secret?.Data;

        /// <summary>
        /// This is a dummy method using secret data.
        /// </summary>
        /// <param name="firstName" example="Bob">User first name</param>
        /// <param name="lastName" example="Lazar">User last name</param>
        /// <returns>Secret data</returns>
        [HttpGet("endpoint", Name = "get-secret")]
        public ActionResult<string> TopSecret([Required] string firstName, string lastName = "") {
            string combined = string.IsNullOrWhiteSpace(lastName) ?
              $"{firstName}" : $"{firstName} {lastName}";
            return $"Hello {combined}, please keep {_topSecretString} a secret.";
        }
    }
}
