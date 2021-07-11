using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParchamProject.MyModels.Product
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
            public List<string> status { get; set; }
            public decimal price { get; set; }
            public decimal compare_price { get; set; }
            public int stock { get; set; }
            public string sku { get; set; }
            public object minimum { get; set; }
            public object maximum { get; set; }
            public object weight { get; set; }
            public object width { get; set; }
            public object length { get; set; }
            public object height { get; set; }
            public string title { get; set; }
            public string type { get; set; }
        }
        public class RootObject
        {
            public string title { get; set; }
            public string caption { get; set; }
            public string description { get; set; }
            public List<Content> contents { get; set; }
            public string image { get; set; }
            public List<string> images { get; set; }
            public bool commenting_enabled { get; set; }
            public List<Field> fields { get; set; }
            public List<Variant> variants { get; set; }
            public string slug { get; set; }
            public object published { get; set; }
            public object expiration { get; set; }
            public object password { get; set; }
            public object meta_title { get; set; }
            public object meta_description { get; set; }
            public object meta_robots { get; set; }
            public object redirect { get; set; }
            public List<int> filters { get; set; }
            public List<int> categories { get; set; }
            public List<string> status { get; set; }
        }
    
}
