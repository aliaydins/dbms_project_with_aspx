using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;

namespace stajProjee
{
    
    public partial class _default : System.Web.UI.Page
    {
        int a;
        sql baglanti = new sql();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                konu_ddl();
                sehir_dll();
                kurum_dll();
            }
          
        
        }

        public void konu_ddl()
        {

            DataTable dt = new DataTable();
            dt = baglanti.Sorgula("Select * from skonu ");

            ddl_konu.DataSource = dt;
            ddl_konu.DataTextField = "skonu";
            ddl_konu.DataBind();

        }
        public void sehir_dll()
        {
            DataTable dt = new DataTable();
            dt = baglanti.Sorgula("Select sehir from sehir ");

            ddl_sehir.DataSource = dt;
            ddl_sehir.DataTextField = "sehir";

            ddl_sehir.DataBind();
        }
        public void kurum_dll()
        {
            DataTable dt = new DataTable();
            dt = baglanti.Sorgula("select kurum from staj_bilgi group by kurum");

            ddl_kurum.DataSource = dt;
            ddl_kurum.DataTextField = "kurum";
            ddl_kurum.DataBind();

        }
        public void gor()
        {

            DataTable dt = new DataTable();
            dt = baglanti.Sorgula("Select stajNo from staj_bilgi where ogrNo='" + txtStaj_OgrNo.Text + "'");

            ddl_stajsil.DataSource = dt;
            ddl_stajsil.DataTextField = "stajNo";
            ddl_stajsil.DataBind();
        }

        public void tumsil()
        {
            baglanti.Query("delete from staj_bilgi where ogrNo='" + txtOgr_No.Text + "'");
            baglanti.Query("delete from ogrenci where ogrNO='" + txtOgr_No.Text + "'");

            Response.Write("<script>alert('Öğrenci Silindi .');</script>");
            lb_ogrno.Visible = false;
            ld_rd.Visible = false;
            Response.Redirect("default.aspx");
        }
        protected void btnOgrKaydet_Click(object sender, EventArgs e)
        {
            if (txtOgr_No.Text == "") { lb_ogrno.Text = "Boş Geçilemez."; }
     
            if(rd_btn1.Checked==false && rd_btn2.Checked==false)
            {
                ld_rd.Visible = true;
            }
            else if (rd_btn1.Checked == true) { a = 1; }
            else { a = 2; } 
   
            DataTable dt1 = new DataTable();
            dt1 = baglanti.Sorgula("Select ogrNo from ogrenci where ogrNo='" + txtOgr_No.Text + "'");
            if (dt1.Rows.Count >= 1)
            {
                lb_ogrno.Visible = true;
                lb_ogrno.Text = "Öğrenci Mevcut.";
               
            }
            else
            {
                if (txtOgr_No.Text != "" && txtOgr_Ad.Text != "" && txtOgr_Soyad.Text!="" )
                {
                    baglanti.Query("insert into ogrenci values('" + txtOgr_No.Text + "', '" + txtOgr_Ad.Text + "','"+txtOgr_Soyad.Text+"','"+a+"')");
                   

                    Response.Write("<script>alert('Üye Eklendi.');</script>");
                 
                 
                   
                }
                else
                {
                    lb_ad.Visible = true;
                    lb_soyad.Visible = true;
                    lb_ogrno.Visible = true;
                    

                }
            }
            

        }

        protected void btnStajKaydet_Click(object sender, EventArgs e)
        {
            if (CheckBox1.Checked == true) 
            {
                if (txtStaj_OgrNo.Text == "" || txt_Kurum.Text == "" && ddl_kurum.Text == "" || ddl_sehir.Text == "" || ddl_konu.Text == "" || ddl_sınıf.Text == "" || txt_Toplam.Text == "" || txt_Bas.Text == "" || txt_Bit.Text == "")
                {
                    Response.Write("<script>alert('Veriler boş geçilemez ');</script>");
                }
                else
                {
                    if (Convert.ToInt16(txt_Toplam.Text) < 15 || Convert.ToInt16(txt_Toplam.Text) > 60) { lbl_toplamGun.Text = "Gün Sayısı 15-60 arasında olmalı ."; lbl_toplamGun.Visible = true; }
                    else
                    {
                        if (ddl_konu.Text == "Ar-Ge" && ddl_sınıf.Text == "2")
                        {
                            kayıt_dgs();

                        }
                        else
                        {
                            if (Convert.ToInt16(txt_Toplam.Text) > 25 && ddl_sınıf.Text == "2") { lbl_toplamGun.Text = "2.Sınıfta 25 günden fazla staj yapılamaz ."; lbl_toplamGun.Visible = true; }
                            else
                            {
                                kayıt_dgs();
                            }
                        }
                    }
                }

            }
            else
            {
                if (txtStaj_OgrNo.Text == "" || txt_Kurum.Text == "" && ddl_kurum.Text == "" || ddl_sehir.Text == "" || ddl_konu.Text == "" || ddl_sınıf.Text == "" || txt_Toplam.Text == "" || txt_Bas.Text == "" || txt_Bit.Text == "")
                {
                    Response.Write("<script>alert('Veriler boş geçilemez ');</script>");
                }
                else
                {
                    if (Convert.ToInt16(txt_Toplam.Text) < 15 || Convert.ToInt16(txt_Toplam.Text) > 60) { lbl_toplamGun.Text = "Gün Sayısı 15-60 arasında olmalı ."; lbl_toplamGun.Visible = true; }
                    else
                    {
                        if (ddl_konu.Text == "Ar-Ge" && ddl_sınıf.Text == "2")
                        {
                            kayıt_cagır();

                        }
                        else
                        {
                            if (Convert.ToInt16(txt_Toplam.Text) > 25 && ddl_sınıf.Text == "2") { lbl_toplamGun.Text = "2.Sınıfta 25 günden fazla staj yapılamaz ."; lbl_toplamGun.Visible = true; }
                            else
                            {
                                kayıt_cagır();
                            }
                        }
                    }
                }
            }
           


        }

        protected void btn_ogrSil_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            dt1 = baglanti.Sorgula("Select ogrNo from ogrenci where ogrNo='" + txtOgr_No.Text + "'");
            if (dt1.Rows.Count ==0)
            {
                lb_ogrno.Visible = true;
                lb_ogrno.Text = "Öğrenci Bulunamadı.";
            } 
            else
            {
                if (txtOgr_No.Text != "")
                {
                    DataTable dt = new DataTable();
                    dt = baglanti.Sorgula("Select stajNo from staj_bilgi where ogrNo='" + txtOgr_No.Text + "'");
                    if (dt.Rows.Count == 0)
                    {
                        baglanti.Query("delete from ogrenci where ogrNO='" + txtOgr_No.Text + "'");

                        Response.Write("<script>alert('Öğrenci Silindi .');</script>");
                        lb_ogrno.Visible = false;
                        ld_rd.Visible = false;
                        Response.Redirect("default.aspx");
                    }
                    else if(dt.Rows.Count==1)
                    {
                        string a = dt.Rows[0]["stajNo"].ToString();
                        baglanti.Query("delete from mulakat where stajNo='" + a + "'");
                        tumsil();
                    }
                    else if(dt.Rows.Count==2)
                    {
                        string a = dt.Rows[0]["stajNo"].ToString();
                        string b = dt.Rows[1]["stajNo"].ToString();
                        baglanti.Query("delete from mulakat where stajNo='" + a + "'");
                        baglanti.Query("delete from mulakat where stajNo='" + b + "'");
                        tumsil();
                    }
                    else
                    {
                        string a = dt.Rows[0]["stajNo"].ToString();
                        string b = dt.Rows[1]["stajNo"].ToString();
                        string c = dt.Rows[2]["stajNo"].ToString();

                        baglanti.Query("delete from mulakat where stajNo='" + a + "'");
                        baglanti.Query("delete from mulakat where stajNo='" + b + "'");
                        baglanti.Query("delete from mulakat where stajNo='" + c + "'");
                        tumsil();

                    }

                   
                }
                else
                {

                    Response.Write("<script>alert('Lütfen değerleri giriniz');</script>");

                }
            }
        }

        protected void btn_ddlgor_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            dt1 = baglanti.Sorgula("Select ogrNo from staj_bilgi where ogrNo='" + txtStaj_OgrNo.Text + "'");
            if (dt1.Rows.Count ==0)
            {
                Response.Write("<script>alert('Girdiğiniz bilgilere ait kayıt yok.');</script>");
            }
            else
            {
                if (txtStaj_OgrNo.Text != "")
                {

                    gor();
                    
                }
                else
                {

                    Response.Write("<script>alert('Lütfen değerleri giriniz');</script>");

                }
            }
        }

        protected void btn_stajsil_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            dt1 = baglanti.Sorgula("Select ogrNo from staj_bilgi where ogrNo='" + txtStaj_OgrNo.Text + "'");
            if (dt1.Rows.Count == 0)
            {
                Response.Write("<script>alert('Girdiğiniz bilgilere ait kayıt yok.');</script>");
            }
            else
            {
                if (txtStaj_OgrNo.Text != "" && ddl_stajsil.Text != "")
                {
                    DataTable dt = new DataTable();
                    dt = baglanti.Sorgula("Select stajNo from mulakat where stajNo='" + ddl_stajsil.Text + "'");
                    if (dt.Rows.Count == 0)
                    {
                        
                        baglanti.Query("delete from staj_bilgi where ogrNO='" + txtStaj_OgrNo.Text + "'and stajNo='" + ddl_stajsil.Text + "'");
                        Response.Write("<script>alert('Staj kaydı silindi .');</script>");
                        Response.Redirect("default.aspx");
                    
                    }
                    else 
                    {
                        baglanti.Query("delete from mulakat where stajNo='" + ddl_stajsil.Text + "'");
                        baglanti.Query("delete from staj_bilgi where ogrNO='" + txtStaj_OgrNo.Text + "'and stajNo='" + ddl_stajsil.Text + "'");
                        Response.Write("<script>alert('Staj kaydı silindi .');</script>");
                        Response.Redirect("default.aspx");
                    }


                }
                else
                {

                    Response.Write("<script>alert('Lütfen değerleri giriniz');</script>");

                }
            }
        }

        protected void btn_pdf_Click(object sender, EventArgs e)
        {
            PdfPTable pd = new PdfPTable(GridView1.HeaderRow.Cells.Count);


            foreach (TableCell headerCell in GridView1.HeaderRow.Cells)
            {

                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text));
           
                pd.AddCell(pdfCell);

            }


            foreach (GridViewRow gvr in GridView1.Rows)
            {
                foreach(TableCell tabeCell in gvr.Cells)
                {
               

                    PdfPCell pdfCell = new PdfPCell(new Phrase(tabeCell.Text));
              
                    pd.AddCell(pdfCell);

                }

            }
            Document pdfDoc = new Document(PageSize.A4, 5f, 10f, 10f, 10f);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(pd);
            pdfDoc.Close();

            Response.ContentType = "aplication/pdf";
            Response.AppendHeader("content-disposition", "attachment;filename=belge.pdf");
            Response.Write(pdfDoc);
            Response.Flush();
            Response.End();


        }
        

        protected void btn_excel_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment; filename=staj.xls");
            Response.ContentType = "application/excel";

            StringWriter stringWriter = new System.IO.StringWriter();

            HtmlTextWriter htw = new HtmlTextWriter(stringWriter);
            GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");

            foreach (TableCell tableCell in GridView1.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#A55129";
            }
            foreach (GridViewRow gridViewRow in GridView1.Rows) {

                gridViewRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)

                { 
                   gridViewRowTableCell.Style["background-color"] = "#FFF7E7";
                }
            }
            GridView1.RenderControl(htw);
            Response.Write(stringWriter.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control) { }
        public void kayıt_cagır()
        {
            DataTable dt1 = new DataTable();
            dt1 = baglanti.Sorgula("Select ogrNo from staj_bilgi where ogrNo='" + txtStaj_OgrNo.Text + "' and sınıf='" + ddl_sınıf.Text + "'");

            if (dt1.Rows.Count == 1)
            {
                Response.Write("<script>alert('Bu sınıfta kaydı mevcut');</script>");
            }
            else
            {
                DataTable dt = new DataTable();
                dt = baglanti.Sorgula("Select ogrNo from ogrenci where ogrNo='" + txtStaj_OgrNo.Text + "'");

                if (dt.Rows.Count == 1)
                {

                    if (txtStaj_OgrNo.Text != "" && txt_Kurum.Text != "" || ddl_kurum.Text != "" && ddl_sehir.Text != "" && ddl_konu.Text != "" && ddl_sınıf.Text != "" && txt_Toplam.Text != "" && txt_Bas.Text != "" && txt_Bit.Text != "")
                    {
                        if (txt_Kurum.Text != "")
                        {
                            baglanti.Query("insert into staj_bilgi(ogrNO ,kurum,sehir,konu,sınıf,topGun,basTarih,bitTarih) values ('" + txtStaj_OgrNo.Text + "','" + txt_Kurum.Text + "','" + ddl_sehir.Text + "','" + ddl_konu.Text + "','" + ddl_sınıf.Text + "','" + txt_Toplam.Text + "','" + txt_Bas.Text + "','" + txt_Bit.Text + "')");

                            Response.Write("<script>alert('Kayıt Oluşturuldu');</script>");

                            Response.Redirect("default.aspx");

                        }
                        else
                        {
                            txt_Kurum.Text = "";
                            baglanti.Query("insert into staj_bilgi(ogrNO ,kurum,sehir,konu,sınıf,topGun,basTarih,bitTarih) values ('" + txtStaj_OgrNo.Text + "','" + ddl_kurum.Text + "','" + ddl_sehir.Text + "','" + ddl_konu.Text + "','" + ddl_sınıf.Text + "','" + txt_Toplam.Text + "','" + txt_Bas.Text + "','" + txt_Bit.Text + "')");

                            Response.Write("<script>alert('Kayıt Oluşturuldu');</script>");
                            Response.Redirect("default.aspx");

                        }
                    }

                    else
                    {
                        Response.Write("<script>alert(' Boş Kutucuk Bırakmayınız ');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert(' Önce Öğrenci Kaydı Yapınız  ');</script>");
                }
            }
               
            }
        public void kayıt_dgs()
        {
            DataTable dt1 = new DataTable();
            dt1 = baglanti.Sorgula("Select ogrNo from staj_bilgi where ogrNo='" + txtStaj_OgrNo.Text + "' and sınıf='" + ddl_sınıf.Text + "'");

            if (dt1.Rows.Count == 1)
            {
                Response.Write("<script>alert('Bu sınıfta kaydı mevcut');</script>");
            }
            else
            {
                DataTable dt = new DataTable();
                dt = baglanti.Sorgula("Select ogrNo from ogrenci where ogrNo='" + txtStaj_OgrNo.Text + "'");

                if (dt.Rows.Count == 1)
                {

                    if (txtStaj_OgrNo.Text != "" && txt_Kurum.Text != "" || ddl_kurum.Text != "" && ddl_sehir.Text != "" && ddl_konu.Text != "" && ddl_sınıf.Text != "" && txt_Toplam.Text != "" && txt_Bas.Text != "" && txt_Bit.Text != "")
                    {
                        if (txt_Kurum.Text != "")
                        {
                            baglanti.Query("insert into staj_bilgi(ogrNO ,kurum,sehir,konu,sınıf,topGun,kabulGun,basTarih,bitTarih,deger) values ('" + txtStaj_OgrNo.Text + "','" + txt_Kurum.Text + "','" + ddl_sehir.Text + "','" + ddl_konu.Text + "','" + ddl_sınıf.Text + "','" + txt_Toplam.Text + "','" + (Convert.ToInt16(txt_Toplam.Text)/2) + "','" + txt_Bas.Text + "','" + txt_Bit.Text + "', 1)");

                            Response.Write("<script>alert('Kayıt Oluşturuldu');</script>");

                            Response.Redirect("default.aspx");

                        }
                        else
                        {
                            txt_Kurum.Text = "";
                            baglanti.Query("insert into staj_bilgi(ogrNO ,kurum,sehir,konu,sınıf,topGun,kabulGun,basTarih,bitTarih,deger) values ('" + txtStaj_OgrNo.Text + "','" + ddl_kurum.Text + "','" + ddl_sehir.Text + "','" + ddl_konu.Text + "','" + ddl_sınıf.Text + "','" + txt_Toplam.Text + "','" + (Convert.ToInt16(txt_Toplam.Text) / 2) + "','" + txt_Bas.Text + "','" + txt_Bit.Text + "',1)");

                            Response.Write("<script>alert('Kayıt Oluşturuldu');</script>");
                            Response.Redirect("default.aspx");

                        }
                    }

                    else
                    {
                        Response.Write("<script>alert(' Boş Kutucuk Bırakmayınız ');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert(' Önce Öğrenci Kaydı Yapınız  ');</script>");
                }
            }

        }

        
    }
        
    }
    
