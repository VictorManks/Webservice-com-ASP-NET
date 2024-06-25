using br.com.fiap.alert.api.Data.Repository;
using br.com.fiap.alert.api.Models;

namespace br.com.fiap.alert.api.Service
{
    public class AlertService : IAlertService
    {
        private readonly IAlertRepository _repository;

        public AlertService(IAlertRepository repository)
        {
            _repository = repository;
        }

        public void AtualizarAlert(AlertModel alertModel) => _repository.Update(alertModel);


        public void CriaAlert(AlertModel alertModel) => _repository.Add(alertModel);


        public void DeletarAlert(int id)
        {
            var alertModel = _repository.GetById(id);
            if (alertModel != null)
            {
                _repository.Delete(alertModel);
            }
        }



        public IEnumerable<AlertModel> ListarTodosAlert() => _repository.GetAll();


        public AlertModel ObeterAlertPoId(int id) => _repository.GetById(id);

    }
}
