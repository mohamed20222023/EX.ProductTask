using Application.Dtos.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TaskERP.Controllers
{
	public class AccountController : Controller
	{
		public UserManager<IdentityUser> UserManager { get; }
		public SignInManager<IdentityUser> SignInManager { get; }

		public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{
			UserManager = userManager;
			SignInManager = signInManager;
		}

		#region Sign Up

		public IActionResult Register()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Register(UserRegisterDto model)
		{
			if (ModelState.IsValid)
			{
				// Check if email already exists
				if (await UserManager.FindByEmailAsync(model.Email) is not null)
				{
					ModelState.AddModelError(string.Empty, "Email already exists");
					return View(model);
				}

				// Check if username already exists
				if (await UserManager.FindByNameAsync(model.UserName) is not null)
				{
					ModelState.AddModelError(string.Empty, "Username already exists");
					return View(model);
				}

				var user = new IdentityUser
				{
					UserName = model.UserName,
					Email = model.Email,
				};

				try
				{
					var result = await UserManager.CreateAsync(user, model.Password);
					if (result.Succeeded)
					{
						await UserManager.AddToRoleAsync(user, "User");
						return RedirectToAction(nameof(Login));
					}
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, "Error creating user account");
					// Log the exception or handle it in some other way
				}
			}

			return View(model);
		}
		#endregion

		#region Sign In

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(UserLoginDto model)
		{
			if (ModelState.IsValid)
			{
				var user = await UserManager.FindByEmailAsync(model.Email);
				if (user != null )
				{
					var passwordIsValid = await UserManager.CheckPasswordAsync(user, model.Password);
					if (passwordIsValid)
					{
						await SignInManager.SignInAsync(user, true);
						var role = await UserManager.GetRolesAsync(user);
						foreach (var item in role)
						{
							if (item == "Admin")
								return RedirectToAction("AdminIndex", "Home");
						}
						return RedirectToAction("Index", "Home");
					}
					
				}
				ModelState.AddModelError(string.Empty, "Invalid Email or Password");
			}
			return View(model);
		}
		#endregion

		#region Sign Out

		public async new Task<IActionResult> SignOut()
		{
			await SignInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}

		#endregion
	}
}
