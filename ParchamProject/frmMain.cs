using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using ParchamProject.MyModels.Product;

namespace ParchamProject
{
    public partial class frmMain : Form
    {

        OleDbConnection con = new OleDbConnection();
        OleDbCommand com = new OleDbCommand();
        OleDbDataAdapter dAdapter = new OleDbDataAdapter();
        DataSet dSet = new DataSet();
        OleDbDataReader dReader;
        string pdp;
        string strToday = "";

        private bool get_Token()
        {
            var requests = (HttpWebRequest)WebRequest.Create(Properties.Settings.Default.SessionWeb);
            var postData = "{\n    \"username\": \"" + txtUserName.Text + "\",\n    \"password\": \"" + txtPassword.Text + "\"\n}";
            var data = Encoding.ASCII.GetBytes(postData);

            requests.Method = "POST";
            requests.ContentType = "application/json";
            requests.ContentLength = data.Length;

            using (var stream = requests.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response2;
            string responseString;

            try
            {
                response2 = (HttpWebResponse)requests.GetResponse();
                responseString = new StreamReader(response2.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show("اطلاعات اشتباه است");
                return false;
            }

            //dynamic dyn_rslt = JsonConvert.DeserializeObject(responseString);
            var success = "";
            var description = "";
            var token = "";
            dynamic json = JObject.Parse(responseString);
            success = json.success;
            description = json.description;
            token = json.token;

            txtToken.Text = token;
            var k = new Persiandttm.Kernel();
            strToday = k.Get_Tarikh();

            return true;
        }

        private void load_Sepehr()
        {
            var k = new Persiandttm.Kernel();
            txtTadt.Text = k.Get_Tarikh();
            var dt = new DataTable();
            var myConString = Properties.Settings.Default.MyConString;
            var SepehrConString = Properties.Settings.Default.SepehrConString;
            // Read From Sepehr app
            var Query = "" +
                " SELECT InvQAll.Merch_Code, InvQAll.Merch_Name, Sum(InvQAll.Qtty) AS SumOfQtty, Merchs.Sales_Price  /10 AS Sales_Price " +
                " FROM InvQAll INNER JOIN Merchs ON InvQAll.Merch_Code = Merchs.Merch_Code " +
                " WHERE Merchs.Merch_Type1 = -1 " +
                " GROUP BY InvQAll.Merch_Code, InvQAll.Merch_Name, Merchs.Sales_Price;";
            pdp = SepehrConString;
            dSet = new DataSet();
            Command(Query);
            SepehrDGV.ReadOnly = true;
            dt = dSet.Tables[0];
            SepehrDGV.DataSource = dt;

            SepehrDGV.Columns[0].HeaderText = "کد کالا";
            SepehrDGV.Columns[1].HeaderText = "نام کالا";
            SepehrDGV.Columns[2].HeaderText = "موجودی";
            SepehrDGV.Columns[3].HeaderText = "قیمت";

            SepehrDGV.Columns[0].Width = 30;
            SepehrDGV.Columns[1].Width = 80;
            SepehrDGV.Columns[2].Width = 20;
            SepehrDGV.Columns[3].Width = 50;


            //registerSepehr_to_appdb();
        }

        private void AutoStatus(bool Stop)
        {
            //var k = new Persiandttm.Kernel();
            //if (strToday != k.Get_Tarikh())
            //{
            //    get_Token();
            //}
            //read_Site_Factors();
            //update_site();

            
            if (rb1.Checked) timer1.Interval = 60 * 60 * 1000;
            if (rb2.Checked) timer1.Interval = 120 * 60 * 1000;
            if (rb30.Checked) timer1.Interval = 30 * 60 * 1000;

            btnStopAuto.Enabled = !Stop;
            timer1.Enabled = !Stop;
            btnAuto.Enabled = Stop;
            btnGetToken.Enabled = Stop;
            btnReadFactors.Enabled = Stop;
            btnReadSite.Enabled = Stop;
            btnRegister.Enabled = Stop;
            btnFix.Enabled = Stop;
            txtAzdt.Enabled = Stop;
            txtTadt.Enabled = Stop;
            txtUserName.Enabled = Stop;
            txtPassword.Enabled = Stop;
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private void Connection(string constring)
        {
            con.Close();
            try
            {
                con.ConnectionString = constring;
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Command(string sqlCommand)
        {
            try
            {
                Connection(pdp);
                dSet.Clear();
                com.Connection = con;
                com.CommandText = sqlCommand;
                dAdapter.SelectCommand = com;
                dAdapter.Fill(dSet);//, TableName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ExecuteCommand(string sqlCommand)
        {
            try
            {
                Connection(pdp);
                //dSet.Clear();
                com.Connection = con;
                com.CommandText = sqlCommand;
                com.ExecuteReader();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(sqlCommand);
                MessageBox.Show(ex.Message);
            }
        }
        private string Get_ExecuteCommand(string sqlCommand)
        {
            try
            {
                //dSet.Clear();
                Connection(pdp);
                
                com.Connection = con;
                com.CommandText = sqlCommand;
                string strval = "";
                strval = com.ExecuteScalar().ToString();
                con.Close();
                return strval;
            }
            catch (Exception ex)
            {
                con.Close();
                //MessageBox.Show(ex.Message);
                return "";
            }
        }

        private void registerSepehr_to_appdb()
        {
            var pc = new PersianCalendar();
            // DELETE ALL PRODUCT ON APP DB
            pdp = Properties.Settings.Default.MyConString;
            ExecuteCommand(" DELETE FROM Sepehr ");
            progressBar1.Maximum = SepehrDGV.RowCount ;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            for (int i = 0; i < SepehrDGV.RowCount; i++)
            {
                var cdt = DateTime.Now;
                var dt = pc.GetYear(cdt) + "/" + pc.GetMonth(cdt).ToString().PadLeft(2, '0') + "/" + pc.GetDayOfMonth(cdt).ToString().PadLeft(2, '0');
                var q = "INSERT INTO Sepehr (Merch_Code,Merch_Name,SumOfQtty,Sales_Price)" +
                    " VALUES ('" + SepehrDGV[0, i].Value.ToString() + "'," +
                    "'" + SepehrDGV[1, i].Value.ToString() + "'," +
                    "" + SepehrDGV[2, i].Value.ToString() + "," +
                    "" + SepehrDGV[3, i].Value.ToString() + "" +
                    ")";
                pdp = Properties.Settings.Default.MyConString;
                ExecuteCommand(q);
                progressBar1.Value += 1;
            }
        }
        private void register_Varient_to_appdb()
        {
            var dtable = new DataTable();
            var myConString = Properties.Settings.Default.MyConString;
            // Read From  app
            var Query = "" +
                " SELECT WebSite.Merch_Code, WebSite.Merch_Name, WebSite.SumOfQtty, WebSite.Sales_Price, WebSite.Merch_Code_Site FROM WebSite;";
            pdp = myConString;
            dSet = new DataSet();
            Command(Query);
            WebDGV.ReadOnly = true;
            dtable = dSet.Tables[0];
            WebDGV.DataSource = dtable;

            WebDGV.Columns[0].HeaderText = "کد کالا";
            WebDGV.Columns[1].HeaderText = "نام کالا";
            WebDGV.Columns[2].HeaderText = "موجودی";
            WebDGV.Columns[3].HeaderText = "قیمت";
            WebDGV.Columns[4].HeaderText = "کد سایت";

            WebDGV.Columns[0].Width = 30;
            WebDGV.Columns[1].Width = 80;
            WebDGV.Columns[2].Width = 20;
            WebDGV.Columns[3].Width = 30;
            WebDGV.Columns[4].Width = 50;

            pdp = Properties.Settings.Default.MyConString;
            ExecuteCommand("DELETE FROM Merchs ");

            //txtToken.Text = "bac7e831f2e649e5834d168b47e3662c";

            if (txtToken.Text == "")
            {
                if (!get_Token())
                {
                    MessageBox.Show("اشکال در دیافت " + " Token");
                    return;
                }

            }

            var pc = new PersianCalendar();
            progressBar1.Maximum = WebDGV.RowCount;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            for (int Counter = 0; Counter < WebDGV.RowCount; Counter++)
            {
                string token = txtToken.Text;
                var requests = (HttpWebRequest)WebRequest.Create(Properties.Settings.Default.WebSiteAddress + "/site/api/v1/manage/store/products/variants?product_id=" + WebDGV[4,Counter].Value.ToString() +"");

                requests.Headers.Add("Authorization", "Bearer " + token);
                requests.Method = "GET";
                requests.Accept = "*/*";
                requests.Host = "fnasociology-2.portal.ir";
                requests.ContentType = "application/json";

                HttpWebResponse response2;
                string responseString;

                try
                {
                    response2 = (HttpWebResponse)requests.GetResponse();
                    responseString = new StreamReader(response2.GetResponseStream()).ReadToEnd();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("اطلاعات اشتباه است");
                    return;
                }


                var total = 0;
                var count = 0;
                JArray variants;
                dynamic json = JObject.Parse(responseString);
                total = json.total;
                count = json.count;
                variants = json.variants;
                var variant = variants[0]["id"];

                var cdt = DateTime.Now;
                var dt = pc.GetYear(cdt) + "/" + pc.GetMonth(cdt).ToString().PadLeft(2, '0') + "/" + pc.GetDayOfMonth(cdt).ToString().PadLeft(2, '0');
                Query = "INSERT INTO Merchs (Merch_Code,Merch_Name,SumOfQtty,Sales_Price,Merch_Code_Site,variants,dtTransfer)" +
                        " VALUES ('" + WebDGV[0, Counter].Value.ToString() + "'," +
                        "'" + WebDGV[1, Counter].Value.ToString() + "'," +
                        "" + WebDGV[2, Counter].Value.ToString() + "," +
                        "" + WebDGV[3, Counter].Value.ToString() + "," +
                        "'" + WebDGV[4, Counter].Value.ToString() + "'," +
                        "'" + variant + "'," +
                        "'" + dt + "'" +
                        ")";
                pdp = Properties.Settings.Default.MyConString;
                ExecuteCommand(Query);
                progressBar1.Value += 1;
            }
            MessageBox.Show("خوانده و ذخیره شد");
        }
        private void update_categury_site_with_sepehr()
        {

            var dtable = new DataTable();
            var myConString = Properties.Settings.Default.MyConString;
            // Read From  app
            var Query = "" +
                " SELECT WebSite.Merch_Code, WebSite.Merch_Name, WebSite.SumOfQtty, WebSite.Sales_Price, WebSite.Merch_Code_Site FROM WebSite;";
            pdp = myConString;
            dSet = new DataSet();
            Command(Query);
            WebDGV.ReadOnly = true;
            dtable = dSet.Tables[0];
            WebDGV.DataSource = dtable;

            WebDGV.Columns[0].HeaderText = "کد کالا";
            WebDGV.Columns[1].HeaderText = "نام کالا";
            WebDGV.Columns[2].HeaderText = "موجودی";
            WebDGV.Columns[3].HeaderText = "قیمت";
            WebDGV.Columns[4].HeaderText = "کد سایت";

            WebDGV.Columns[0].Width = 30;
            WebDGV.Columns[1].Width = 80;
            WebDGV.Columns[2].Width = 20;
            WebDGV.Columns[3].Width = 30;
            WebDGV.Columns[4].Width = 50;

            if (txtToken.Text == "")
            {
                if (!get_Token())
                {
                    MessageBox.Show("اشکال در دیافت " + " Token");
                    return;
                }

            }

            string token = txtToken.Text;

            var pc = new PersianCalendar();

            for (int i = 0; i < WebDGV.RowCount; i++)
            {

                string cate = "";
                //iran ehtezaz
                if (WebDGV[0, i].Value.ToString().StartsWith("2030501")) cate = "148826575";
                else if (WebDGV[0, i].Value.ToString().StartsWith("2030502")) cate = "148826575";
                else if (WebDGV[0, i].Value.ToString().StartsWith("2030503")) cate = "148826575";
                //iran roomizi
                else if (WebDGV[0, i].Value.ToString().StartsWith("2030504")) cate = "148939172";
                //mazhabi
                else if (WebDGV[0, i].Value.ToString().StartsWith("20307")) cate = "148939378";
                //tablighat
                else if (WebDGV[0, i].Value.ToString().StartsWith("20302")) cate = "148939381";
                else if (WebDGV[0, i].Value.ToString().StartsWith("20303")) cate = "148939381";
                else if (WebDGV[0, i].Value.ToString().StartsWith("20304")) cate = "148939381";
                //khareji ehtezaz
                else if (WebDGV[0, i].Value.ToString().StartsWith("2030803")) cate = "148941282";
                //khareji roomizi
                else if (WebDGV[0, i].Value.ToString().StartsWith("2030801")) cate = "148941284";
                else cate = "148826444";


                string product = "{" +
                    "\r\n\t\"title\": \"" + WebDGV[1, i].Value.ToString() + "\"," +
                    "\r\n\t\"caption\": \"" + WebDGV[0, i].Value.ToString() + "\"," +
                    "\r\n\t\"commenting_enabled\": true," +
                    "\r\n\t\"variants\": [\r\n\t\t{\r\n\t\t\t\"status\": [\r\n\t\t\t\t\"approved\"," +
                                                                         "\r\n\t\t\t\t\"bank_payment\"," +
                                                                         "\r\n\t\t\t\t\"online_payment\"," +
                                                                         "\r\n\t\t\t\t\"cash_on_delivery\"," +
                                                                         "\r\n\t\t\t\t\"shipping_required\"\r\n\t\t\t]," +
                                                  "\r\n\t\t\t\"price\": " + WebDGV[3, i].Value.ToString() + "," +
                                                  "\r\n\t\t\t\"compare_price\": 0," +
                                                  "\r\n\t\t\t\"stock\": " + WebDGV[2, i].Value.ToString() + "," +
                                                  "\r\n\t\t\t\"title\": \"primary\"," +
                                                  "\r\n\t\t\t\"type\": \"commodity\"\r\n\t\t}\r\n\t]," +
                    "\r\n\t\"slug\": \"" + WebDGV[0, i].Value.ToString() + "\"," +
                    "\r\n\t\"categories\": [" + cate + "]," +
                    "\r\n\t\"status\": [\r\n\t\t\"approved\",\r\n\t\t\"most\",\r\n\t\t\"featured\"\r\n\t]\r\n}";
                /*
                  "\r\n\t\"filters\": [100889212]," +
                "\r\n\t\"categories\": [" + Properties.Settings.Default.Categury + "]," +
                 */
                var requests = (HttpWebRequest)WebRequest.Create(Properties.Settings.Default.Product + "/" + WebDGV[4,i].Value.ToString());
                var postData = product;
                var data = Encoding.UTF8.GetBytes(postData);

                requests.Headers.Add("Authorization", "Bearer " + token);
                requests.Method = "PUT";
                requests.Accept = "*/*";
                requests.Host = "fnasociology-2.portal.ir";
                requests.ContentType = "application/json";
                requests.ContentLength = data.Length;

                using (var stream = requests.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                HttpWebResponse response2;
                string responseString;

                try
                {
                    response2 = (HttpWebResponse)requests.GetResponse();
                    responseString = new StreamReader(response2.GetResponseStream()).ReadToEnd();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("اطلاعات اشتباه است");
                    return;
                }
            }

        }

        private void read_Site_Pages()
        {
            //txtToken.Text = "bac7e831f2e649e5834d168b47e3662c";
            if (txtToken.Text == "")
            {
                if (!get_Token())
                {
                    MessageBox.Show("اشکال در دیافت " + " Token");
                    return;
                }

            }

            pdp = Properties.Settings.Default.MyConString;
            ExecuteCommand("DELETE FROM Pages ");

            var pc = new PersianCalendar();
            progressBar1.Minimum = 0;

            
            string token = txtToken.Text;

            var requests = (HttpWebRequest)WebRequest.Create(Properties.Settings.Default.WebSiteAddress + "/site/api/v1/pages");

            requests.Headers.Add("Authorization", "Bearer " + token);
            requests.Method = "GET";
            requests.Accept = "*/*";
            requests.Host = "fnasociology-2.portal.ir";
            requests.ContentType = "application/json";

            HttpWebResponse response2;
            string responseString;

            try
            {
                response2 = (HttpWebResponse)requests.GetResponse();
                responseString = new StreamReader(response2.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show("اطلاعات اشتباه است");
                return;
            }

            var success = "";
            string q = "";
            JArray pages;
            dynamic json = JObject.Parse(responseString);
            success = json.success;
            pages = json.pages;
            var countRoot = pages.Count;

            try
            {
                for (int a = 0; a < countRoot; a++)
                {
                    var id = pages[a]["id"].ToString();
                    var title = pages[a]["title"].ToString();
                    var type = pages[a]["type"].ToString();
                    q = " INSERT INTO Pages (IDSite,PageName,PageType,Partner) " +
                            " VALUES ('" + id + "','" + title + "','" + type + "','0') ";
                    ExecuteCommand(q);
                    if (type == "store")
                    {
                        var Count1 = pages[a]["subset"].Count();
                        if (Count1 != 0)
                        {
                            var pages1 = pages[a]["subset"].ToList();
                            for (int b = 0; b < Count1; b++)
                            {
                                var id1 = pages1[b]["id"].ToString();
                                var title1 = pages1[b]["title"].ToString();
                                var type1 = pages1[b]["type"].ToString();
                                q = " INSERT INTO Pages (IDSite,PageName,PageType,Partner) " +
                                        " VALUES ('" + id1 + "','" + title1 + "','" + type1 + "','" + pages[a]["id"].ToString() + "') ";
                                ExecuteCommand(q);
                                if (type1 == "store")
                                {
                                    var count2 = pages1[b]["subset"].Count();
                                    if (count2 != 0)
                                   {
                                        var pages2 = pages1[b]["subset"].ToList();
                                        for (int c = 0; c < count2; c++)
                                        {
                                            var id2 = pages2[c]["id"].ToString();
                                            var title2 = pages2[c]["title"].ToString();
                                            var type2 = pages2[c]["type"].ToString();
                                            q = " INSERT INTO Pages (IDSite,PageName,PageType,Partner) " +
                                                    " VALUES ('" + id2 + "','" + title2 + "','" + type2 + "','" + pages1[b]["id"].ToString() + "') ";
                                            ExecuteCommand(q);
                                            if (type2 == "store")
                                            {
                                                var count3 = pages2[c]["subset"].Count();
                                                if (count3 != 0)
                                                {
                                                    var pages3 = pages2[c]["subset"].ToList();
                                                    for (int d = 0; d < count3; d++)
                                                    {
                                                        var id3 = pages3[d]["id"].ToString();
                                                        var title3 = pages3[d]["title"].ToString();
                                                        var type3 = pages3[d]["type"].ToString();
                                                        q = " INSERT INTO Pages (IDSite,PageName,PageType,Partner) " +
                                                                " VALUES ('" + id3 + "','" + title3 + "','" + type3 + "','" + pages2[c]["id"].ToString() + "') ";
                                                        if (type3 == "store")
                                                        {
                                                            ExecuteCommand(q);
                                                            var count4 = pages3[d]["subset"].Count();
                                                            if (count4 != 0)
                                                            {
                                                                var pages4 = pages3[d]["subset"].ToList();
                                                                for (int e = 0; e < count4; e++)
                                                                {
                                                                    var id4 = pages4[e]["id"].ToString();
                                                                    var title4 = pages4[e]["title"].ToString();
                                                                    var type4 = pages4[e]["type"].ToString();
                                                                    q = " INSERT INTO Pages (IDSite,PageName,PageType,Partner) " +
                                                                            " VALUES ('" + id4 + "','" + title4 + "','" + type4 + "','" + pages3[d]["id"].ToString() + "') ";
                                                                    ExecuteCommand(q);
                                                                    if (type4 == "store")
                                                                    {
                                                                        var count5 = pages4[e]["subset"].Count();
                                                                        if (count5 != 0)
                                                                        {
                                                                            var pages5 = pages4[e]["subset"].ToList();
                                                                            for (int f = 0; f < count5; f++)
                                                                            {
                                                                                var id5 = pages5[f]["id"].ToString();
                                                                                var title5 = pages5[f]["title"].ToString();
                                                                                var type5 = pages5[f]["type"].ToString();
                                                                                q = " INSERT INTO Pages (IDSite,PageName,PageType,Partner) " +
                                                                                        " VALUES ('" + id5 + "','" + title5 + "','" + type5 + "','" + pages4[e]["id"].ToString() + "') ";
                                                                                ExecuteCommand(q);
                                                                                if (type5 == "store")
                                                                                {
                                                                                    var count6 = pages5[f]["subset"].Count();
                                                                                    if (count6 != 0)
                                                                                    {
                                                                                        var pages6 = pages5[f]["subset"].ToList();
                                                                                        for (int g = 0; g < count6; g++)
                                                                                        {
                                                                                            var id6 = pages6[g]["id"].ToString();
                                                                                            var title6 = pages6[g]["title"].ToString();
                                                                                            var type6 = pages6[g]["type"].ToString();

                                                                                            q = " INSERT INTO Pages (IDSite,PageName,PageType,Partner) " +
                                                                                                " VALUES ('" + id6 + "','" + title6 + "','" + type6 + "','" + pages5[f]["id"].ToString() + "') ";
                                                                                            ExecuteCommand(q);
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        private void read_Site_Product()
        {
            //txtToken.Text = "bac7e831f2e649e5834d168b47e3662c";
            if (txtToken.Text == "")
            {
                if (!get_Token())
                {
                    MessageBox.Show("اشکال در دیافت " + " Token");
                    return;
                }

            }

            pdp = Properties.Settings.Default.MyConString;
            ExecuteCommand("DELETE FROM WebSite ");

            var pc = new PersianCalendar();
            progressBar1.Minimum = 0;

            var total = 1;
            int min = 0;
            int max = 20;
            int current = 0;
            while (current <= total)
            {
                min += 1;
                string token = txtToken.Text;

                var requests = (HttpWebRequest)WebRequest.Create(Properties.Settings.Default.Product + "?page=" + min.ToString() + "&size=" + max.ToString());

                requests.Headers.Add("Authorization", "Bearer " + token);
                requests.Method = "GET";
                requests.Accept = "*/*";
                requests.Host = "fnasociology-2.portal.ir";
                requests.ContentType = "application/json";

                HttpWebResponse response2;
                string responseString;

                try
                {
                    response2 = (HttpWebResponse)requests.GetResponse();
                    responseString = new StreamReader(response2.GetResponseStream()).ReadToEnd();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("اطلاعات اشتباه است");
                    return;
                }

                var success = "";

                JArray Products;
                dynamic json = JObject.Parse(responseString);
                success = json.success;
                total = json.total;
                Products = json.products;

                progressBar1.Maximum = total;


                if (max >= total)
                {
                    max = total;
                }
                try
                {
                    for (int i = 0; i < max; i++)
                    {
                        current += 1;
                        var id = Products[i]["id"];
                        var title = Products[i]["title"];
                        var caption = Products[i]["caption"];
                        var price = Products[i]["price"];
                        var stock = Products[i]["stock"];

                        if (price.ToString() == "") price = 0;
                        if (stock.ToString() == "") stock = 0;

                        var cdt = DateTime.Now;
                        var dt = pc.GetYear(cdt) + "/" + pc.GetMonth(cdt).ToString().PadLeft(2, '0') + "/" + pc.GetDayOfMonth(cdt).ToString().PadLeft(2, '0');
                        var q = "INSERT INTO WebSite (Merch_Code,Merch_Name,SumOfQtty,Sales_Price,Merch_Code_Site,dtRead)" +
                            " VALUES ('" + caption + "'," +
                            "'" + title + "'," +
                            "" + stock + "," +
                            "" + price + "," +
                            "'" + id + "'," +
                            "'" + dt + "'" +
                            ")";
                        pdp = Properties.Settings.Default.MyConString;
                        ExecuteCommand(q);

                        progressBar1.Value = current;

                    }
                }
                catch (Exception ex)
                {

                }

            }

            var dtable = new DataTable();
            var myConString = Properties.Settings.Default.MyConString;
            // Read From  app
            var Query = "" +
                " SELECT WebSite.Merch_Code, WebSite.Merch_Name, WebSite.SumOfQtty, WebSite.Sales_Price, WebSite.Merch_Code_Site FROM WebSite;";
            pdp = myConString;
            dSet = new DataSet();
            Command(Query);
            WebDGV.ReadOnly = true;
            dtable = dSet.Tables[0];
            WebDGV.DataSource = dtable;

            WebDGV.Columns[0].HeaderText = "کد کالا";
            WebDGV.Columns[1].HeaderText = "نام کالا";
            WebDGV.Columns[2].HeaderText = "موجودی";
            WebDGV.Columns[3].HeaderText = "قیمت";
            WebDGV.Columns[4].HeaderText = "کد سایت";

            WebDGV.Columns[0].Width = 30;
            WebDGV.Columns[1].Width = 80;
            WebDGV.Columns[2].Width = 20;
            WebDGV.Columns[3].Width = 30;
            WebDGV.Columns[4].Width = 50;
        }
        private void update_site()
        {
            if (txtToken.Text == "")
            {
                if (!get_Token())
                {
                    MessageBox.Show("اشکال در دیافت " + " Token");
                    return;
                }

            }
            // DELETE ALL PRODUCT ON APP DB
            pdp = Properties.Settings.Default.MyConString;
            ExecuteCommand(" DELETE FROM Sepehr ");

            string token = txtToken.Text;

            var pc = new PersianCalendar();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = SepehrDGV.RowCount;
            for (int i = 0; i < SepehrDGV.RowCount; i++)
            {
                var _SumOfQtty = SepehrDGV[2, i].Value.ToString();
                var _Sales_Price = SepehrDGV[3, i].Value.ToString();

                if (_SumOfQtty == "") _SumOfQtty = "0";
                if (_Sales_Price == "") _Sales_Price = "0";

                var cdt = DateTime.Now;
                var dt = pc.GetYear(cdt) + "/" + pc.GetMonth(cdt).ToString().PadLeft(2, '0') + "/" + pc.GetDayOfMonth(cdt).ToString().PadLeft(2, '0');
                var q = "INSERT INTO Sepehr (Merch_Code,Merch_Name,SumOfQtty,Sales_Price)" +
                    " VALUES ('" + SepehrDGV[0, i].Value.ToString() + "'," +
                    "'" + SepehrDGV[1, i].Value.ToString() + "'," +
                    "" + _SumOfQtty + "," +
                    "" + _Sales_Price + "" +
                    ")";
                pdp = Properties.Settings.Default.MyConString;
                ExecuteCommand(q);

                //FIND PRODUCT ON WEB SITE
                var requests = (HttpWebRequest)WebRequest.Create(Properties.Settings.Default.WebSiteAddress + "/site/api/v1/store/products?page=1&size=10&q=" + SepehrDGV[0, i].Value.ToString());

                requests.Headers.Add("Authorization", "Bearer " + token);
                requests.Method = "GET";
                requests.Accept = "*/*";
                requests.Host = "fnasociology-2.portal.ir";
                requests.ContentType = "application/json";

                HttpWebResponse response2;
                string responseString;

                try
                {
                    response2 = (HttpWebResponse)requests.GetResponse();
                    responseString = new StreamReader(response2.GetResponseStream()).ReadToEnd();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("اطلاعات اشتباه است");
                    return;
                }

                var success = "";
                var total = 0;
                var id = "";
                dynamic json = JObject.Parse(responseString);
                success = json.success;
                total = json.total;

                if (total == 0)
                {
                    ////add web sitE
                    string product = "{" +
                        "\r\n\t\"title\": \"" + SepehrDGV[1, i].Value.ToString() + "\"," +
                        "\r\n\t\"caption\": \"" + SepehrDGV[0, i].Value.ToString() + "\"," +
                        "\r\n\t\"commenting_enabled\": true," +
                        "\r\n\t\"variants\": [\r\n\t\t{\r\n\t\t\t\"status\": [\r\n\t\t\t\t\"approved\"," +
                                                                             "\r\n\t\t\t\t\"bank_payment\"," +
                                                                             "\r\n\t\t\t\t\"online_payment\"," +
                                                                             "\r\n\t\t\t\t\"cash_on_delivery\"," +
                                                                             "\r\n\t\t\t\t\"shipping_required\"\r\n\t\t\t]," +
                                                      "\r\n\t\t\t\"price\": " + _Sales_Price + "," +
                                                      "\r\n\t\t\t\"compare_price\": 0," +
                                                      "\r\n\t\t\t\"stock\": " + _SumOfQtty + "," +
                                                      "\r\n\t\t\t\"title\": \"primary\"," +
                                                      "\r\n\t\t\t\"type\": \"commodity\"\r\n\t\t}\r\n\t]," +
                        "\r\n\t\"slug\": \"" + SepehrDGV[0, i].Value.ToString() + "\"," +
                        "\r\n\t\"status\": [\r\n\t\t\"approved\",\r\n\t\t\"most\",\r\n\t\t\"featured\"\r\n\t]\r\n}";
                    requests = (HttpWebRequest)WebRequest.Create(Properties.Settings.Default.Product);
                    var postData = product;// "{\r\n\t\"title\": \"پرچم استقلال\",\r\n\t\"caption\": \"استقلال\",\r\n\t\"description\": \"توضیحات مختصری در مورد محصول\",\r\n\t\"contents\": [\r\n\t\t{\r\n\t\t\t\"name\": \"نقد و بررسی\",\r\n\t\t\t\"value\": \"<p>بدون شرح...</p>\"\r\n\t\t}\r\n\t],\r\n\t\"image\": \"/uploads/products/3fdf19.png\",\r\n\t\"images\": [\r\n\t\t\"/uploads/products/3fdf19.png\"\r\n\t],\r\n\t\"commenting_enabled\": true,\r\n\t\"fields\": [\r\n\t\t{\r\n\t\t\t\"name\": \"پردازنده\",\r\n\t\t\t\"value\": \"Intel Core I7\"\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"حافظه\",\r\n\t\t\t\"value\": \"16GB\"\r\n\t\t}\r\n\t],\r\n\t\"variants\": [\r\n\t\t{\r\n\t\t\t\"status\": [\r\n\t\t\t\t\"approved\",\r\n\t\t\t\t\"bank_payment\",\r\n\t\t\t\t\"online_payment\",\r\n\t\t\t\t\"cash_on_delivery\",\r\n\t\t\t\t\"shipping_required\"\r\n\t\t\t],\r\n\t\t\t\"price\": 120000,\r\n\t\t\t\"compare_price\": 132000,\r\n\t\t\t\"stock\": 10,\r\n\t\t\t\"sku\": \"test\",\r\n\t\t\t\"minimum\": null,\r\n\t\t\t\"maximum\": null,\r\n\t\t\t\"weight\": null,\r\n\t\t\t\"width\": null,\r\n\t\t\t\"length\": null,\r\n\t\t\t\"height\": null,\r\n\t\t\t\"title\": \"primary\",\r\n\t\t\t\"type\": \"commodity\"\r\n\t\t}\r\n\t],\r\n\t\"slug\": \"test\",\r\n\t\"published\": null,\r\n\t\"expiration\": null,\r\n\t\"password\": null,\r\n\t\"meta_title\": null,\r\n\t\"meta_description\": null,\r\n\t\"meta_robots\": null,\r\n\t\"redirect\": null,\r\n\t\"filters\": [100889212],\r\n\t\"categories\": [100889212],\r\n\t\"status\": [\r\n\t\t\"approved\",\r\n\t\t\"most\",\r\n\t\t\"featured\"\r\n\t]\r\n}";
                    var data = Encoding.UTF8.GetBytes(postData);

                    requests.Headers.Add("Authorization", "Bearer " + token);
                    requests.Method = "POST";
                    requests.Accept = "*/*";
                    requests.Host = "fnasociology-2.portal.ir";
                    requests.ContentType = "application/json";
                    requests.ContentLength = data.Length;

                    using (var stream = requests.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }


                    try
                    {
                        response2 = (HttpWebResponse)requests.GetResponse();
                        responseString = new StreamReader(response2.GetResponseStream()).ReadToEnd();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("اطلاعات اشتباه است");
                        return;
                    }

                    success = "";
                    var description = "";
                    id = "";
                    json = JObject.Parse(responseString);
                    success = json.success;
                    description = json.description;
                    id = json.id;

                    requests = (HttpWebRequest)WebRequest.Create(Properties.Settings.Default.WebSiteAddress + "/site/api/v1/store/products/" + id);

                    requests.Headers.Add("Authorization", "Bearer " + token);
                    requests.Method = "GET";
                    requests.Accept = "*/*";
                    requests.Host = "fnasociology-2.portal.ir";
                    requests.ContentType = "application/json";

                    try
                    {
                        response2 = (HttpWebResponse)requests.GetResponse();
                        responseString = new StreamReader(response2.GetResponseStream()).ReadToEnd();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("اطلاعات اشتباه است");
                        return;
                    }

                    success = "";
                    description = "";

                    json = JObject.Parse(responseString);
                    success = json.success;
                    JArray variants;
                    variants = json.product.variants;
                    var variant = variants[0]["id"];
                    q = "";
                    q = "INSERT INTO Merchs (Merch_Code,Merch_Name,SumOfQtty,Sales_Price,Merch_Code_Site,variants,dtTransfer)" +
                        " VALUES ('" + SepehrDGV[0, i].Value.ToString() + "'," +
                        "'" + SepehrDGV[1, i].Value.ToString() + "'," +
                        "" + _SumOfQtty + "," +
                        "" + _Sales_Price + "," +
                        "'" + id + "'," +
                        "'" + variant + "'," +
                        "'" + dt + "'" +
                        ")";
                    pdp = Properties.Settings.Default.MyConString;
                    ExecuteCommand(q);
                }
                else
                {
                    id = json.products[0]["id"];
                    //Update web sitE varient


                    requests = (HttpWebRequest)WebRequest.Create(Properties.Settings.Default.WebSiteAddress + "/site/api/v1/manage/store/products/variants?product_id=" + id + "");

                    requests.Headers.Add("Authorization", "Bearer " + token);
                    requests.Method = "GET";
                    requests.Accept = "*/*";
                    requests.Host = "fnasociology-2.portal.ir";
                    requests.ContentType = "application/json";

                    try
                    {
                        response2 = (HttpWebResponse)requests.GetResponse();
                        responseString = new StreamReader(response2.GetResponseStream()).ReadToEnd();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("اطلاعات اشتباه است");
                        return;
                    }


                    var j_total = 0;
                    var j_count = 0;
                    JArray variants;
                    json = JObject.Parse(responseString);
                    j_total = json.total;
                    j_count = json.count;
                    variants = json.variants;
                    var variant = variants[0]["id"];

                    //string str_variant1 = "{\n    \"price\": " + SepehrDGV[3, i].Value.ToString() + ",\n    \"stock\": " + SepehrDGV[2, i].Value.ToString() + ",\n    \"status\": [\n        \"approved\",\n        \"online_payment\",\n        \"bank_payment\",\n        \"cash_on_delivery\"\n    ]\n";
                    string str_variant = "{" +
                       "\r\n\t\"price\": " + _Sales_Price + "," +
                       "\r\n\t\"stock\": " + _SumOfQtty + "," +
                       "\r\n\t\"status\": [\r\n\t\t\"approved\"," +
                                          "\r\n\t\t\"bank_payment\"," +
                                          "\r\n\t\t\"online_payment\"," +
                                          "\r\n\t\t\"cash_on_delivery\"," +
                                          "\r\n\t\t\"shipping_required\"\r\n\t\t]" +
                                       "}";

                    var postData = str_variant;
                    var data = Encoding.UTF8.GetBytes(postData);


                    requests = (HttpWebRequest)WebRequest.Create(Properties.Settings.Default.WebSiteAddress + "/site/api/v1/manage/store/products/variants/" + variant + "");

                    requests.Headers.Add("Authorization", "Bearer " + token);
                    requests.Method = "PUT";
                    requests.Accept = "*/*";
                    requests.Host = "fnasociology-2.portal.ir";
                    requests.ContentType = "application/json";
                    requests.ContentLength = data.Length;

                    using (var stream = requests.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    try
                    {
                        response2 = (HttpWebResponse)requests.GetResponse();
                        responseString = new StreamReader(response2.GetResponseStream()).ReadToEnd();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("اطلاعات اشتباه است");
                        return;
                    }



                    success = "";
                    var description = "";
                    json = JObject.Parse(responseString);
                    success = json.success;
                    description = json.description;

                    q = "";
                    q = " UPDATE Merchs SET " +
                        " SumOfQtty = " + _SumOfQtty + "," +
                        " Sales_Price = " + _Sales_Price + "," +
                        " dtTransfer = '" + dt + "'" +
                        " WHERE Merch_Code = '" + SepehrDGV[0, i].Value.ToString() + "'";
                    pdp = Properties.Settings.Default.MyConString;
                    ExecuteCommand(q);
                }

                progressBar1.Value = i;
            }
            //MessageBox.Show("انجام شد");
            lblLastUpdate.Text = "آخرین بروز رسانی" + System.Environment.NewLine + new Persiandttm.Kernel().Get_Tarikh() + System.Environment.NewLine + new Persiandttm.Kernel().Get_Time();
            progressBar1.Value = 0;
        } 
        private void read_Site_Factors()
        {
            if (txtToken.Text == "")
            {
                if (!get_Token())
                {
                    MessageBox.Show("اشکال در دیافت " + " Token");
                    return;
                }

            }
            var success = "";
            var count = 0;

            pdp = Properties.Settings.Default.MyConString;
            ExecuteCommand("DELETE FROM Orders ");

            var pc = new PersianCalendar();
            progressBar1.Minimum = 0;

            var total = 1;
            int min = 0;
            int max = 20;
            int current = 0;
            while (current <= total)
            {
                min += 1;
                string token = txtToken.Text;

                var requests = (HttpWebRequest)WebRequest.Create(Properties.Settings.Default.Order + "?page=" + min + "&size=" + max + "&start=" + txtAzdt.Text.Replace("/", "") + "&end=" + txtTadt.Text.Replace("/", "") + "");

                requests.Headers.Add("Authorization", "Bearer " + token);
                requests.Method = "GET";
                requests.Accept = "*/*";
                requests.Host = "fnasociology-2.portal.ir";
                requests.ContentType = "application/json";

                HttpWebResponse response2;
                string responseString;

                try
                {
                    response2 = (HttpWebResponse)requests.GetResponse();
                    responseString = new StreamReader(response2.GetResponseStream()).ReadToEnd();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("اطلاعات اشتباه است");
                    return;
                }


                JArray orders;
                dynamic json = JObject.Parse(responseString);
                success = json.success;
                total = json.total;
                count = json.count;

                if (total == 0)
                {
                    //MessageBox.Show("فاکتوری در این محدوده زمانی یافت نشد");
                    return;
                }

                orders = json.orders;

                progressBar1.Maximum = total;


                if (max >= total)
                {
                    max = total;
                }
                try
                {
                    for (int i = 0; i < max; i++)
                    {
                        current += 1;
                        var orders_id = orders[i]["id"];
                        var status_Count = orders[i]["status"].Count();
                        var status = "";
                        //try
                        //{
                        //    status = orders[i]["payments"].ToString();
                        //}
                        //catch (Exception ex)
                        //{

                        //}
                        for (int r = 0; r < status_Count; r++)
                        {
                            if (status == "")
                                status += orders[i]["status"][r];
                            else
                                status += "," + orders[i]["status"][r];
                        }
                        var date = orders[i]["created"]["date"];
                        requests = (HttpWebRequest)WebRequest.Create(Properties.Settings.Default.Order + "/" + orders_id);

                        requests.Headers.Add("Authorization", "Bearer " + token);
                        requests.Method = "GET";
                        requests.Accept = "*/*";
                        requests.Host = "fnasociology-2.portal.ir";
                        requests.ContentType = "application/json";

                        responseString = "";

                        try
                        {
                            response2 = (HttpWebResponse)requests.GetResponse();
                            responseString = new StreamReader(response2.GetResponseStream()).ReadToEnd();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("اطلاعات اشتباه است");
                            return;
                        }


                        JArray products;
                        json = JObject.Parse(responseString);
                        success = json.success;

                        products = json.order.items;

                        for (int J = 0; J < products.Count; J++)
                        {
                            var product_id = products[J]["variant"]["id"];
                            var price = products[J]["price"];
                            var quantity = products[J]["quantity"];

                            if (price.ToString() == "") price = 0;
                            if (quantity.ToString() == "") price = 0;


                            var cdt = DateTime.Now;
                            var dt = pc.GetYear(cdt) + "/" + pc.GetMonth(cdt).ToString().PadLeft(2, '0') + "/" + pc.GetDayOfMonth(cdt).ToString().PadLeft(2, '0');
                            var q = "INSERT INTO Orders (Order_id,dt,variants,quantity,price,Status)" +
                                " VALUES ('" + orders_id + "'," +
                                "'" + date + "'," +
                                "'" + product_id + "'," +
                                "" + quantity + "," +
                                "" + price + "," +
                                "'" + status + "'" +
                                ")";
                            pdp = Properties.Settings.Default.MyConString;
                            ExecuteCommand(q);
                        }

                        progressBar1.Value = current;

                    }
                }
                catch (Exception ex)
                {

                }

            }

            var dtable = new DataTable();
            var myConString = Properties.Settings.Default.MyConString;
            // Read From app
            var Query = "" +
                " SELECT Orders.Order_id, Orders.dt, Merchs.Merch_Name, Orders.quantity, Orders.price FROM Orders INNER JOIN Merchs ON Orders.variants = Merchs.variants;";
            pdp = myConString;
            dSet = new DataSet();
            Command(Query);
            FactorDGV.ReadOnly = true;
            dtable = dSet.Tables[0];
            FactorDGV.DataSource = dtable;

            FactorDGV.Columns[0].HeaderText = "کد فاکتور";
            FactorDGV.Columns[1].HeaderText = "تاریخ فاکتور";
            FactorDGV.Columns[2].HeaderText = "نام محصول";
            FactorDGV.Columns[3].HeaderText = "تعداد";
            FactorDGV.Columns[4].HeaderText = "قیمت";


            FactorDGV.Columns[0].Width = 50;
            FactorDGV.Columns[1].Width = 50;
            FactorDGV.Columns[2].Width = 50;
            FactorDGV.Columns[3].Width = 50;
            FactorDGV.Columns[4].Width = 50;

            //MessageBox.Show("فاکتور ها خوانده شد و ذخیره گردید");

            // ersal be sepehr

            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 0;

            //SELECT  Merch_Code,Merch_Name, dt, SumOfquantity, SumOfprice  FROM  Factors

            var factors_dtable = new DataTable();
            // Read From app
            // foroosh
            var Q = "" +
                " SELECT  Merch_Code,Merch_Name, dt, Sales_Price,SumOfquantity, SumOfprice  FROM  Factors WHERE status = '1';";
            pdp = Properties.Settings.Default.MyConString;
            dSet = new DataSet();
            Command(Q);
            factors_dtable = dSet.Tables[0];

            progressBar1.Maximum = factors_dtable.Rows.Count;
            progressBar1.Value = 0;
            pdp = Properties.Settings.Default.SepehrConString;

            string tarikh_factor = "";
            string sdoc_no = "";
            int ROW = 0;
            for (int m = 0; m < factors_dtable.Rows.Count; m++)
            {
                var MerchCode = factors_dtable.Rows[m][0].ToString();
                var price = factors_dtable.Rows[m][3].ToString();
                var qty = factors_dtable.Rows[m][4].ToString();
                var priceqty = factors_dtable.Rows[m][5].ToString();

                //check tarikh
                if (tarikh_factor != factors_dtable.Rows[m][2].ToString())
                {
                    //GEREFTANE TARIKH
                    tarikh_factor = factors_dtable.Rows[m][2].ToString();
                    //peyda kardane sanade anbar 
                    Q = " SELECT TOP 1 SDoc_No  FROM Store_Docs WHERE SDoc_Type = 201 AND Store_No = '1' AND InOut = -1 AND SDoc_Date = '" + tarikh_factor.Substring(2) + "'";
                    sdoc_no = Get_ExecuteCommand(Q);
                    if (sdoc_no == "")
                    {
                        //DARYAFT CODE JADID
                        Q = " SELECT MAX(SDoc_No) + 1 FROM Store_Docs WHERE Store_No = '1' AND InOut = -1 ";
                        sdoc_no = Get_ExecuteCommand(Q);
                        if (sdoc_no == "")
                        {
                            Q = "SELECT SDocNoLen FROM Init";
                            var len = Get_ExecuteCommand(Q);
                            string NewCode = "1";
                            string anbar = "11";
                            sdoc_no = anbar + NewCode.PadLeft(int.Parse(len), '0');
                        }
                        //EZAFE KARDANE SANADE ANBAR
                        Q = " INSERT INTO Store_Docs (Store_No,SDoc_No,InOut,SDoc_Type,SDoc_Date) " +
                            " VALUES ('1','" + sdoc_no + "',-1,201,'" + tarikh_factor.Substring(2) + "')";
                        ExecuteCommand(Q);
                        ROW = 0;
                    }
                    Q = " DELETE FROM Store_Details WHERE SDoc_No = '" + sdoc_no + "' ";
                    ExecuteCommand(Q);
                }
                // ADD ARTICKLE ANBAR
                ROW += 1;
                Q = " INSERT INTO Store_Details(Row,SDoc_No,Merch_Code,Price,Qty,Total) " +
                    " VALUES (" + ROW + ",'" + sdoc_no + "','" + MerchCode + "'," + price + "," + qty + "," + priceqty + ")";
                ExecuteCommand(Q);


                progressBar1.Value += 1;
            }

            //Bargasht az foroosh 

            Q = "" +
                " SELECT  Merch_Code,Merch_Name, dt, Sales_Price,SumOfquantity, SumOfprice  FROM  Factors WHERE status = '0';";
            pdp = Properties.Settings.Default.MyConString;
            dSet = new DataSet();
            Command(Q);
            factors_dtable = dSet.Tables[0];

            progressBar1.Maximum = factors_dtable.Rows.Count;
            progressBar1.Value = 0;
            pdp = Properties.Settings.Default.SepehrConString;

            tarikh_factor = "";
            sdoc_no = "";
            ROW = 0;
            for (int m = 0; m < factors_dtable.Rows.Count; m++)
            {
                var MerchCode = factors_dtable.Rows[m][0].ToString();
                var price = factors_dtable.Rows[m][3].ToString();
                var qty = factors_dtable.Rows[m][4].ToString();
                var priceqty = factors_dtable.Rows[m][5].ToString();

                //check tarikh
                if (tarikh_factor != factors_dtable.Rows[m][2].ToString())
                {
                    //GEREFTANE TARIKH
                    tarikh_factor = factors_dtable.Rows[m][2].ToString();
                    //peyda kardane sanade anbar 
                    Q = " SELECT TOP 1 SDoc_No  FROM Store_Docs WHERE SDoc_Type = 105 AND Store_No = '1' AND InOut = 0 AND SDoc_Date = '" + tarikh_factor.Substring(2) + "'";
                    sdoc_no = Get_ExecuteCommand(Q);
                    if (sdoc_no == "")
                    {
                        //DARYAFT CODE JADID
                        Q = " SELECT MAX(SDoc_No) + 1 FROM Store_Docs WHERE Store_No = '1' AND InOut = 0 ";
                        sdoc_no = Get_ExecuteCommand(Q);
                        if(sdoc_no == "")
                        {
                            Q = "SELECT SDocNoLen FROM Init";
                            var len = Get_ExecuteCommand(Q);
                            string NewCode = "1";
                            string anbar = "10";
                            sdoc_no = anbar + NewCode.PadLeft(int.Parse(len),'0');
                        }

                        //EZAFE KARDANE SANADE ANBAR
                        Q = " INSERT INTO Store_Docs (Store_No,SDoc_No,InOut,SDoc_Type,SDoc_Date) " +
                            " VALUES ('1','" + sdoc_no + "',0,105,'" + tarikh_factor.Substring(2) + "')";
                        ExecuteCommand(Q);
                        ROW = 0;
                    }
                    Q = " DELETE FROM Store_Details WHERE SDoc_No = '" + sdoc_no + "' ";
                    ExecuteCommand(Q);
                }
                // ADD ARTICKLE ANBAR
                ROW += 1;
                Q = " INSERT INTO Store_Details(Row,SDoc_No,Merch_Code,Price,Qty,Total) " +
                    " VALUES (" + ROW + ",'" + sdoc_no + "','" + MerchCode + "'," + price + "," + qty + "," + priceqty + ")";
                ExecuteCommand(Q);


                progressBar1.Value += 1;
            }


            //MessageBox.Show("فاکتور ها به نرم افزار سپهر انتقال یافت");

            load_Sepehr();
        }
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            load_Sepehr();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            update_site();
        }

        private void btnGetToken_Click(object sender, EventArgs e)
        {
            if (get_Token()) 
                MessageBox.Show("Token " + " دریافت شد");
        }

        private void btnReadSite_Click(object sender, EventArgs e)
        {
            
            read_Site_Pages();

            read_Site_Product();
            // register_Varient_to_appdb();
            
            //update_categury_site_with_sepehr();
        }

        private void btnReadFactors_Click(object sender, EventArgs e)
        {
            read_Site_Factors();
        }

        private void txtTadt_Validating(object sender, CancelEventArgs e)
        {
            var k = new Persiandttm.Kernel();
            txtTadt.Text = k.MakeDate(txtTadt.Text);
        }

        private void txtAzdt_Validating(object sender, CancelEventArgs e)
        {
            var k = new Persiandttm.Kernel();
            txtAzdt.Text = k.MakeDate(txtAzdt.Text);
        }

        private void txtTadt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsDigit(e.KeyChar) == false) && (char.IsControl(e.KeyChar) == false) && (char.IsPunctuation(e.KeyChar) == false))
                e.Handled = true;
        }

        private void txtAzdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsDigit(e.KeyChar) == false) && (char.IsControl(e.KeyChar) == false) && (char.IsPunctuation(e.KeyChar) == false))
                e.Handled = true;
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            AutoStatus(false);
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            MessageBox.Show("در صورتی که در هنگام بروز رسانی پیام خطا در یافت کرده این از ترمیم استفاده کنید");
            string msg = "آیا تمایل به ترمیم دارید؟";
            if (MessageBox.Show(msg, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                registerSepehr_to_appdb();
                register_Varient_to_appdb();
            }
        }

        private void btnStopAuto_Click(object sender, EventArgs e)
        {
            AutoStatus(true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var k = new Persiandttm.Kernel();
            if(strToday !=k.Get_Tarikh())
            {
                get_Token();
            }
            read_Site_Factors();
            update_site();
        }
    }
}

/*

    string product = "{" +
                        "\r\n\t\"title\": \"" + SepehrDGV[1, i].Value.ToString() + "\"," +
                        "\r\n\t\"caption\": \"" + SepehrDGV[0, i].Value.ToString() + "\"," +
                        "\r\n\t\"description\": \"\"," +
                        "\r\n\t\"image\": \"\"," +
                        "\r\n\t\"images\": [\r\n\t\t\"\"\r\n\t]," +
                        "\r\n\t\"commenting_enabled\": true," +
                        "\r\n\t\"variants\": [\r\n\t\t{\r\n\t\t\t\"status\": [\r\n\t\t\t\t\"approved\"," +
                                                                             "\r\n\t\t\t\t\"bank_payment\"," +
                                                                             "\r\n\t\t\t\t\"online_payment\"," +
                                                                             "\r\n\t\t\t\t\"cash_on_delivery\"," +
                                                                             "\r\n\t\t\t\t\"shipping_required\"\r\n\t\t\t]," +
                                                      "\r\n\t\t\t\"price\": " + SepehrDGV[3, i].Value.ToString() + "," +
                                                      "\r\n\t\t\t\"compare_price\": 0," +
                                                      "\r\n\t\t\t\"stock\": " + SepehrDGV[2, i].Value.ToString() + "," +
                                                      "\r\n\t\t\t\"sku\": \"\"," +
                                                      "\r\n\t\t\t\"minimum\": null," +
                                                      "\r\n\t\t\t\"maximum\": null," +
                                                      "\r\n\t\t\t\"weight\": null," +
                                                      "\r\n\t\t\t\"width\": null," +
                                                      "\r\n\t\t\t\"length\": null," +
                                                      "\r\n\t\t\t\"height\": null," +
                                                      "\r\n\t\t\t\"title\": \"primary\"," +
                                                      "\r\n\t\t\t\"type\": \"commodity\"\r\n\t\t}\r\n\t]," +
                        "\r\n\t\"slug\": \"" + SepehrDGV[0, i].Value.ToString() + "\"," +
                        "\r\n\t\"published\": null," +
                        "\r\n\t\"expiration\": null," +
                        "\r\n\t\"password\": null," +
                        "\r\n\t\"meta_title\": null," +
                        "\r\n\t\"meta_description\": null," +
                        "\r\n\t\"meta_robots\": null," +
                        "\r\n\t\"redirect\": null," +
                        "\r\n\t\"filters\": [100889212]," +
                        "\r\n\t\"categories\": [" + Properties.Settings.Default.Categury + "]," +
                        "\r\n\t\"status\": [\r\n\t\t\"approved\",\r\n\t\t\"most\",\r\n\t\t\"featured\"\r\n\t]\r\n}";

*/

/*


    string product = "{" +
                        "\r\n\t\"title\": \"" + SepehrDGV[1, i].Value.ToString() + "\"," +
                        "\r\n\t\"caption\": \"" + SepehrDGV[0, i].Value.ToString() + "\"," +
                        "\r\n\t\"description\": \"\"," +
                        "\r\n\t\"image\": \"\"," +
                        "\r\n\t\"images\": [\r\n\t\t\"\"\r\n\t]," +
                        "\r\n\t\"commenting_enabled\": true," +
                        "\r\n\t\"variants\": [\r\n\t\t{\r\n\t\t\t\"status\": [\r\n\t\t\t\t\"approved\"," +
                                                                             "\r\n\t\t\t\t\"bank_payment\"," +
                                                                             "\r\n\t\t\t\t\"online_payment\"," +
                                                                             "\r\n\t\t\t\t\"cash_on_delivery\"," +
                                                                             "\r\n\t\t\t\t\"shipping_required\"\r\n\t\t\t]," +
                                                      "\r\n\t\t\t\"price\": " + SepehrDGV[3, i].Value.ToString() + "," +
                                                      "\r\n\t\t\t\"compare_price\": 0," +
                                                      "\r\n\t\t\t\"stock\": " + SepehrDGV[2, i].Value.ToString() + "," +
                                                      "\r\n\t\t\t\"sku\": \"\"," +
                                                      "\r\n\t\t\t\"minimum\": null," +
                                                      "\r\n\t\t\t\"maximum\": null," +
                                                      "\r\n\t\t\t\"weight\": null," +
                                                      "\r\n\t\t\t\"width\": null," +
                                                      "\r\n\t\t\t\"length\": null," +
                                                      "\r\n\t\t\t\"height\": null," +
                                                      "\r\n\t\t\t\"title\": \"primary\"," +
                                                      "\r\n\t\t\t\"type\": \"commodity\"\r\n\t\t}\r\n\t]," +
                        "\r\n\t\"slug\": \"" + SepehrDGV[0, i].Value.ToString() + "\"," +
                        "\r\n\t\"published\": null," +
                        "\r\n\t\"expiration\": null," +
                        "\r\n\t\"password\": null," +
                        "\r\n\t\"meta_title\": null," +
                        "\r\n\t\"meta_description\": null," +
                        "\r\n\t\"meta_robots\": null," +
                        "\r\n\t\"redirect\": null," +
                        "\r\n\t\"filters\": [100889212]," +
                        "\r\n\t\"categories\": [" + Properties.Settings.Default.Categury + "]," +
                        "\r\n\t\"status\": [\r\n\t\t\"approved\",\r\n\t\t\"most\",\r\n\t\t\"featured\"\r\n\t]\r\n}";
*/
