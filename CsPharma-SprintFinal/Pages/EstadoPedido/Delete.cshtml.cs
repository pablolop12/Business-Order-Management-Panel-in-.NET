using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace CsPharma_V4.Pages.EstadoPedido
{
    /** Esta clase es el controlador de nuestra página
     * de borrado de pedidos de nuestra App.
     */
    [Authorize(Roles = "Administrador, Empleado")]//Limita el acceso solo a usuairos con rol Administrador y Empleado
    public class DeleteModel : PageModel
    {
        //Variable y constructor de inicialización del contexto (Con esto Accedemos a nuestra base de datos)
        private readonly DAL.Models.CsPharmaSprintFinalContext _context; //Variable del contexto
        public DeleteModel(DAL.Models.CsPharmaSprintFinalContext context)
        {
            _context = context;
        }

        [BindProperty]//Anotación que indica que TdcTchEstadoPedido va a recibir información a traves del HTML
        public TdcTchEstadoPedido TdcTchEstadoPedido { get; set; }



        //METODO GET DE LA VISTA (A partir del ID seleccionado)
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TdcTchEstadoPedidos == null)
            {
                return NotFound();
            }

            //Incluyo las tablas referenciadas (Para recibir la descripcion de los registros
            //en lugar del código de los mismos).
            //CODIGO RECICLADO DE LA VISTA INDEX
            TdcTchEstadoPedido = await _context.TdcTchEstadoPedidos
                .Include(t => t.CodEstadoDevolucionNavigation)
                .Include(t => t.CodEstadoEnvioNavigation)
                .Include(t => t.CodEstadoPagoNavigation)
                .Include(t => t.CodLineaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            //Si el registro es nulo, saltará un error
            if (TdcTchEstadoPedido == null)
            {
                return NotFound();
            }
            return Page();
        }


        //METODO POST DE LA VISTA (A partir del id seleccionado)
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            //Si la id es nula o no se encuentra el registro, devuelve error
            if (id == null || _context.TdcTchEstadoPedidos == null)
            {
                return NotFound();
            }

            //Se busca un registro de la tabla pedidos por ID, y se almacena en la variable
            //TdcTchEstadoPedido, para procesarlo a continuación
            TdcTchEstadoPedido = await _context.TdcTchEstadoPedidos.FindAsync(id);

            //Si la variable no es nula, ejecuta el código de eliminación Remove
            if (TdcTchEstadoPedido != null)
            {
                _context.TdcTchEstadoPedidos.Remove(TdcTchEstadoPedido);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
