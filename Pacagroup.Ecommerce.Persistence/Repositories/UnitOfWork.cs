﻿using Pacagroup.Ecommerce.Application.Interface.Persistence;

namespace Pacagroup.Ecommerce.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        public ICustomerRepository Customers { get; }

        public IUserRepository Users { get; }

        public ICategoriesRepository Categories { get; }

        /// <summary>
        /// Patron Unidad de trabajo
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="userRepository"></param>
        /// <param name="categoriesRepository"></param>
        public UnitOfWork(ICustomerRepository customerRepository,
            IUserRepository userRepository,
            ICategoriesRepository categoriesRepository)
        {
            Customers = customerRepository;
            Users = userRepository;
            Categories = categoriesRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}