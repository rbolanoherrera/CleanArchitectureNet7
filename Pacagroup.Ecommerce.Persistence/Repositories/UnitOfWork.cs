using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Persistence.Contexts;

namespace Pacagroup.Ecommerce.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ICustomerRepository Customers { get; }

        public IUserRepository Users { get; }

        public ICategoriesRepository Categories { get; }


        public IDiscountRepository Discounts { get; }


        /// <summary>
        /// Patron Unidad de trabajo
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="userRepository"></param>
        /// <param name="categoriesRepository"></param>
        public UnitOfWork(ICustomerRepository customerRepository,
            IUserRepository userRepository,
            ICategoriesRepository categoriesRepository,
            IDiscountRepository discounts,
            ApplicationDbContext applicationDbContext)
        {
            Customers = customerRepository;
            Users = userRepository;
            Categories = categoriesRepository;
            Discounts = discounts;
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await applicationDbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        
    }
}