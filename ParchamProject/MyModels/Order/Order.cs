using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParchamProject.MyModels.Order
{
    public class Country
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class State
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Contact
    {
        public string name { get; set; }
        public string mobile { get; set; }
        public object phone { get; set; }
        public string email { get; set; }
        public Country country { get; set; }
        public State state { get; set; }
        public City city { get; set; }
        public string zipcode { get; set; }
        public string address { get; set; }
        public object latitude { get; set; }
        public object longitude { get; set; }
    }

    public class Item
    {
        public int product_id { get; set; }
        public string title { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public object weight { get; set; }
        public object shipping { get; set; }
        public object discount { get; set; }
        public string sku { get; set; }
        public object tax { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public object nickname { get; set; }
        public object national_code { get; set; }
        public object avatar { get; set; }
    }

    public class Created
    {
        public string year { get; set; }
        public string month { get; set; }
        public string month_name { get; set; }
        public string day { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string universal { get; set; }
        public int timestamp { get; set; }
        public string subtract { get; set; }
        public bool past { get; set; }
    }

    public class Order
    {
        public int id { get; set; }
        public object description { get; set; }
        public List<string> status { get; set; }
        public int quantity { get; set; }
        public int weight { get; set; }
        public int shipping { get; set; }
        public int subtotal { get; set; }
        public int discount { get; set; }
        public int tax { get; set; }
        public int price { get; set; }
        public int remaining_price { get; set; }
        public string ip { get; set; }
        public Contact contact { get; set; }
        public List<Item> items { get; set; }
        public object coupons { get; set; }
        public object shipments { get; set; }
        public object payments { get; set; }
        public User user { get; set; }
        public object label { get; set; }
        public Created created { get; set; }
        public object due_date { get; set; }
        public object delivery { get; set; }
        public object updated { get; set; }
    }

    public class RootObject
    {
        public bool success { get; set; }
        public Order order { get; set; }
    }
}
