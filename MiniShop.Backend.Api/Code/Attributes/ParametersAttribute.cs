namespace System.ComponentModel
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ParametersAttribute : Attribute
    {
        public string name { get; set; }

        public string param { get; set; }

        public ParametersAttribute()
        {
        }
    }
}
