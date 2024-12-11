using System.Windows;
using ValbyKino.Views;

namespace ValbyKino
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new LoginView();

        }



        //Når brugeren klikker på knappen, erstattes indholdet af Main med MovieView, hvilket betyder, at MovieView nu bliver vist på hovedsiden.
        private void Button_Click_Movie(object sender, RoutedEventArgs e)
        {
            //Main refererer  til et element/Frame på hovedsiden , som bruges til at vise indhold.
            //Content er en egenskab på dette Main-element, som bestemmer, hvad der vises.
            //new MovieView() opretter en ny instans/objekt af MovieView, 
            //Når du klikker på knappen i min applikation, skaber new MovieView() en ny instans af MovieView, som derefter bliver vist i brugergrænsefladen.
            Main.Content = new MovieView();
        }

        //Når brugeren klikker på knappen, erstattes indholdet af Main med ShowView, hvilket betyder, at ShowView nu bliver vist på hovedsiden.
        private void Button_Click_Forestalling(object sender, RoutedEventArgs e)
        {

            Main.Content = new ShowView();

        }

        private void Button_Click_AnnualRapport(object sender, RoutedEventArgs e)
        {
            Main.Content = new AnnualReportView();
        }
    }
}