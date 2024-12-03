using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Version Version { get; set; }
        public int ScreeningFormat { get; set; }
        public string Category { get; set; }
        public int RoomNumber { get; set; }
        public Movie Movie { get; set; }

        public Show(DateTime date, Version version, int screeningFormat, string category, int roomNumber)
        {
            Date = date;
            Version = version;
            ScreeningFormat = screeningFormat;
            Category = category;
            RoomNumber = roomNumber;
        }
        
        public Show()
        { }
    }
}
