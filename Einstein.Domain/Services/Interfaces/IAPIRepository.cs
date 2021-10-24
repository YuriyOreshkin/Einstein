using Einstein.Domain.Entities;
using System.Linq;

namespace Einstein.Domain.Repository.Interfaces
{
    public interface IAPIRepository
    {
        IQueryable<Employee> GetEmployees(string filter);
        Employee GetEmployee(long id);
        Employee GetEmployee(string login);

        IQueryable<TreeViewEmployee> GetEmployeesWithDepartments(string filter);
    }

}
