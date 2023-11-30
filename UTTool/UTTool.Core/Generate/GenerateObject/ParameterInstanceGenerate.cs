using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;
using UTTool.Core.Extension;

namespace UTTool.Core.Generate.GenerateObject
{
    internal class ParameterInstanceGenerate : IGenerate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descripterNode"></param>
        public ParameterInstanceGenerate(DescripterNode descripterNode)
        {
            DescripterNode = descripterNode;
        }
        /// <summary>
        /// 
        /// </summary>
        public DescripterNode DescripterNode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="generateContext"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Generate(GenerateContext generateContext)
        {
            var param = this.DescripterNode as ParameterDescripter;
            if (param.Type != null)
            {
                var index = 0;
                var sb = new StringBuilder();
                if (!param.Type.IsGenericType && !param.Type.IsArray && !param.Type.IsInterface)
                {
                    var outputSetup = new OutputSetup();
                    sb.Append($"      var {param.ParameterName.GetFirstLowerString()} = new {param.Type.Name}()");
                    sb.Append(Environment.NewLine);
                    sb.Append("      {");
                    sb.Append(Environment.NewLine);

                    var pList = param.Type.GetProperties().ToList();
                    pList.ForEach(p =>
                    {
                        var sign = index < pList.Count - 1 ? "," : "";
                        outputSetup.Setup(sb, sign, p);
                        sb.Append(Environment.NewLine);
                        index++;
                    });
                    //sb = sb.Remove(sb.Length - 1, 1);
                    sb.Append("      };");
                    sb.Append(Environment.NewLine);
                }
                else if (param.Type.IsGenericType)
                {
                    sb.Append($"      var {param.ParameterName.GetFirstLowerString()} = {GetGenericTypeRender(param.Type)}");
                    sb.Append(";");
                }
                else
                {
                    sb.Append($"      var {param.ParameterName.GetFirstLowerString()} = null;");
                }
                generateContext.Text.Append(sb.ToString());
            }
        }
        private string GetGenericTypeRender(Type type)
        {
            var name = type.GetGenericTypeDefinition().Name.Split(new char[] { '`' })[0];
            name = name == "IEnumerable" ? "List" : name;
            var args = type.GetGenericArguments();
            var sb = new StringBuilder();

            sb.Append($" new {name}<");
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
    internal class OutputSetup
    {
        public OutputSetup() { }
        public void Setup(StringBuilder sb, string sign, PropertyInfo property)
        {
            if (property.PropertyType.IsValueType || property.PropertyType == typeof(string) || property.PropertyType.IsGenericType)
            {
                this.OutputValueType("            ", sb, sign, property);
            }
            else
            {
                if (property.PropertyType.IsClass)
                {
                    this.OutputClass(sb, sign, property);
                }
            }
        }

        private void OutputClass(StringBuilder sb, string sign, PropertyInfo propertyInfo)
        {
            var index = 0;
            sb.Append($"            {propertyInfo.Name} = ");
            sb.Append(Environment.NewLine);
            sb.Append("            {");
            sb.Append(Environment.NewLine);
            var pList = propertyInfo.PropertyType.GetProperties().ToList();
            pList.ForEach(p =>
            {
                var sign = index < pList.Count - 1 ? "," : "";
                if (p.PropertyType.IsValueType || p.PropertyType == typeof(string) || p.PropertyType.IsGenericType)
                {
                    this.OutputValueType("                  ", sb, sign, p);
                    sb.Append(Environment.NewLine);
                }
                else
                {
                    sb.Append($"                  {propertyInfo.Name} = null{sign}");
                }
                index++;
            });
            sb.Append("            }");
            sb.Append(sign);
        }
        private void OutputValueType(string blank,StringBuilder sb, string sign, PropertyInfo propertyInfo)
        {
            sb.Append($"{blank}{propertyInfo.Name} = {this.GetPropertyDefaultValue(propertyInfo.PropertyType)} {sign}");
        }
    }
}
