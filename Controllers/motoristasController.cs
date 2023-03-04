using L01_2020MC601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020MC601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class motoristasController : ControllerBase
    {
        private readonly restaurenteDBcontext _restaurenteDBcontext;

        public motoristasController(restaurenteDBcontext restauranteDBcontext)
        {
            _restaurenteDBcontext = restauranteDBcontext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<motoristas> listadoMotoristas = (from e in _restaurenteDBcontext.motoristas select e).ToList();

            if (listadoMotoristas.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoMotoristas);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult AddMoto([FromBody] motoristas _motoristas)
        {
            try
            {
                _restaurenteDBcontext.motoristas.Add(_motoristas);
                _restaurenteDBcontext.SaveChanges();
                return Ok(_motoristas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update/{id}")]

        public IActionResult updateMoto(int id, [FromBody] motoristas _motoristasMod)
        {
            motoristas? motoristasAct = (from e in _restaurenteDBcontext.motoristas where e.motoristaId == id select e).FirstOrDefault();

            if (motoristasAct == null)
            {
                return NotFound();
            }
            motoristasAct.nombreMotorista = _motoristasMod.nombreMotorista;

            _restaurenteDBcontext.Entry(motoristasAct).State = EntityState.Modified;
            _restaurenteDBcontext.SaveChanges();
            return Ok(_motoristasMod);
        }

        [HttpDelete]
        [Route("Delete/{id}")]

        public IActionResult DeleteMoto(int id)
        {
            motoristas? motorista = (from e in _restaurenteDBcontext.motoristas where e.motoristaId == id select e).FirstOrDefault();

            if (motorista == null)
            {
                return NotFound();
            }

            _restaurenteDBcontext.motoristas.Attach(motorista);
            _restaurenteDBcontext.motoristas.Remove(motorista);
            _restaurenteDBcontext.SaveChanges();

            return Ok(motorista);
        }

        [HttpGet]
        [Route("FindByNombre/{nombre}")]

        public IActionResult FindByPrecio(string nombre)
        {
            List<motoristas> listadoMoto = (from e in _restaurenteDBcontext.motoristas where e.nombreMotorista.Contains(nombre) select e).ToList();

            if (listadoMoto == null)
            {
                return NotFound();
            }
            return Ok(listadoMoto);
        }
    }
}
