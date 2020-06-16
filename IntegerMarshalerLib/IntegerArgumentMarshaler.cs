using ArgumentMarshalerLib;
using System;
using ArgumentsException = ArgumentMarshalerLib.ArgumentsException;
using ErrorCode = ArgumentMarshalerLib.ErrorCode;

namespace IntegerMarshalerLib
{
    public class IntegerArgumentMarshaler : ArgumentMarshaler
    {
        public override string Schema => "#";

        public override void Set(Iterator<string> currentArgument)
        {
            string parameter = null;

            try
            {
                parameter = currentArgument.Next();
                Value = int.Parse(parameter);
            }
            catch (NullReferenceException)
            {
                throw new IntegerArgumentMarshalerException(ErrorCode.INVALID_PARAMETER, null);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new IntegerArgumentMarshalerException(ErrorCode.MISSING);
            }
            catch (FormatException)
            {
                throw new IntegerArgumentMarshalerException(ErrorCode.INVALID, parameter);
            }
        }

        public class IntegerArgumentMarshalerException : ArgumentsException
        {
            public IntegerArgumentMarshalerException(ErrorCode errorCode) : base(errorCode)
            {

            }

            public IntegerArgumentMarshalerException(ErrorCode errorCode, string parameter) : base(errorCode, parameter)
            {

            }

            public override string ErrorMessage()
            {
                switch (ErrorCode)
                {
                    case ErrorCode.OK:
                        return "TILT: Should not be reached!";
                    case ErrorCode.INVALID_PARAMETER:
                        return $"'-{ErrorParameter}' is not valid!";
                    case ErrorCode.MISSING:
                        return $"Parameter is missing!";
                    case ErrorCode.GLOBAL:
                        return $"{ErrorParameter}";
                    default:
                        return string.Empty;
                }
            }
        }
    }
}
