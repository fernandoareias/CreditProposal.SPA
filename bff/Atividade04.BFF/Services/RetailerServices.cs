using System;
using Atividade04.BFF.DTOs.Retailer.v1.Requests;
using Atividade04.BFF.Models;
using Atividade04.BFF.Models.Interfaces.Repositories;
using Atividade04.BFF.Models.Interfaces.Services;

namespace Atividade04.BFF.Services
{
    public class RetailerServices : IRetailerServices
    {
        private readonly IRetailerRepository _retailerRepository;
        public RetailerServices(IRetailerRepository retailerRepository)
        {
            _retailerRepository = retailerRepository;
        }

        public async Task<Retailer> Create(CreateRetalierRequest request)
        {
            if ((await _retailerRepository.GetByEmail(request.Email)) != null)
                return null;

            var retailer = new Retailer(
                request.FirstName + " " + request.LastName,
                request.Email,
                request.Password,
                request.CNPJ,
                ERole.SalesAssociete
            );

            _retailerRepository.Add(retailer);

            await _retailerRepository.unitOfWork.Commit();

            return retailer;

        }
    }
}

