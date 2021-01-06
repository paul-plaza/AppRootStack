using AppRootStack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppRootStack.Services
{
    public interface IDataService
    {
        Task<Data> GetAll(int page, int totalItems);
    }
}