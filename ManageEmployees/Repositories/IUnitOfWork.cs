using ManageEmployees.Interfaces;

namespace ManageEmployees.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        int Complete();
    }
}
