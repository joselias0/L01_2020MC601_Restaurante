using L01_2020MC601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020MC601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidosController : ControllerBase
    {
        private readonly restaurenteDBcontext _restaurenteDBcontext;

        public pedidosController(restaurenteDBcontext restauranteDBcontext) 
        {
            _restaurenteDBcontext= restauranteDBcontext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<pedidos> listadoPedidos = (from e in _restaurenteDBcontext.pedidos select e).ToList();

            if (listadoPedidos.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoPedidos);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult AddPedido([FromBody] pedidos _pedidos)
        {
            try
            {
                _restaurenteDBcontext.pedidos.Add(_pedidos);
                _restaurenteDBcontext.SaveChanges();
                return Ok(_pedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update/{id}")]

        public IActionResult updatePedido(int id, [FromBody] pedidos _pedidosMod) 
        {
            pedidos? pedidoAct = (from e in _restaurenteDBcontext.pedidos where e.pedidoId == id select e).FirstOrDefault();

            if (pedidoAct ==null) 
            {
                return NotFound();
            }

            
            pedidoAct.motoristaId = _pedidosMod.motoristaId;
            pedidoAct.clienteId=_pedidosMod.clienteId;
            pedidoAct.platoId=_pedidosMod.platoId;
            pedidoAct.cantidad=_pedidosMod.cantidad;
            pedidoAct.precio = _pedidosMod.precio;
            
            _restaurenteDBcontext.Entry(pedidoAct).State=EntityState.Modified;
            _restaurenteDBcontext.SaveChanges();
            return Ok(_pedidosMod);
        }

        [HttpDelete]
        [Route("Delete/{id}")]

        public IActionResult DeletePedido(int id)
        {
            pedidos? pedido = (from e in _restaurenteDBcontext.pedidos where e.pedidoId == id select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }

            _restaurenteDBcontext.pedidos.Attach(pedido);
            _restaurenteDBcontext.pedidos.Remove(pedido);
            _restaurenteDBcontext.SaveChanges();

            return Ok(pedido);
        }

        [HttpGet]
        [Route("FindByCliente/{cliente}")]

        public IActionResult FindByCliente(int cliente)
        {
            pedidos? pedido = (from e in _restaurenteDBcontext.pedidos where e.clienteId==cliente select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpGet]
        [Route("FindByMotorista/{motorista}")]

        public IActionResult FindByMotorista(int motoris)
        {
            pedidos? pedido = (from e in _restaurenteDBcontext.pedidos where e.clienteId == motoris select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }
    }
}
