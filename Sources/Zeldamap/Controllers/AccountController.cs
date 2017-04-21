using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zeldassistant.Models;
using zeldassistant.ViewModel;
using Microsoft.Extensions.Logging;
using zeldassistant.Data;
using System.Security.Claims;
using zeldassistant;
using PaulMiami.AspNetCore.Mvc.Recaptcha;

namespace zeldassistant.web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private UserService _userService;
        private MarkerService _markerService;

        public AccountController(ILogger<AccountController> logger, ZeldaDataContext dbcontext)
        {
            this._logger = logger;
            this._userService = new UserService(dbcontext);
            this._markerService = new MarkerService(dbcontext);

        }

        public IActionResult Login(string returnUrl)
        {
            VmLogin model = new VmLogin();
            model.ReturnUrl = returnUrl;
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync(MagicConstants.CookieScheme);

            return new RedirectResult("/");
        }


        private async Task<bool> setAuthenticationAsync(User user)
        {
            List<Claim> claims;


            claims = new List<Claim>
                    {
                        new Claim("sub", "1"),
                        new Claim("name", user.Login),
                        new Claim("id",user.Id.ToString())

                    };
            var id = new ClaimsIdentity(claims, "local", "name", "role");
            await HttpContext.Authentication.SignInAsync(MagicConstants.CookieScheme, new ClaimsPrincipal(id));
            return true;
        }


        [HttpPost]
        public async Task<IActionResult> Login(VmLogin loginInfo)
        {
            if (loginInfo != null && !String.IsNullOrEmpty(loginInfo.Login) && !String.IsNullOrEmpty(loginInfo.Password))
            {
                // _logger.LogInformation(loginInfo.Login);
                User u = _userService.Validate(loginInfo.Login, loginInfo.Password);
                if (u != null)
                {
                    await setAuthenticationAsync(u);

                    string redirectUrl = String.IsNullOrEmpty(loginInfo.ReturnUrl) ? "/" : loginInfo.ReturnUrl;
                    return new RedirectResult(redirectUrl);

                }
                return View(new VmLogin() { Login = loginInfo.Login });
            }
            VmLogin model = new VmLogin();
      
            return View(model); 
         
        }

        public IActionResult Register()
        {
            VmLogin model = new VmLogin();
            model.ReturnUrl = "/";
            return View(model);
        }

        [ValidateRecaptcha]
        //[ReCaptcha]
        [HttpPost]
        public async Task<IActionResult> Register(VmLogin loginInfo)
        {


            if (String.IsNullOrEmpty(loginInfo.Login) || string.IsNullOrEmpty(loginInfo.Password))
            {
                ModelState.AddModelError("login", "Login or Password cannot be empty ! ");
                
            }
            if (ModelState.IsValid)
            {



                User u = new Models.User();
                u.Login = loginInfo.Login;
                u.PasswordHash = loginInfo.Password;

                var userTask = _userService.CreateNewUser(u, _markerService);

                userTask.Wait();

                if (u != null)
                {
                    await setAuthenticationAsync(u);

                    return new RedirectResult("/");

                }
                else
                {
                    ModelState.AddModelError("RegistrationFailed","Registration failed, perhaps the login already exists (?)");
                }
            }
           
                
               
            return View(new VmLogin() { Login = loginInfo.Login });
        }

    }
}