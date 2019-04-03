using System;

namespace Security
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SecureModuleAttribute : Attribute
    {

    }
}
