using CodeChallenge.Models;
using System;
using System.Collections.Generic;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {

        List<Compensation> GetAll();

        Compensation Create(Compensation compensation);

        Compensation GetById(String id);

    }
}
