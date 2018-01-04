using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ListViewExample.BAL.InterfaceLayer;
using ListViewExample.JSONServiceLayer;
using ListViewExample.JSONServiceLayer.JSONModels;
using ListViewExample.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ListViewExample.BAL.BusinessAccessLayer
{
    public class GetCountriesBAL : IGetCountriesBAL
    {
        public GetCountriesBAL(){}

        public async Task<GetCountries> GetAllCountryDetails()
        {
            GetCountries getCountriesResponse = new GetCountries();
            List<CountryDetails> countryDetails = new List<CountryDetails>();
            try
            {
                var responseStr = await HttpClientSource<GetCountriesCls>.RetriveDataWithGetAsync("");
                //var remainderResponse = JsonConvert.DeserializeObject<ArchiveListRes>(responseStr);

                getCountriesResponse.Message = responseStr.message;
                getCountriesResponse.Code = responseStr.message;

                foreach (var item in responseStr.country_details)
                {
                    var detailItem = new CountryDetails()
                    {
                        AltSpellings=item.altSpellings,
                        Area=item.area,
                        CallingCode=item.callingCode,
                        Capital=item.capital,
                        Cca2=item.cca2,
                        Cca3=item.cca3,
                        Ccn3=item.ccn3,
                        Cioc=item.cioc,
                        Currency=item.currency,
                        LandLocked=item.landlocked,
                        NameNativePapOfficial=item.name_native_pap_official,
                        NameOfficial=item.name_official,
                        Region=item.region,
                        SubRegion=item.subregion,
                        Tld=item.tld,
                        TranslationsDeuOfficial=item.translations_deu_official
                    };
                    countryDetails.Add(detailItem);
                }

                getCountriesResponse.CountryDetails = countryDetails;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                getCountriesResponse = null;
            }
            return getCountriesResponse;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CountryBAL() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}

