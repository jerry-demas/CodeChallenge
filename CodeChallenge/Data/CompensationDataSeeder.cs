using CodeChallenge.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Data
{

    public class CompensationDataSeeder
    {
        private CompensationContext _compensationContext;        
        public CompensationDataSeeder(CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
        }

        public async Task Seed()
        {
            if (!_compensationContext.Compensations.Any())
            {
                List<Compensation> compensations = LoadCompensations();
                _compensationContext.Compensations.AddRange(compensations);

                await _compensationContext.SaveChangesAsync();
            }
        }

        private List<Compensation> LoadCompensations()
        {

            List<Compensation> compensations = new List<Compensation>();
            return compensations;
        }

    }
  
      
}
