using System;
using System.Collections.Generic;//ObjectModel;
using System.Linq;
using System.Text;
using Firebase.Database;
using System.Threading.Tasks;
using Xamarin.Forms;
using WiseMonitor.Models;
using Firebase.Database.Query;

namespace WiseMonitor.ViewModels
{
    public class VMProducts
    {
        FirebaseClient firebase = new FirebaseClient("https://wise-monitor-price-default-rtdb.asia-southeast1.firebasedatabase.app/");
        
        public async Task<List<Products>> GetAllJayaGrocer()
        {

            return (await firebase
              .Child("JayaGrocer")
              .OnceAsync<Products>()).Select(item => new Products
              {
                  name = item.Object.name,
                  price = item.Object.price
              }).ToList();

        }
        public async Task<List<Products>> GetAllSunshine()
        {

            return (await firebase
              .Child("Sunshine")
              .OnceAsync<Products>()).Select(item => new Products
              {
                  name = item.Object.name,
                  price = item.Object.price
              }).ToList();

        }

        public async Task<List<Products>> GetAllPacific()
        {

            return (await firebase
              .Child("Pacific")
              .OnceAsync<Products>()).Select(item => new Products
              {
                  name = item.Object.name,
                  price = item.Object.price
              }).ToList();

        }

        public async Task<List<Products>> GetFavourite()
        {

            return (await firebase
              .Child("Favourite")
              .OnceAsync<Products>()).Select(item => new Products
              {
                  name = item.Object.name,
                  price = item.Object.price
              }).ToList();

        }

        public async Task<List<Products>> GetShopList()
        {

            return (await firebase
              .Child("ShoppingList")
              .OnceAsync<Products>()).Select(item => new Products
              {
                  name = item.Object.name,
                  price = item.Object.price
              }).ToList();

        }

        public async Task DeleteSLproduct(Products model)
        {
            var name1 = model.name; 
            var toDeletePrdt = (await firebase
              .Child("ShoppingList")
              .OnceAsync<Products>()).Where(a => a.Object.name.Contains(name1)).FirstOrDefault();
            await firebase.Child("ShoppingList").Child(toDeletePrdt.Key).DeleteAsync();

        }

        public async Task DeleteFavproduct(Products model)
        {
            var name2 = model.name;
            var toDeletePrdt = (await firebase
              .Child("Favourite")
              .OnceAsync<Products>()).Where(b => b.Object.name.Contains(name2)).FirstOrDefault();
            await firebase.Child("Favourite").Child(toDeletePrdt.Key).DeleteAsync();

        }

        public async Task AddFav(string xname, string xprice)
        {

            await firebase
              .Child("Favourite")
              .PostAsync(new Products() { name = xname, price = xprice });
        }

        public async Task AddSL(string xname, string xprice)
        {

            await firebase
              .Child("ShoppingList")
              .PostAsync(new Products() { name = xname, price = xprice });
        }
    }
}
