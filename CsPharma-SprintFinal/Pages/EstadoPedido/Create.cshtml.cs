using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace CsPharma_V4.Pages.EstadoPedido
{
    /** Esta clase es el controlador de nuestra página
     * de creación de pedidos de nuestra App.
     */
    [Authorize(Roles = "Administrador, Empleado")]// Solo se permite el acceso a los usuarios con rol Administrador o Empleado.
    public class CreateModel : PageModel
    {


        //Variable y constructor de inicialización del contexto (Con esto Accedemos a nuestra base de datos)
        private readonly DAL.Models.CsPharmaSprintFinalContext _context;
        public CreateModel(DAL.Models.CsPharmaSprintFinalContext context)
        {
            _context = context;
        }

        // Método que se ejecuta cuando se accede a la página de creación de un estado de pedido.
        // Carga los datos necesarios para mostrar las opciones de selección en el formulario.
        public IActionResult OnGet()
        {
            ViewData["CodEstadoDevolucion"] = new SelectList(_context.TdcCatEstadosDevolucionPedidos, "CodEstadoDevolucion", "DesEstadoDevolucion");
            ViewData["CodEstadoEnvio"] = new SelectList(_context.TdcCatEstadosEnvioPedidos, "CodEstadoEnvio", "DesEstadoEnvio");
            ViewData["CodEstadoPago"] = new SelectList(_context.TdcCatEstadosPagoPedidos, "CodEstadoPago", "DesEstadoPago");
            ViewData["CodLinea"] = new SelectList(_context.TdcCatLineasDistribucions, "CodLinea", "CodLinea");
            return Page();
        }

        
        [BindProperty]//Anotación que indica que TdcTchEstadoPedido va a recibir información a traves del HTML
        public TdcTchEstadoPedido TdcTchEstadoPedido { get; set; }

        // Método que se ejecuta cuando se envía el formulario para crear un estado de pedido.
        public async Task<IActionResult> OnPostAsync()
        {
            

            // Se genera un nuevo UUID y se asigna la fecha actual al estado de pedido.
            TdcTchEstadoPedido.MdUuid = Guid.NewGuid().ToString();
            TdcTchEstadoPedido.MdDate = DateTime.Now;

            // Se agrega el estado de pedido a la base de datos y se guarda el cambio.
            _context.TdcTchEstadoPedidos.Add(TdcTchEstadoPedido);
            await _context.SaveChangesAsync();

            // Redirige a la página de lista de estados de pedido.
            return RedirectToPage("./Index");
        }
    }
}
