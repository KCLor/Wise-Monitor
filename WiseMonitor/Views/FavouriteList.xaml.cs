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
    public partial class FavouriteList : ContentPage
    {
        readonly VMProducts vmProduct = new VMProducts();

        public  FavouriteList()
        {
            InitializeComponent();          
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var Prdt = await vmProduct.GetFavourite();
            Favlist.ItemsSource = Prdt;
        }

            private async void btnRefreshlist_Clicked(object sender, EventArgs e)
        {
            Favlist.ItemsSource = null;
            var Prdt = await vmProduct.GetFavourite();
            Favlist.ItemsSource = Prdt;
        }

        private void Favlist_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private async void addToSL_Invoked(object sender, EventArgs e)
        { 
            SwipeItem item = sender as SwipeItem;
            Products model = item.BindingContext as Products;
            await vmProduct.AddSL(model.name, model.price);
            await DisplayAlert("Success", "Product Added to Shopping List", "OK");
        }

        private async void delete_Invoked(object sender, EventArgs e)
        {
            SwipeItem item = sender as SwipeItem;
            Products model = item.BindingContext as Products;
            await vmProduct.DeleteFavproduct(model);
            var Prdt = await vmProduct.GetFavourite();
            Favlist.ItemsSource = Prdt;
        }
    }
}