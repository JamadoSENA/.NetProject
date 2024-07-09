using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedistockApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace MedistockApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly MedistockContext _context;

        public UsersController(MedistockContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var medistockContext = _context.Users.Include(u => u.FkIdRoleNavigation);
            return View(await medistockContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.FkIdRoleNavigation)
                .FirstOrDefaultAsync(m => m.IdUser == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["FkIdRole"] = new SelectList(_context.Roles, "IdRole", "IdRole");
            return View();
        }
        // Lógica de registro
        public IActionResult OnPostRegister(int idUser, string documentType, string name, string lastname, string birthdate, 
                                            int age, string gender, string phoneNumber, string profession, string address, 
                                            string email, string password, int fkIdRole)
        {
            // Validar y guardar el nuevo usuario en la base de datos
            if (ModelState.IsValid)
            {
                var usuario = new User
                {
                    IdUser = idUser,
                    DocumentType = documentType,
                    Name = name,
                    Lastname = lastname,
                    Birthdate = birthdate,
                    Age = age,
                    Gender = gender,
                    PhoneNumber = phoneNumber,
                    Profession = profession,
                    Address = address,
                    Email = email,
                    Password = password,
                    FkIdRole = fkIdRole
                };
                // Guardar el usuario en la base de datos utilizando el almacenamiento correspondiente
                return RedirectToPage("/Index");
            }
            return View();
        }

        // Lógica de inicio de sesión
        public IActionResult OnPostLogin(string email, string password)
        {
            // Validar las credenciales del usuario y autenticar utilizando las funciones de autenticación de .NET 8
            if (ModelState.IsValid)
            {
                var user = email;// Obtener el usuario correspondiente al correo electrónico proporcionado
        if (user != null && user.Password == password)
                {
                    // Autenticar al usuario utilizando las funciones de autenticación de .NET 8
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.name),
                // Agrega otros claims si es necesario
            };
                    var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                    var authProperties = new AuthenticationProperties
                    {
                        // Agrega propiedades de autenticación si es necesario
                    };
                    await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToPage("/Index");
                }
            }
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUser,DocumentType,Name,Lastname,Birthdate,Age,Gender,PhoneNumber,Profession,Address,Email,Password,FkIdRole")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkIdRole"] = new SelectList(_context.Roles, "IdRole", "IdRole", user.FkIdRole);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["FkIdRole"] = new SelectList(_context.Roles, "IdRole", "IdRole", user.FkIdRole);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUser,DocumentType,Name,Lastname,Birthdate,Age,Gender,PhoneNumber,Profession,Address,Email,Password,FkIdRole")] User user)
        {
            if (id != user.IdUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.IdUser))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkIdRole"] = new SelectList(_context.Roles, "IdRole", "IdRole", user.FkIdRole);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.FkIdRoleNavigation)
                .FirstOrDefaultAsync(m => m.IdUser == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Lógica de cierre de sesión
        public async Task<IActionResult> OnPostLogout()
        {
            // Cerrar la sesión del usuario utilizando las funciones de autenticación de .NET 8
            await HttpContext.SignOutAsync("CookieAuthentication");
            return RedirectToPage("/Index");
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.IdUser == id);
        }
    }
}
