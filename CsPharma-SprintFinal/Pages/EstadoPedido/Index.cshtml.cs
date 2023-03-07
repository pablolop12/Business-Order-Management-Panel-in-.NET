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
     * de visionado de pedidos de nuestra App.
     */
    [Authorize(Roles = "Administrador, Empleado")]//Limita el acceso solo a usuairos con rol Administrador y Empleado
    public class IndexModel : PageModel
    {
        //Variable y constructor de inicialización del contexto (Con esto Accedemos a nuestra base de datos)
        private readonly DAL.Models.CsPharmaSprintFinalContext _context;
        public IndexModel(DAL.Models.CsPharmaSprintFinalContext context)
        {
            _context = context;
        }

        public IList<TdcTchEstadoPedido> TdcTchEstadoPedido { get;set; } = default!; //Lista que almacenará todos los registros

        //METODO GET DE LA VISTA 
        public async Task OnGetAsync()
        {
            //Carga a la lista TdcTchEstadoPedido todos los campos del registro
            if (_context.TdcTchEstadoPedidos != null)
            {
                TdcTchEstadoPedido = await _context.TdcTchEstadoPedidos
                .Include(t => t.CodEstadoDevolucionNavigation)
                .Include(t => t.CodEstadoEnvioNavigation)
                .Include(t => t.CodEstadoPagoNavigation)
                .Include(t => t.CodLineaNavigation).ToListAsync();
            }
        }
    }
}
