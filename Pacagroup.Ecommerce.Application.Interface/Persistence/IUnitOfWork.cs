namespace Pacagroup.Ecommerce.Application.Interface.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IUserRepository Users { get; }
        ICategoriesRepository Categories { get; }
    }
}