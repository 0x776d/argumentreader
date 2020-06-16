using ArgumentMarshalerLib;
using BooleanMarshalerLib;
using Xunit;
using static BooleanMarshalerLib.BooleanArgumentMarshaler;

namespace BooleanMarshalerLibTest
{
    public class BooleanArgumentMarshalerTest
    {
        private string[] _args;

        BooleanArgumentMarshaler _argumentMarshaler;
        Iterator<string> _iterator;

        public BooleanArgumentMarshalerTest()
        {
            _args = new string[] { "-logging", "-port", "1", "-name", "com1" };
            _argumentMarshaler = new BooleanArgumentMarshaler();
            _iterator = new Iterator<string>(_args);
        }

        [Fact]
        public void BooleanArgumentMarshalerSetValue_PassingTest()
        {
            _argumentMarshaler.Set(_iterator);

            Assert.True((bool)_argumentMarshaler.Value);
        }

        [Fact]
        public void BooleanArgumentMarshalerSetNullValue_PassingTest()
        {
            _argumentMarshaler.Set(null);

            Assert.True((bool)_argumentMarshaler.Value);
        }

        [Fact]
        public void BooleanArgumentMarshalerExceptionWithErrorCodeOK()
        {
            BooleanArgumentMarshalerException argumentMarshalerException = new BooleanArgumentMarshalerException(ErrorCode.OK);

            Assert.Equal(ErrorCode.OK, argumentMarshalerException.ErrorCode);
            Assert.Equal("TILT: Should not be reached!", argumentMarshalerException.ErrorMessage());
        }
    }
}
