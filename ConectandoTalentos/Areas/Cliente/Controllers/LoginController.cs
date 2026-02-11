using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using ConectandoTalentosSolucion.Models;
using ConectandoTalentosSolucion.Models.ViewModels.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConectandoTalentos.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    [AllowAnonymous]
    public class LoginController : Controller

    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public LoginController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registro() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegistroVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var newUser = new ApplicationUser
            {
                UserName = vm.Email,
                Email = vm.Email,
                Nombre1 = vm.Nombre1,
                Nombre2 = vm.Nombre2,
                Apellido1 = vm.Apellido1,
                Apellido2 = vm.Apellido2,
                Direccion = vm.Direccion,
                Ciudad = vm.Ciudad,
                NumIdentificacion = vm.NumeroDocumento
            };

            var resultado = await _userManager.CreateAsync(newUser, vm.Password);
            if (resultado.Succeeded)
            {
                var agregarRol = await _userManager.AddToRoleAsync(newUser,"Cliente");
                if (agregarRol.Succeeded)
                {
                    TempData["Success"] = "Usuario creado exitosamente, intenta iniciar sesión.";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Failed"] = "ROL - Ups ocurrio un error en el proceso, intentalo nuevamente";
                return View(vm);
            }
            TempData["Failed"] = "Ups ocurrio un error en el proceso, intentalo nuevamente";
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Acceder(AccesoVM vm) 
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var loginUser = await _signInManager.PasswordSignInAsync(
                vm.Email,
                vm.Password,
                isPersistent: false,
                lockoutOnFailure: false
                );

            if (loginUser.Succeeded)
            {
                return RedirectToAction("Index", "Home", new { area = "Cliente" });
            }

            if (loginUser.IsLockedOut)
            {
                TempData["Failed"] = "Tu cuenta esta bloqueada.";
                return View(vm);
            }
            TempData["Failed"] = "Correo o contraseña incorrectos, intentalo nuevamente.";
            return View(vm);
        }
    }
}
