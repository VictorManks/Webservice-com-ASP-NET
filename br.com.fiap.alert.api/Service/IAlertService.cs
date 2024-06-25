using br.com.fiap.alert.api.Models;

namespace br.com.fiap.alert.api.Service
{
    public interface IAlertService
    {
        IEnumerable<AlertModel> ListarTodosAlert();
        IEnumerable<AlertModel> ListarTodosAlert(int pagina = 0, int tamanho = 10);
        IEnumerable<AlertModel> ListarTodosAlertUltimaReferencia(int ultimoId = 0, int tamanho = 10);
        AlertModel ObeterAlertPoId(int id);
        void CriaAlert(AlertModel alertModel);
        void AtualizarAlert(AlertModel alertModel);
        void DeletarAlert(int id);
    }
}
