using System.Collections.Generic;

namespace UnoBench.Model.Data.Entity
{
    public class CatEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PrincipalImage { get; set; }
        public IEnumerable<CatImageEntity> Images { get; set; }
    }
}
