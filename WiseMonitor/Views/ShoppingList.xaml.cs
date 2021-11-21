using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WiseMonitor.ViewModels;
using WiseMonitor.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WiseMonitor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingList : ContentPage
    {
        readonly VMProducts vmProduct = new VMProducts();
        Products products = new Products();
        string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notes.txt");
        public ShoppingList()
        {
            InitializeComponent();
            // Read the file.
            if (File.Exists(_fileName))
            {
                editor.Text = File.ReadAllText(_fileName);
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var Prdt = await vmProduct.GetShopList();
            Shoppinglist1.ItemsSource = Prdt;
        }

        void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // Save the file.
            File.WriteAllText(_fileName, editor.Text);
        }

        void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            // Delete the file.
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
            editor.Text = string.Empty;
        }

        private async void Delete_Invoked(object sender, EventArgs e)
        {
            SwipeItem item = sender as SwipeItem;
            Products model = item.BindingContext as Products;
            await vmProduct.DeleteSLproduct(model);
            var Prdt = await vmProduct.GetShopList();
            Shoppinglist1.ItemsSource = Prdt;           
        }

        private async void Favourite_Invoked(object sender, EventArgs e)
        {
            SwipeItem item = sender as SwipeItem;
            Products model = item.BindingContext as Products;
            await vmProduct.AddFav(model.name, model.price);
            await DisplayAlert("Attention", "Product is added to Favourite", "OK");
        }
    }
    
}