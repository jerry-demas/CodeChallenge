using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;
                
        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        public Compensation Create(Compensation compensation)
        {

            if (compensation != null)
            {
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync(); 
            }

            return compensation;
        }


        public List<Compensation> GetAll()
        {
            return _compensationRepository.GetAll();
        }


        public Compensation GetById(string id)
        {

            if(!String.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetById(id);
            }

            return null;
                        
        }
    }
}
