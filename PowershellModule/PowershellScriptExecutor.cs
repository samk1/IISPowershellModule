using Microsoft.PowerShell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace PowershellModule
{
    public class PowershellScriptExecutor
    {
        private PowerShell GetPowershell(InitialSessionState initialSessionState)
        {
            return PowerShell.Create(initialSessionState);
        }

        private InitialSessionState GetInitialSessionState(IDictionary<string, object> variables)
        {
            var initialSessionState = InitialSessionState.CreateDefault();

            // remove execution policy
            initialSessionState.AuthorizationManager = new AuthorizationManager("Microsoft.Powershell");

            variables.ToList()
                .ForEach(kvp => initialSessionState.Variables.Add(CreateStateVariableEntry(kvp)));

            return initialSessionState;
        }

        private static SessionStateVariableEntry CreateStateVariableEntry(KeyValuePair<string, object> kvp)
        {
            return new SessionStateVariableEntry(kvp.Key, kvp.Value, "");
        }

        public string ExecuteScript(string scriptPath)
        {
            return ExecuteScript(scriptPath, new Dictionary<string, object>());
        }

        public string ExecuteScript(string scriptPath, IDictionary<string, object> variables)
        {
            string absolutePath = Path.GetFullPath(scriptPath);

            StringBuilder sb = new StringBuilder();

            using (PowerShell ps = GetPowershell(GetInitialSessionState(variables)))
            {
                ps.AddCommand(absolutePath);
                ps.Invoke().ToList().ForEach(o => sb.Append(o ?? o.BaseObject));
                return sb.ToString();
            }
        }
    }
}
