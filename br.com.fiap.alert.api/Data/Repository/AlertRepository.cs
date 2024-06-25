using br.com.fiap.alert.api.Data.Contexts;
using br.com.fiap.alert.api.Models;
using Microsoft.EntityFrameworkCore;

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
        public IEnumerable<AlertModel> GetAll(int page, int size)
        {
            return _databaseContext.Alerts
                            .Skip( (page - 1) * page  )
                            .Take( size )
                            .AsNoTracking()
                            .ToList();  
        }

        public IEnumerable<AlertModel> GetAllReference(int lastReference, int size)
        {
            var clientes = _databaseContext.Alerts
                                .Where(c => c.AlertId > lastReference)
                                .OrderBy( c => c.AlertId) 
                                .Take(size)
                                .AsNoTracking()
                                .ToList();

            return clientes;
        }

        public AlertModel GetById(int id) => _databaseContext.Alerts.Find(id);


        public void Update(AlertModel alertModel)
        {
            _databaseContext.Alerts.Update(alertModel);
            _databaseContext.SaveChanges();
        }
    }
}
