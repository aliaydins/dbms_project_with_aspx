using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace stajProjee
{
    public class sql
    {
        public SqlConnection Connect()
        {

            SqlConnection baglanti = new SqlConnection("Server=.;Database=staj;Integrated Security=True");
            baglanti.Open();
            return (baglanti);
        }
        public void Query(string sorgu)
        {
            SqlConnection baglan = this.Connect();
            SqlCommand cmd = new SqlCommand(sorgu, baglan);
            cmd.ExecuteNonQuery();
            baglan.Close();

        }
        public DataTable Sorgula(string sorgu)
        {
            SqlConnection baglan = this.Connect();
            DataTable dtable = new DataTable();
            SqlDataAdapter datable = new SqlDataAdapter(sorgu, baglan);
            datable.Fill(dtable);
            baglan.Close();
            return (dtable);


        }
    }


}
