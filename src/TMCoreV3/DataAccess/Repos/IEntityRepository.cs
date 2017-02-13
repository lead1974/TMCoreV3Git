﻿using TMCoreV3.DataAccess.Models.User;
using TMCoreV3.DataAccess.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TMCoreV3.DataAccess.Repos
{
    public interface IEntityRepository<T>
    {
        IEnumerable<T> GetAll { get; }
        IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProeprties);
        T Find(int id);
        void insertOrUpdate(T entity);
        void Delete(int id);
        void Save();
    }

    public interface IUserRepository<T> : IEntityRepository<AuthUser>
    {
    }
    public interface ICustomerRepository 
    {
        IEnumerable<Customer> GetAll();
        IEnumerable<Customer> GetAllWithApplianceProblems();
        Customer FindById(int Id);
        Customer FindByName(string Name);
        void Add(Customer newCustomer);
        void Update(Customer newCustomer);
        void Delete(Customer newCustomer);
        bool SaveAll();     
    }

    public interface ICustomerApplianceTypeRepository
    {
        IEnumerable<CustomerApplianceType> GetAll();
        IEnumerable<CustomerApplianceType> GetAllWithBrands();
        CustomerApplianceType FindById(int Id);
        CustomerApplianceType FindByName(string Name);
        void Add(CustomerApplianceType newCustomerApplianceType);
        void Update(CustomerApplianceType newCustomerApplianceType);
        void Delete(CustomerApplianceType newCustomerApplianceType);
        bool SaveAll();
    }

    public interface ICustomerApplianceBrandRepository
    {
        IEnumerable<CustomerApplianceBrand> GetAll();
        IEnumerable<CustomerApplianceBrand> GetAllWithBrands();
        CustomerApplianceBrand FindById(int Id);
        CustomerApplianceBrand FindByName(string Name);
        void Add(CustomerApplianceBrand newCustomerApplianceBrand);
        void Update(CustomerApplianceBrand newCustomerApplianceBrand);
        void Delete(CustomerApplianceBrand newCustomerApplianceBrand);
        bool SaveAll();
    }
}
