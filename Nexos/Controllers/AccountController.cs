using System.Net.Mail;
using System.Security.Claims;
using Nexos.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nexos.Models;
using Nexos.Helpers;
using Nexos.Data;
namespace Nexos.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly UserManager<Usuario> _userManager;
    private readonly IWebHostEnvironment _host;
    private readonly AppDbContext _db;

    public AccountController(

    ILogger<AccountController> logger,
    SignInManager<Usuario> signInManager,
    UserManager<Usuario> userManager,
    IWebHostEnvironment host,
    AppDbContext db
    )
    {
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
        _host = host;
        _db = db;

    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        LoginVM login = new()
        {
            UrlRetorno = returnUrl ?? Url.Content("~/")
        };
        return View(login);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM login)
    {
        if (ModelState.IsValid)
        {
            string userName = login.Email;
            if (IsValidEmail(login.Email))
            {
                var user = await _userManager.FindByEmailAsync(login.Email);
                if (user != null)
                    userName = user.UserName;
            }

            var result = await _signInManager.PasswordSignInAsync(
                userName, login.Senha, login.Lembrar, lockoutOnFailure: true
            );

            if (result.Succeeded)
            {
                _logger.LogInformation($"Usuário {login.Email} acessou o sistema");
                return LocalRedirect(login.UrlRetorno);
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning($"Usuário {login.Email} está bloqueado");
                ModelState.AddModelError("", "Sua conta está bloqueada, aguarde alguns minutos e tente novamente!!");
            }
            else if (result.IsNotAllowed)
            {
                _logger.LogWarning($"Usuário {login.Email} não confirmou sua conta");
                ModelState.AddModelError(string.Empty, "Sua conta não está confirmada, verifique seu email!!");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Usuário e/ou Senha Inválidos!!!");
            }
        }

        return View(login);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        _logger.LogInformation($"Usuário {ClaimTypes.Email} fez logoff");
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register(string returnUrl)
    {
        RegistroVM register = new()
        {
            UrlRetorno = returnUrl ?? Url.Content("~/")

        };
        return View(register);
    }

    public bool IsValidEmail(string email)
    {
        try
        {
            MailAddress m = new(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegistroVM registro)
    {
        if (ModelState.IsValid)
        {
            var usuario = Activator.CreateInstance<Usuario>();
            usuario.Nome = registro.Nome;
            usuario.DataNascimento = registro.DataNascimento;
            usuario.UserName = registro.Email;
            usuario.NormalizedUserName = registro.Email.ToUpper();
            usuario.Email = registro.Email;
            usuario.NormalizedEmail = registro.Email.ToUpper();
            usuario.EmailConfirmed = true;
            var result = await _userManager.CreateAsync(usuario, registro.Senha);

            if (result.Succeeded)
            {
                _logger.LogInformation($"Novo usuário registrado com o email {registro.Email}.");

                await _userManager.AddToRoleAsync(usuario, "Usuário");

                TempData["Success"] = "Conta Criada com Sucesso!";
                return RedirectToAction(nameof(Login));
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, TranslateIdentityErrors.TranslateErrorMessage(error.Code));
        }

        return View(registro);
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public async Task<IActionResult> Perfil()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login");
        }

        var perfil = new PerfilVM
        {
            Nome = user.Nome,
            Email = user.Email,
            DataNascimento = user.DataNascimento,
            FotoAtual = user.Foto,
            UserName = user.UserName
        };

        return View(perfil);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public async Task<IActionResult> Perfil(PerfilVM perfil)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            // Atualizar dados do usuário
            user.Nome = perfil.Nome;
            user.Email = perfil.Email;
            user.NormalizedEmail = perfil.Email.ToUpper();
            user.UserName = perfil.Email;
            user.NormalizedUserName = perfil.Email.ToUpper();
            user.DataNascimento = perfil.DataNascimento;

            // Upload da foto de perfil
            if (perfil.Foto != null && perfil.Foto.Length > 0)
            {
                var uploadsFolder = Path.Combine(_host.WebRootPath, "img", "perfil");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Deletar foto antiga se existir
                if (!string.IsNullOrEmpty(user.Foto))
                {
                    var oldPhotoPath = Path.Combine(_host.WebRootPath, user.Foto.TrimStart('/'));
                    if (System.IO.File.Exists(oldPhotoPath))
                    {
                        System.IO.File.Delete(oldPhotoPath);
                    }
                }

                // Salvar nova foto
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + perfil.Foto.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await perfil.Foto.CopyToAsync(fileStream);
                }
                user.Foto = "/img/perfil/" + uniqueFileName;
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                _logger.LogInformation($"Usuário {user.Email} atualizou seu perfil");
                TempData["Success"] = "Perfil atualizado com sucesso!";
                return RedirectToAction(nameof(Perfil));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, TranslateIdentityErrors.TranslateErrorMessage(error.Code));
            }
        }

        return View(perfil);
    }

    [HttpGet]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public IActionResult AlterarSenha()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public async Task<IActionResult> AlterarSenha(AlterarSenhaVM model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.SenhaAtual, model.NovaSenha);

            if (result.Succeeded)
            {
                _logger.LogInformation($"Usuário {user.Email} alterou sua senha");
                await _signInManager.RefreshSignInAsync(user);
                TempData["Success"] = "Senha alterada com sucesso!";
                return RedirectToAction(nameof(Perfil));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, TranslateIdentityErrors.TranslateErrorMessage(error.Code));
            }
        }

        return View(model);
    }
}
