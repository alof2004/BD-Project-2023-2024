using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // Ensure this is included
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgroTrack
{
    public partial class Form1 : Form
    {
        private SqlConnection cn;

        public Form1()
        {
            InitializeComponent();
            verifySGBDConnection();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // You can initialize your connection here if needed
            cn = getSGBDConnection();
        }

        private SqlConnection getSGBDConnection()
        {
            return new SqlConnection("data source = tcp:mednat.ieeta.pt\\SQLSERVER, 8101; Initial Catalog = p8g3; uid = p8g3; password = UmMilhaoDeEuros1#");
        }

        private bool verifySGBDConnection()
        {
            if (cn == null)
                cn = getSGBDConnection();

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }
    }
}
