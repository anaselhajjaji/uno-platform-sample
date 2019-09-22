using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using UnoBench.Model.Domain;
using Refit;

namespace UnoBench.Model.Data.Net
{
    [Headers("x-api-key: cac1a061-43b7-4e5a-975f-a1d77e135ef4")]
    public interface ICatsApi
    {
        [Get("/breeds?page={page}&limit={limit}")]
        Task<ObservableCollection<Cat>> FetchCats(int page, int limit);

        [Get("/images/search?breed_id={catId}&page={page}&limit={limit}")]
        Task<List<CatImage>> FetchCatImages(string catId, int page, int limit);
    }
}
