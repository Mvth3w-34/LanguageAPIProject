using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LanguageProjectBackend.Data;
using LanguageProjectBackend.Models;
using LanguageProjectBackend.Dtos;

namespace LanguageProjectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _userRepository;

        public UsersController(IUserRepo repository)
        {
            _userRepository= repository;
        }

        [HttpPost]

        public ActionResult<string> RegisterUser(UserDto userDto) {

            //Maps the user data to the internal data user model
            User usermodel = new User { 
             FirstName = userDto.FirstName,
             LastName = userDto.LastName,
             Email = userDto.Email,
             LanguagePreference =userDto.LanguagePreference,
             EmailFrequency = userDto.EmailFrequency
            
            };

            //Save data to DB

            //Can also add confirmation functionality using sendgrid.

            return Ok("Thank you for subscribing!");
        }


    }
}
