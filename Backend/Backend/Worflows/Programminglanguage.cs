using Elsa.Http;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Contracts;
using Elsa.Workflows.Memory;
namespace Backend.Worflows
{
   /* public class Programminglanguage : WorkflowBase
    {
        protected override void Build(IWorkflowBuilder builder)
        {

            var routeDataVariable = builder.WithVariable<IDictionary<string, object>>();
            var progVaribale = builder.WithVariable<string>();
            var items = new List<string> { "c#", "c", "python" };
            var newItems = new List<string>();
            var currentItem = new Variable<string>();

            builder.Root = new Sequence
            {
                Activities = {
                     new HttpEndpoint
                    {
                     Path = new("programmes/{prog}"),
                     CanStartWorkflow = true,
                     SupportedMethods = new(new[] { HttpMethods.Get }),
                     RouteData = new(routeDataVariable)
                    },
                      new SetVariable
                     {
                        Variable = progVaribale,
                        Value = new(context =>
                        {
                            var routeData = routeDataVariable.Get(context)!;
                            var prog = routeData["prog"].ToString();
                            return prog;
                         })
                     },
                        new If
                        {
                            Condition = new(context => !items.Contains(progVaribale.Get(context)!)),
                            Then = new Sequence
                            {
                                Activities =
                                {
                                new SetVariable
                                {
                                    Variable = currentItem,
                                    Value = new(context => progVaribale.Get(context) ?? string.Empty)
                                  },
                                new SetVariable
                                {
                                    Variable = progVaribale,
                                    Value = new(context =>
                                    {
                                        var currentProg = progVaribale.Get(context);
                                        if (!items.Contains(currentProg!))
                                        {
                                            items.Add(currentProg!);
                                        }
                                        return currentProg;
                                    })
                                }
                                }
                             }
                        },
                   new WriteHttpResponse
                    {
                       
                    Content = new(items)
                    }
        }
            };
        }
    }*/
}

/*
 *  new ForEach<string>
                    {
                        Items=new Input<ICollection<string>>(items),
                        Body=new WriteLine(context=>currentItem.Get(context))
                    },
                    new WriteLine("Done")*/
