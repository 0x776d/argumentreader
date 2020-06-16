using ArgumentMarshalerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsLib
{
    public class Arguments
    {
        private string _directory;
        private Dictionary<string, ArgumentMarshaler> _marshalers;
        private Iterator<string> _currentArgument;
        private List<string> _argumentsFound;
        private MarshalerReflector _reflector;

        public Arguments(string schema, IEnumerable<string> args, string directory)
        {
            if (string.IsNullOrWhiteSpace(schema))
                throw new ArgumentException(ErrorCode.GLOBAL, "Schema must not be null or whitespace only!");

            if (args == null || args.Count() < 1)
                throw new ArgumentException(ErrorCode.GLOBAL, "There are no given arguments!");

            _directory = directory ?? throw new ArgumentException(ErrorCode.GLOBAL, "Directory must not be null");
            _reflector = new MarshalerReflector(_directory);

            _marshalers = new Dictionary<string, ArgumentMarshaler>();
            _argumentsFound = new List<string>();

            ParseSchema(schema);
            ParseArgumentStrings(new List<string>(args));
        }

        public IEnumerable<string> ArgumentsFound => _argumentsFound;

        public IEnumerable<string> Schema
        {
            get
            {
                List<string> schemaList = new List<string>();

                foreach (string marshaler in _marshalers.Keys)
                {
                    schemaList.Add(marshaler);
                }

                return schemaList;
            }
        }

        public T GetValue<T>(string argument)
        {
            try
            {
                if (_marshalers.ContainsKey(argument))
                {
                    if (_marshalers[argument].Value == null)
                        return default(T);
                    else
                        return (T)_marshalers[argument].Value;
                }
                else
                {
                    throw new ArgumentException(ErrorCode.INVALID_PARAMETER, argument);
                }
            }
            catch (ArgumentsException ex)
            {
                throw ex;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentException(ErrorCode.GLOBAL, "Argument must not be null!");
            }
            catch (Exception)
            {
                throw new ArgumentException(ErrorCode.GLOBAL, "There is no value with given Type for given Argument!");
            }
        }

        private void ParseSchema(string schema)
        {
            // "a,b*,c#"
            foreach (string argument in schema.Split(','))
            {
                if (argument.Length > 0 && !string.IsNullOrWhiteSpace(argument))
                {
                    ParseSchemaArgument(argument.Trim());
                }
            }
        }

        private void ParseSchemaArgument(string argument)
        {
            // 1.) a 
            // 2.) b* 
            // 3.) c#
            StringBuilder argumentId = new StringBuilder();
            StringBuilder argumentTail = new StringBuilder();

            foreach (char c in argument)
            {
                if (char.IsLetter(c))
                {
                    argumentId.Append(c);
                }
                else
                {
                    argumentTail.Append(c);
                    break;
                }
            }

            var reflectorResult = _reflector.GetInstanceBySchema(argumentTail.ToString()) ?? throw new ArgumentException(ErrorCode.INVALID_ARGUMENT_NAME, argumentId.ToString(), null);
            _marshalers.Add(argumentId.ToString(), reflectorResult);
        }

        private void ParseArgumentStrings(List<string> argumentList)
        {
            // -a                   1.) Schleifendurchlauf
            // -b                   2.) Schleifendurchlauf
            // Das ist ein Test
            // -c                   3.) Schleifendurchlauf
            // 1234
            for (_currentArgument = new Iterator<string>(argumentList); _currentArgument.HasNext;)
            {
                string argumentString = _currentArgument.Next();    // -a

                if (argumentString.Length > 0 && argumentString.ElementAt(0) == '-')
                {
                    ParseArgumentString(argumentString.Substring(1));
                }
                else
                {
                    throw new ArgumentException(ErrorCode.INVALID, argumentString);
                }
            }
        }

        private void ParseArgumentString(string argumentChars)
        {
            ParseArgumentChar(argumentChars);
        }

        private void ParseArgumentChar(string argumentChar)
        {
            // Prüfen ob das Argument in unserem Wörterbuch (Schema) steht
            // Wäre auch mit ContainsKey() möglich
            if (!_marshalers.TryGetValue(argumentChar, out ArgumentMarshaler m))
                throw new ArgumentException(ErrorCode.UNEXPECTED_ARGUMENT, argumentChar, null);

            _argumentsFound.Add(argumentChar);

            try
            {
                // 1. Iteration
                // _currentArgument = Referenz auf Iterator
                // Marshaler = BooleanArgumentMarshaler

                // 1. Iteration
                // _currentArgument = Referenz auf Iterator
                // Marshaler = StringArgumentMarshaler
                m.Set(_currentArgument);
            }
            catch (ArgumentsException ex)
            {
                ex.ErrorArgumentId = argumentChar;
                throw ex;
            }
        }
    }
}
