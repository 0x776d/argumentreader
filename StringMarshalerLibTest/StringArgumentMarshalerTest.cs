using ArgumentMarshalerLib;
using StringMarshalerLib;
using Xunit;
using static StringMarshalerLib.StringArgumentMarshaler;

namespace StringMarshalerLibTest
{
    public class StringArgumentMarshalerTest
    {
        private string[] _args;

        StringArgumentMarshaler _argumentMarshaler;
        Iterator<string> _iterator;

        public StringArgumentMarshalerTest()
        {
            _args = new string[] { "-logging", "-port", "1", "-name", "com1" };
            _argumentMarshaler = new StringArgumentMarshaler();
            _iterator = new Iterator<string>(_args);
        }

        [Fact]
        public void StringArgumentMarshalerSetValue_PassingTest()
        {
            _argumentMarshaler.Set(_iterator);

            Assert.Equal(_iterator.Previous(), _argumentMarshaler.Value);
        }

        [Fact]
        public void StringArgumentMarshalerSetNullValue_FailingTest()
        {
            Assert.Throws<StringArgumentMarshalerException>(() => _argumentMarshaler.Set(null));
        }

        [Fact]
        public void StringArgumentMarshalerSetLastIteratorValue_FailingTest()
        {
            for (int i = 0; i < _args.Length; i++)
            {
                _iterator.Next();
            }

            Assert.Throws<StringArgumentMarshalerException>(() => _argumentMarshaler.Set(_iterator));
        }

        [Fact]
        public void StringArgumentMarshalerExceptionCreateReferenceWithErrorCodeOK_PassingTest()
        {
            StringArgumentMarshalerException argumentMarshalerException = new StringArgumentMarshalerException(ErrorCode.OK);

            Assert.Equal(ErrorCode.OK, argumentMarshalerException.ErrorCode);
            Assert.Equal("TILT: Should not be reached!", argumentMarshalerException.ErrorMessage());
        }

        [Fact]
        public void StringArgumentMarshalerExceptionCreateReferenceWithErrorCodeMISSING_PassingTest()
        {
            StringArgumentMarshalerException argumentMarshalerException = new StringArgumentMarshalerException(ErrorCode.MISSING);

            Assert.Equal(ErrorCode.MISSING, argumentMarshalerException.ErrorCode);
            Assert.Equal("Parameter is missing!", argumentMarshalerException.ErrorMessage());
        }

        [Fact]
        public void StringArgumentMarshalerExceptionCreateReferenceWithErrorCodeINVALIDPARAMETER_PassingTest()
        {
            StringArgumentMarshalerException argumentMarshalerException = new StringArgumentMarshalerException(ErrorCode.INVALID_PARAMETER);

            Assert.Equal(ErrorCode.INVALID_PARAMETER, argumentMarshalerException.ErrorCode);
            Assert.Equal($"'-{null}' is not valid!", argumentMarshalerException.ErrorMessage());
        }
    }
}
