using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using WiseMonitor.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WiseMonitor.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingView : ContentPage
	{
		public SettingView()
		{
			InitializeComponent();
			switch (Settings.Theme)
			{
				case 0:
					RadioButtonSystem.IsChecked = true;		
					break;
				case 1:
					RadioButtonLight.IsChecked = true;
					break;
				case 2:
					RadioButtonDark.IsChecked = true;
					break;
			}
		}

		bool loaded;
		protected override void OnAppearing()
		{
			base.OnAppearing();
			loaded = true;
		}

		void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
		{ 
			if (!loaded)
				return;

			if (!e.Value)
				return;

			var val = (sender as RadioButton)?.Value as string;
			if (string.IsNullOrWhiteSpace(val))
				return;

			switch (val)
			{
				case "System":
					Settings.Theme = 0;
					break;
				case "Light":
					Settings.Theme = 1;
					break;
				case "Dark":
					Settings.Theme = 2;
					break;
			}

			TheTheme.SetTheme();
		}
	}
}