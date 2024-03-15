using Elsa.Http;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Contracts;
using Elsa.Workflows.Memory;
using Backend.Models;
using Backend.Activities;

namespace Backend.Worflows
{
 /*   public class Sum : WorkflowBase
    {
        protected override void Build(IWorkflowBuilder builder)
        {
            var a = new Variable<int>();
            var b = new Variable<int>();
            var sum = new Variable<int>();
            var customerVariable = builder.WithVariable<SumMddel>();

            builder.Root = new Sequence
            {  
                Variables = { sum, a, b },
                Activities =
              {
                 new HttpEndpoint
                 {
                    Path = new("/sum"),
                    SupportedMethods = new(new[] { HttpMethods.Post }),
                    CanStartWorkflow = true,
                   ParsedContent = new(customerVariable)
                 },
                  new SetVariable
                     {
                        Variable = a,
                        Value = new(context =>
                        {
                            var payload = customerVariable.Get(context)!;
                            var FirstVarilable = payload.a;
                            return FirstVarilable;
                         })
                     },
                    new SetVariable
                     {
                        Variable = b,
                        Value = new(context =>
                        {
                            var payload = customerVariable.Get(context)!;
                            var SecondVariable = payload.b;
                            return SecondVariable;
                         })
                     },
                 new SumAct(a,b)
                 {
                    Result = new(sum)
                 },
                  new WriteHttpResponse
                  {
                      Content = new(context =>
                      {
                          var aValue = a.Get(context);
                          var bValue = b.Get(context);
                          var sumValue = sum.Get(context); 
                          return new
                          {
                                 Message = $" {aValue} + {bValue} = {sumValue}",
                           };
                      }),
                  }
               }
            };
        }
    }*/
}


/*new HttpEndpoint
             {
                 Path = new("users/{userid}"),
                 SupportedMethods = new(new[] { HttpMethods.Get }),
                 CanStartWorkflow = true,
                 RouteData = new(routeDataVariable)
             },*/