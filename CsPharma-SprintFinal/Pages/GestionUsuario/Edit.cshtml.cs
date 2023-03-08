using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CsPharma_V4.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using CsPharma_V4.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using CsPharma_V4.Models;

namespace CsPharma_V4.Pages.GestionUsuario
{
    [Authorize(Roles = "Administrador")]//Limita el acceso solo a usuairos con rol Administrador
    public class EditModel : PageModel
    {
        private readonly IUnionRepository _unionRolUser;
        private readonly SignInManager<User> _signInManager;

        public EditModel(IUnionRepository unionRolUser, SignInManager<User> signInManager)
        {
            _unionRolUser = unionRolUser;
            _signInManager = signInManager;
        }

        [BindProperty]//Anotación que indica que TdcTchEstadoPedido va a recibir información a traves del HTML
        public UserModel UserModel { get; set; } = default!;


        //METODO GET DE LA VISTA
        //este código es utilizado para recuperar la información de un USUARIO y sus ROLES,
        //crear una lista de elementos de selección de roles y pasarlos a una página.
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var user = _unionRolUser.User.GetUser(id);
            var roles = _unionRolUser.Role.GetRoles();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);//Recupera la informacion del usuario

            var roleItems = roles.Select(role =>
                new SelectListItem(
                    role.Name,
                    role.Id,
                    userRoles.Any(ur => ur.Contains(role.Name))
                )
            ).ToList();

            var userModel = new UserModel
            {
                User = user,
                Roles = roleItems
            };

            UserModel = userModel;

            return Page();
        }


        //METODO POST DE LA VISTA\
        //ste código se utiliza para actualizar la información de un usuario en la
        //base de datos y sus roles asignados a partir de un objeto "UserModel"
        public async Task<IActionResult> OnPostAsync(UserModel UserModel)
        {
            var user = _unionRolUser.User.GetUser(UserModel.User.Id);//Obtiene el Usuario en cuestión

            if (user == null)
            {
                return NotFound();
            }

            var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);//Obtiene los roles del Usuario

            var rolesToAdd = new List<string>();//Lista que almacena los roles que se van a agregar del user
            var rolesToRemove = new List<string>();//Lista que almacena los roles que se van a eliminar del user

            foreach (var role in UserModel.Roles)
            {
                var assignedInDb = userRolesInDb.FirstOrDefault(ur => ur == role.Text);

                if (role.Selected)
                {
                    if (assignedInDb == null)
                    {
                        rolesToAdd.Add(role.Text);//Si se selecciona la casilla se añáde el rol a la lista de roles
                                                  //a agregar
                    }
                }
                else
                {
                    if (assignedInDb != null)
                    {
                        rolesToRemove.Add(role.Text);//Si se deselecciona la casilla se añade el rol a la lista de roles
                                                     //a eliminar
                    }
                }
            }

            if (rolesToAdd.Any())
            {
                await _signInManager.UserManager.AddToRolesAsync(user, rolesToAdd); //Añade los roles de la lista 
            }
            if (rolesToRemove.Any())
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, rolesToRemove);//Elimina los roles de la lista
            }

            user.UserName = UserModel.User.UserName;
            user.Email = UserModel.User.Email;
            user.PhoneNumber = UserModel.User.PhoneNumber;

            _unionRolUser.User.UpdateUser(user);//Actualizacion de la parte de campos de usuario

            return RedirectToPage("./Index");
        }
    }
}
