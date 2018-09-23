using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PowershellModule
{
    public class PowershellHandler : IHttpHandler
    {
        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            var executor = new PowershellScriptExecutor();
            var variables = new Dictionary<string, object>();

            variables.Add("HTTP_REQUEST", context);

            string output = executor.ExecuteScript(context.Request.PhysicalPath);
            context.Response.Write(output);
        }
    }
}
