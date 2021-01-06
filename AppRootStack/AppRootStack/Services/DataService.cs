using AppRootStack.ApiServices;
using AppRootStack.Common.Constants;
using AppRootStack.Models;
using Refit;
using Splat;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppRootStack.Services
{
    public class DataService : IDataService
    {
        public async Task<Data> GetAll(int page, int totalItems)
        {
            var dataApi = RestService.For<IDataApiService>(Constants.EndPoint);
            var dataResponse = await dataApi.GetAllData(page, totalItems, Constants.Seed,
                new string[] { "gender", "name", "email", "picture", "phone" });
            return dataResponse;
        }
    }
}
