using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Models
{    
    public class Compensation
    {
        public string CompensationId { get; set; }
        public Employee Employee { get; set; }
        public float Salary { get; set; }
        public DateTime EffectiveDate { get; set; }

    }
}
