using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiseMonitor.Models;
using WiseMonitor.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WiseMonitor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrowseStore : TabbedPage
    {
        readonly VMProducts vmProduct = new VMProducts();
        public BrowseStore()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var jgPrdt = await vmProduct.GetAllJayaGrocer();
            JayaGrocerResults.ItemsSource = jgPrdt;
            var pacPrdt = await vmProduct.GetAllPacific();
            PacificResults.ItemsSource = pacPrdt;
            var sunPrdt = await vmProduct.GetAllSunshine();
            SunshineResults.ItemsSource = sunPrdt;
        }

        private async void Handle_JGSBTextChanged(object sender, TextChangedEventArgs e)
        {
            JayaGrocerResults.BeginRefresh();
            var lst = await vmProduct.GetAllJayaGrocer();
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                JayaGrocerResults.ItemsSource = lst;
            }
            else
            {
                JayaGrocerResults.ItemsSource = lst.Where(a => a.name.ToLower().Contains(JayaGrocerSearchBar.Text.ToLower()));
            }
            JayaGrocerResults.EndRefresh();
        }

        private async void Handle_PSBTextChanged(object sender, TextChangedEventArgs e)
        {
            PacificResults.BeginRefresh();
            var lst = await vmProduct.GetAllPacific();
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                PacificResults.ItemsSource = lst;
            }
            else
            {
                PacificResults.ItemsSource = lst.Where(b => b.name.ToLower().Contains(PacificSearchBar.Text.ToLower()));
            }
            PacificResults.EndRefresh();
        }

        private async void Handle_SSBTextChanged(object sender, TextChangedEventArgs e)
        {
            SunshineResults.BeginRefresh();
            var lst = await vmProduct.GetAllSunshine();
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                SunshineResults.ItemsSource = lst;
            }
            else
            {
                SunshineResults.ItemsSource = lst.Where(c => c.name.ToLower().Contains(SunshineSearchBar.Text.ToLower()));
            }
            SunshineResults.EndRefresh();
        }

        private async void Handle_SB4ATextChanged(object sender, TextChangedEventArgs e)
        {
            JayaGrocerResults2.BeginRefresh();
            PacificResults2.BeginRefresh();
            SunshineResults2.BeginRefresh();

            var JGlst = await vmProduct.GetAllJayaGrocer();
            var Paclst = await vmProduct.GetAllPacific();
            var sunlst = await vmProduct.GetAllSunshine();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                JayaGrocerResults2.ItemsSource = JGlst;
                PacificResults2.ItemsSource = Paclst;
                SunshineResults2.ItemsSource = sunlst;
            }
            else
            {
                JayaGrocerResults2.ItemsSource = JGlst.Where(d => d.name.ToLower().Contains(SearchBar4All.Text.ToLower())); ;
                PacificResults2.ItemsSource = Paclst.Where(f => f.name.ToLower().Contains(SearchBar4All.Text.ToLower())); ;
                SunshineResults2.ItemsSource = sunlst.Where(g => g.name.ToLower().Contains(SearchBar4All.Text.ToLower())); ;
            }
            JayaGrocerResults2.EndRefresh();
            PacificResults2.EndRefresh();
            SunshineResults2.EndRefresh();
        }

        private async void JayaGrocerResults2_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (JayaGrocerResults2.SelectedItem != null)
                {
                    Products product = (Products)e.SelectedItem;
                    if (product != null)
                    {
                        var display = await DisplayActionSheet(product.name, "Cancel",
                        null, new string[] { "Favourite", "Add to Shopping List" });
                        var passname = product.name;
                        var passprice = product.price;
                        if (display.Equals("Favourite"))
                        {
                            await vmProduct.AddFav(passname, passprice);
                            await DisplayAlert("Success", "Product Added to Favourite", "OK");
                        }
                        else if (display.Equals("Add to Shopping List"))
                        {
                            await vmProduct.AddSL(passname, passprice);
                            await DisplayAlert("Success", "Product Added to Shopping List", "OK");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            JayaGrocerResults2.SelectedItem = null;
        }

        private async void PacificResults2_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (PacificResults2.SelectedItem != null)
                {
                    Products product = (Products)e.SelectedItem;
                    if (product != null)
                    {
                        var display = await DisplayActionSheet(product.name, "Cancel",
                        null, new string[] { "Favourite", "Add to Shopping List" });
                        var passname = product.name;
                        var passprice = product.price;
                        if (display.Equals("Favourite"))
                        {
                            await vmProduct.AddFav(passname,passprice);
                            await DisplayAlert("Success", "Product Added to Favourite", "OK");
                            
                        }
                        else if (display.Equals("Add to Shopping List"))
                        {
                            await vmProduct.AddSL(passname, passprice);
                            await DisplayAlert("Success", "Product Added to Shopping List", "OK");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            PacificResults2.SelectedItem = null;
        }

        private async void SunshineResults2_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (SunshineResults2.SelectedItem != null)
                {
                    Products product = (Products)e.SelectedItem;
                    if (product != null)
                    {
                        var display = await DisplayActionSheet(product.name, "Cancel",
                        null, new string[] { "Favourite", "Add to Shopping List" });
                        var passname = product.name;
                        var passprice = product.price;
                        if (display.Equals("Favourite"))
                        {
                            await vmProduct.AddFav(passname, passprice);
                            await DisplayAlert("Success", "Product Added to Favourite", "OK");
                        }
                        else if (display.Equals("Add to Shopping List"))
                        {
                            await vmProduct.AddSL(passname, passprice);
                            await DisplayAlert("Success", "Product Added to Shopping List", "OK");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            SunshineResults2.SelectedItem = null;
        }

        private async void btnRefresh_Clicked(object sender, EventArgs e)
        {
            JayaGrocerResults2.ItemsSource = null;
            PacificResults2.ItemsSource = null;
            SunshineResults2.ItemsSource = null;

            var jgPrdt = await vmProduct.GetAllJayaGrocer();
            JayaGrocerResults2.ItemsSource = jgPrdt;
            var pacPrdt = await vmProduct.GetAllPacific();
            PacificResults2.ItemsSource = pacPrdt;
            var sunPrdt = await vmProduct.GetAllSunshine();
            SunshineResults2.ItemsSource = sunPrdt;
        }

        private async void btnRefreshJG_Clicked(object sender, EventArgs e)
        {
            JayaGrocerResults.ItemsSource = null;
            var jgPrdt = await vmProduct.GetAllJayaGrocer();
            JayaGrocerResults.ItemsSource = jgPrdt;
        }

        private async void btnRefreshPac_Clicked(object sender, EventArgs e)
        {
            PacificResults.ItemsSource = null;
            var pacPrdt = await vmProduct.GetAllPacific();
            PacificResults.ItemsSource = pacPrdt;
        }

        private async void btnRefreshSun_Clicked(object sender, EventArgs e)
        {
            SunshineResults.ItemsSource = null;
            var sunPrdt = await vmProduct.GetAllSunshine();
            SunshineResults.ItemsSource = sunPrdt;
        }
    }
}