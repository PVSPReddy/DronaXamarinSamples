using System;
using System.Threading.Tasks;
using ListViewExample.Models;

namespace ListViewExample.BAL.InterfaceLayer
{
    public interface IGetCountriesBAL : IDisposable
    {
        Task<GetCountries> GetAllCountryDetails();
    }
}
