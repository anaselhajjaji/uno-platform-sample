using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using UnoBench.Model.Data.Entity;
using UnoBench.Model.Data.Net;
using UnoBench.Model.Data.Repository;
using UnoBench.Model.Domain;

namespace UnoBench.Model.Data.Repository
{
    public class CatsRepository : ICatsRepository
    {
        readonly ICatsApi catsAPI;
        IMapper mapper;

        public CatsRepository(ICatsApi catsAPI)
        {
            InitializeAutomapper();
            this.catsAPI = catsAPI;
        }

        void InitializeAutomapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CatEntity, Cat>();
                cfg.CreateMap<CatImageEntity, CatImage>();
            });
            mapper = configuration.CreateMapper();
        }

        public async Task<ObservableCollection<Cat>> FetchCats(int page, int limit)
        {
            ObservableCollection<CatEntity> cats = await catsAPI?.FetchCats(page, limit);
            return new ObservableCollection<Cat>(cats?.Select(x => mapper.Map<CatEntity, Cat>(x)));
        }

        public async Task<List<CatImage>> FetchCatImages(string catId, int page, int limit)
        {
            List<CatImageEntity> catImages = await catsAPI?.FetchCatImages(catId, page, limit);
            return new List<CatImage>(catImages?.Select(x => mapper.Map<CatImageEntity, CatImage>(x)));
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
