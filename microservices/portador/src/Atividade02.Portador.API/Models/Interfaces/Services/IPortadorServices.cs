using System;
using Atividade02.Proponent.API.Events;

namespace Atividade02.Proponent.API.Models.Interfaces.Services
{
    public interface IPortadorServices
    {

        Task Create(ProposalApprovedEvent request);
    }
}

