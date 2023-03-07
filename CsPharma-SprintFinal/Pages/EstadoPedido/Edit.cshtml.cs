using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace CsPharma_V4.Pages.EstadoPedido
{
    /** Esta clase es el controlador de nuestra página
     * de edición de pedidos de nuestra App.
     */
    [Authorize(Roles = "Administrador, Empleado")]//Limita el acceso solo a usuairos con rol Administrador y Empleado
    public class EditModel : PageModel
    {

        //Variable y constructor de inicialización del contexto (Con esto Accedemos a nuestra base de datos)
        private readonly DAL.Models.CsPharmaSprintFinalContext _context;//Variable del contexto
        public EditModel(DAL.Models.CsPharmaSprintFinalContext context)
        {
            _context = context;
        }

        [BindProperty]//Anotación que indica que TdcTchEstadoPedido va a recibir información a traves del HTML
        public TdcTchEstadoPedido TdcTchEstadoPedido { get; set; } = default!;


        //METODO GET DE LA VISTA
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            //Si el registro que se recibe es nulo devolverá error
            if (id == null || _context.TdcTchEstadoPedidos == null)
            {
                return NotFound();
            }

            //Se busca el registro que tenga la ID que se le proporciona y se almacena en una variable,
            //a continuación se chequea que no sea nulo.
            var tdctchestadopedido =  await _context.TdcTchEstadoPedidos.FirstOrDefaultAsync(m => m.Id == id);
            if (tdctchestadopedido == null)
            {
                return NotFound();
            }
            TdcTchEstadoPedido = tdctchestadopedido;

            //En esta parte establecemos que información de nuestros campos de la pantalla de edición
           //Asignamos en los ViewData que reciba las descripciones para mostrarlas en los campos.
           ViewData["CodEstadoDevolucion"] = new SelectList(_context.TdcCatEstadosDevolucionPedidos, "CodEstadoDevolucion", "DesEstadoDevolucion");
           ViewData["CodEstadoEnvio"] = new SelectList(_context.TdcCatEstadosEnvioPedidos, "CodEstadoEnvio", "DesEstadoEnvio");
           ViewData["CodEstadoPago"] = new SelectList(_context.TdcCatEstadosPagoPedidos, "CodEstadoPago", "DesEstadoPago");
           ViewData["CodLinea"] = new SelectList(_context.TdcCatLineasDistribucions, "CodLinea", "CodLinea");
            return Page();
        }

        //METODO ONPOST DE LA VISTA
        public async Task<IActionResult> OnPostAsync()
        {
            //Esto rastrea los cambios en la entidad que estamos modificando para luego
            //Actualizarlo en SaveChangesAsync()
            _context.Attach(TdcTchEstadoPedido).State = EntityState.Modified;

            try
            {
                TdcTchEstadoPedido.MdDate = DateTime.Now;//Actualiza la fecha del MdDate, para actualizar el campo de ultima actualización
                await _context.SaveChangesAsync();//Salva los cambios que hayamos podido introducir
            }

            //Captura de posibles errores.
            catch (DbUpdateConcurrencyException)
            {
                if (!TdcTchEstadoPedidoExists(TdcTchEstadoPedido.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
        
        //Metodo que comprueba si el registro existe y devuelve true o false
        //(Se usa en el try catch final)
        private bool TdcTchEstadoPedidoExists(int id)
        {
          return _context.TdcTchEstadoPedidos.Any(e => e.Id == id);
        }
    }
}
