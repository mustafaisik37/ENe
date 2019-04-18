using calismawf.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calismawf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TSQLDATAEntities db = new TSQLDATAEntities();

        private void BtnGetir_Click(object sender, EventArgs e)
        {
            /* Orders tablosundan fiyatı 500 ve 800 arasında olan siparişleri getir  ve ülkelerine göre sırala*/
            dataGridView1.DataSource = db.Orders.Where(or=> or.freight>500 && or.freight<800).OrderBy(o=> o.shipcountry).Select(x=> new
            {
                x.shipcountry,
                x.shipcity,
                x.custid,
                x.empid,
                x.freight
            }
                ).ToList();
        }

        private void BtnUrunGetir_Click(object sender, EventArgs e)
        {
            /*.fiyatı 20'den büyük ve içinde A harfi içeren ürünlerin id'sini adını ve fiyatını getirme*/
            dataGridView1.DataSource = db.Products.Where(p=>p.unitprice>20 && p.productname.Contains("A")).Select(x => new
            {
                x.productid,
                x.productname,
                x.unitprice
            }).ToList();
        }

        private void BtnSipMus_Click(object sender, EventArgs e)
        {
            /* Siparişler ve çalışanlar tablosundan veri çekme ve ülke ve şehirlere göre sıralama*/
            dataGridView1.DataSource = (from o in db.Orders
                                        join em in db.Employees on o.empid equals em.empid
                                        where o.empid == em.empid
                                        select new
                                        {
                                            o.empid,
                                            o.custid,
                                            o.Customer,
                                            o.shipcountry,
                                            o.shipcity,
                                            o.shipname,
                                            em.firstname,
                                            em.lastname,
                                            em.region
                                        }).OrderBy(x=> x.shipcountry).OrderBy(y=>y.shipcity).ToList();


        }
    }
}
