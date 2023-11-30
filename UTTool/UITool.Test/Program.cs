using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using UITool;
using UTTool.Core;

namespace UITool.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string file = "";
            //using FileStream fileStream = File.OpenRead(file);
            //using PEReader peReader = new PEReader(fileStream);

            //var mdReader = peReader.GetMetadataReader();

            //foreach (var typeHandler in mdReader.TypeDefinitions)
            //{
            //    var typeDef = mdReader.GetTypeDefinition(typeHandler);
            //    string name = mdReader.GetString(typeDef.Name);
            //    string nameSpace = mdReader.GetString(typeDef.Namespace);
            //    Console.WriteLine($"***********{nameSpace}.{name}***********");
            //    foreach (var methodHandler in typeDef.GetMethods())
            //    {
            //        var methodDef = mdReader.GetMethodDefinition(methodHandler);

            //        Console.WriteLine(mdReader.GetString(methodDef.Name));
            //    }
            //}

            var moduleDef = AsmResolver.DotNet.ModuleDefinition.FromFile(file);//用的不是
           
        }


    }
}