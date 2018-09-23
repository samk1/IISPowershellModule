namespace PowershellModule
{
    using System.Collections.Generic;
    using System.IO;
    using System.Management.Automation;
    using System.Management.Automation.Runspaces;
    using System.Text;

    public class PowershellScriptExecutor
    {
        public string ExecuteScript(string scriptPath)
        {
            return this.ExecuteScript(scriptPath, new Dictionary<string, object>());
        }

        public string ExecuteScript(string scriptPath, IDictionary<string, object> variables)
        {
            string absolutePath = Path.GetFullPath(scriptPath);

            StringBuilder sb = new StringBuilder();

            using (PowerShell ps = this.GetPowershell(this.GetInitialSessionState(variables)))
            {
                ps.AddCommand(absolutePath);

                foreach (var resultObject in ps.Invoke())
                {
                    if (resultObject != null)
                    {
                        sb.Append(resultObject.BaseObject);
                    }
                }

                return sb.ToString();
            }
        }

        private static SessionStateVariableEntry CreateStateVariableEntry(KeyValuePair<string, object> kvp)
        {
            return new SessionStateVariableEntry(kvp.Key, kvp.Value, string.Empty);
        }

        private PowerShell GetPowershell(InitialSessionState initialSessionState)
        {
            return PowerShell.Create(initialSessionState);
        }

        private InitialSessionState GetInitialSessionState(IDictionary<string, object> variables)
        {
            var initialSessionState = InitialSessionState.CreateDefault();

            // remove execution policy
            initialSessionState.AuthorizationManager = new AuthorizationManager("Microsoft.Powershell");

            foreach (var variable in variables)
            {
                initialSessionState.Variables.Add(CreateStateVariableEntry(variable));
            }

            return initialSessionState;
        }
    }
}
