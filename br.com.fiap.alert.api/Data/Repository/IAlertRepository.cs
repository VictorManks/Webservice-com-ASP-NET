using br.com.fiap.alert.api.Models;

namespace br.com.fiap.alert.api.Data.Repository
{
    public interface IAlertRepository
    {
        IEnumerable<AlertModel> GetAll();
        AlertModel GetById(int id);
        void Add(AlertModel alertModel);
        void Update(AlertModel alertModel);
        void Delete(AlertModel alertModel);
    }
}
