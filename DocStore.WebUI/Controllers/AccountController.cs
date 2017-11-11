using DocStore.Domain.Model;
using DocStore.WebUI.Infrastructure.Abstract;
using DocStore.WebUI.Models;
using System.Web.Mvc;
using System.Linq;
using Services.Manager;

namespace DocStore.WebUI.Controllers
{
    public class AccountController : Controller {

        private IAuthProvider authProvider { get; set; }
        private IUserManager userManager { get; set; }

        public AccountController(IAuthProvider authProvider, 
            IUserManager userManager) {
            this.authProvider = authProvider;
            this.userManager = userManager;
        }

        [HttpGet]
        public ViewResult Login() {
            return View();
        }

        [HttpGet]
        public ViewResult Register() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model) {

            if (!ModelState.IsValid) {
                return View(model);
            }

            var result = userManager.Check(model.Login, model.Password);

            if (result) {
                authProvider.Authenticate(model.Login);
                return RedirectToAction("Index", "Document");
            }

            ModelState.AddModelError("", "Неверный логин или пароль");
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model) {

            if (!ModelState.IsValid) {
                return View(model);
            }

            var user = userManager.Get(model.Login);
            if (user != null) {
                ModelState.AddModelError("", "Пользоваьтель с таким логином уже существует");
                return View(model);
            }

            var newUser = new User {

                Login = model.Login,
                Password = model.Password,

            };

            userManager.Save(newUser);

            authProvider.Authenticate(model.Login);
            return RedirectToAction("Index", "Document");
        }

        public ActionResult ExitToStore() {
            authProvider.Exit();
            return RedirectToAction("Login");
        }

    }
}