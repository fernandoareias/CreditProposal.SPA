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

        public async Task Delete(string email)
        {
            var retailer = await _retailerRepository.GetByEmail(email);

            if (retailer is null) throw new NullReferenceException("Retailer not exists");

            _retailerRepository.Remove(retailer._id);

            await _retailerRepository.unitOfWork.Commit(); 
        }

        public async Task<Retailer> Update(string email, UpdateRetalierRequest request)
        {
            var retailer = await _retailerRepository.GetByEmail(email);

            if (retailer is null) throw new NullReferenceException("Retailer not exists");

            retailer.ChangeName(request.Name);

            _retailerRepository.Update(retailer);

            await _retailerRepository.unitOfWork.Commit();

            return retailer;
        }
    }
}

