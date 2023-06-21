using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UTTool.Core.Descriptor
{
    public class MethodDescipter : DescripterNode
    {
        /// <summary>
        /// 
        /// </summary>
        public MethodDescipter(MethodInfo methodInfo)
        {
            this.MethodInfo = methodInfo;
            this.Name = methodInfo.Name;
            this.IsGeneric = methodInfo.IsGenericMethod;
        }
        /// <summary>
        /// 
        /// </summary>
        public MethodInfo MethodInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsGeneric { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="decoraterContext"></param>
        /// <exception cref="NotImplementedException"></exception>
        internal override void FIlteringChildren(DecoraterContext decoraterContext)
        {
            this.Children.ForEach(child =>
            {
                child.FIlteringChildren(decoraterContext);
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="decoraterContext"></param>
        /// <exception cref="NotImplementedException"></exception>
        internal override void Load(DecoraterContext decoraterContext)
        {
            this.Children = new List<DescripterNode>();
            var parameters = this.MethodInfo.GetParameters();
            foreach (var param in parameters)
            {
                if (param.ParameterType.IsGenericType)
                {
                    if (param.ParameterType.GenericTypeArguments.Length > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var arg in param.ParameterType.GenericTypeArguments)
                        {
                            sb.Append(arg.Name);
                            sb.Append(", ");
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                        this.Children.Add(new ParameterDescripter() { ParameterName = param.Name, Name = param.Name + ":" + param.ParameterType.ToString(), Type = param.ParameterType, TypeName = param.ParameterType.ToString(), NodeType = NodeType.Parameter, Parent = this });
                    }
                }
                else
                {
                    this.Children.Add(new ParameterDescripter() { ParameterName = param.Name, Name = param.Name + ":" + param.ParameterType.Name, Type = param.ParameterType, TypeName = param.ParameterType.Name, NodeType = NodeType.Parameter, Parent = this });
                }
            }
        }
    }
}
