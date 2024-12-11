using System.Windows.Controls;
using ValbyKino.ViewModels;

namespace ValbyKino.Views
{
    /// <summary>
    /// Interaction logic for AnnualRaportView.xaml
    /// </summary>
    public partial class AnnualReportView : Page
    {
        public AnnualReportView()
        {
            InitializeComponent();
            DataContext = new AnnualReportViewModel();
        }
    }
}
