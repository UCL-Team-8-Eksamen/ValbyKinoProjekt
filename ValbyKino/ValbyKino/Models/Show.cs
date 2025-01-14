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
        public string ScreeningFormat { get; set; }
        public string Category { get; set; }
        public int RoomNumber { get; set; }
        public Movie Movie { get; set; }
        public int ShowID { get; set; } = 0;
        public double Admissions { get; set; } = 0;
        public double Price { get; set; } = 0;

        public string YA { get; set; }

        public Show(DateTime date, DateTime time, Version version, string screeningFormat, string category, int roomNumber, double price, int admissions)
        {
            Date = date;
            Time = time;
            Version = version;
            ScreeningFormat = screeningFormat;
            Category = category;
            RoomNumber = roomNumber;
            Price = price;
            Admissions = admissions;
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

        public Show(DateTime date, DateTime time, Version version, string screeningFormat, string category, int roomNumber, Movie movie, double price, int admissions)
        {
            Date = date;
            Time = time;
            Version = version;
            ScreeningFormat = screeningFormat;
            Category = category;
            RoomNumber = roomNumber;
            Movie = movie;
            Price = price;
            Admissions = admissions;
        }



        public Show()
        { }


    }
}
