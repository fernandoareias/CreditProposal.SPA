using System;
using Atividade02.Proponent.API.Data.Common.Interfaces;
using Atividade02.Proponent.API.Models.Interfaces;

namespace Atividade02.Proponent.API.Data.Repositories
{
    public class PortadorRepository : BaseRepository<Atividade02.Proponent.API.Models.Portador>, IPortadorRepository
    {

        public PortadorRepository(IMongoContext context) : base(context)
        {
        }

    }
}

