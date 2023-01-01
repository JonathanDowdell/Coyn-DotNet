using Coyn.Token.Model;
using Coyn.Token.Service;
using Coyn.User.Model;
using Coyn.User.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coyn.User.Controller;

[ApiController]
[Route("user"), Authorize]
public class UserController: Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public UserController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpGet("hw")]
    public ActionResult<String> HelloWorld()
    {
        return Ok("Hello World");
    }

    /// <summary> The GetAllUsers function returns a list of all users in the database.</summary>
    ///
    ///
    /// <returns> An ienumerable of userresponse objects.</returns>
    [HttpGet, Authorize]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers()
    {
        var userEntities = await _userService.GetAllUsersAsync();
        return Ok(userEntities.Select(user => new UserResponse(user)));
    }

    /// <summary> The SignUpUser function is used to create a new user in the database.
    /// It takes a SignUpUserRequest object as an argument and returns a TokenResponse object.</summary>
    ///
    /// <param name="signUpUserRequest"> The user's email and password</param>
    ///
    /// <returns> A tokenresponse object. this is the response that will be sent to the client.</returns>
    [HttpPost("signup"), AllowAnonymous]
    public async Task<ActionResult<TokenResponse>> SignUpUser(SignUpUserRequest signUpUserRequest)
    {
        var userResponse = await _userService.SignUpUserAsync(signUpUserRequest);
        var tokenResponse = await _tokenService.CreateServerToken(userResponse);
        return Created("/signup", tokenResponse);
    }
    

    /// <summary> The SignInUser function is used to sign in a user. It takes the SignInUserRequest object and returns a UserResponse object.</summary>
    ///
    /// <param name="signInUserRequest"> The user's email and password</param>
    ///
    /// <returns> A userresponse object.</returns>
    [HttpPost("signin"), AllowAnonymous]
    public async Task<ActionResult<TokenResponse>> SignInUser(SignInUserRequest signInUserRequest)
    {
        var userResponse = await _userService.SignInUserAsync(signInUserRequest);
        var tokenResponse = await _tokenService.CreateServerToken(userResponse);
        return Ok(tokenResponse);
    }
}