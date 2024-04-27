using System;
using Atividade02.Core.Common.CQRS;
using MongoDB.Driver;

namespace Atividade02.Proponent.API.Data.Common.Interfaces
{
    public interface IMongoContext : IUnitOfWork
    {
        void AddCommand(Func<Task> func, AggregateRoot? entity = null);
        Task<int> SaveChanges();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}

