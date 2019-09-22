using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace UnoBench.Model.Domain
{
    public class Cat : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string principalImage;

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PrincipalImage
        {
            get
            {
                return principalImage;
            }
            set
            {
                principalImage = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<CatImage> Images { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
