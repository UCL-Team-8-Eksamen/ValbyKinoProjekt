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
        public int ScreeningFormat { get; set; }
        public string Category { get; set; }
        public int RoomNumber { get; set; }
        public Movie Movie { get; set; }

        public Show(DateTime date, DateTime time, Version version, int screeningFormat, string category, int roomNumber)
        {
            Date = date;
            Time = time;
            Version = version;
            ScreeningFormat = screeningFormat;
            Category = category;
            RoomNumber = roomNumber;
        }

        public Show(DateTime date, DateTime time, Version version, int screeningFormat, string category, int roomNumber, Movie movie)
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
