using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlServerCe;
using Accessors;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Core;
using Castle.Core.Resource;

namespace WebUI
{
    public partial class MyORMAcc : System.Web.UI.Page
    {
        private static int op = 0;
        private IWindsorContainer _container = new WindsorContainer();

        protected void Page_Load(object sender, EventArgs e)
        {
            _container.Register(
               Component.For<IDataAccessors<string>>().ImplementedBy<MemoryAccessors<string>>().Named("MemAcc"),
               Component.For<IDataAccessors<string>>().ImplementedBy<DataFileAccessors<string>>().Named("FileAcc"),
               Component.For<IDataAccessors<Student>>().ImplementedBy<ADONetAccessors<Student>>().Named("ADOAcc"),
               Component.For<IDataAccessors<Student>>().ImplementedBy<MyORM<Student>>().Named("MyORMAccStud"),
               Component.For<IDataAccessors<Group>>().ImplementedBy<MyORM<Group>>().Named("MyORMAccGroup"));

            Panel2.Visible = false;
            Panel3.Visible = false;
            PnlMessage.Visible = false;
        }

        protected void BtnAll_Click(object sender, EventArgs e)
        {
            var myORMStud = _container.Resolve<MyORM<Student>>("MyORMAccStud");
            var myORMGroup = _container.Resolve<MyORM<Group>>("MyORMAccGroup");
            var str = ConfigurationManager.ConnectionStrings["DBStudent"].ConnectionString;

            if (RadioButton1.Checked)
            {
                try
                {
                    myORMStud.OpenConnection(str);
                }
                catch (SqlCeException ex)
                {
                    PnlMessage.Visible = true;
                    LbMessage.Text = "Data Base was not connected! Exit!";
                    return;
                }
                
                GrViStud.DataSource = myORMStud.GetAll();
                GrViStud.DataBind();
                
                myORMStud.CloseConnection();
                _container.Release(myORMStud);
                return;
            }

            if (RadioButton2.Checked)
            {
                try
                {
                    myORMGroup.OpenConnection(str);
                }
                catch (SqlCeException ex)
                {
                    PnlMessage.Visible = true;
                    LbMessage.Text = "Data Base was not connected! Exit!";
                    return;
                }
                
                GrViStud.DataSource = myORMGroup.GetAll();
                GrViStud.DataBind();
                
                myORMGroup.CloseConnection();
                _container.Release(myORMGroup);
                return;
            }
        }

        protected void BtnFind_Click(object sender, EventArgs e)
        {
            GrViStud.DataSource = null;
            GrViStud.DataBind();

            if (RadioButton1.Checked)
            {
                op = 1;
                
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
                Panel2.Visible = true;
            }

            if (RadioButton2.Checked)
            {
                op = 1;

                LbInfo0.Text = "Find Group";

                LbIdGrp.Visible = false;
                LbNameGrp.Visible = true;

                TbIdGroup.Visible = false;
                TbNameGroup.Visible = true;

                TbIdGroup.Text = "";
                TbNameGroup.Text = "";
                Panel3.Visible = true;
            }
        }

        protected void BtnInsert_Click(object sender, EventArgs e)
        {
            GrViStud.DataSource = null;
            GrViStud.DataBind();

            if (RadioButton1.Checked)
            {
                op = 3;

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
                Panel2.Visible = true;
            }

            if (RadioButton2.Checked)
            {
                op = 3;
                
                LbInfo0.Text = "New Group";

                LbIdGrp.Visible = true;
                LbNameGrp.Visible = true;

                TbIdGroup.Visible = true;
                TbNameGroup.Visible = true;

                TbIdGroup.Text = "";
                TbNameGroup.Text = "";
                Panel3.Visible = true;
            }
        }

        protected void BtnOk_Click(object sender, EventArgs e)
        {
            var myORMStud = _container.Resolve<MyORM<Student>>("MyORMAccStud");
            var str = ConfigurationManager.ConnectionStrings["DBStudent"].ConnectionString;
            
            try
            {
                myORMStud.OpenConnection(str);
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
                    if (myORMStud.GetByName(TbNameStud.Text.Trim()) == null)
                    {
                        PnlMessage.Visible = true;
                        LbMessage.Text = String.Format("Student with Name {0} does not exist!", 
                            TbNameStud.Text.Trim());
                        break;
                    }

                    List<Student> lstStudent = new List<Student>();
                    lstStudent.Add(myORMStud.GetByName(TbNameStud.Text.Trim()));
                    
                    GrViStud.DataSource = lstStudent;
                    GrViStud.DataBind();
                    break;
                case 2:
                    try
                    {
                        myORMStud.DeleteByName(TbNameStud.Text.Trim());
                    }
                    catch (SqlCeException ex)
                    {
                        PnlMessage.Visible = true;
                        LbMessage.Text = "Record failed to delete!";
                        return;
                    }
                    break;
                case 3:
                    Student stud = new Student();
                    
                    stud.field1 = int.Parse(TbIdStud.Text.Trim());
                    stud.field2 = TbNameStud.Text.Trim();
                    stud.field3 = int.Parse(TbIdGrp.Text.Trim());
                    
                    try
                    {
                        myORMStud.Insert(stud);
                    }
                    catch (SqlCeException ex)
                    {
                        PnlMessage.Visible = true;
                        LbMessage.Text = "Record was not added!";
                        return;
                    }
                    break;
            }

            myORMStud.CloseConnection();
            _container.Release(myORMStud);

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
                op = 2;
                
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
                Panel2.Visible = true;
            }

            if (RadioButton2.Checked)
            {
                op = 2;
                
                LbInfo0.Text = "Delete Group";

                LbIdGrp.Visible = false;
                LbNameGrp.Visible = true;

                TbIdGroup.Visible = false;
                TbNameGroup.Visible = true;

                TbIdGroup.Text = "";
                TbNameGroup.Text = "";
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
            var myORMGroup = _container.Resolve<MyORM<Group>>("MyORMAccGroup");
            var str = ConfigurationManager.ConnectionStrings["DBStudent"].ConnectionString;
           
            try
            {
                myORMGroup.OpenConnection(str);
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
                    if (myORMGroup.GetByName(TbNameGroup.Text.Trim()) == null)
                    {
                        PnlMessage.Visible = true;
                        LbMessage.Text = String.Format("Group with Name {0} does not exist!", 
                            TbNameGroup.Text.Trim());
                        break;
                    }

                    List<Group> lstGroup = new List<Group>();
                    lstGroup.Add(myORMGroup.GetByName(TbNameGroup.Text.Trim()));
                    
                    GrViStud.DataSource = lstGroup;
                    GrViStud.DataBind();
                    break;
                case 2:
                    try
                    {
                        myORMGroup.DeleteByName(TbNameGroup.Text.Trim());
                    }
                    catch (SqlCeException ex)
                    {
                        PnlMessage.Visible = true;
                        LbMessage.Text = "Record failed to delete!";
                        return;
                    }
                    break;
                case 3:
                    Group group = new Group();
                    
                    group.field1 = int.Parse(TbIdGroup.Text.Trim());
                    group.field2 = TbNameGroup.Text.Trim();
                    
                    try
                    {
                        myORMGroup.Insert(group);
                    }
                    catch (SqlCeException ex)
                    {
                        PnlMessage.Visible = true;
                        LbMessage.Text = "Record was not added!";
                        return;
                    }
                    break;
            }

            myORMGroup.CloseConnection();
            _container.Release(myORMGroup);

            TbIdGroup.Text = "";
            TbNameGroup.Text = "";
            Panel3.Visible = false;
        }
    }
}