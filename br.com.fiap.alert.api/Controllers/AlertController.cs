
using AutoMapper;
using br.com.fiap.alert.api.Models;
using br.com.fiap.alert.api.Service;
using br.com.fiap.alert.api.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace br.com.fiap.alert.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertController : Controller
    {
        private readonly IAlertService _service;
        private readonly IMapper _mapper;

        public AlertController(IAlertService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AlertPaginacaoViewModel>> Get([FromQuery] int referencia = 0, [FromQuery] int tamanho = 10)
        {
            var clientes = _service.ListarTodosAlert(referencia, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<AlertViewModel>>(clientes);

            var viewModel = new AlertPaginacaoRefencialViewModel
            {
                Alerts = viewModelList,
                PageSize = tamanho,
                Ref = referencia,
                NextRef = viewModelList.Last().AlertId
            };


            return Ok(viewModel);
        }


        [HttpGet("{id}")]
        public ActionResult<AlertViewModel> Get(int id)
        {
            var alert = _service.ObeterAlertPoId(id);
            if (alert == null)
                return NotFound();

            var viewModel = _mapper.Map<AlertViewModel>(alert);
            return Ok(viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Post([FromBody] AlertCreateViewModel viewModel)
        {
            var alert = _mapper.Map<AlertModel>(viewModel);
            _service.CriaAlert(alert);
            return CreatedAtAction(nameof(Get), new { id = alert.AlertId }, alert);
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Put(int id, [FromBody] AlertUpdateViewModel viewModel)
        {
            var alertExistente = _service.ObeterAlertPoId(id);
            if (alertExistente == null)
                return NotFound();

            _mapper.Map(viewModel, alertExistente);
            _service.AtualizarAlert(alertExistente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            _service.DeletarAlert(id);
            return NoContent();
        }
    }

}
