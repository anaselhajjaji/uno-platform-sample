using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UnoBench.Model.Data.Net;
using UnoBench.Model.Data.Repository;
using UnoBench.Model.Domain;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UnoBunch
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const int PageSize = 10;
        public ObservableCollection<Cat> Items { get; set; }

        protected ICatsRepository CatsRepo { get; set; }

        public async Task Populate()
        {
            await LoadItems();
        }

        private async Task LoadItems()
        {
            int page = Items.Count / PageSize;
            ObservableCollection<Cat> cats = await CatsRepo?.Populate(page, PageSize);
            foreach (var cat in cats)
            {
                Items.Add(cat);
            }
        }

        public MainPage()
        {
            Items = new ObservableCollection<Cat>();
            CatsRepo = new CatsRepository(new CatsApi());

            this.InitializeComponent();

            Loaded += Page_Loaded;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
           await Populate();
        }
    }
}
