using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.DTO.Enums;
using Pacagroup.Ecommerce.Domain.Entities;
using Pacagroup.Ecommerce.Domain.Entity.Enums;
using Pacagroup.Ecommerce.Transversal.Mapper.Base;

namespace Pacagroup.Ecommerce.Transversal.Mapper
{
    public class DiscountBuilder : BuilderBase<Discount, DiscountDTO>
    {
        public override Discount Convert(DiscountDTO param)
        {
            return new Discount()
            {
                Id = param.Id,
                Name = param.Name,
                Description = param.Description,
                Percent = param.Percent,
                Status = converEnum(param.Status),
            };
        }

        public override DiscountDTO Convert(Discount param)
        {
            return new DiscountDTO()
            {
                Id = param.Id,
                Name = param.Name,
                Description = param.Description,
                Percent = param.Percent,
                Status = converEnumDTO(param.Status),
            };
        }

        private DiscountStatus converEnum(DiscountStatusDTO status)
        {
            DiscountStatus discountStatus;

            switch (status) {
                case DiscountStatusDTO.Inactive:
                    discountStatus = DiscountStatus.Inactive;
                    break;
                case DiscountStatusDTO.Active:
                    discountStatus = DiscountStatus.Active;
                    break;
                default:
                    discountStatus = DiscountStatus.Inactive;
                    break;
            }

            return discountStatus;
        }

        private DiscountStatusDTO converEnumDTO(DiscountStatus status)
        {
            DiscountStatusDTO discountStatus;

            switch (status)
            {
                case DiscountStatus.Inactive:
                    discountStatus = DiscountStatusDTO.Inactive;
                    break;
                case DiscountStatus.Active:
                    discountStatus = DiscountStatusDTO.Active;
                    break;
                default:
                    discountStatus = DiscountStatusDTO.Inactive;
                    break;
            }

            return discountStatus;
        }
    }
}