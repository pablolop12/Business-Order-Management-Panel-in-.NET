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
        private readonly IUnionRepository _unitOfWork;
        private readonly SignInManager<User> _signInManager;

        public EditModel(IUnionRepository unitOfWork, SignInManager<User> signInManager)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }

        [BindProperty]
        public UserModel UserModel { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(string id)
        {
            var user = _unitOfWork.User.GetUser(id);
            var roles = _unitOfWork.Role.GetRoles();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

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


        public async Task<IActionResult> OnPostAsync(UserModel UserModel)
        {
            var user = _unitOfWork.User.GetUser(UserModel.User.Id);

            if (user == null)
            {
                return NotFound();
            }

            var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);

            var rolesToAdd = new List<string>();
            var rolesToRemove = new List<string>();

            foreach (var role in UserModel.Roles)
            {
                var assignedInDb = userRolesInDb.FirstOrDefault(ur => ur == role.Text);

                if (role.Selected)
                {
                    if (assignedInDb == null)
                    {
                        rolesToAdd.Add(role.Text);
                    }
                }
                else
                {
                    if (assignedInDb != null)
                    {
                        rolesToRemove.Add(role.Text);
                    }
                }
            }

            if (rolesToAdd.Any())
            {
                await _signInManager.UserManager.AddToRolesAsync(user, rolesToAdd);
            }
            if (rolesToRemove.Any())
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, rolesToRemove);
            }

            user.UserName = UserModel.User.UserName;
            user.Email = UserModel.User.Email;
            user.PhoneNumber = UserModel.User.PhoneNumber;

            _unitOfWork.User.UpdateUser(user);

            return RedirectToPage("./Index");
        }
    }
}
