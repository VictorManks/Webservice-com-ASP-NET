using br.com.fiap.alert.api.Data.Contexts;
using br.com.fiap.alert.api.Models;

namespace br.com.fiap.alert.api.Data.Repository
{
    public class AlertRepository : IAlertRepository
    {
        private readonly DatabaseContext _databaseContext;

        public AlertRepository(DatabaseContext context)
        {
            _databaseContext = context;
        }
        public void Add(AlertModel alertModel)
        {
            _databaseContext.Alerts.Add(alertModel);
            _databaseContext.SaveChanges();
        }

        public void Delete(AlertModel alertModel)
        {
            _databaseContext.Alerts.Remove(alertModel);
            _databaseContext.SaveChanges();
        }

        public IEnumerable<AlertModel> GetAll() => _databaseContext.Alerts.ToList();


        public AlertModel GetById(int id) => _databaseContext.Alerts.Find(id);


        public void Update(AlertModel alertModel)
        {
            _databaseContext.Alerts.Update(alertModel);
            _databaseContext.SaveChanges();
        }
    }
}
