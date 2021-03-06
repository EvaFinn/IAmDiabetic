﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using ImDiabetic.Models;
using ImDiabetic.Views;
using ImDiabetic.Views.More;
using Xamarin.Forms;

namespace ImDiabetic.ViewModels
{
    public class MoreViewModel : BaseViewModel
    {
        private INavigation _navigation;
        private AppUser User { get; set; }
        public ICommand ButtonCommand => new Command<string>(MorePageButtons);

        public MoreViewModel(INavigation navigation, AppUser user)
        {
            _navigation = navigation;
            User = user;
        }

        async public void MorePageButtons(string page)
        {
            switch (page)
            {
                case "Achievement":
                    await _navigation.PushAsync(new AchievementsPage(User));
                    break;
                case "Education":
                    await _navigation.PushAsync(new EducationPage(User));
                    break;
                case "Quiz":
                    await _navigation.PushAsync(new QuizTopicsPage(User));
                    break;
            }
        }
    }
}
