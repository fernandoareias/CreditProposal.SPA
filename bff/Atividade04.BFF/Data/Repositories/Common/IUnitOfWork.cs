using System;
namespace Atividade02.BFF.Data.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();
    }
}

