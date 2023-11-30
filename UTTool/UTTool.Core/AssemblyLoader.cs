using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;
using UTTool.Core.Extension;

namespace UTTool.Core
{
    internal class AssemblyLoader
    {
        static AssemblyLoader()
        {
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyLoadResolve.AssemblyResolve;
        }
        internal static string BasePath {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public static AssemblyDescriptor Load(string path)
        {
            Assembly assembly = null;
            try
            {
                AssemblyLoader.BasePath = Path.GetDirectoryName(path);
                assembly = Assembly.LoadFrom(path);
                //try invoke for check whether the assembly is load successed
                assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException reflection)
            {
                if (reflection != null)
                {
                    var ex = new LoadAssemblyException(reflection.Message) { ExceptionType = ExceptionType.LoadFailure, AssemblyName = assembly.FullName?.GetLimitName() };
                    foreach (var exception in reflection.LoaderExceptions)
                    {
                        ex.RTExceptions.Add(new ReflectionTypeLoadSubException(exception, reflection) { AssemblyName = assembly.FullName?.GetLimitName() });
                    }
                    throw ex;
                }
            }
            catch (LoadAssemblyException ex)
            {
                throw ex;
            }
            return new AssemblyDescriptor(assembly);
        }
    }
}
