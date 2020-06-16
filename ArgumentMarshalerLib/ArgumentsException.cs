using System;

namespace ArgumentMarshalerLib
{
    public enum ErrorCode
    {
        OK,
        UNEXPECTED_ARGUMENT,
        MISSING,
        INVALID,
        INVALID_PARAMETER,
        INVALID_ARGUMENT_NAME,
        INVALID_SCHEMA,
        GLOBAL
    }

    public abstract class ArgumentsException : Exception
    {
        public ArgumentsException()
        {

        }

        public ArgumentsException(string message) : base(message)
        {

        }

        public ArgumentsException(ErrorCode errorCode)
        {
            this.ErrorCode = errorCode;
        }

        public ArgumentsException(ErrorCode errorCode, string errorParameter)
        {
            this.ErrorCode = errorCode;
            this.ErrorParameter = errorParameter;
        }

        public ArgumentsException(ErrorCode errorCode, string errorArgumentId, string errorParameter)
        {
            this.ErrorCode = errorCode;
            this.ErrorArgumentId = errorArgumentId;
            this.ErrorParameter = errorParameter;
        }

        public string ErrorArgumentId { get; set; }
        public string ErrorParameter { get; set; }
        public ErrorCode ErrorCode { get; set; }

        public abstract string ErrorMessage();
    }
}
