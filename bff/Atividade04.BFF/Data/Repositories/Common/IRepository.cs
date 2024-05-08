using System;
using MongoDB.Bson;

namespace Atividade02.BFF.Data.Common.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        Task<TEntity> GetById(string id);
        Task<IEnumerable<TEntity>> GetAll();
        void Update(TEntity obj);
        void Remove(ObjectId id);

        IUnitOfWork unitOfWork { get; }
    }
}

