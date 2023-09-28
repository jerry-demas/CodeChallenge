using CodeChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace CodeChallengeWebPages.Pages
{
    public class Task2Model : PageModel
    {

        public bool dataEmployeesFound = false;
        public string employeeId = "";
        public List<Employee> employees = new List<Employee>();
        public Compensation compensation = new Compensation();


        public async Task OnGet()
        {
            //Getting the employee list
            employees = await GetEmployees();
        }

        public async Task OnPost(string value)
        {
            //Getting the employee list
            employees = await GetEmployees();

            employeeId = Request.Form["employeeList"].ToString();
            if (value == "GetData")
            {                
                if (employeeId != "")
                {
                    compensation = await GetCompensation(employeeId);
                }
            } else
            {
                if (employeeId != "")
                {
                    // This is add comp
                    string sal = Request.Form["employeeSalary"].ToString();
                    compensation = await AddCompensation(employeeId, sal);  
                    
                }
            }                    
        }

        public async Task<Compensation> AddCompensation(string id, string salary)
        {
            
            Employee emp = new Employee();

            //Get the client
            using (var httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(String.Format("http://localhost:8080/api/Employee/{0}", id)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        emp = JsonConvert.DeserializeObject<Employee>(apiResponse);
                        
                    }
                    else
                    {
                        return null;
                    }
                }                
            }
            
            //Now build the compensation
            Compensation comp = new Compensation();
            comp.Employee = emp;            
            comp.Salary = (float)Convert.ToDouble(salary);
            comp.EffectiveDate = DateTime.Now;

            //Now send it off to be saved.
            using (var httpClient = new HttpClient())
            {

                var body =  new StringContent(JsonConvert.SerializeObject(comp), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("http://localhost:8080/api/Employee/AddComp", body);
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //emp = JsonConvert.DeserializeObject<Employee>(apiResponse);

                }
                else
                {
                    return null;
                }
                return comp;
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

                    }
                    else
                    {
                        return null;
                    }

                    if (employees != null && employees.Count > 0)
                    {
                        dataEmployeesFound = true;

                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return employees;

        }
        
        
        public async Task<Compensation> GetCompensation(string id)
        {
            Compensation comp = new Compensation();

            using (var httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(String.Format("http://localhost:8080/api/Employee/comp/{0}", id)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        comp = JsonConvert.DeserializeObject<Compensation>(apiResponse);
                        return comp;
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
