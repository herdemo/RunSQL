using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace RunSql
{
    public partial class Default : System.Web.UI.Page
    {
        public string ConStr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ConStr = ConfigurationManager.AppSettings["ConString"];
            if (ConStr == "")
                TBConStr.Text = "Connection String Emtpy";
            else
                TBConStr.Text = ConStr;

        }

        protected void btn_RunSQL(object sender, EventArgs e)
        {
            if(ConStr == "")
            {
                TBResult.Text = "Please check your connection string";
            }else if(ConStr != TBConStr.Text){
                UpdateAppSettings("ConString", TBConStr.Text);
                ConStr = TBConStr.Text;
            }

            using(SqlConnection con = new SqlConnection(ConStr))
            {
                SqlCommand cmd;
                SqlDataAdapter da;
                DataSet ds = new DataSet();
                string ret = "";

                try
                {
                    con.Open();
                }
                catch (Exception ex)
                {
                    TBResult.Text = "CONNECTION ERROR PLEASE CHECK YOUR CONNECTIN STRING<br/>" + ConStr + "<br/>" + ex.ToString();
                    return;
                }

                try
                {
                    using (cmd = new SqlCommand())
                        cmd.Connection = con;
                    da = new SqlDataAdapter(TBSQL.Text, con);
                    da.Fill(ds);

                    if(ds.Tables[0].Rows.Count == 0)
                    {
                        TBResult.Text = "SQL Result Not Found";
                        con.Close();
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++){
                            foreach(var d in ds.Tables[0].Rows[i].ItemArray)
                            {
                                ret += d.ToString();
                                ret += "|";
                            }
                            ret += "\n";
                        }

                        TBResult.Text = ret;
                    }



                }
                catch (Exception)
                {

                    throw;
                }


            }


        }



        private void UpdateAppSettings(string key, string value)
        {
            Configuration configFile = null;
            if (System.Web.HttpContext.Current != null)
            {
                configFile = WebConfigurationManager.OpenWebConfiguration("~");
            }
            else
            {
                configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            var settings = configFile.AppSettings.Settings;
            if (settings[key] == null)
            {
                settings.Add(key, value);
            }
            else
            {
                settings[key].Value = value;
            }
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }
    }
}