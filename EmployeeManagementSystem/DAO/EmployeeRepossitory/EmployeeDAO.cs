using Models;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ISession = NHibernate.ISession;

namespace DAO.EmployeeRepossitory
{
	public interface IEmployeeDAO
	{
		Task<List<Employee>> GetAll();
		Task<Employee> GetById(int id);
		Task CreateAsync(Employee employee);
		Task UpdateAsync(Employee employee);
		Task DeleteAsync(int id);
	}
	public class EmployeeDAO : IEmployeeDAO
	{
		private readonly ISession _session;

		public EmployeeDAO(ISession session)
		{
			_session = session;
		}

		public async Task CreateAsync(Employee employee)
		{
			await _session.SaveAsync(employee);
		}

		public async Task<List<Employee>> GetAll()
		{
			return await _session.Query<Employee>().ToListAsync();
		}

		public async Task<Employee> GetById(int id)
		{
			return await _session.GetAsync<Employee>(id);
		}

		public async Task UpdateAsync(Employee employee)
		{
			await _session.UpdateAsync(employee);
		}

		public async Task DeleteAsync(int id)
		{
			var employeeToDelete = await _session.GetAsync<Employee>(id);

			if (employeeToDelete != null)
			{
				await _session.DeleteAsync(employeeToDelete);
			}
		}
	}
}
