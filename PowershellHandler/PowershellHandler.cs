using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PowershellHandler
{
    public class PowershellHandler : IHttpHandler
    {
        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            InitialSessionState initialSessionState = GetInitialSessionState(context);
            Command script = GetPowershellScript(context);


            PowerShell ps = PowerShell.Create(initialSessionState);
            ps.Commands.AddCommand(script);

            IEnumerable<PSObject> output = ps.Invoke();

            output.ToList()
                .ForEach(o => WritePSObjectToResponse(o, context.Response));
        }

        private Command GetPowershellScript(HttpContext context)
        {
            return new Command(context.Request.PhysicalPath);
        }

        private InitialSessionState GetInitialSessionState(HttpContext context)
        {
            return InitialSessionState.Create();
        }

        private void WritePSObjectToResponse(PSObject o, HttpResponse response)
        {
            response.Write(o);
        }
    }
}
