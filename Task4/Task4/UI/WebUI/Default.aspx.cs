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
    public partial class _Default : System.Web.UI.Page
    {
        private static int op = 0;
        private MemoryAccessors<string> MemAcc = new MemoryAccessors<string>();
        
        protected void Page_Load(object sender, EventArgs e)
        {            
            Panel2.Visible = false;
            PnlMessage.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GridView2.DataSource = MemAcc.GetAll();
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
            switch(op)
            {
                case 1:
                    PnlMessage.Visible = true;
                    if (MemAcc.GetByName(TbNameStud.Text.Trim()) == null)
                    {
                        LbMessage.Text = String.Format("Person with Name {0} does not exist!", TbNameStud.Text.Trim());
                        break;
                    }
                    LbMessage.Text = String.Format("Person with Name {0} exist", TbNameStud.Text.Trim());
                    break;
                case 2:
                    MemAcc.DeleteByName(TbNameStud.Text.Trim());
                    break;
                case 3:
                    MemAcc.Insert(TbNameStud.Text.Trim());
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
