using System;
using System.Collections.Generic;

namespace ListViewExample.JSONServiceLayer.JSONModels
{
    public class GetCountriesCls
    {
        public string message { get; set; }
        public string code { get; set; }
        public List<CountryDetail> country_details { get; set; }
    }

    public class CountryDetail
    {
        public string tld { get; set; }
        public string cca3 { get; set; }
        public string ccn3 { get; set; }
        public string region { get; set; }
        public string altSpellings { get; set; }
        public string cioc { get; set; }
        public string landlocked { get; set; }
        public string capital { get; set; }
        public string area { get; set; }
        public string name_official { get; set; }
        public string currency { get; set; }
        public string cca2 { get; set; }
        public string name_native_pap_official { get; set; }
        public string translations_deu_official { get; set; }
        public string subregion { get; set; }
        public string callingCode { get; set; }
    }
}
