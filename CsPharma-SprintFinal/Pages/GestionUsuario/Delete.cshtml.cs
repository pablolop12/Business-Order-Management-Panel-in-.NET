using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using CsPharma_V4.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using System.Data;
namespace CsPharma_V4.Pages.GestionUsuario
{

    [Authorize(Roles = "Administrador")]//Limita el acceso solo a usuairos con rol Administrador
    public class DeleteModel : PageModel
    {

        //Variable y constructor de inicialización del contexto (Con esto Accedemos a nuestra base de datos)
        private readonly LoginContexto _loginContexto;//Variable del contexto
        public DeleteModel(LoginContexto loginContexto)
        {
            _loginContexto = loginContexto;
        }

        [BindProperty]//Anotación que indica que User va a recibir información a traves del HTML
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _loginContexto.UserSet == null)
            {
                return NotFound();
            }

            var applicationUser = await _loginContexto.UserSet.FirstOrDefaultAsync(m => m.Id == id);

            if (applicationUser == null)
            {
                return NotFound();
            }
            else
            {
                User = applicationUser;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _loginContexto.UserSet == null)
            {
                return NotFound();
            }
            var user = await _loginContexto.UserSet.FindAsync(id);

            if (user != null)
            {
                User = user;
                _loginContexto.UserSet.Remove(User);
                await _loginContexto.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
