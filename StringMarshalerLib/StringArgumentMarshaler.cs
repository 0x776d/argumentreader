using ArgumentMarshalerLib;
using System;
using ArgumentsException = ArgumentMarshalerLib.ArgumentsException;
using ErrorCode = ArgumentMarshalerLib.ErrorCode;

namespace StringMarshalerLib
{
    public class StringArgumentMarshaler : ArgumentMarshaler
    {
        public override string Schema => "*";

        public override void Set(Iterator<string> currentArgument)
        {
            try
            {
                Value = currentArgument.Next();
            }
            catch (NullReferenceException)
            {
                throw new StringArgumentMarshalerException(ErrorCode.INVALID_PARAMETER, null);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new StringArgumentMarshalerException(ErrorCode.MISSING);
            }
            catch (Exception ex)
            {
                throw new StringArgumentMarshalerException(ErrorCode.GLOBAL, ex.Message);
            }
        }

        public class StringArgumentMarshalerException : ArgumentsException
        {

            public StringArgumentMarshalerException(ErrorCode errorCode) : base(errorCode)
            {

            }

            public StringArgumentMarshalerException(ErrorCode errorCode, string parameter) : base(errorCode, parameter)
            {

            }

            public override string ErrorMessage()
            {
                switch (ErrorCode)
                {
                    case ErrorCode.OK:
                        return "TILT: Should not be reached!";
                    case ErrorCode.MISSING:
                        return "Parameter is missing!";
                    case ErrorCode.INVALID_PARAMETER:
                        return $"'-{ErrorParameter}' is not valid!";
                    case ErrorCode.GLOBAL:
                        return $"{ErrorParameter}";
                    default:
                        return string.Empty;
                }
            }
        }
    }
}
