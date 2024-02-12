using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Contracts;
using Elsa.Http;

namespace Backend.Worflows
{
    public class Employe : WorkflowBase
    {
        protected override void Build(IWorkflowBuilder builder)
        {
            var queryStringsVariable = builder.WithVariable<IDictionary<string, object>>();
            var employee = builder.WithVariable<object>();
            builder.Root = new Sequence
            {
                Activities =
            {
                 new HttpEndpoint
                {
                    Path = new("/employe"),
                    QueryStringData = new(queryStringsVariable),
                    CanStartWorkflow = true,
                },
                new SetVariable
                {
                    Variable = employee,
                    Value = new(context =>
                    {
                        var queryStrings = queryStringsVariable.Get(context)!;
                        var message = queryStrings.TryGetValue("emp", out var messageValue) ? messageValue.ToString() :"";
                        return message;
                    })
                },
                 new WriteHttpResponse
                {
                    Content = new(employee)
                }
             }
          };
        }
    }
}
