using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;
using UTTool.Core.Extension;

namespace UTTool.Core.Generate.GenerateObject
{
    internal class InjectionMethodGenerate : IGenerate
    {
        public InjectionMethodGenerate(DescripterNode descripterNode, bool _isVerify)
        {
            this.IsVerify = _isVerify;
            this.DescripterNode = descripterNode;
        }
        public DescripterNode DescripterNode { get; set; }
        public bool IsVerify { get; set; }
        public void Generate(GenerateContext generateContext)
        {
            var scope = generateContext.GetOrCreateSetUpScope();
            var parent = scope.GenerateItems.Where(g => g.DescripterNode == this.DescripterNode.Parent).FirstOrDefault();
            if (parent != null)
            {
                if (!IsVerify)
                {
                    generateContext.Text.Append($"      _{parent.DescripterNode.Name.Substring(1).GetFirstLowerString()}.Setup(obj => obj.{this.DescripterNode.Name}({SetItExperssion()})).{this.SetReturnParameters()}Verifiable();\r\n");
                }
                else
                {
                    generateContext.Text.Append($"      _{parent.DescripterNode.Name.Substring(1).GetFirstLowerString()}.Verify(obj => obj.{this.DescripterNode.Name}({SetItExperssion()}),Times.Once);\r\n");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string SetItExperssion()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var para in (this.DescripterNode as MethodDescipter).MethodInfo.GetParameters())
            {
                sb.Append("It.IsAny<");
                if (!para.ParameterType.IsGenericType)
                {
                    sb.Append(para.ParameterType.Name);
                }
                else
                {
                    sb.Append(para.ParameterType.GetGenericArguments()[0].Name);
                }
                sb.Append(">()");
                sb.Append(",");
            }
            if (sb.Length > 0)
            {
                sb = sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string SetReturnParameters()
        {
            var rp = (this.DescripterNode as MethodDescipter).MethodInfo.ReturnParameter;
            if (rp.ParameterType == typeof(void))
            {
                return "";
            }
            if (!rp.ParameterType.IsGenericType)
            {
                if (rp.ParameterType.IsValueType || rp.ParameterType == typeof(string))
                {
                    return $"Returns({this.GetPropertyDefaultValue(rp.ParameterType)}).";
                }
                else
                {
                    var constructor = rp.ParameterType.GetConstructors().FirstOrDefault();
                    if (constructor != null)
                    {
                        var sb = new StringBuilder();
                        foreach (var cp in constructor.GetParameters())
                        {
                            sb.Append(this.GetPropertyDefaultValue(cp.ParameterType));
                            sb.Append(",");
                        }
                        if (sb.Length > 0)
                        {
                            sb = sb.Remove(sb.Length - 1, 1);
                        }
                        return $"Returns(new {rp.ParameterType.Name}({sb.ToString()})).";
                    }
                    else
                    {
                        return $"Returns(new {rp.ParameterType.Name}()).";
                    }
                }
            }
            else
            {
                if (rp.ParameterType.GetGenericTypeDefinition().IsAssignableFrom(typeof(Nullable<>)))
                {
                    return $"Returns({this.GetPropertyDefaultValue(rp.ParameterType.GetGenericArguments()[0])}).";
                }
                else if (rp.ParameterType.GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>)))
                {
                    return $"Returns(new List<{rp.ParameterType.GetGenericArguments()[0]}>()).";
                }
            }
            return "";
        }
    }
}
