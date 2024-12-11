using System.Windows.Controls;
using ValbyKino.ViewModels;

namespace ValbyKino.Views
{
    /// <summary>
    /// Interaction logic for ShowView.xaml
    /// </summary>
    public partial class ShowView : Page
    {
        public ShowView()
        {
            InitializeComponent();
            DataContext = new ShowViewModel();

        }
    }
}
