using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnoBench.Model.Domain;
using UnoBench.Model.Data.Net;
using Newtonsoft.Json;
using Refit;

namespace UnoBench.Model.Data.Net
{
    public class CatsApi : ICatsApi
    {
        const string API_URL = "https://api.thecatapi.com/v1";

        public async Task<ObservableCollection<Cat>> FetchCats(int page, int limit)
        {
            var catsApi = RestService.For<ICatsApi>(API_URL);
            return await catsApi.FetchCats(page, limit);
         }

        public async Task<List<CatImage>> FetchCatImages(string catId, int page, int limit)
        {
            var catsApi = RestService.For<ICatsApi>(API_URL);
            return await catsApi.FetchCatImages(catId, page, limit);
        }
    }
}
