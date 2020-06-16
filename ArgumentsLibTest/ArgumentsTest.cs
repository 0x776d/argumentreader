using ArgumentsLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using ArgumentException = ArgumentsLib.ArgumentException;

namespace ArgumentsLibTest
{
    public class ArgumentsTest
    {
        private const string _DIRECTORY = @"./Marshaler";
        private const string _SCHEMA = "logging,port#,name*";

        private string[] _args;

        public ArgumentsTest()
        {
            _args = new string[] { "-logging", "-port", "1", "-name", "com1" };
        }

        [Fact]
        public void ArgumentsCreateReferenceWithEmptySchema_FailingTest()
        {
            Arguments arguments;

            ArgumentException ex = Assert.Throws<ArgumentException>(() => arguments = new Arguments(string.Empty, _args, _DIRECTORY));

            Assert.Equal("Schema must not be null or whitespace only!", ex.ErrorMessage());
        }

        [Fact]
        public void ArgumentsCreateReferenceWithNullSchema_FailingTest()
        {
            Arguments arguments;

            ArgumentException ex = Assert.Throws<ArgumentException>(() => arguments = new Arguments(null, _args, _DIRECTORY));

            Assert.Equal("Schema must not be null or whitespace only!", ex.ErrorMessage());
        }

        [Fact]
        public void ArgumentsCreateReferenceWithEmptyArgs_FaillingTest()
        {
            Arguments arguments;

            ArgumentException ex = Assert.Throws<ArgumentException>(() => arguments = new Arguments(_SCHEMA, new List<string>(), _DIRECTORY));

            Assert.Equal("There are no given arguments!", ex.ErrorMessage());
        }

        [Fact]
        public void ArgumentsCreateReferenceWithNullArgs_FaillingTest()
        {
            Arguments arguments;

            ArgumentException ex = Assert.Throws<ArgumentException>(() => arguments = new Arguments(_SCHEMA, null, _DIRECTORY));

            Assert.Equal("There are no given arguments!", ex.ErrorMessage());
        }

        [Fact]
        public void ArgumentsCreateReferenceWithEmptyDirectory_Faillingtest()
        {
            Arguments arguments;

            ArgumentException ex = Assert.Throws<ArgumentException>(() => arguments = new Arguments(_SCHEMA, _args, string.Empty));

            Assert.Equal("Directory does not exist! ()", ex.ErrorMessage());
        }

        [Fact]
        public void ArgumentsCreateReferenceWithNullDirectory_Faillingtest()
        {
            Arguments arguments;

            ArgumentException ex = Assert.Throws<ArgumentException>(() => arguments = new Arguments(_SCHEMA, _args, null));

            Assert.Equal("Directory must not be null", ex.ErrorMessage());
        }

        [Fact]
        public void ArgumentsGetValue_PassingTest()
        {
            Arguments arguments = new Arguments(_SCHEMA, _args, _DIRECTORY);

            bool logging = arguments.GetValue<bool>("logging");
            int port = arguments.GetValue<int>("port");
            string name = arguments.GetValue<string>("name");

            Assert.True(logging);
            Assert.Equal(_args[2].ToString(), 1.ToString());
            Assert.Equal(_args[4].ToString(), name);
        }

        [Fact]
        public void ArgumentsGetValueWithEmptyArgument_FailingTest()
        {
            Arguments arguments = new Arguments(_SCHEMA, _args, _DIRECTORY);

            ArgumentException ex = Assert.Throws<ArgumentException>(() => arguments.GetValue<string>(string.Empty));

            Assert.Equal("'-' is not valid!", ex.ErrorMessage());
        }

        [Fact]
        public void ArgumentsGetValueWithNullArgument_FailingTest()
        {
            Arguments arguments = new Arguments(_SCHEMA, _args, _DIRECTORY);

            ArgumentException ex = Assert.Throws<ArgumentException>(() => arguments.GetValue<string>(null));

            Assert.Equal("Argument must not be null!", ex.ErrorMessage());
        }

        [Fact]
        public void ArgumentsGetValueWithNotExistingKey_FailingTest()
        {
            Arguments arguments = new Arguments(_SCHEMA, _args, _DIRECTORY);

            ArgumentException ex = Assert.Throws<ArgumentException>(() => arguments.GetValue<string>("hallo"));

            Assert.Equal("'-hallo' is not valid!", ex.ErrorMessage());
        }

        [Fact]
        public void ArgumentsGetValueWithWrongDatatype_FailingTest()
        {
            Arguments arguments = new Arguments(_SCHEMA, _args, _DIRECTORY);

            ArgumentException ex = Assert.Throws<ArgumentException>(() => arguments.GetValue<double>("logging"));

            Assert.Equal("There is no value with given Type for given Argument!", ex.ErrorMessage());
        }
    }
}
