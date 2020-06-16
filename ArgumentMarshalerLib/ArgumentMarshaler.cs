using System;
using System.Collections.Generic;
using System.Text;

namespace ArgumentMarshalerLib
{
    public abstract class ArgumentMarshaler
    {
        public abstract string Schema { get; }

        public object Value { get; protected set; }

        public abstract void Set(Iterator<string> currentArgument);
    }
}
