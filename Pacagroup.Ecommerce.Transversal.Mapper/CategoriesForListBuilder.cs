using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Domain.Entities;
using Pacagroup.Ecommerce.Transversal.Mapper.Base;

namespace Pacagroup.Ecommerce.Transversal.Mapper
{
    public class CategoriesForListBuilder : BuilderBase<Categories, CategoriesListDTO>
    {
        public override Categories Convert(CategoriesListDTO param)
        {
            return new Categories()
            {
                CategoryID = param.CategoryID,
                CategoryName = param.CategoryName
            };
        }

        public override CategoriesListDTO Convert(Categories param)
        {
            return new CategoriesListDTO()
            {
                CategoryID = param.CategoryID,
                CategoryName = param.CategoryName
            };
        }
    }
}