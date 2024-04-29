using System;
using Atividade02.BFF.Data.Common.Interfaces;
using Elastic.CommonSchema;

namespace Atividade04.BFF.Models.Interfaces.Repositories
{
    public interface IRetailerRepository : IRepository<Retailer>
    {
        Task<Retailer?> GetByEmail(string email);
    }
}

