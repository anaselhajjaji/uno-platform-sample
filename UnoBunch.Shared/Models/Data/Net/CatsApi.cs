using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnoBench.Model.Data.Entity;
using UnoBench.Model.Data.Net;
using Newtonsoft.Json;
using Refit;

namespace UnoBench.Model.Data.Net
{
    public class CatsApi : ICatsApi
    {
        const string API_URL = "https://api.thecatapi.com/v1";

        private readonly HttpClient _httpClient;
        private readonly ICatsApi _theCatsApi;

        public CatsApi()
        {
#if __WASM__
            var innerHandler = new Uno.UI.Wasm.WasmHttpHandler();
#else
            var innerHandler = new HttpClientHandler();
#endif
            _httpClient = new HttpClient(innerHandler) { BaseAddress = new Uri(API_URL) };
            _theCatsApi = RestService.For<ICatsApi>(_httpClient);
        }

        public async Task<ObservableCollection<CatEntity>> FetchCats(int page, int limit)
        {
            return await _theCatsApi.FetchCats(page, limit);
         }

        public async Task<List<CatImageEntity>> FetchCatImages(string catId, int page, int limit)
        {
            return await _theCatsApi.FetchCatImages(catId, page, limit);
        }
    }
}
