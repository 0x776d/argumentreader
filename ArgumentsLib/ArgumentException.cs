using ArgumentMarshalerLib;

namespace ArgumentsLib
{
    public class ArgumentException : ArgumentsException
    {
        public ArgumentException()
        {
        }

        public ArgumentException(string message) : base(message)
        {
        }

        public ArgumentException(ErrorCode errorCode) : base(errorCode)
        {

        }

        public ArgumentException(ErrorCode errorCode, string errorParameter) : base(errorCode, errorParameter)
        {

        }

        public ArgumentException(ErrorCode errorCode, string errorArgumentId, string errorParameter) : base(errorCode, errorArgumentId, errorParameter)
        {

        }

        public override string ErrorMessage()
        {
            switch (ErrorCode)
            {
                case ErrorCode.OK:
                    return "TILT: Should not be reached!";
                case ErrorCode.UNEXPECTED_ARGUMENT:
                    return $"Argument - {ErrorArgumentId} unexpected!";
                case ErrorCode.MISSING:
                    return $"Could not find parameter foreach - {ErrorArgumentId}!";
                case ErrorCode.INVALID:
                    return $"Argument - {ErrorArgumentId} does not expect '{ErrorParameter}'!";
                case ErrorCode.INVALID_PARAMETER:
                    return $"'-{ErrorParameter}' is not valid!";
                case ErrorCode.INVALID_ARGUMENT_NAME:
                    return $"'-{ErrorArgumentId}' is not a valid argument name!";
                case ErrorCode.GLOBAL:
                    return $"{ErrorParameter}";
                default:
                    return string.Empty;
            }
        }
    }
}
