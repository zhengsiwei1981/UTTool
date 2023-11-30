using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTTool.Core;

namespace UITool.UI
{
    public partial class Child : Form
    {
        public Child(List<LoadAssemblyException> loadAssemblyExceptions)
        {
            InitializeComponent();

            var refExecptions = new List<ReflectionTypeLoadSubException>();
            loadAssemblyExceptions.ForEach(l =>
            {
                refExecptions.AddRange(l.RTExceptions);
            });

            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = refExecptions.Distinct(new rtCompare()).ToList();
        }
    }
    public class rtCompare : IEqualityComparer<ReflectionTypeLoadSubException>
    {
        public bool Equals(ReflectionTypeLoadSubException? x, ReflectionTypeLoadSubException? y)
        {  
            return x.AssemblyName == y.AssemblyName && x.TargetAssemblyName == y.TargetAssemblyName;
        }

        public int GetHashCode(ReflectionTypeLoadSubException obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
