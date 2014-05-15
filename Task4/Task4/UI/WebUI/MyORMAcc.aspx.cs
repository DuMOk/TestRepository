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
    public partial class MyORMAcc : System.Web.UI.Page
    {
        private static int op = 0;
        private MyORM<ClsStudent> MyORMStud = new MyORM<ClsStudent>();
        private MyORM<ClsGroup> MyORMGroup = new MyORM<ClsGroup>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Panel2.Visible = false;
            Panel3.Visible = false;
            PnlMessage.Visible = false;
        }

        protected void BtnAll_Click(object sender, EventArgs e)
        {
            var str = ConfigurationManager.ConnectionStrings["DBStudent"].ConnectionString;

            if (RadioButton1.Checked)
            {
                try
                {
                    MyORMStud.OpenConnection(str);
                }
                catch (SqlCeException ex)
                {
                    PnlMessage.Visible = true;
                    LbMessage.Text = "Data Base was not connected! Exit!";
                    return;
                }
                GrViStud.DataSource = MyORMStud.GetAll();
                GrViStud.DataBind();
                MyORMStud.CloseConnection();
                return;
            }

            if (RadioButton2.Checked)
            {
                try
                {
                    MyORMGroup.OpenConnection(str);
                }
                catch (SqlCeException ex)
                {
                    PnlMessage.Visible = true;
                    LbMessage.Text = "Data Base was not connected! Exit!";
                    return;
                }
                GrViStud.DataSource = MyORMGroup.GetAll();
                GrViStud.DataBind();
                MyORMGroup.CloseConnection();
                return;
            }
        }

        protected void BtnFind_Click(object sender, EventArgs e)
        {
            GrViStud.DataSource = null;
            GrViStud.DataBind();

            if (RadioButton1.Checked)
            {
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

            if (RadioButton2.Checked)
            {
                LbInfo0.Text = "Find Group";

                LbIdGrp.Visible = false;
                LbNameGrp.Visible = true;

                TbIdGroup.Visible = false;
                TbNameGroup.Visible = true;

                TbIdGroup.Text = "";
                TbNameGroup.Text = "";
                op = 1;
                Panel3.Visible = true;
            }
        }

        protected void BtnInsert_Click(object sender, EventArgs e)
        {
            GrViStud.DataSource = null;
            GrViStud.DataBind();

            if (RadioButton1.Checked)
            {
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

            if (RadioButton2.Checked)
            {
                LbInfo0.Text = "New Group";

                LbIdGrp.Visible = true;
                LbNameGrp.Visible = true;

                TbIdGroup.Visible = true;
                TbNameGroup.Visible = true;

                TbIdGroup.Text = "";
                TbNameGroup.Text = "";
                op = 3;
                Panel3.Visible = true;
            }
        }

        protected void BtnOk_Click(object sender, EventArgs e)
        {
            var str = ConfigurationManager.ConnectionStrings["DBStudent"].ConnectionString;
            try
            {
                MyORMStud.OpenConnection(str);
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
                    if (MyORMStud.GetByName(TbNameStud.Text.Trim()) == null)
                    {
                        PnlMessage.Visible = true;
                        LbMessage.Text = String.Format("Student with Name {0} does not exist!", TbNameStud.Text.Trim());
                        break;
                    }

                    List<ClsStudent> lstStudent = new List<ClsStudent>();
                    lstStudent.Add(MyORMStud.GetByName(TbNameStud.Text.Trim()));
                    GrViStud.DataSource = lstStudent;
                    GrViStud.DataBind();

                    break;
                case 2:
                    try
                    {
                        MyORMStud.DeleteByName(TbNameStud.Text.Trim());
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
                        MyORMStud.Insert(stud);
                    }
                    catch (SqlCeException ex)
                    {
                        PnlMessage.Visible = true;
                        LbMessage.Text = "Record was not added!";
                        return;
                    }
                    break;
            }

            MyORMStud.CloseConnection();
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

            if (RadioButton1.Checked)
            {
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

            if (RadioButton2.Checked)
            {
                LbInfo0.Text = "Delete Group";

                LbIdGrp.Visible = false;
                LbNameGrp.Visible = true;

                TbIdGroup.Visible = false;
                TbNameGroup.Visible = true;

                TbIdGroup.Text = "";
                TbNameGroup.Text = "";
                op = 2;
                Panel3.Visible = true;
            }
        }

        protected void BtnClear1_Click(object sender, EventArgs e)
        {
            TbIdGroup.Text = "";
            TbNameGroup.Text = "";
            Panel3.Visible = false;
        }

        protected void BtnOk1_Click(object sender, EventArgs e)
        {
            var str = ConfigurationManager.ConnectionStrings["DBStudent"].ConnectionString;
            try
            {
                MyORMGroup.OpenConnection(str);
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
                    if (MyORMGroup.GetByName(TbNameGroup.Text.Trim()) == null)
                    {
                        PnlMessage.Visible = true;
                        LbMessage.Text = String.Format("Group with Name {0} does not exist!", TbNameGroup.Text.Trim());
                        break;
                    }

                    List<ClsGroup> lstGroup = new List<ClsGroup>();
                    lstGroup.Add(MyORMGroup.GetByName(TbNameGroup.Text.Trim()));
                    GrViStud.DataSource = lstGroup;
                    GrViStud.DataBind();

                    break;
                case 2:
                    try
                    {
                        MyORMGroup.DeleteByName(TbNameGroup.Text.Trim());
                    }
                    catch (SqlCeException ex)
                    {
                        PnlMessage.Visible = true;
                        LbMessage.Text = "Record failed to delete!";
                        return;
                    }
                    break;
                case 3:
                    ClsGroup group = new ClsGroup();
                    group.field1 = int.Parse(TbIdGroup.Text.Trim());
                    group.field2 = TbNameGroup.Text.Trim();
                    try
                    {
                        MyORMGroup.Insert(group);
                    }
                    catch (SqlCeException ex)
                    {
                        PnlMessage.Visible = true;
                        LbMessage.Text = "Record was not added!";
                        return;
                    }
                    break;
            }

            MyORMGroup.CloseConnection();
            TbIdGroup.Text = "";
            TbNameGroup.Text = "";
            Panel3.Visible = false;
        }
    }
}