using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CodeChallenge.Models;
using System;
using Newtonsoft.Json;
using System.Data.SqlTypes;

namespace CodeChallengeWebPages.Pages
{
    public class Task1Model : PageModel
    {
        public bool dataEmployeesFound = false;        
        public string employeeId = "";

        public List<Employee> employees = new List<Employee>();
        public ReportingStructure reporting = new ReportingStructure();

        public async Task OnGet()
        {
            //Getting the employee list
            employees = await GetEmployees();            
        }

        public async Task OnPost()
        {
                        
            //Getting the employee list
            employees = await GetEmployees();

            employeeId = Request.Form["employeeId"].ToString();
            if (employeeId != "")
            {
                //Now show the reporting structure
                reporting = await GetStructure(employeeId);
            }

        }

        
        public async Task<List<Employee>> GetEmployees()
        {
            

            List<Employee> employees = new List<Employee>();

            using (var httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync("http://localhost:8080/api/Employee")) 
                {
                    if (response.IsSuccessStatusCode)
                    {                        
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        employees = JsonConvert.DeserializeObject<List<Employee>>(apiResponse);

                    } else
                    {
                        return null;
                    }

                    if ( employees != null && employees.Count > 0) 
                    {   
                        dataEmployeesFound = true; 

                    } else
                    {
                        return null;
                    }                    
                 }
            }

            return employees;

        }
        

        public async Task<Employee> GetEmployee(string id)
        {
            Employee employee = new ();    

            using (var httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(String.Format("http://localhost:8080/api/Employee/{0}",id)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        employee = JsonConvert.DeserializeObject<Employee>(apiResponse);

                    }
                    else
                    {
                        return null;
                    }

                    if (employees != null && employees.Count > 0)
                    {
                        dataEmployeesFound = true;
                        return employee;
                    }
                    else
                    {
                        return null;
                    }
                }
            }











        }


        public async Task<ReportingStructure> GetStructure(string id)
        {
            ReportingStructure structure = new ReportingStructure();

            using (var httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(String.Format("http://localhost:8080/api/Employee/reports/{0}", id)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        structure = JsonConvert.DeserializeObject<ReportingStructure>(apiResponse);
                        return structure;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
         }

    }
}
