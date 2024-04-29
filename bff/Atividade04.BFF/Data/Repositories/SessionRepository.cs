using System;
using Atividade02.BFF.Data.Common.Interfaces;
using Atividade02.BFF.Data.Repositories;
using Atividade04.BFF.Models;
using Atividade04.BFF.Models.Interfaces;
using MongoDB.Driver;

namespace Atividade04.BFF.Data.Repositories
{
    public class SessionRepository : BaseRepository<Session>, ISessionRepository
    {
        private FindOptions<Session> _options;
        public SessionRepository(IMongoContext context) : base(context)
        {
            _options = new FindOptions<Session>
            {
                MaxTime = TimeSpan.FromSeconds(5) // Defina o tempo limite desejado
            };
        }


        public async Task<Session?> GetByAggregateId(string aggregateId)
        {
            var options = new FindOptions<Session>
            {
                MaxTime = TimeSpan.FromSeconds(5) // Defina o tempo limite desejado
            };

            var filter = Builders<Session>.Filter.Eq("AggregateId", aggregateId);

            var data = await DbSet.FindAsync(filter, options);

            return data.FirstOrDefault();
        }

    }
}

