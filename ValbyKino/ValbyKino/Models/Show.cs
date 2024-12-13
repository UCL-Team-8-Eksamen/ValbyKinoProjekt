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
        public double Admissions { get; set; }
        public double Price { get; set; } = 0;
        private string ya;
        private int screeningFormat;

        public string YA { get; set; }

        public Show(DateTime date, DateTime time, Version version, string screeningFormat, string category, int roomNumber, double price)
        {
            Date = date;
            Time = time;
            Version = version;
            ScreeningFormat = screeningFormat;
            Category = category;
            RoomNumber = roomNumber;
            Time = time;
            Price = price;
            //if (category.Equals("Børnebiffen")) YA = "1";
        }

        public Show(DateTime date, DateTime time, Version version, string screeningFormat, string category, int roomNumber, Movie movie)
        {
            Date = date;
            Time = time;
            Version = version;
            ScreeningFormat = screeningFormat;
            Category = category;
            RoomNumber = roomNumber;
            Movie = movie;
        }


        public Show()
        { }


    }
}
