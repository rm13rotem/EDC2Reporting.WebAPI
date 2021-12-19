using System;
using System.Collections.Generic;
using System.Text;

namespace DataServices.Interfaces
{
    public interface IHasJsonValue
    {
        string JsonValue { get; set; }
    }
}
