
namespace WebAPiCaching.Services
{
    public interface IEmployeeService<TEntity, TIdentity>
    {
        Task<List<TEntity>> GetEmployees();
        Task<TEntity> GetEmployeeById(TIdentity id);
        Task<bool> AddEmployee(TEntity entity);
        Task<TEntity> UpdateEmployee(TEntity entity);
        Task<bool> DeleteEmployee(TIdentity id);

    }
}
