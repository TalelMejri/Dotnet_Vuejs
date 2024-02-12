using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Memory;
using Elsa.Workflows.Models;
using System.Xml;

namespace Backend.Activities
{
    public class readpbmn : CodeActivity<string>
    {
        public readpbmn(Variable<string> Name) {
            nameFile = new(Name);
        }
        public Input<string> nameFile { get; set; }
            public string ExtractUserIdFromBpmn(string bpmnFilePath)
             {
                 try
                 {
                     XmlDocument xmlDoc = new XmlDocument();
                     xmlDoc.Load(bpmnFilePath);
                     XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                     nsmgr.AddNamespace("bpmn", "http://www.omg.org/spec/BPMN/20100524/MODEL");
                     XmlNode serviceTaskNode = xmlDoc.SelectSingleNode("//bpmn:serviceTask", nsmgr);
                     if (serviceTaskNode == null)
                      {
                          return "videe";
                     }
                     // Extract the value of the name attribute
                     string taskName = serviceTaskNode.Attributes["name"].Value;
                     return taskName;
                 }
                 catch (Exception ex)
                 {
                     return "5";
                 }
             }

        protected override void Execute(ActivityExecutionContext context)
        {
            string bpmnFilePath = context.Get(nameFile)!;
            Result.Set(context, ExtractUserIdFromBpmn(bpmnFilePath));
        }

    }
}


