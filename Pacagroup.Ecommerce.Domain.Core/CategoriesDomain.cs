using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Infrastructure.Interface;

namespace Pacagroup.Ecommerce.Domain.Core
{
    public class CategoriesDomain : ICategoriesDomain
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Categories> GetAll()
        {
            return _unitOfWork.Categories.GetAll();
        }
    }
}