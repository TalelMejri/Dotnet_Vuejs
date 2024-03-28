using Backend.Activities;
using Elsa.Http;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Contracts;
using JetBrains.Annotations;
namespace Backend.Worflows
{
    public class BDConnectionWorkflow : WorkflowBase
    {
        private readonly string _data;
        private readonly string _requete;
        private readonly string _type;
        public BDConnectionWorkflow(string data,string requete, string type)
        {
            _data = data;
            _requete = requete;
            _type = type;
        }

        protected override void Build(IWorkflowBuilder builder)
        {
            var req = builder.WithVariable<string>(_requete);
            var conn = builder.WithVariable<string>(_data);
            var type=builder.WithVariable<string>(_type);
            builder.Root = new Sequence
            {
                Activities =
            {
                new ExecuterRequetek(req,conn,type),
                new WriteHttpResponse
                {
                    Content = new("Python Executed")
                }
            }
            };
        }
    }
}
