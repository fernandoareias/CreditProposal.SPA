using System;
using Atividade02.BFF.Data.Common.Interfaces;
using Elastic.CommonSchema;

namespace Atividade04.BFF.Models.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        Task<Session?> GetByAggregateId(string aggregateId);
    }
}

