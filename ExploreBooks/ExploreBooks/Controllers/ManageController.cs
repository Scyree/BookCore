using System;
using System.Threading.Tasks;
using Business.Interfaces;
using Domain.Data;
using ExploreBooks.Models.ManageViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExploreBooks.Controllers
{
    [Authorize]
    [Route("manage/[action]")]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IApplicationUserServices _service;
        private readonly IApplicationPictureLogic _pictureLogic;

        public ManageController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<ManageController> logger, IApplicationUserServices service, IApplicationPictureLogic pictureLogic)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _service = service;
            _pictureLogic = pictureLogic;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("SomethingWentWrong", "Errors");
            }
            
            var model = new IndexViewModel
            {
                Username = user.User,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = user.Description,
                Country = user.Country
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("SomethingWentWrong", "Errors");
            }

            var firstName = user.FirstName;
            if (model.FirstName != firstName)
            {
                user.FirstName = model.FirstName;
            }

            var lastName = user.LastName;
            if (model.LastName != lastName)
            {
                user.LastName = model.LastName;
            }

            var country = user.Country;
            if (model.Country != country)
            {
                user.Country = model.Country;
            }

            var description = user.Description;
            if (model.Description != description)
            {
                user.Description = model.Description;
            }
            
            await _userManager.UpdateAsync(user);

            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet, ActionName("changePicture")]
        public async Task<IActionResult> ChangePicture()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("SomethingWentWrong", "Errors");
            }
            
            var model = new ChangePictureViewModel()
            {
                Folder = user.Folder,
                ImageName = user.ImageName,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost, ActionName("changePicture")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePicture(ChangePictureViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("SomethingWentWrong", "Errors");
            }

            if (model.Image != null)
            {
                var path = user.Folder;
                user.ImageName = model.Image.FileName;

                await _pictureLogic.UpdatePicture(path, model.Image);
            }
            
            await _userManager.UpdateAsync(user);

            _logger.LogInformation("User changed his profile picture successfully.");
            StatusMessage = "Your profile picture has been changed.";

            return RedirectToAction(nameof(ChangePicture));
        }

        [HttpPost, ActionName("sendVerificationEmail")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerificationEmail(IndexViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("SomethingWentWrong", "Errors");
            }
            
            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet, ActionName("changePassword")]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("SomethingWentWrong", "Errors");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToAction(nameof(SetPassword));
            }

            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost, ActionName("changePassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("SomethingWentWrong", "Errors");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            return RedirectToAction(nameof(ChangePassword));
        }

        [HttpGet, ActionName("setPassword")]
        public async Task<IActionResult> SetPassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("SomethingWentWrong", "Errors");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToAction(nameof(ChangePassword));
            }

            var model = new SetPasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost, ActionName("setPassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("SomethingWentWrong", "Errors");
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                AddErrors(addPasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "Your password has been set.";

            return RedirectToAction(nameof(SetPassword));
        }
        
        [HttpGet, ActionName("deleteProfile")]
        public async Task<IActionResult> DeleteProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("SomethingWentWrong", "Errors");
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost, ActionName("deleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            _service.DeleteApplicationUser(user);

            await _signInManager.SignOutAsync();
            await _userManager.DeleteAsync(user);

            return RedirectToAction("RedirectAfterDelete");
        }

        [AllowAnonymous]
        public IActionResult RedirectAfterDelete()
        {
            return View("DeletePages/RedirectAfterDelete");
        }

        #region Helpers
        
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        
        #endregion
    }
}
