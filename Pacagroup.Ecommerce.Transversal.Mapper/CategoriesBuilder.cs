using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Transversal.Mapper.Base;

namespace Pacagroup.Ecommerce.Transversal.Mapper
{
    public class CategoriesBuilder : BuilderBase<Categories, CategoriesDTO>
    {
        public override Categories Convert(CategoriesDTO param)
        {
            return new Categories()
            {
                CategoryID = param.CategoryID,
                CategoryName = param.CategoryName,
                Description = param.Description,
                Picture = param.Picture
            };
        }

        public override CategoriesDTO Convert(Categories param)
        {
            return new CategoriesDTO()
            {
                CategoryID = param.CategoryID,
                CategoryName = param.CategoryName,
                Description = param.Description,
                Picture = param.Picture
            };
        }
    }
}