using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CsPharma_V4.Areas.Identity.Data;

    // Cladee que añade las propiedades Nombre y apellido a las propias que tiene el Identity por defecto
    public class User : IdentityUser
    {

        public string NombreUsuario { get; set; }
        public string ApellidosUsuario { get; set; }
    }



