using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UTTool.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class LoadAssemblyException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public LoadAssemblyException(string message) : base(message)
        {
        }
        public ExceptionType ExceptionType {
            get; set;
        }
        public string AssemblyName {
            get; set;
        }
        public string TargetAssembly {
            get;set;
        }
        public List<ReflectionTypeLoadSubException> RTExceptions = new List<ReflectionTypeLoadSubException>();
    }
    /// <summary>
    /// 
    /// </summary>
    public class DirectoryLoadException : Exception 
    {
        public string Id {
            get;
            set;
        }
        public DirectoryLoadException()
        {
            this.Id = Guid.NewGuid().ToString();
            loadAssemblyExceptions = new List<LoadAssemblyException>();
        }
        public List<LoadAssemblyException> loadAssemblyExceptions { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ReflectionTypeLoadSubException
    {
        public ReflectionTypeLoadSubException() { }
        public ReflectionTypeLoadSubException(Exception ex, ReflectionTypeLoadException parent)
        {
            this.Name = parent.Message;      
            this.Parent = parent;
            this.TargetAssemblyName = (ex.InnerException as LoadAssemblyException)?.TargetAssembly;
            this.Message = this.Parent.Message;
        }
        public ReflectionTypeLoadException Parent {
            get; set;
        }
        public string TargetAssemblyName { get; set; }
        public string AssemblyName { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public enum ExceptionType
    {
        Unknown,
        NotFindConfig = 1,
        LoadFailure = 2
    }
}
