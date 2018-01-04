using System;
using System.Collections.Generic;

namespace ListViewExample.Models
{
    public class GetCountries
    {
        public string Message { get; set; }
        public string Code { get; set; }
        public List<CountryDetails> CountryDetails { get; set; }
    }

    public class CountryDetails
    {
        public string Tld { get; set; }
        public string Cca3 { get; set; }
        public string Ccn3 { get; set; }
        public string Region { get; set; }
        public string AltSpellings { get; set; }
        public string Cioc { get; set; }
        public string LandLocked { get; set; }
        public string Capital { get; set; }
        public string Area { get; set; }
        public string NameOfficial { get; set; }
        public string Currency { get; set; }
        public string Cca2 { get; set; }
        public string NameNativePapOfficial { get; set; }
        public string TranslationsDeuOfficial { get; set; }
        public string SubRegion { get; set; }
        public string CallingCode { get; set; }
    }
}
