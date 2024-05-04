using System;
using Atividade02.BFF.Data.Common.Interfaces;
using Atividade02.BFF.Data.Repositories;
using Atividade04.BFF.Models;
using Atividade04.BFF.Models.Interfaces.Repositories;
using Elastic.CommonSchema;
using MongoDB.Driver;

namespace Atividade04.BFF.Data.Repositories
{
    public class RetailerRepository : BaseRepository<Retailer>, IRetailerRepository
    {
        private FindOptions<Retailer> _options;
        public RetailerRepository(IMongoContext context) : base(context)
        {
            _options = new FindOptions<Retailer>
            {
                MaxTime = TimeSpan.FromSeconds(5) // Defina o tempo limite desejado
            };
        }

        public async Task<Retailer?> GetByEmail(string email)
        { 

            var filter = Builders<Retailer>.Filter.Eq("Email.Address", email);

            var data = await DbSet.FindAsync(filter, _options);

            return data.FirstOrDefault();
        }
    }
}

