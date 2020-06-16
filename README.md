[![Version: 1.0 Release](https://img.shields.io/badge/Version-1.0%20Release-green.svg)](https://github.com/0x776d) [![Build Status](https://travis-ci.org/0x776d/argumentreader.svg?branch=master)](https://travis-ci.org/0x776d/argumentreader) [![License: GPL v3](https://img.shields.io/badge/License-GPL%20v3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)

# Argument Reader
---

### Description:

With Argument Reader command line arguments can be passed into a .net core application. The standard project assist 3 types of arguments:

* Boolean
* Strings *(\*)*
* Integers *(#)*

Own argument types can be build with own classes. They need to inherit from the **ArgumentMarshalerLib**. Libraries are loading dynamically on startup. It is not necessary to recompile the complete solution.

---

## Structure

``` csharp
Arguments parameter = new Arguments("SCHEMA", "ARGUMENT ARRAY", "PATH TO MARSHALER LIBRARIES");
```

### Available Marshalers (Standard)

* BooleanMarshalerLib.dll
* StringMarshalerLib.dll
* Integer MarshalerLib.dll

### Schema

1. Parameter name
1. Marshaler type

**Example**

``` csharp
Arguments parameter = new Arguments("enabled,text*,number#", "...", "...");
```

---

## Parse Arguments

### Boolean:

``` bash
Arguments.exe -a
```

``` csharp
static void Main(string[] args)
{
Arguments parameter = new Arguments("a,b", args, @".\Marshaler");
bool a = parameter.GetValue<bool>("a");       // True
bool b = parameter.GetValue<bool>("b");        // False
}
```

### String:

``` bash
Arguments.exe -a "This is a Text"
```

``` csharp
static void Main(string[] args)
{
Arguments parameter = new Arguments("a*", args, @".\Marshaler");
string a = parameter.GetValue<string>("a");     // This is a Text
}
```

### Integer:

``` bash
Arguments.exe -a 1234
```

``` csharp
static void Main(string[] args)
{
Arguments parameter = new Arguments("a#", args, @".\Marshaler");
int a = parameter.GetValue<int>("a");     // 1234
}
```
---

## Build your own Marshaler

1. Create a new VisualStudio .NET Standard Class Library (**??MarshalerLib**)
1. Link a new project reference ArgumentMarshalerLib.dll (in this repository)
1. Write Marshaler (See example code below)
1. Copy the TestMarshalerLib.dll to the Marshaler directory in your project
1. Implement the *?* in your schema (e.g. "mymarshaler?")

``` csharp

using ArgumentMarshalerLib;
using System;

namespace TestMarshalerLib
{
    public class TestArgumentMarshaler : ArgumentMarshaler
    {
        // Only Schemas allowed that are not used (string.Empty, *, # are already used from standard marshalers)
        public override string Schema => "?";

        public override void Set(Iterator<string> currentArgument)
        {
            try
            {
                // If Implementation should use an argument behind the command (e.g. -a "??")
                // it is necessary to move the Iterator to the next position
                Value = currentArgument.Next();
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new TestArgumentMarshalerException(ErrorCode.MISSING);
            }

            // If no argument behind the command is used just add your value
            Value = "This is my personal number";
        }

        public class TestArgumentMarshalerException : ArgumentsException
        {
            public TestArgumentMarshalerException(ErrorCode errorCode) : base(errorCode) { }

            public override string ErrorMessage()
            {
                switch (ErrorCode)
                {
                    case ErrorCode.MISSING:
                        return $"Could not find parameter!";
                    default:
                        return string.Empty;
                }
            }
        }
    }
}

```

---

## References

The original Argument Marshaler was written in Java and published by Robert C. Martin in his book Clean Code. This project adapt his implementations and extends it dynamically.