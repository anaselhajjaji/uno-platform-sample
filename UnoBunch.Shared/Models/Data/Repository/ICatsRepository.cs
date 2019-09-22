using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using UnoBench.Model.Domain;

namespace UnoBench.Model.Data.Repository
{
    public interface ICatsRepository
    {
        Task<ObservableCollection<Cat>> FetchCats(int page, int limit);

        Task<List<CatImage>> FetchCatImages(string catId, int page, int limit);

        Task<ObservableCollection<Cat>> Populate(int page, int pageSize);
    }
}
