using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowershellModule;

namespace PowershellModule.Tests
{
    [TestClass]
    public class Test_PowershellScriptExecutor
    {
        [TestMethod]
        [DeploymentItem("Scripts/hello.ps1")]
        public void Test_ExcecuteHelloScript()
        {
            var executor = new PowershellScriptExecutor();

            var output = executor.ExecuteScript("hello.ps1");

            Assert.AreEqual("Hello world!", output);
        }

        [TestMethod]
        [DeploymentItem("Scripts/two_strings.ps1")]
        public void Test_ExecuteTwoStringsScript()
        {
            var executor = new PowershellScriptExecutor();

            var output = executor.ExecuteScript("two_strings.ps1");

            Assert.AreEqual("Hello world!", output);
        }


        [TestMethod]
        [DeploymentItem("Scripts/variable.ps1")]
        public void Test_ExecuteScriptWithVariable()
        {
            var executor = new PowershellScriptExecutor();

            IDictionary<string, object> variables = new Dictionary<string, object>();

            variables.Add("testVar", "Hello world!");
            var output = executor.ExecuteScript("variable.ps1", variables);

            Assert.AreEqual("Hello world!", output);
        }
    }
}
