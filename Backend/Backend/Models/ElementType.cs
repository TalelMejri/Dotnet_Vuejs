using Newtonsoft.Json;
using System.Collections.Generic;

namespace Backend.Models
{
    public class ElementType
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public List<Documentation> Documentation { get; set; }
        public string Id { get; set; }
        public string EventDefinitions { get; set; }
        public List<ExtensionElement> ExtensionElements { get; set; }
    }

    public class Documentation
    {
        [JsonProperty("textFormat")]
        public string TextFormat { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }


    public class ExtensionElement
    {
        public string Time { get; set; }
        public string Code { get; set; }
        public string TypeSgbd { get; set; }
        public string Path { get; set; }
        public string Requete { get; set; }
        public string ConnectionString { get; set; }
        [JsonProperty("type")]
        public string TypeDef { get; set; }
        public string Retries { get; set; }
        public List<Value> Values { get; set; }
        public List<Input> InputParameters { get; set; }
        public List<Output> OutputParameters { get; set; }
        public List<Property> Properties { get; set; }
        public List<CommentProp> Comments { get; set; }
    }

    public class Value
    {
        public string Key { get; set; }
        public string Values { get; set; }
    }
    public class CommentProp
    {
        public string IdUser { get; set; }
        public string Comment { get; set; }
    }
    public class Input
    {
        public string Source { get; set; }
        public string Target { get; set; }
    }
    public class Output
    {
        public string Source { get; set; }
        public string Target { get; set; }
    }
    public class Property
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
