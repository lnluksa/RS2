using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace RS2
{
    /*
     * Sadrzi konstante i metode koje sve klase treba da vide.
     * U sustini, sluzi kao alat za razne kalkulacije.
     */
    public class Constants
    {
        // konvertuje DateTime u sekunde (potrebno nam je kod prenosa DateTime-a po stanicama)
        public static long convertDateTimeIntoSecs(DateTime value)
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long timeStamp = (long)(value - unixEpoch).TotalSeconds;
            Debug.WriteLine("generisane sekunde = " + timeStamp);
            
            return timeStamp;
            
        }

        // konvertuje sekunde u DateTime
        public static DateTime convertSecondsToDateTime(long seconds)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return unixEpoch.AddSeconds(seconds);
        }
    }
}