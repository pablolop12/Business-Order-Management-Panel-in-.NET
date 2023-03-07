using CsPharma_V4.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CsPharma_V4.Models
{
    public class UserModel
    {
        public User User { get; set; }

        public IList<SelectListItem> Roles { get; set; }


    }
}
