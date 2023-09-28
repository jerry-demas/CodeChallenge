using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        List<Compensation> GetAll();

        Compensation Add(Compensation compensation);

        Compensation GetById(String Id);

        Task SaveAsync();
    }
}
