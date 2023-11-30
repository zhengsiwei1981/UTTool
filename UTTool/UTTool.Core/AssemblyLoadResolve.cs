using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Configration;
using UTTool.Core.Extension;

namespace UTTool.Core
{
    internal static class AssemblyLoadResolve
    {
        internal static UTToolConfig? _section;
        /// <summary>
        /// 
        /// </summary>
        static AssemblyLoadResolve()
        {
            _section = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).GetSection("ResolveCollectionSection") as UTToolConfig;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static UTToolConfigElement? GetResolveAssembly(string name)
        {
            if (_section != null)
            {
                return _section.Collection.GetElement(name);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        internal static Assembly AssemblyResolve(object? sender, ResolveEventArgs args)
        {
            var assName = args.Name.GetLimitName();
            if (!string.IsNullOrEmpty(assName))
            {
                var element = GetResolveAssembly(assName);
                if (element != null)
                {
                    if (element.PreInstall)
                    {
                        return Assembly.Load(element.Map);
                    }
                    else
                    {
                        return Assembly.LoadFrom(element.Map);
                    }
                }
                else
                {
                    var dllPath = $"{AppDomain.CurrentDomain.BaseDirectory}dependency\\{assName}.dll";
                    if (File.Exists(dllPath))
                    {
                        return Assembly.LoadFrom(dllPath);
                    }
                    else
                    {
                        string dllPath2 = "";
                        if (System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription == ".NET Framework 4.8.4645.0")
                        {
                            dllPath2 = $"{AssemblyLoader.BasePath}\\{assName}.dll";
                        }
                        else
                        {
                            dllPath2 = $"{new DirectoryInfo(args.RequestingAssembly.Location).Parent.FullName}\\{assName}.dll";
                        }
                        if (File.Exists(dllPath2))
                        {
                            return Assembly.LoadFrom(dllPath2);
                        }
                    }
                    throw new LoadAssemblyException(string.Format("didn't find config element with Name : {0}", assName)) { ExceptionType = ExceptionType.NotFindConfig, AssemblyName = assName };
                }
            }
            else
            {
                throw new LoadAssemblyException("it's not acceptable assembly name") { TargetAssembly = assName};
            }
        }
    }


}
