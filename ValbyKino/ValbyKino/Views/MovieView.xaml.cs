using System.Windows.Controls;
using ValbyKino.ViewModels;

namespace ValbyKino.Views
{
    /// <summary>
    /// Interaction logic for MovieView.xaml
    /// </summary>
    public partial class MovieView : Page
    {
        public MovieView()
        {
            InitializeComponent();
            DataContext = new MovieViewModel();
        }
    }
}
