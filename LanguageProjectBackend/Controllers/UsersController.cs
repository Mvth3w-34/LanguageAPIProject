using LanguageProjectBackend.Data;
using LanguageProjectBackend.Dtos;
using LanguageProjectBackend.Models;
using LanguageProjectBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanguageProjectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _userRepository;

        public UsersController(IUserRepo repository)
        {
            _userRepository = repository;
        }


        [HttpPost]
        public ActionResult<string> RegisterUser([FromBody] UserDto userDto)
        {
            EmailSender emailSender = new EmailSender();
            //Maps the user data to the internal data user model
            User userModel = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                LanguagePreference = userDto.LanguagePreference,
                EmailFrequency = userDto.EmailFrequency

            };

            //Save data to DB
            _userRepository.CreateUser(userModel);
            _userRepository.SaveChanges();

            //Can also add confirmation functionality using sendgrid.
            emailSender.SendConfirmation(userModel.Email);
            return Ok("Thank you for subscribing!");
        }


    }
}
