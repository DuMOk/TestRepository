using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlServerCe;
using Accessors;

namespace WebUI
{
    public partial class ADONetAcc : System.Web.UI.Page
    {
        private static int op = 0;
        private ADONetAccessors<ClsStudent> ADOAcc = new ADONetAccessors<ClsStudent>();        

        protected void Page_Load(object sender, EventArgs e)
        {
            Panel2.Visible = false;
            PnlMessage.Visible = false;
        }

        protected void BtnAll_Click(object sender, EventArgs e)
        {
            var str = ConfigurationManager.ConnectionStrings["DBStudent"].ConnectionString;
            try
            {
                ADOAcc.OpenConnection(str);
            }
            catch (SqlCeException ex)
            {
                PnlMessage.Visible = true;
                LbMessage.Text = "Data Base was not connected! Exit!";
                return;
            }

            GrViStud.DataSource = ADOAcc.GetAll();
            GrViStud.DataBind();
            
            ADOAcc.CloseConnection();
        }

        protected void BtnFind_Click(object sender, EventArgs e)
        {
            GrViStud.DataSource = null;
            GrViStud.DataBind();

            LbInfo.Text = "Find Student";

            LbIdStud.Visible = false;
            LbFIO.Visible = true;
            LbIdGr.Visible = false;
            
            TbIdStud.Visible = false;
            TbNameStud.Visible = true;
            TbIdGrp.Visible = false;
            
            TbIdStud.Text = "";
            TbNameStud.Text = "";
            TbIdGrp.Text = "";
            op = 1;
            Panel2.Visible = true;     
        }

        protected void BtnInsert_Click(object sender, EventArgs e)
        {
            GrViStud.DataSource = null;
            GrViStud.DataBind();

            LbInfo.Text = "New Student";

            LbIdStud.Visible = true;
            LbFIO.Visible = true;
            LbIdGr.Visible = true;
            
            TbIdStud.Visible = true;
            TbNameStud.Visible = true;
            TbIdGrp.Visible = true;
            
            TbIdStud.Text = "";
            TbNameStud.Text = "";
            TbIdGrp.Text = "";
            op = 3;
            Panel2.Visible = true;
        }

        protected void BtnOk_Click(object sender, EventArgs e)
        {
            var str = ConfigurationManager.ConnectionStrings["DBStudent"].ConnectionString;
            try
            {
                ADOAcc.OpenConnection(str);
            }
            catch (SqlCeException ex)
            {
                PnlMessage.Visible = true;
                LbMessage.Text = "Data Base was not connected! Exit!";
                return;
            }

            switch (op)
            {
                case 1:
                    if (ADOAcc.GetByName(TbNameStud.Text.Trim()) == null)
                    {
                        PnlMessage.Visible = true;
                        LbMessage.Text = String.Format("Student with Name {0} does not exist!", TbNameStud.Text.Trim());
                        break;
                    }
                    
                    List<ClsStudent> lstStudent = new List<ClsStudent>();
                    lstStudent.Add(ADOAcc.GetByName(TbNameStud.Text.Trim()));
                    GrViStud.DataSource = lstStudent;
                    GrViStud.DataSource = lstStudent;
                    GrViStud.DataBind();

                    break;
                case 2:
                    try
                    {
                        ADOAcc.DeleteByName(TbNameStud.Text.Trim());
                    }
                    catch (SqlCeException ex)
                    {
                        PnlMessage.Visible = true;
                        LbMessage.Text = "Record failed to delete!";
                        return;
                    }
                    break;
                case 3:
                    ClsStudent stud = new ClsStudent();
                    stud.field1 = int.Parse(TbIdStud.Text.Trim());
                    stud.field2 = TbNameStud.Text.Trim();
                    stud.field3 = int.Parse(TbIdGrp.Text.Trim());
                    try
                    {
                        ADOAcc.Insert(stud);
                    }
                    catch (SqlCeException ex)
                    {
                        PnlMessage.Visible = true;
                        LbMessage.Text = "Record was not added!";
                        return;
                    }
                    break;
            }

            ADOAcc.CloseConnection();
            TbIdStud.Text = "";
            TbNameStud.Text = "";
            TbIdGrp.Text = "";
            Panel2.Visible = false;
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            TbIdStud.Text = "";
            TbNameStud.Text = "";
            TbIdGrp.Text = "";
            Panel2.Visible = false;
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            GrViStud.DataSource = null;
            GrViStud.DataBind();

            LbInfo.Text = "Delete Student";

            LbIdStud.Visible = false;
            LbFIO.Visible = true;
            LbIdGr.Visible = false;
            
            TbIdStud.Visible = false;
            TbNameStud.Visible = true;
            TbIdGrp.Visible = false;

            TbIdStud.Text = "";
            TbNameStud.Text = "";
            TbIdGrp.Text = "";
            op = 2;
            Panel2.Visible = true;
        }
    }
}