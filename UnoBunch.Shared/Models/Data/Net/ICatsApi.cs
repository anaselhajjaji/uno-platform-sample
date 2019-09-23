using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UnoBench.Model.Domain;

namespace UnoBench.Model.Data.Net
{
    public interface ICatsApi
    {
        Task<ObservableCollection<Cat>> FetchCats(int page, int limit);

        Task<List<CatImage>> FetchCatImages(string catId, int page, int limit);
    }
}
