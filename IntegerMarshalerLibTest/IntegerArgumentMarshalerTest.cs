using ArgumentMarshalerLib;
using IntegerMarshalerLib;
using System;
using System.Linq;
using Xunit;
using static IntegerMarshalerLib.IntegerArgumentMarshaler;

namespace IntegerMarshalerLibTest
{
    public class IntegerArgumentMarshalerTest
    {
        private string[] _args;

        IntegerArgumentMarshaler _argumentMarshaler;
        Iterator<string> _iterator;

        public IntegerArgumentMarshalerTest()
        {
            _args = new string[] { "-logging", "-port", "1", "-name", "com1" };
            _argumentMarshaler = new IntegerArgumentMarshaler();
            _iterator = new Iterator<string>(_args);
        }

        [Fact]
        public void IntegerArgumentMarshalerSetValue_PassingTest()
        {
            for (int i = 0; i < _args.Length; i++)
            {
                if (_iterator.Current.Any(x => char.IsDigit(x)))
                {
                    break;
                }

                _iterator.Next();
            }

            _argumentMarshaler.Set(_iterator);

            Assert.Equal(Convert.ToInt32(_iterator.Previous()), _argumentMarshaler.Value);
        }

        [Fact]
        public void IntegerArgumentMarshalerSetNullValue_FailingTest()
        {
            Assert.Throws<IntegerArgumentMarshalerException>(() => _argumentMarshaler.Set(null));
        }

        [Fact]
        public void IntegerArgumentMarshalerSetLastIteratorValue_FailingTest()
        {
            for (int i = 0; i < _args.Length; i++)
            {
                _iterator.Next();
            }

            Assert.Throws<IntegerArgumentMarshalerException>(() => _argumentMarshaler.Set(_iterator));
        }

        [Fact]
        public void IntegerArgumentMarshalerCreateReferenceAndSetInvalidFormat_FailingTest()
        {
            Assert.Throws<IntegerArgumentMarshalerException>(() => _argumentMarshaler.Set(_iterator));
        }

        [Fact]
        public void IntegerArgumentMarshalerExceptionCreateReferenceWithErrorCode_PassingTest()
        {
            IntegerArgumentMarshalerException argumentMarshalerException = new IntegerArgumentMarshalerException(ErrorCode.OK);

            Assert.Equal(ErrorCode.OK, argumentMarshalerException.ErrorCode);
            Assert.Equal("TILT: Should not be reached!", argumentMarshalerException.ErrorMessage());
        }

        [Fact]
        public void IntegerArgumentMarshalerExceptionCreateReferenceWithErrorCodeAndParameter()
        {
            IntegerArgumentMarshalerException argumentMarshalerException = new IntegerArgumentMarshalerException(ErrorCode.INVALID_PARAMETER, null);

            Assert.Equal(ErrorCode.INVALID_PARAMETER, argumentMarshalerException.ErrorCode);
            Assert.Equal($"'-{null}' is not valid!", argumentMarshalerException.ErrorMessage());
        }
    }
}
