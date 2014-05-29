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
    public partial class ADONetAcc : System.Web.UI.Page
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
            PnlMessage.Visible = false;
        }

        protected void BtnAll_Click(object sender, EventArgs e)
        {
            var adoAcc = _container.Resolve<ADONetAccessors<Student>>("ADOAcc");
            var str = ConfigurationManager.ConnectionStrings["DBStudent"].ConnectionString;
            
            try
            {
                adoAcc.OpenConnection(str);
            }
            catch (SqlCeException ex)
            {
                PnlMessage.Visible = true;
                LbMessage.Text = "Data Base was not connected! Exit!";
                return;
            }

            GrViStud.DataSource = adoAcc.GetAll();
            GrViStud.DataBind();

            _container.Release(adoAcc);
            adoAcc.CloseConnection();
        }

        protected void BtnFind_Click(object sender, EventArgs e)
        {
            op = 1;

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
            Panel2.Visible = true;     
        }

        protected void BtnInsert_Click(object sender, EventArgs e)
        {
            op = 3;
            
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
            Panel2.Visible = true;
        }

        protected void BtnOk_Click(object sender, EventArgs e)
        {
            if (!captcha.RightInput)
            {
                PnlMessage.Visible = true;
                LbMessage.Text = "Entered wrong captcha! Try again";
                captcha.Clear();
                return;
            }
            
            var adoAcc = _container.Resolve<ADONetAccessors<Student>>("ADOAcc");
            var str = ConfigurationManager.ConnectionStrings["DBStudent"].ConnectionString;
            
            try
            {
                adoAcc.OpenConnection(str);
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
                    if (adoAcc.GetByName(TbNameStud.Text.Trim()) == null)
                    {
                        PnlMessage.Visible = true;
                        LbMessage.Text = String.Format("Student with Name {0} does not exist!", 
                            TbNameStud.Text.Trim());
                        break;
                    }
                    
                    List<Student> lstStudent = new List<Student>();
                    lstStudent.Add(adoAcc.GetByName(TbNameStud.Text.Trim()));
                    
                    GrViStud.DataSource = lstStudent;
                    GrViStud.DataBind();

                    break;
                case 2:
                    try
                    {
                        adoAcc.DeleteByName(TbNameStud.Text.Trim());
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
                        adoAcc.Insert(stud);
                    }
                    catch (SqlCeException ex)
                    {
                        PnlMessage.Visible = true;
                        LbMessage.Text = "Record was not added!";
                        return;
                    }
                    break;
            }

            adoAcc.CloseConnection();
            _container.Release(adoAcc);

            captcha.Clear();
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
            op = 2;
            
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
            Panel2.Visible = true;
        }
    }
}