using WebApplicationEWX.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Humanizer.Localisation.TimeToClockNotation;

namespace WebApplicationEWX.Repo
{
    public class EmployeesRepo : IEmployeesRepo
    {
        SqlConnection _connection;
        public EmployeesRepo(SqlConnection con)
        {
            _connection = con;
        }
        public async Task<Employee?> GetEmployee(int id)
        {
            if (_connection.State != System.Data.ConnectionState.Open)
                _connection.Open();
            var sql = "SELECT * FROM Employees WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<Employee>(sql, new { Id = id });
        }
        public async Task<IEnumerable<Employee>> GetEmployees(int pageSize, int pageId)
        {
            if (_connection.State != System.Data.ConnectionState.Open)
                _connection.Open();
            var sql = "Select * From Employees Order By Id Offset @pageSize * (@pageId-1) Rows Fetch Next @pageSize Rows Only";
            return await _connection.QueryAsync<Employee>(sql, new { pageSize, pageId });
        }
        public async Task<int> CreateEmployee(Employee employee)
        {
            if (_connection.State != System.Data.ConnectionState.Open)
                _connection.Open();
            var sql = "Insert into [Employees] ([FirstName], [LastName], [Email]) values ( @FirstName, @LastName, @Email)";
            return await _connection.ExecuteAsync(sql, employee);
        }
        public async Task<int> EditEmployee(Employee employee)
        {
            if (_connection.State != System.Data.ConnectionState.Open)
                _connection.Open();
            var sql = "Update [Employees] Set FirstName = @FirstName, [LastName] = @LastName, [Email]= @Email   where Id= @Id";
            return await _connection.ExecuteAsync(sql, employee);
        }
        public async Task<int> Delete(int Id)
        {
            if (_connection.State != System.Data.ConnectionState.Open)
                _connection.Open();
            var sql = "Delete [Employees] Where  Id=@Id";
            return await _connection.ExecuteAsync(sql, new { Id });
        }
        public async Task<int> GetEmployeeCount()
        {
            if (_connection.State != System.Data.ConnectionState.Open)
                _connection.Open();
            var sql = "Select Count(Id) from Employees";
            return await _connection.ExecuteScalarAsync<int>(sql);
        }
    }
}
