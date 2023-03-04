using L01_2020MC601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020MC601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        private readonly restaurenteDBcontext _restaurenteDBcontext;

        public platosController(restaurenteDBcontext restauranteDBcontext)
        {
            _restaurenteDBcontext = restauranteDBcontext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<platos> listadoPlatos = (from e in _restaurenteDBcontext.platos select e).ToList();

            if (listadoPlatos.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoPlatos);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult AddPlato([FromBody] platos _platos)
        {
            try
            {
                _restaurenteDBcontext.platos.Add(_platos);
                _restaurenteDBcontext.SaveChanges();
                return Ok(_platos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update/{id}")]

        public IActionResult updatePlato(int id, [FromBody] platos _platosMod)
        {
            platos? platosAct = (from e in _restaurenteDBcontext.platos where e.platoId == id select e).FirstOrDefault();

            if (platosAct == null)
            {
                return NotFound();
            }
            platosAct.nombrePlato = _platosMod.nombrePlato;
            platosAct.precio=_platosMod.precio;

            _restaurenteDBcontext.Entry(platosAct).State = EntityState.Modified;
            _restaurenteDBcontext.SaveChanges();
            return Ok(_platosMod);
        }

        [HttpDelete]
        [Route("Delete/{id}")]

        public IActionResult DeletePlatos(int id)
        {
            platos? plato = (from e in _restaurenteDBcontext.platos where e.platoId == id select e).FirstOrDefault();

            if (plato == null)
            {
                return NotFound();
            }

            _restaurenteDBcontext.platos.Attach(plato);
            _restaurenteDBcontext.platos.Remove(plato);
            _restaurenteDBcontext.SaveChanges();

            return Ok(plato);
        }


    }
}
