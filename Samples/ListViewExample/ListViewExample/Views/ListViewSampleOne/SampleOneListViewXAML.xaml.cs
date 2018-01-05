using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ListViewExample.BAL.BusinessAccessLayer;
using ListViewExample.BAL.InterfaceLayer;
using ListViewExample.Models;
using Xamarin.Forms;

namespace ListViewExample.Views.ListViewSampleOne
{
    public partial class SampleOneListViewXAML : ContentPage
    {
        List<CountryDetails> listEmployeeData, temporarylistEmployeeData;

        int loadingRepitation = 0, totalLoadingRepitationRequired, allowedNumberOfItems = 20;
        public SampleOneListViewXAML()
        {
            listEmployeeData = new List<CountryDetails>();
            temporarylistEmployeeData = new List<CountryDetails>();
            //listEmployeeData = new List<EmployeeData>()
            //{
            //    new EmployeeData(){ EmployeeName="Venkata Sivaprasad Reddy Pulagam", EmployeeDesignation="Junoir Software Developer", EmployeeEmailId="pvsivapr@gmail", EmployeeExperience="1.5 years", EmployeeAddress="Hyderabad" },
            //    new EmployeeData(){ EmployeeName="Venkata Swamy Balaraju", EmployeeDesignation="Junoir Software Developer", EmployeeEmailId="venkysofttech@gmail", EmployeeExperience="1.5 years", EmployeeAddress="Gurgaon" },
            //    new EmployeeData(){ EmployeeName="Vaka Gopinath Reddy", EmployeeDesignation="Junoir Software Developer", EmployeeEmailId="gopinathreddyvaka@gmail", EmployeeExperience="1.5 years", EmployeeAddress="chennai" },
            //    new EmployeeData(){ EmployeeName="Venkateswarulu Nagella", EmployeeDesignation="Software Developer", EmployeeEmailId="nvenky0423@gmail", EmployeeExperience="3.5 years", EmployeeAddress="Hyderabad" },
            //    new EmployeeData(){ EmployeeName="Alokdas", EmployeeDesignation="Junior SEO", EmployeeEmailId="alokdas134@gmail", EmployeeExperience="1.5 years", EmployeeAddress="Hyderabad" },
            //    new EmployeeData(){ EmployeeName="Raju Pandem", EmployeeDesignation="SEO", EmployeeEmailId="rajupandem@gmail", EmployeeExperience="7 years", EmployeeAddress="Hyderabad" },
            //    new EmployeeData(){ EmployeeName="Sandeep Dulipalla", EmployeeDesignation="Project Manager", EmployeeEmailId="sandeepdullipalla@gmail", EmployeeExperience="8 years", EmployeeAddress="Hyderabad" },
            //    new EmployeeData(){ EmployeeName="Venkata Swamy Balaraju", EmployeeDesignation="Junoir Software Developer", EmployeeEmailId="venkysofttech@gmail", EmployeeExperience="1.5 years", EmployeeAddress="Gurgaon" },
            //    new EmployeeData(){ EmployeeName="Venkata Swamy Balaraju", EmployeeDesignation="Junoir Software Developer", EmployeeEmailId="venkysofttech@gmail", EmployeeExperience="1.5 years", EmployeeAddress="Gurgaon" }
            //};
            InitializeComponent();
            GetCountriesList();

            listViewDataDisplay.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) => 
            {
            };

            listViewDataDisplay.ItemAppearing += async (object sender, ItemVisibilityEventArgs e) => 
            {
                try
                {
                    var itemAppeared = e.Item as CountryDetails;
                    var itemAppearedIndex = listEmployeeData.IndexOf(itemAppeared);
                    System.Diagnostics.Debug.WriteLine(itemAppearedIndex.ToString());
                    if(itemAppearedIndex == ((loadingRepitation*allowedNumberOfItems)-1))
                    {
                        bool isItemAlreadyPresent = true;

                        for (int i = loadingRepitation * allowedNumberOfItems; i < ((loadingRepitation + 1) * allowedNumberOfItems); i++)
                        {
                            if (i < listEmployeeData.Count)
                            {
                                isItemAlreadyPresent = temporarylistEmployeeData.Contains(listEmployeeData[i]);
                                if (!isItemAlreadyPresent)
                                {

                                    temporarylistEmployeeData.Add(listEmployeeData[i]);
                                }
                                else
                                {

                                }
                            }
                        }
                        var UpdatedList = new List<CountryDetails>();
                        if (!isItemAlreadyPresent)
                        {
                            foreach (var item in temporarylistEmployeeData)
                            {
                                UpdatedList.Add(item);
                            }
                            loadingRepitation += 1;
                            //}
                            listViewDataDisplay.BeginRefresh();
                            listViewDataDisplay.ItemsSource = UpdatedList;//temporarylistEmployeeData;
                            await Task.Delay(1000);
                            listViewDataDisplay.ScrollTo(itemAppeared, ScrollToPosition.MakeVisible, false);
                            listViewDataDisplay.EndRefresh();
                        }
                    }
                }
                catch(Exception ex)
                {
                    var msg = ex.Message;
                }
            };

        }

        public void GetNoOfCyclesRequired(int totalNoOfItems)
        {
            try
            {
                totalLoadingRepitationRequired = totalNoOfItems / allowedNumberOfItems;
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public async void GetCountriesList()
        {
            try
            {
                using(IGetCountriesBAL getCountries = new GetCountriesBAL())
                {
                    var getCountriesResp = await getCountries.GetAllCountryDetails();
                    for (int i = 0; i < allowedNumberOfItems; i++ )
                    {
                        temporarylistEmployeeData.Add(getCountriesResp.CountryDetails[i]);
                    }
                    listViewDataDisplay.ItemsSource = temporarylistEmployeeData;
                    loadingRepitation = 1;
                    if (getCountriesResp.CountryDetails.Count > 0)
                    {
                        listEmployeeData = getCountriesResp.CountryDetails;
                        //listViewDataDisplay.ItemsSource = listEmployeeData;
                        GetNoOfCyclesRequired(listEmployeeData.Count);
                    }
                    else
                    {
                        await DisplayAlert("Alert", "List is empty", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }


    public class EmployeeData
    {
        public string EmployeeName { get; set; }

        public string EmployeeDesignation { get; set; }

        public string EmployeeEmailId { get; set; }

        public string EmployeeExperience { get; set; }

        public string EmployeeAddress { get; set; }
    }
}
