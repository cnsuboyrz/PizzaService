using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using Microsoft.SqlServer.Server;


namespace ApiDataGetService
{
    public class TransactionsPart
    {

        private readonly Timer _timer;
        private int counter=0;

        public TransactionsPart()
        {
            //her 6 saniyede bir çalışıyor
            _timer = new Timer(6000) { AutoReset = true }; // kodun tekrar tekrar çalışmasını sağlayacak
            _timer.Elapsed += Timer_Elapsed; //event oluşturuyoruz

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) //event kısmı
        {
            
            SqlConnection baglanti = new SqlConnection("");
           
            if (baglanti.State == ConnectionState.Closed)
                {
                   baglanti.Open();
               }

            SqlCommand komut = new SqlCommand("SELECT * FROM PİZZAS", baglanti);

            SqlDataReader cikti = komut.ExecuteReader();

            String yazdir = "";

            int rowCount = 0;

            if (cikti.HasRows)
            {
                while (cikti.Read())
                {
                    rowCount++;
                }
            }
            cikti.Close();
            komut.Cancel();

            SqlCommand komut2;
            SqlDataReader cikti2;
            for (int i = 1; i < rowCount+2; i++)
            {
                komut2= new SqlCommand("SELECT * FROM PİZZAS WHERE IsWritten='false' and Id="+i, baglanti);
                cikti2 = komut2.ExecuteReader();
                if (cikti2.Read())
                {
                    yazdir = cikti2[1] + "         " + cikti2[2] + "         " + cikti2[3];
                    komut2 = new SqlCommand("UPDATE PİZZAS SET IsWritten='true' WHERE Id=" + i, baglanti);
                    cikti2.Close();
                    cikti2 = komut2.ExecuteReader();
                    cikti2.Read();
                    cikti2.Close();
                    goto yazdir;
                }
                cikti2.Close();
            }

            yazdir:
            
            baglanti.Close();

            string[] lines = new string[] { DateTime.Now.ToString(), yazdir };
            File.AppendAllLines(@"\PizzaMenu2.txt", lines);



            /*

int rowCount = 0;
if (dr.HasRows)  
{  
    while (dr.Read())  
        {  
            rowCount++;
        
        }  
            for(int i =0;i<row sayisi;i++ )
              "SELECT * FROM PİZZAS WHERE IsWritten=false and Id="i doğru ise
            döngüden çık
            bu satırı yazdır

             */

            //string stm = "SELECT COUNT(*) FROM PİZZAS";
            //SqlCommand cmd = new SqlCommand(stm, baglanti);
            //Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            //while (counter == count)
            //{
            //   _timer.Stop();
            //    count = Convert.ToInt32(cmd.ExecuteScalar());
            //}


            //  throw new NotImplementedException(); kod yazılmadığında exception atar default olarak geliyor
        }

        public void Start()
        {
            _timer.Start();
        }
        public void Stop()
        {
            _timer.Stop();
        }
    }
}
