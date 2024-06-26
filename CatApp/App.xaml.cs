﻿using Aptabase.Maui;
using CatApp.Model.User;
using CatApp.View.COTD;
using CatApp.View.Endless;
using CatApp.View.TherapyMode;
using CatApp.ViewModel.COTD;
using CatApp.ViewModel.Endless;
using CatApp.ViewModel.TherapyMode;

namespace CatApp
{
    public partial class App : Application
    {
        // Analytics
        IAptabaseClient _aptabase;
        // Models
        public CatOfTheDayPageViewModel CotdViewModel { get; set; }
        public EndlessCatsPageViewModel EndlessCatsViewModel { get; set; }
        public TherapyModeViewModel TherapyViewModel { get; set; }
        // User model
        public UserModel UserModel { get; set; }

        public App(IAptabaseClient aptabase, CatOfTheDayPageViewModel cotdViewModel, EndlessCatsPageViewModel endlessCatsViewModel, TherapyModeViewModel therapyViewModel, UserModel userModel)
        {
            BindingContext = CotdViewModel = cotdViewModel;
            BindingContext = EndlessCatsViewModel = endlessCatsViewModel;
            BindingContext = TherapyViewModel = therapyViewModel;
            UserModel = userModel;
            InitializeComponent();

            // Analytics
            _aptabase = aptabase;

            TrackAppOpen();
            CheckReviewStatus(); 

            MainPage = new AppShell();
        }

        private async Task CheckReviewStatus()
        {
            var reviewStatus = await SecureStorage.Default.GetAsync("has_reviewed");
            Console.WriteLine($"");
            Console.WriteLine($"reviewStatus {reviewStatus}");

            if (reviewStatus == "true")
            {
                UserModel.HasReviewedApp = true;
            }
        }

        private void TrackAppOpen()
        {
            _aptabase.TrackEvent("app_started");
        }

        protected override void OnSleep()
        {
            // Stop audio and video playback here
            CotdViewModel.PauseAudioPlayback();
            EndlessCatsViewModel.PauseAudioPlayback();
            TherapyViewModel.PauseAudioPlayback();
        }
        protected override void OnResume()
        {
            if (MainPage is Shell shell)
            {
                // Get the current page
                var currentPage = shell.CurrentItem?.CurrentItem as IShellSectionController;
                var visiblePage = currentPage?.PresentedPage;

                // Restart audio
                if (visiblePage is CatOfTheDayPage)
                {
                    CotdViewModel.PlayAudioPlayback();
                }
                else if (visiblePage is EndlessCatsPage)
                {
                    EndlessCatsViewModel.PlayAudioPlayback();
                }
                else if (visiblePage is TherapyModePage)
                {
                    TherapyViewModel.PlayAudioPlayback();
                }
            }
        }
    }
}
