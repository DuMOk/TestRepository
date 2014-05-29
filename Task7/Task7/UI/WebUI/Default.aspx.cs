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
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Core;
using Castle.Core.Resource;

namespace WebUI
{
    public partial class _Default : System.Web.UI.Page
    {
        private static int _op = 0;
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            var memAcc = _container.Resolve<MemoryAccessors<string>>("MemAcc");
            
            GridView2.DataSource = memAcc.GetAll();
            GridView2.DataBind();
            
            _container.Release(memAcc);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            GridView2.DataSource = null;
            GridView2.DataBind();

            LbInfo.Text = "Find Person";

            TbNameStud.Text = "";
            _op = 1;
            Panel2.Visible = true;            
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            _op = 3;

            GridView2.DataSource = null;
            GridView2.DataBind();
            
            LbInfo.Text = "New Person";
            TbNameStud.Text = "";
            Panel2.Visible = true;
        }

        protected void BtnOk_Click(object sender, EventArgs e)
        {
            var memAcc = _container.Resolve<IDataAccessors<string>>("MemAcc");

            if (!captcha.RightInput)
            {
                PnlMessage.Visible = true;
                LbMessage.Text = "Entered wrong captcha! Try again";
                captcha.Clear();
                return;
            }
            
            switch(_op)
            {
                case 1:
                    PnlMessage.Visible = true;

                    if (memAcc.GetByName(TbNameStud.Text.Trim()) == null)
                    {
                        LbMessage.Text = String.Format("Person with Name {0} does not exist!", 
                            TbNameStud.Text.Trim());
                        break;
                    }
                    
                    LbMessage.Text = String.Format("Person with Name {0} exist", TbNameStud.Text.Trim());
                    break;
                case 2: memAcc.DeleteByName(TbNameStud.Text.Trim()); break;
                case 3: memAcc.Insert(TbNameStud.Text.Trim()); break;
            }

            _container.Release(memAcc);

            captcha.Clear();
            TbNameStud.Text = "";
            Panel2.Visible = false;
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            TbNameStud.Text = "";
            captcha.Clear();
            Panel2.Visible = false;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            _op = 2;

            GridView2.DataSource = null;
            GridView2.DataBind();

            LbInfo.Text = "Delete Person";
            TbNameStud.Text = "";
            Panel2.Visible = true;
        }
    }
}
