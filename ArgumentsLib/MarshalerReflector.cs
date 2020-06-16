using ArgumentMarshalerLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ArgumentsLib
{
    public class MarshalerReflector
    {
        private readonly string _directory;

        private string[] _filePaths;
        private List<Assembly> _assemblies;
        private List<Type> _types;

        public MarshalerReflector(string directory)
        {
            if (!Directory.Exists(directory))
                throw new ArgumentException(ErrorCode.GLOBAL, $"Directory does not exist! ({directory})");

            _directory = directory;

            _assemblies = new List<Assembly>();
            _types = new List<Type>();

            var paths = SetFilePaths();
            if (paths.Length == 0)
                throw new ArgumentException(ErrorCode.GLOBAL, "Couldn't find any assembly!");

            _filePaths = paths;
            _assemblies = LoadAssemblies();
            _types = SetTypes(_assemblies);
        }

        private string[] SetFilePaths()
        {
            return Directory.GetFiles(_directory, "*.dll");
        }

        private List<Assembly> LoadAssemblies()
        {
            List<Assembly> assemblies = new List<Assembly>();

            foreach (string path in _filePaths)
            {
                assemblies.Add(Assembly.LoadFrom(path));
            }

            return assemblies;
        }

        private List<Type> SetTypes(List<Assembly> assemblies)
        {
            List<Type> types = new List<Type>();

            foreach (Assembly assembly in assemblies)
            {
                // Problem:
                // Die Klasse mit dem Schema muss die erste oder einzige in dem Namespace sein damit es funktioniert
                types.Add(assembly.GetType(assembly.DefinedTypes.First().FullName));
            }

            return types;
        }

        public ArgumentMarshaler GetInstanceBySchema(string schema)
        {
            Type correctType = null;

            if (schema == null)
                throw new ArgumentException(ErrorCode.GLOBAL, "Schema must not be null!");

            foreach (Type type in _types)
            {
                object instance = Activator.CreateInstance(type);
                PropertyInfo instanceInfo = type.GetProperty("Schema");
                string value = instanceInfo.GetValue(instance).ToString();

                if (value == null)
                    throw new ArgumentException(ErrorCode.GLOBAL, "Schema must not be null!");

                if (schema == value)
                {
                    correctType = type;
                    break;
                }
            }

            return (ArgumentMarshaler)Activator.CreateInstance(correctType);
        }
    }
}
