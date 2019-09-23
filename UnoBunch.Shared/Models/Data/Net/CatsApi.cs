using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using UnoBench.Model.Domain;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;

namespace UnoBench.Model.Data.Net
{
    public class CatsApi : ICatsApi
    {
        const string API_URL = "https://api.thecatapi.com/v1";

        private static HttpClient CreateHttp()
        {
#if __WASM__
            var handler = new Uno.UI.Wasm.WasmHttpHandler();
			var httpClient = new HttpClient(handler);
#else
            var httpClient = new HttpClient();

#endif
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/json"));
            httpClient.DefaultRequestHeaders.Add("x-api-key", "cac1a061-43b7-4e5a-975f-a1d77e135ef4");
            return httpClient;
        }

        public async Task<ObservableCollection<Cat>> FetchCats(int page, int limit)
        {
            using (var client = CreateHttp())
            {
                var url = $"https://api.thecatapi.com/v1/breeds?page={page}&limit={limit}";

                var response = await client.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responsePayload = await response.Content.ReadAsStringAsync();
                    return await Task.Run(() => {
                        return JsonConvert.DeserializeObject<ObservableCollection<Cat>>(responsePayload);
                     });
                }

                return new ObservableCollection<Cat>();
            }
         }

        public async Task<List<CatImage>> FetchCatImages(string catId, int page, int limit)
        {
            using (var client = CreateHttp())
            {
                var url = $"https://api.thecatapi.com/v1/images/search?breed_id={catId}&page={page}&limit={limit}";

                var response = await client.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responsePayload = await response.Content.ReadAsStringAsync();
                    return await Task.Run(() => {
                        return JsonConvert.DeserializeObject<List<CatImage>>(responsePayload);
                    });
                }

                return new List<CatImage>();
            }
        }
    }
}
