using WebApplicationEWX.Models;

namespace WebApplicationEWX.Repo
{
    public interface IEmployeesRepo
    {
        Task<int> CreateEmployee(Employee employee);
        Task<int> Delete(int Id);
        Task<int> EditEmployee(Employee employee);
        Task<Employee?> GetEmployee(int id);
        Task<int> GetEmployeeCount();
        Task<IEnumerable<Employee>> GetEmployees(int pageSize, int pageId);
    }
}