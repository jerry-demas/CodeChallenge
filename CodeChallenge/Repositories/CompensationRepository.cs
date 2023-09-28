using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;
using Microsoft.AspNetCore.Mvc;


namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {

        private readonly CompensationContext _compensationContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
            _logger = logger;

        }


        public Compensation Add(Compensation compensation)
        {
            compensation.CompensationId = Guid.NewGuid().ToString();
            _compensationContext.Compensations.Add(compensation);
            return compensation;

        }

        public Compensation GetById(string Id)
        {
            return _compensationContext.Compensations.ToList().FirstOrDefault(c => c.Employee.EmployeeId == Id);
        }

        public List<Compensation> GetAll()
        {
            return _compensationContext.Compensations.ToList();
        }


        public Task SaveAsync()
        {           
            return _compensationContext.SaveChangesAsync();           
        }
    }
}
