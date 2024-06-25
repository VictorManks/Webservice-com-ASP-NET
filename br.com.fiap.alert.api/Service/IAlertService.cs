using br.com.fiap.alert.api.Models;

namespace br.com.fiap.alert.api.Service
{
    public interface IAlertService
    {
        IEnumerable<AlertModel> ListarTodosAlert();
        AlertModel ObeterAlertPoId(int id);
        void CriaAlert(AlertModel alertModel);
        void AtualizarAlert(AlertModel alertModel);
        void DeletarAlert(int id);
    }
}
