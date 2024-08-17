using Microsoft.EntityFrameworkCore;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Domain.Entities;
using Pacagroup.Ecommerce.Persistence.Contexts;

namespace Pacagroup.Ecommerce.Persistence.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ApplicationDbContext dbContext;

        public DiscountRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #region "Metodos Sincronos"

        public Discount Get(string id)
        {
            return dbContext.Discounts.FirstOrDefault(d => d.Id == Convert.ToInt32(id));
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Discount> GetAll()
        {
            return dbContext.Discounts.ToArray();
        }

        public bool Insert(Discount entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Discount entity)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region "Metodos Asincronos"


        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await dbContext.Set<Discount>().FirstOrDefaultAsync(d => d.Id.Equals(int.Parse(id)));

            if (entity == null)
                return await Task.FromResult(false);

            dbContext.Remove(entity);

            return await Task.FromResult(true);
        }

        public async Task<List<Discount>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbContext.Set<Discount>().AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Discount>> GetAllAsync()
        {
            return await dbContext.Discounts.ToArrayAsync();
        }

        public async Task<Discount> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await dbContext.Set<Discount>().AsNoTracking().FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        }

        public async Task<Discount> GetAsync(string id)
        {
            return await dbContext.Set<Discount>().AsNoTracking().FirstOrDefaultAsync(d => d.Id == int.Parse(id));
        }

        public async Task<bool> InsertAsync(Discount entity)
        {
            dbContext.Add(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(Discount discount)
        {
            var entity = await dbContext.Set<Discount>().AsNoTracking().SingleOrDefaultAsync(d => d.Id.Equals(discount.Id));

            if(entity == null)
                return await Task.FromResult(false);

            entity.Name = discount.Name;
            entity.Description = discount.Description;
            entity.Percent = discount.Percent;
            entity.Status = discount.Status;

            dbContext.Update(entity);

            return await Task.FromResult(true);
        }

        #endregion

    }
}