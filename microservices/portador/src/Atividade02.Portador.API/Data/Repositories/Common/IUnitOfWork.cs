using System;
namespace Atividade02.Proponent.API.Data.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();
    }
}

