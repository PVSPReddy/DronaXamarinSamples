﻿using System;
using AdMob.DependencyServices;
using AdMob.Droid.DependencyServices;
using Android.Gms.Ads;
using Xamarin.Forms;

[assembly: Dependency(typeof(AdInterstitial_Droid))]
namespace AdMob.Droid.DependencyServices
{
    public class AdInterstitial_Droid : IAdInterstitial
    {
        InterstitialAd interstitialAd;

        public AdInterstitial_Droid()
        {
            interstitialAd = new InterstitialAd(Android.App.Application.Context);

            // TODO: change this id to your admob id
            interstitialAd.AdUnitId = "Your AdMob id";
            LoadAd();
        }

        void LoadAd()
        {
            var requestbuilder = new AdRequest.Builder();
            interstitialAd.LoadAd(requestbuilder.Build());
        }

        public void ShowAd()
        {
            if (interstitialAd.IsLoaded)
                interstitialAd.Show();

            LoadAd();
        }
    }
}