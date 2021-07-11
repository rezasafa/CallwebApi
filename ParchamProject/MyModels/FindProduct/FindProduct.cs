using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParchamProject.MyModels.FindProduct
{
    public class Content
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Field
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Variant
    {
        public int id { get; set; }
        public string title { get; set; }
        public int price { get; set; }
        public int compare_price { get; set; }
        public object tax { get; set; }
        public object shipping { get; set; }
        public object weight { get; set; }
        public object length { get; set; }
        public object width { get; set; }
        public object height { get; set; }
        public int stock { get; set; }
        public object minimum { get; set; }
        public object maximum { get; set; }
        public string sku { get; set; }
        public object attachment { get; set; }
        public string type { get; set; }
        public List<string> status { get; set; }
    }

    public class Published
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

    public class Creator
    {
        public int id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public object nickname { get; set; }
        public object avatar { get; set; }
    }

    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
        public string caption { get; set; }
        public string description { get; set; }
        public object image { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public object rate { get; set; }
        public object rate_count { get; set; }
        public object password { get; set; }
        public object layout { get; set; }
        public bool commenting_enabled { get; set; }
        public object meta_title { get; set; }
        public object meta_description { get; set; }
        public object meta_robots { get; set; }
        public object redirect { get; set; }
        public int stats { get; set; }
        public object comments { get; set; }
        public int position { get; set; }
        public List<string> status { get; set; }
        public List<Content> contents { get; set; }
        public List<Field> fields { get; set; }
        public object images { get; set; }
        public object category { get; set; }
        public object categories { get; set; }
        public object filters { get; set; }
        public object attributes { get; set; }
        public List<Variant> variants { get; set; }
        public object expiration { get; set; }
        public Published published { get; set; }
        public Created created { get; set; }
        public Creator creator { get; set; }
    }

    public class RootObject
    {
        public bool success { get; set; }
        public Product product { get; set; }
    }
}
