namespace PowershellModule
{
    using System.Collections.Generic;
    using System.Web;

    public class PowershellHandler : IHttpHandler
    {
        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            var executor = new PowershellScriptExecutor();
            var variables = new Dictionary<string, object>();

            variables.Add("HTTP_REQUEST", context);

            string output = executor.ExecuteScript(context.Request.PhysicalPath, variables);
            context.Response.Write(output);
        }
    }
}
