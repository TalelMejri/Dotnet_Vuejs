using Elsa.Http.Models;
using Elsa.Http;
using Elsa.Workflows.Activities;
using System.Dynamic;
using System.Net;
using Elsa.Workflows;
using Elsa.Workflows.Contracts;
using Backend.Activities;
using Elsa.Workflows.Memory;

namespace Backend.Worflows
{
    public class GetUser : WorkflowBase
    {
        protected override void Build(IWorkflowBuilder builder)
        {
            var routeDataVariable = builder.WithVariable<IDictionary<string, object>>();
            var userIdVariable = builder.WithVariable<string>();
            var userVariable = builder.WithVariable<ExpandoObject>();

            var Name = new Variable<string>("C:\\Users\\talel\\Desktop\\Dotnet_Vuejs\\Backend\\Backend\\fileBpmn\\diagram.bpmn");

            var userId = new Variable<string>();

            builder.Root = new Sequence
            {
                Variables = { Name,userId },
                Activities =
              {
                new HttpEndpoint
                {
                    Path = new ("/users"),
                    SupportedMethods = new (new[] { HttpMethods.Get }),
                    CanStartWorkflow = true,
                },
                new readpbmn(Name)
                {
                    Result = new(userId)
                },
                new SendHttpRequest
                {
                    Url = new(context =>
                    {
                        var id = userId.Get(context);
                        return new Uri($"https://reqres.in/api/users/{id}");
                    }),
                    Method = new(HttpMethods.Get),
                    ParsedContent = new(userVariable),
                    ExpectedStatusCodes =
                    {
                        new HttpStatusCodeCase
                        {
                            StatusCode = StatusCodes.Status200OK,
                            Activity = new WriteHttpResponse
                            {
                                Content = new(context =>
                                {
                                    var user = (dynamic)userVariable.Get(context)!;
                                    return user.data.first_name + " "+user.data.last_name;
                                }),
                                StatusCode = new(HttpStatusCode.OK)
                            }
                        },
                        new HttpStatusCodeCase
                        {
                            StatusCode = StatusCodes.Status404NotFound,
                            Activity = new WriteHttpResponse
                            {
                                Content = new("User not found"),
                                StatusCode = new(HttpStatusCode.NotFound)
                            }
                        }
                    }
                }
          
              }
            };
        }
    }
}

