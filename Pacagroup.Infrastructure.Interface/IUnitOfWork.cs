namespace Pacagroup.Ecommerce.Infrastructure.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IUserRepository Users { get; }
        ICategoriesRepository Categories { get; }
    }
}