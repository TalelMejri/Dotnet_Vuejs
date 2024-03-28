using Backend.Models;
using Elsa.Expressions;
using Elsa.Workflows;
using Elsa.Workflows.Memory;
using Elsa.Workflows.Models;
using Python.Runtime;

namespace Backend.Activities
{
    public class PythonScriptTask : Activity
    {
  

        public PythonScriptTask(Variable<string> code)
        {
            Script = code.Value.ToString()!;
            TestPython();
        }

        public string Script { get; set; }
        public void TestPython()
        {
           Runtime.PythonDLL = @"C:\Users\talel\AppData\Local\Programs\Python\Python312\Python312.dll";
          
           PythonEngine.Initialize();
           PythonEngine.BeginAllowThreads(); 
          
           using (Py.GIL())
           {
                PythonEngine.RunSimpleString(Script);
           }
           PythonEngine.Shutdown();
          
        }
        protected override void Execute(ActivityExecutionContext context)
        {
         
        }
    }
}
