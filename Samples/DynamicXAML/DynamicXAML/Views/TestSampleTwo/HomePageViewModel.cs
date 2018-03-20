using System;

using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.Generic;

namespace DynamicXAML
{
    public class HomePageViewModel : BaseViewModel
    {
        public ICommand _CommandClicked { get; private set; }
        public ICommand AddMemoryClick { get; private set; }
        public INavigation Navigation { get; set; }
        //public List<CompanionsData> CompanionData { get; set; }
        public List<MemoriesData> MemoriesData { get; set; }

        public HomePageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            _CommandClicked = new Command(() => CommandClicked());
            AddMemoryClick = new Command(() => AddMemoryClicked());
            //CompanionData = new List<CompanionsData>()
            //{
            //    new CompanionsData(){ CompanionName="SivaPrasad", DateOfBirth="02/07/1992", MemoriesCount="30" },
            //    new CompanionsData(){ CompanionName="SivaPrasad Reddy", DateOfBirth="02/01/1992", MemoriesCount="10" },
            //    new CompanionsData(){ CompanionName="Venkata SivaPrasad", DateOfBirth="24/02/1992", MemoriesCount="20" },
            //    new CompanionsData(){ CompanionName="Venkata SivaPrasad Reddy", DateOfBirth="12/09/1992", MemoriesCount="2" },
            //    new CompanionsData(){ CompanionName="P Venkata SivaPrasad", DateOfBirth="09/08/1992", MemoriesCount="3" },
            //    new CompanionsData(){ CompanionName="P Venkata SivaPrasad Reddy", DateOfBirth="05/05/1992", MemoriesCount="5" },
            //    new CompanionsData(){ CompanionName="Pulagam Venkata SivaPrasad", DateOfBirth="016/06/1992", MemoriesCount="1" },
            //    new CompanionsData(){ CompanionName="Pulagam Venkata SivaPrasad Reddy", DateOfBirth="01/01/1992", MemoriesCount="9" }
            //};
            MemoriesData = new List<MemoriesData>()
            {
                new MemoriesData(){ LocationName="Pathimeda", DateOfMemory="02/07/1995", CompanionsNoCount="6" },
                new MemoriesData(){ LocationName="Jaipur", DateOfMemory="05/09/1995", CompanionsNoCount="7" },
                new MemoriesData(){ LocationName="Araku Valley", DateOfMemory="09/12/1995", CompanionsNoCount="5" },
                new MemoriesData(){ LocationName="Hyderabad", DateOfMemory="12/04/1995", CompanionsNoCount="6" },
                new MemoriesData(){ LocationName="Shiridi", DateOfMemory="16/08/2001", CompanionsNoCount="4" },
                new MemoriesData(){ LocationName="Delhi", DateOfMemory="11/05/1995", CompanionsNoCount="8" },
                new MemoriesData(){ LocationName="Agra", DateOfMemory="30/09/1995", CompanionsNoCount="3" },
                new MemoriesData(){ LocationName="Mathura", DateOfMemory="02/08/1995", CompanionsNoCount="7" },
                new MemoriesData(){ LocationName="Mount Abu", DateOfMemory="14/03/2014", CompanionsNoCount="2" }
            };
        }

        void AddMemoryClicked()
        {
            try
            {
                //Navigation.PushModalAsync(new CreateMemory());
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        void CommandClicked()
        {
            try
            {
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
    public class MemoriesData
    {
        public string LocationName { get; set; }

        public string DateOfMemory { get; set; }

        public string CompanionsNoCount { get; set; }
    }
}

