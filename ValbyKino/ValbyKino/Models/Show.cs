using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ValbyKino.Models
{
    public enum Version
    {
        VO,
        DB,
        ST
    }


    public class Show
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public Version Version { get; set; }
        //int?
        public string ScreeningFormat { get; set; }
        public string Category { get; set; }
        public int RoomNumber { get; set; }
        public Movie Movie { get; set; }
        public int ShowID { get; set; }
        public int Admissions { get; set; }
        public int Price { get; set; } = 0;
        private string ya;
        public string YA { get; set; }

        public Show(DateTime date, DateTime time, Version version, string screeningFormat, string category, int roomNumber)
        {
            Date = date;
            Version = version;
            ScreeningFormat = screeningFormat;
            Category = category;
            RoomNumber = roomNumber;
            Time = time;
            //if (category.Equals("Børnebiffen")) YA = "1";
        }

        public Show(DateTime date, DateTime time, Version version, string screeningFormat, string category, int roomNumber, Movie movie)
        {
            Date = date;
            Version = version;
            ScreeningFormat = screeningFormat;
            Category = category;
            RoomNumber = roomNumber;
            Time = time;
            Movie = movie;
        }

        public Show()
        { }
    }
}
