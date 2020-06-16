using ArgumentMarshalerLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooleanMarshalerLib
{
    public class BooleanArgumentMarshaler : ArgumentMarshaler
    {
        public override string Schema => "";

        public override void Set(Iterator<string> currentArgument)
        {
            Value = true;
        }

        public class BooleanArgumentMarshalerException : ArgumentsException
        {
            public BooleanArgumentMarshalerException(ErrorCode errorCode) : base(errorCode)
            {

            }

            public override string ErrorMessage()
            {
                switch (ErrorCode)
                {
                    case ErrorCode.OK:
                        return "TILT: Should not be reached!";
                    default:
                        return string.Empty;
                }
            }
        }
    }

}
