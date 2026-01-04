using System;

namespace DataServices.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SanitizeInputAttribute : Attribute
    {
    }

}
