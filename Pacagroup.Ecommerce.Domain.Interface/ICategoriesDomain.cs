﻿using Pacagroup.Ecommerce.Domain.Entity;

namespace Pacagroup.Ecommerce.Domain.Interface
{
    public interface ICategoriesDomain
    {
        IEnumerable<Categories> GetAll();
    }
}