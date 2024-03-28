using Backend.Activities;
using Elsa.Http;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Contracts;
using JetBrains.Annotations;

namespace Backend.Worflows
{
    public class ScriptTaskWorkflow : WorkflowBase
    {
      private readonly string _data;

        public ScriptTaskWorkflow(string data)
        {
            _data = data;
        }
     
        protected override void Build(IWorkflowBuilder builder)
        {
            var code = builder.WithVariable<string>(_data);
            builder.Root = new Sequence
            {
                Activities =
            {
                new PythonScriptTask(code),
                new WriteHttpResponse
                {
                    Content = new("Python Executed")
                }
            }
            };
        }
    }
}
