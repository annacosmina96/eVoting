using Business;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;



        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }


        public IActionResult ConfirmEmail(string email)
        {

            ViewBag.Email = email;
            return View();
        }  


        [HttpPost]
        public async Task<IActionResult> Login(AccountViewModel accountViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(accountViewModel);
            }

            var user = await _userManager.FindByEmailAsync(accountViewModel.LoginViewModel.Email);

            if (user != null)
            {

                var result = await _signInManager.PasswordSignInAsync(user, accountViewModel.LoginViewModel.Password, false, false);
                if (result.Succeeded)
                {

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Password"] = "Wrong password";
                    return View();
                }
            }
            if (user == null)
            {
                TempData["Email"] = "This email is not associated with an account.";
                return View();

            }

            ModelState.AddModelError("", " Error");

            return View(accountViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountViewModel accountViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isValid = Regex.IsMatch(accountViewModel.RegisterViewModel.CNP, @"^[1-9]\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])(0[1-9]|[1-4]\d|5[0-2]|99)(00[1-9]|0[1-9]\d|[1-9]\d\d)\d$");

                if (isValid == false)
                {
                    TempData["CNPInvalid"] = "CNP invalid";
                    return View();
                }

                if (_userManager.FindByEmailAsync(accountViewModel.RegisterViewModel.Email).Result != null)
                {
                    TempData["EmailExistent"] = "You already have an account";
                    return View();
                }

                var user = new User() { UserName= accountViewModel.RegisterViewModel.Email, CNP = accountViewModel.RegisterViewModel.CNP, Email= accountViewModel.RegisterViewModel.Email, EmailConfirmed=false };
                string password = GenerateRandomPassword.GeneratePassword();

                var result = await _userManager.CreateAsync(user, password);
                SendEmail(user,password);

                if (result.Succeeded)
                {
                    return RedirectToAction("ConfirmEmail", new { email = user.Email });
                }
               
                AddErrors(result); 

            }
            return View(accountViewModel);


        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public static void SendEmail(User user,string password)
        {

            string guid = user.Id;
            MailMessage email = new MailMessage("eVotingRomania@gmail.com",
                                                  user.Email);

            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("eVotingRomania@gmail.com", "123456?654321");

            email.Subject = "Welcome!";
            email.Body = "Your password is: " + password+". Activation link: https://localhost:44344/Account/Verify/" + guid;

            try
            {
                client.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IActionResult> Verify(string id)
        {
          
                 var user = await _userManager.FindByIdAsync(id);
                 if (user != null && user.EmailConfirmed == false)
                    {
                         user.EmailConfirmed = true;
                         await _userManager.UpdateAsync(user);
                

                     }

            return View("Login");
        }

     
    }
}
