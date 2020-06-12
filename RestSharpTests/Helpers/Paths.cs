using System;
using System.Reflection;

namespace RestSharpTests.Helpers
{
    public class Paths
    {
        
        private string solutionName;
        private string solutionPath;
        public string solutionRuntimeFilesPath;
        private string projectPath;
        public string ProjectTestResultsPath { get; }

        private string slash;

        public Paths()
        {
            GetProjectAndSolutionInfo();
            ProjectTestResultsPath = $"{projectPath}{slash}TestResults{slash}";
        }

        private void GetProjectAndSolutionInfo()
        {
            AssemblyProductAttribute myProject = (AssemblyProductAttribute)AssemblyProductAttribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyProductAttribute));
            string runningPath = AppDomain.CurrentDomain.BaseDirectory;

            if (runningPath.IndexOf("\\") > 0){
                slash = "\\";
            } else {
                slash = "/";
            }
            
            int indexProjectName = runningPath.IndexOf(myProject.Product);
             
            solutionName = myProject.Product;
            solutionPath = runningPath.Substring(0, indexProjectName + myProject.Product.Length);

            int indexBinFolder = runningPath.IndexOf($"{slash}bin");
            projectPath = runningPath.Substring(0, indexBinFolder);
        }

    }
}