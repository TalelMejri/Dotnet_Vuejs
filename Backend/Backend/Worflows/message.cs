using Elsa.Http;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Contracts;

namespace Backend.Worflows
{
    public class message : WorkflowBase
    {
        protected override void Build(IWorkflowBuilder builder)
        {
            builder.Root = new Sequence
            {
                Activities =
            {
                new HttpEndpoint
                {
                    Path = new("/v1/test"),
                    SupportedMethods = new (new[] { HttpMethods.Get }),
                    CanStartWorkflow = true,
                },
                new WriteHttpResponse
                {
                    Content = new("Test")
                }
            }
            };
        }
    }
}
