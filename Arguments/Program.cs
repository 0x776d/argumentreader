using ArgumentMarshalerLib;
using ArgumentsLib;
using System;
using ArgumentException = ArgumentMarshalerLib.ArgumentsException;

namespace Argument
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //          bool | string | int
                // Args.exe -a -b "Das ist ein Test" -c 1234
                Arguments parameter = new Arguments("logging,text*", args, @$"{Environment.CurrentDirectory}\Marshaler");

                Console.WriteLine(parameter.GetValue<bool>("logging"));
                Console.WriteLine(parameter.GetValue<string>("text"));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.ErrorMessage());
            }

            Console.ReadKey();
        }
    }
}
