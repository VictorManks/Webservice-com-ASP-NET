using br.com.fiap.alert.api.Models;

namespace br.com.fiap.alert.api.Data.Repository
{
    public interface IAlertRepository
    {
        IEnumerable<AlertModel> GetAll();
        IEnumerable<AlertModel> GetAll(int page, int size);
        IEnumerable<AlertModel> GetAllReference(int lastReference, int size);

        AlertModel GetById(int id);
        void Add(AlertModel alertModel);
        void Update(AlertModel alertModel);
        void Delete(AlertModel alertModel);
    }
}
