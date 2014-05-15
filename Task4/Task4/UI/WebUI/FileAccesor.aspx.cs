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
        private DataFileAccessors<string> FileAcc = new DataFileAccessors<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Panel2.Visible = false;
            PnlMessage.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            /*var str1 = ConfigurationManager.ConnectionStrings["FilePers"].ConnectionString;
            var str2 = ConfigurationManager.ConnectionStrings["DBStudent"].ConnectionString;
            Path.GetDirectoryName(str1);
            Path.GetFullPath(str1);*/
            
            FileAcc.path = ConfigurationManager.ConnectionStrings["FilePers"].ConnectionString;
            GridView2.DataSource = FileAcc.GetAll();
            GridView2.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            GridView2.DataSource = null;
            GridView2.DataBind();

            LbInfo.Text = "Find Person";

            TbNameStud.Text = "";
            op = 1;
            Panel2.Visible = true;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            GridView2.DataSource = null;
            GridView2.DataBind();

            LbInfo.Text = "New Person";

            TbNameStud.Text = "";
            op = 3;
            Panel2.Visible = true;
        }

        protected void BtnOk_Click(object sender, EventArgs e)
        {
            FileAcc.path = ConfigurationManager.ConnectionStrings["FilePers"].ConnectionString;
            switch (op)
            {
                case 1:
                    PnlMessage.Visible = true;
                    if (FileAcc.GetByName(TbNameStud.Text.Trim()) == null)
                    {
                        LbMessage.Text = String.Format("Person with Name {0} does not exist!", TbNameStud.Text.Trim());
                        break;
                    }
                    LbMessage.Text = String.Format("Person with Name {0} exist", TbNameStud.Text.Trim());
                    break;
                case 2:
                    FileAcc.DeleteByName(TbNameStud.Text.Trim());
                    break;
                case 3:
                    FileAcc.Insert(TbNameStud.Text.Trim());
                    break;
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
            GridView2.DataSource = null;
            GridView2.DataBind();

            LbInfo.Text = "Delete Person";

            TbNameStud.Text = "";
            op = 2;
            Panel2.Visible = true;
        }
    }
}