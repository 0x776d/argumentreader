using ArgumentMarshalerLib;
using ArgumentsLib;
using Xunit;
using ArgumentException = ArgumentsLib.ArgumentException;

namespace ArgumentsLibTest
{
    public class MarshalerReflectorTest
    {
        private const string _DIRECTORY = @"./Marshaler";

        MarshalerReflector _reflector;

        public MarshalerReflectorTest()
        {
            _reflector = new MarshalerReflector(_DIRECTORY);
        }

        [Fact]
        public void MarshalerReflectorCreateReferenceWithNullDirectory_FaillingTest()
        {
            MarshalerReflector reflector;

            ArgumentException ex = Assert.Throws<ArgumentException>(() => reflector = new MarshalerReflector(null));

            Assert.Equal("Directory does not exist! ()", ex.ErrorMessage());
        }

        [Fact]
        public void MarshalerReflectorCreateReferenceWithNotExistingDirectory_FaillingTest()
        {
            MarshalerReflector reflector;
            string directory = @"./HalloBlaBla";

            ArgumentException ex = Assert.Throws<ArgumentException>(() => reflector = new MarshalerReflector(directory));

            Assert.Equal($"Directory does not exist! ({directory})", ex.ErrorMessage());
        }

        [Fact]
        public void MarshalerReflectorCreateReferenceWithoutAssembliesInPath_FaillingTest()
        {
            MarshalerReflector reflector;
            string directory = @"../";

            ArgumentException ex = Assert.Throws<ArgumentException>(() => reflector = new MarshalerReflector(directory));

            Assert.Equal("Couldn't find any assembly!", ex.ErrorMessage());
        }

        [Fact]
        public void MarshalerReflectorGetInstanceBySchema_PassingTest()
        {
            string schema = "";

            ArgumentMarshaler argumentMarshaler = _reflector.GetInstanceBySchema(schema);

            Assert.NotNull(argumentMarshaler);
        }

        [Fact]
        public void MarshalerReflectorGetInstanceBySchemaWithNull_FailingTest()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => _reflector.GetInstanceBySchema(null));

            Assert.Equal("Schema must not be null!", ex.ErrorMessage());
        }
    }
}
