using AppRootStack.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppRootStack.ApiServices
{
    public interface IDataApiService
    {
        //https://randomuser.me/api/?page=3&results=10&seed=abc&inc=gender,name,email,picture
        [Get("")]
        Task<Data> GetAllData([AliasAs("page")] int page, [AliasAs("results")] int result,
            [AliasAs("seed")] string seed, [AliasAs("inc")][Query(CollectionFormat.Csv)] string[] includes);
    }
}
