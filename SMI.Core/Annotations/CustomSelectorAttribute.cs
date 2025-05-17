using System;

namespace SMI.Core.Annotations {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CustomSelectorAttribute : Attribute {
        public Type? ValueSelectorType { get; set; }
        public Type? FieldSelectorType { get; set; }
    }
}
