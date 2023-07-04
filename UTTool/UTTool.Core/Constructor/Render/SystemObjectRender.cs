using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Extension;

namespace UTTool.Core.Constructor.Render
{
    internal class SystemObjectRender : IRender
    {
        public void Render(in ParameterMapping parameterMapping)
        {
            var p = parameterMapping as ReferenceParameterMapping;
            if (!p.Parameter.ParameterType.IsGenericType)
            {
                if (!p.Parameter.ParameterType.IsArray)
                {
                    p.Context.Text.Append($"            new {p.Parameter.ParameterType.Name}(),");
                    p.Context.Text.Append(Environment.NewLine);
                }
                else
                {
                    var elementType = p.Parameter.ParameterType.GetElementType();
                    if (elementType != null)
                    {
                        p.Context.Text.Append($"            new {elementType.Name.ToLower()}[]{{}},");
                        p.Context.Text.Append(Environment.NewLine);
                    }
                }
            }
            else
            {
                p.Context.Text.Append($"{GetGenericTypeRender(p.Parameter.ParameterType)},");
                p.Context.Text.Append(Environment.NewLine);
            }
        }
        private string GetGenericTypeRender(Type type)
        {
            var name = type.GetGenericTypeDefinition().Name.Split(new char[] { '`' })[0];
            var args = type.GetGenericArguments();
            var sb = new StringBuilder();

            sb.Append($"            new {name}<");
            foreach (var arg in args)
            {
                sb.Append(GetArgTypeName(arg));
                sb.Append(",");
            }
            sb = sb.Remove(sb.Length - 1, 1);
            sb.Append(">()");
            return sb.ToString();
        }
        private string GetArgTypeName(Type type)
        {
            if (type.IsValueType || type == typeof(string))
            {
                return type.Name.ToLower();
            }
            return type.Name;
        }
    }
}
