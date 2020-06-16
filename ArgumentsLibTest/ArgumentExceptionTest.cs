using ArgumentMarshalerLib;
using Xunit;
using ArgumentException = ArgumentsLib.ArgumentException;

namespace ArgumentsLibTest
{
    public class ArgumentExceptionTest
    {
        public ArgumentExceptionTest()
        {

        }

        [Fact]
        public void ArgumentExceptionCreateReference_PassingTest()
        {
            ArgumentException argumentException = new ArgumentException();

            Assert.NotNull(argumentException);
        }

        [Fact]
        public void ArgumentExceptionCreateReferenceWithMessage_PassingTest()
        {
            string message = "Das ist ein Test";
            ArgumentException argumentException = new ArgumentException(message);

            Assert.Equal(message, argumentException.Message);
        }

        [Fact]
        public void ArgumentExceptionCreateReferenceWithErrorCode_PassingTest()
        {
            ErrorCode errorCode = ErrorCode.OK;
            ArgumentException argumentException = new ArgumentException(errorCode);

            Assert.Equal(errorCode, argumentException.ErrorCode);
            Assert.Equal("TILT: Should not be reached!", argumentException.ErrorMessage());
        }

        [Fact]
        public void ArgumentExceptionCreateReferenceWithErrorCodeAndErrorParameter()
        {
            ErrorCode errorCode = ErrorCode.INVALID_PARAMETER;
            string parameter = "null";

            ArgumentException argumentException = new ArgumentException(errorCode, parameter);

            Assert.Equal(errorCode, argumentException.ErrorCode);
            Assert.Equal(parameter, argumentException.ErrorParameter);
            Assert.Equal("'-null' is not valid!", argumentException.ErrorMessage());
        }

        [Fact]
        public void ArgumentExceptionCreateReferenceWithErrorCodeAndErrorArgumentIdAndErrorParameter()
        {
            ErrorCode errorCode = ErrorCode.INVALID;
            string errorArgumentId = "port";
            string errorParameter = "Port1";

            ArgumentException argumentException = new ArgumentException(errorCode, errorArgumentId, errorParameter);

            Assert.Equal(errorCode, argumentException.ErrorCode);
            Assert.Equal(errorArgumentId, argumentException.ErrorArgumentId);
            Assert.Equal(errorParameter, argumentException.ErrorParameter);
            Assert.Equal($"Argument - {errorArgumentId} does not expect '{errorParameter}'!", argumentException.ErrorMessage());
        }
    }
}
