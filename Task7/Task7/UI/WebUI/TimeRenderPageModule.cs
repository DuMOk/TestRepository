using System;
using System.Web;

namespace WebUI
{
    public class TimerRenderPageModule : IHttpModule
    {
        public TimerRenderPageModule()
        {

        }

        public void OnBeginRequest(object o, EventArgs ea)
        {
            HttpApplication httpApp = (HttpApplication)o;

            DateTime dateTimeBeginRequest = DateTime.Now;

            HttpContext ctx;
            ctx = HttpContext.Current;
            ctx.Items["dateTimeBeginRequest"] = dateTimeBeginRequest;
        }

        public void OnEndRequest(object o, EventArgs ea)
        {
            HttpApplication httpApp = (HttpApplication)o;

            DateTime dateTimeEndRequest = DateTime.Now;

            HttpContext ctx;
            ctx = HttpContext.Current;
            DateTime dateTimeBeginRequest =
               (DateTime)ctx.Items["dateTimeBeginRequest"];


            TimeSpan duration = dateTimeEndRequest - dateTimeBeginRequest;


            ctx.Response.Write("<p align = \"center\"> Generated in " + 
                duration.ToString() + "</p>");
        }

        public void Init(HttpApplication httpApp)
        {

            httpApp.BeginRequest +=
               new EventHandler(this.OnBeginRequest);

            httpApp.EndRequest +=
               new EventHandler(this.OnEndRequest);
        }

        public void Dispose() 
        {

        }
    }
}
