using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CsPharma_V4.Areas.Identity.Data;
using CsPharma_V4.Core.Repositories;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CsPharma_V4.Pages.GestionUsuario
{
    [Authorize(Roles = "Administrador")]//Limita el acceso solo a usuairos con rol Administrador
    public class IndexModel : PageModel
    {

        //Variable y constructor de inicialización del contexto (Con esto Accedemos a nuestra base de datos)
        private readonly LoginContexto _loginContexto;
        public IndexModel(LoginContexto loginContexto)
        {
            _loginContexto = loginContexto;
        }

        public IList<User> UserList { get; set; } = default!;
         
        //METODO GET DE NUESTRA LISTA
        public async Task OnGetAsync()
        {
            if (_loginContexto.UserSet != null)
            {
                UserList = await _loginContexto.UserSet.ToListAsync();
            }
        }
    }
}
