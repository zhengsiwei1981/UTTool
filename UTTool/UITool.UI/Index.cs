using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UTTool.Core;
using UTTool.Core.Descriptor;
using UTTool.Core.Generate;
using UTTool.Core.Generate.Batch;

namespace UITool.UI
{
    public partial class Index : Form
    {
        UTToolLoader uTToolLoader = new UTToolLoader();
        DocumentGenerater documentGenerater = new DocumentGenerater();
        TreeNode CurrentEventNode = null;
        QueryThreadDispatch queryThreadDispatch = null;
        List<DescripterNode> BatchList = null;
        /// <summary>
        /// 
        /// </summary>
        public Index()
        {
            InitializeComponent();

            queryThreadDispatch = new QueryThreadDispatch(uTToolLoader, this.trvQueryList);

            documentGenerater.BasicAction = context =>
            {
                if (context is FullGenerateContext)
                {
                    if ((context as FullGenerateContext).IsReadForBatch == false)
                    {
                        System.IO.File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Text", context.DescripterNode.Name + "_Test.cs"), context.Text.ToString());
                        MessageBox.Show("generate done");
                    }
                    else
                    {
                        this.pnlCheckList.Visible = this.pnlCheckList.Visible == false ? true : true;
                        this.pnlCheckList.Location = Cursor.Position;
                        this.checkList.Items.Clear();

                        this.BatchList = (context as FullGenerateContext).DescripterNode.Children.
                        Where(c => c.NodeType == NodeType.Member && (c as MemberDescripter).IsInterface == false && (c as MemberDescripter).BaseType.IsAbstract == false).ToList();

                        this.BatchList.ForEach(item =>
                        {
                            this.checkList.Items.Add(item.Name);
                        });
                    }
                }
                else
                {
                    this.textBox1.Text = context.Text.ToString();
                    var nodes = context.GetSetupNodes();

                    this.trvObjectList.Nodes.Clear();

                    var objectNode = new TreeNode("Object");
                    var interfaceNode = new TreeNode("Interface");

                    nodes.Where(n => ((MemberDescripter)n).IsInterface == false).ToList().ForEach(node =>
                    {
                        var oNode = new DescripterTreeNode(node.Name) { DescripterNode = node };
                        if (node.Children != null)
                        {
                            node.Children.ForEach(child =>
                            {
                                var cNode = new DescripterTreeNode(child.Name) { DescripterNode = child };
                                oNode.Nodes.Add(cNode);
                            });
                        }
                        objectNode.Nodes.Add(oNode);
                    });
                    nodes.Where(n => ((MemberDescripter)n).IsInterface == true).ToList().ForEach(node =>
                    {
                        var iNode = new DescripterTreeNode(node.Name) { DescripterNode = node };
                        if (node.Children != null)
                        {
                            node.Children.ForEach(child =>
                            {
                                var cNode = new DescripterTreeNode(child.Name) { DescripterNode = child };
                                iNode.Nodes.Add(cNode);
                            });
                        }
                        interfaceNode.Nodes.Add(iNode);
                    });

                    this.trvObjectList.Nodes.Add(objectNode);
                    this.trvObjectList.Nodes.Add(interfaceNode);
                    objectNode.Expand();
                    interfaceNode.Expand();
                }
            };
            documentGenerater.TreeNodeChangeAction = (obj, e, c) =>
            {
                //if (this.CurrentEventNode != null)
                //{
                //    this.CurrentEventNode.ForeColor = c;
                //}
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ofLoadFile_FileOk(object sender, CancelEventArgs e)
        {
            LoadAssemblyException exception = null;

            Task.Run(() =>
            {
                //uTToolLoader.Descriptors.Clear();
                try
                {
                    uTToolLoader.Load(this.ofLoadFile.FileName);
                }
                catch (LoadAssemblyException ex)
                {
                    exception = ex;
                }
                finally
                {
                    this.LoadTreeData();
                }
            }).ContinueWith(t =>
            {
                if (exception != null)
                {
                    if (exception.ExceptionType != ExceptionType.LoadFailure)
                    {
                        MessageBox.Show(exception.Message);
                    }
                    else
                    {
                        Child child = new Child(new List<LoadAssemblyException>() { exception });
                        child.StartPosition = FormStartPosition.CenterScreen;
                        child.ShowDialog();
                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        /// <summary>
        /// 
        /// </summary>
        private void LoadTreeData()
        {
            if (uTToolLoader.Descriptors.Count == 0)
            {
                return;
            }
            MethodInvoker invoker = new MethodInvoker(() =>
            {
                this.trvDescripterTree.Nodes.Clear();
                uTToolLoader.Descriptors.ForEach(desc =>
                {
                    var node = new DescripterTreeNode(desc.Name) { DescripterNode = desc };
                    this.InnerRecursion(node, desc);
                    this.trvDescripterTree.Nodes.Add(node);
                });
            });
            this.trvDescripterTree.BeginInvoke(invoker);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        private void InnerRecursion(DescripterTreeNode node, DescripterNode desc)
        {
            if (node != null)
            {
                if (desc.Children != null)
                {
                    desc.Children.ForEach(child =>
                    {
                        var childNode = new DescripterTreeNode(child.Name) { DescripterNode = child };
                        if (child.NodeType == NodeType.Director)
                        {
                            childNode.ImageIndex = 0;
                        }
                        else if (child.NodeType == NodeType.Member)
                        {
                            childNode.ImageIndex = 2;
                        }
                        else if (child.NodeType == NodeType.Method)
                        {
                            childNode.ImageIndex = 4;
                        }
                        else if (child.NodeType == NodeType.Parameter)
                        {
                            childNode.ImageIndex = 3;
                        }
                        else
                        {
                            childNode.ImageIndex = 1;
                        }
                        childNode.SelectedImageIndex = childNode.ImageIndex;
                        this.InnerRecursion(childNode, child);
                        if (this.documentGenerater.GetAllNode().Contains(child))
                        {
                            childNode.ForeColor = Color.Blue;
                        }
                        node.Nodes.Add(childNode);
                    });
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ofLoadFile.ShowDialog();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void directoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = this.fbLoad.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.toolStripProgressBar1.Visible = true;
                this.toolStripProgressBar1.Value = 0;
                DirectoryLoadException exception = null;

                uTToolLoader.OnLoadFinished = (num, desc, count) =>
                {
                    var perNumber = (count - (count % 100)) / 100;
                    if (num % perNumber == 0)
                    {
                        MethodInvoker invoker2 = new MethodInvoker(() =>
                        {
                            if (this.toolStripProgressBar1.Value < 100)
                            {
                                this.toolStripProgressBar1.Value += 1;
                            }
                            else
                            {
                                this.toolStripProgressBar1.Value = 100;
                            }
                        });
                        this.statusStrip1.BeginInvoke(invoker2);
                    }
                };

                Task.Run(() =>
                {
                    documentGenerater.Clear();
                    try
                    {
                        uTToolLoader.LoadDirectory(this.fbLoad.SelectedPath);
                    }
                    catch (DirectoryLoadException ex)
                    {
                        exception = ex;
                    }
                    finally
                    {
                        this.LoadTreeData();
                    }
                }).ContinueWith(t =>
                {
                    this.toolStripProgressBar1.Value = 100;
                    this.toolStripProgressBar1.Visible = false;
                    if (exception != null)
                    {
                        Child child = new Child(exception.loadAssemblyExceptions);
                        child.StartPosition = FormStartPosition.CenterScreen;

                        child.ShowDialog();
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvDescripterTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node != null)
                {
                    this.CurrentEventNode = e.Node;
                    this.contextMenuStrip1.Items.Clear();
                    this.documentGenerater.SetUp((e.Node as DescripterTreeNode).DescripterNode);
                    var documentObjects = this.documentGenerater.GetDocumentObjects();

                    if (documentObjects != null)
                    {
                        foreach (var documentObject in documentObjects)
                        {
                            this.contextMenuStrip1.Items.Add(documentObject.EventName, null, documentObject.EventHandler);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvObjectList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node != null && e.Node is DescripterTreeNode)
                {
                    this.CurrentEventNode = e.Node;
                    this.contextMenuStrip1.Items.Clear();
                    this.documentGenerater.SetUp((e.Node as DescripterTreeNode).DescripterNode);
                    var documentObjects = this.documentGenerater.GetDocumentObjects();

                    if (documentObjects != null)
                    {
                        foreach (var documentObject in documentObjects)
                        {
                            this.contextMenuStrip1.Items.Add(documentObject.EventName, null, documentObject.EventHandler);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var t = ((TextBox)sender).Text;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.trvObjectList.Visible = !this.trvObjectList.Visible;
            if (this.trvQueryList.Visible)
            {
                this.pnlQueryBox.Visible = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.pnlQueryBox.Visible = !this.pnlQueryBox.Visible;
            if (this.pnlQueryBox.Visible)
            {
                this.trvObjectList.Visible = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvQueryList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node != null)
                {
                    this.CurrentEventNode = e.Node;
                    this.contextMenuStrip1.Items.Clear();
                    this.documentGenerater.SetUp((e.Node as DescripterTreeNode).DescripterNode);
                    var documentObjects = this.documentGenerater.GetDocumentObjects();

                    if (documentObjects != null)
                    {
                        foreach (var documentObject in documentObjects)
                        {
                            this.contextMenuStrip1.Items.Add(documentObject.EventName, null, documentObject.EventHandler);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQuery_TextChanged(object sender, EventArgs e)
        {
            queryThreadDispatch.Query(this.txtQuery.Text);
        }
        /// <summary>
        /// 
        /// </summary>
        public class QueryThreadDispatch
        {
            public QueryThreadDispatch(UTToolLoader _loader, TreeView _trvQueryLis)
            {
                this.loader = _loader;
                this.trvQueryList = _trvQueryLis;
            }
            private TreeView trvQueryList;
            private UTToolLoader loader;
            private bool _isRun;
            private int _waitNum;
            private string waitString;
            /// <summary>
            /// 
            /// </summary>
            private Task RunTask { get; set; }

            private void trvLoad(List<DescripterNode> result)
            {
                if (result != null)
                {
                    this.trvQueryList.Nodes.Clear();
                    result.ForEach(r =>
                    {
                        var node = new DescripterTreeNode(r.Name) { DescripterNode = r };
                        if (r.Children != null)
                        {
                            r.Children.ForEach(child =>
                            {
                                var cNode = new DescripterTreeNode(child.Name) { DescripterNode = child };
                                node.Nodes.Add(cNode);
                            });
                        }
                        this.trvQueryList.Nodes.Add(node);
                    });
                }
            }
            /// <summary>
            /// 
            /// </summary>
            public async void Query(string queryText)
            {
                if (this.loader.Descriptors.Count == 0)
                {
                    return;
                }
                if (queryText.Length == 0)
                {
                    this.trvQueryList.Nodes.Clear();
                    return;
                }
                if (_isRun == false)
                {
                    var result = await Task.Run(() =>
                    {
                        SpinWait.SpinUntil(() => _isRun == false);
                        _isRun = true;
                        return this.loader.Query(d => d.Name.Contains(queryText));
                    });
                    _isRun = false;
                    this.trvLoad(result);
                }
                else
                {
                    if (Interlocked.CompareExchange(ref _waitNum, 1, 0) == 0)
                    {
                        waitString = queryText;
                        var result = await Task.Run(() =>
                        {
                            SpinWait.SpinUntil(() => _isRun == false);
                            Interlocked.Exchange(ref _waitNum, 0);
                            _isRun = true;
                            return this.loader.Query(d => d.Name.Contains(waitString));
                        });
                        _isRun = false;
                        this.trvLoad(result);
                    }
                    else
                    {
                        waitString = queryText;
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.pnlCheckList.Visible = false;
            this.checkList.Items.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkList.Items.Count; i++)
            {
                this.checkList.SetItemChecked(i, this.btnAllSelect.Text == "全选");
            }
            this.btnAllSelect.Text = this.btnAllSelect.Text == "全选" ? "取消" : "全选";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (this.BatchList == null)
            {
                MessageBox.Show("没有任何生成项！");
                return;
            }
            var checkItems = this.BatchList.Where(d => this.checkList.CheckedItems.Contains(d.Name)).ToList();
            var contextList = FullPublicGenerate.BatchGenerate(checkItems);

            contextList.ForEach(context =>
            {
                System.IO.File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Text", context.DescripterNode.Name + "_Test.cs"), context.Text.ToString());
            });
            MessageBox.Show("generate done");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class DescripterTreeNode : TreeNode
    {
        public DescripterTreeNode(string text) : base(text)
        {
        }
        public DescripterNode DescripterNode {
            get; set;
        }
    }
}
