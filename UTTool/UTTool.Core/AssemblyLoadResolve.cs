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
                        throw new LoadAssemblyException("Couldn't not find dependecy dll in specified directory") { TargetAssembly = assName};
                    }
                    //throw new LoadAssemblyException(string.Format("didn't find config element with Name : {0}", nameSplitVal[0])) { ExceptionType = ExceptionType.NotFindConfig, AssemblyName = nameSplitVal[0] };
                }
            }
            else
            {
                throw new LoadAssemblyException("it's not acceptable assembly name") { TargetAssembly = assName};
            }
        }
    }


}
