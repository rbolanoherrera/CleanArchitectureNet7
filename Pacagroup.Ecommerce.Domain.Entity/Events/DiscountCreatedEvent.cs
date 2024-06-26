﻿using Pacagroup.Ecommerce.Domain.Entity.Enums;

namespace Pacagroup.Ecommerce.Domain.Entity.Events
{
    public class DiscountCreatedEvent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Percent { get; set; }
        public DiscountStatus Status { get; set; }
    }
}