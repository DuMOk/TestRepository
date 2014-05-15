using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlServerCe;

using Accessors;

namespace WebUI
{
    public partial class FileAccesor : System.Web.UI.Page
    {

        private static int op = 0;
        private DataFileAccessors<string> fileAcc = new DataFileAccessors<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Panel2.Visible = false;
            PnlMessage.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {          
            fileAcc.Path = ConfigurationManager.ConnectionStrings["FilePers"].ConnectionString;
            GridView2.DataSource = fileAcc.GetAll();
            GridView2.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            op = 1;

            GridView2.DataSource = null;
            GridView2.DataBind();

            LbInfo.Text = "Find Person";
            TbNameStud.Text = "";
            Panel2.Visible = true;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            op = 3;

            GridView2.DataSource = null;
            GridView2.DataBind();

            LbInfo.Text = "New Person";
            TbNameStud.Text = "";
            Panel2.Visible = true;
        }

        protected void BtnOk_Click(object sender, EventArgs e)
        {
            fileAcc.Path = ConfigurationManager.ConnectionStrings["FilePers"].ConnectionString;
            
            switch (op)
            {
                case 1:
                    PnlMessage.Visible = true;
                    
                    if (fileAcc.GetByName(TbNameStud.Text.Trim()) == null)
                    {
                        LbMessage.Text = String.Format("Person with Name {0} does not exist!", TbNameStud.Text.Trim());
                        break;
                    }
                    
                    LbMessage.Text = String.Format("Person with Name {0} exist", TbNameStud.Text.Trim());
                    break;
                case 2: fileAcc.DeleteByName(TbNameStud.Text.Trim()); break;
                case 3: fileAcc.Insert(TbNameStud.Text.Trim()); break;
            }

            TbNameStud.Text = "";
            Panel2.Visible = false;
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            TbNameStud.Text = "";
            Panel2.Visible = false;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            op = 2;

            GridView2.DataSource = null;
            GridView2.DataBind();

            LbInfo.Text = "Delete Person";
            TbNameStud.Text = "";
            Panel2.Visible = true;
        }
    }
}