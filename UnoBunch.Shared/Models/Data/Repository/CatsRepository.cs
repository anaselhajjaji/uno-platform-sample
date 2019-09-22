using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using UnoBench.Model.Data.Net;
using UnoBench.Model.Data.Repository;
using UnoBench.Model.Domain;

namespace UnoBench.Model.Data.Repository
{
    public class CatsRepository : ICatsRepository
    {
        readonly ICatsApi catsAPI;

        public CatsRepository(ICatsApi catsAPI)
        {
            this.catsAPI = catsAPI;
        }

        public async Task<ObservableCollection<Cat>> FetchCats(int page, int limit)
        {
            return await catsAPI?.FetchCats(page, limit);
        }

        public async Task<List<CatImage>> FetchCatImages(string catId, int page, int limit)
        {
            return await catsAPI?.FetchCatImages(catId, page, limit);
        }

        public async Task<ObservableCollection<Cat>> Populate(int page, int pageSize)
        {
            ObservableCollection<Cat> cats = await FetchCats(page, pageSize);
            await FetchCatsImages(cats);
            return cats;
        }

        private async Task FetchCatsImages(ObservableCollection<Cat> cats)
        {
            foreach (var cat in cats)
            {
                cat.PrincipalImage = "cat_161476_960_720.png";

                await FetchCatImages(cat);
            }
        }

        private async Task FetchCatImages(Cat cat)
        {
            cat.Images = await FetchCatImages(cat.Id, 0, 1);

            if (cat.Images != null && cat.Images.Count() != 0)
            {
                cat.PrincipalImage = cat.Images.First().Url;
            }
        }
    }
}
