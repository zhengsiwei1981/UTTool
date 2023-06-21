using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;
using UTTool.Core.Extension;

namespace UTTool.Core
{
    public class UTToolLoader
    {
        public Action<int, AssemblyDescriptor, int> OnLoadFinished;
        public List<AssemblyDescriptor> Descriptors {
            get; set;
        }
        public UTToolLoader()
        {
            this.Descriptors = new List<AssemblyDescriptor>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public List<DescripterNode> Query(Func<DescripterNode, bool> func)
        {
            var queryResultList = new List<DescripterNode>();
            this.Descriptors.ForEach(desc =>
            {
                var result = desc.FindList(func);
                if (result != null)
                {
                    queryResultList.AddRange(result);
                }
            });
            return queryResultList.Where(d => d.NodeType == NodeType.Member).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Load(string path)
        {
            var assName = "";
            try
            {
                var Descriptor = AssemblyLoader.Load(path);
                assName = Descriptor.Name;
                if (this.Descriptors.Exists(d => d.Name == Descriptor.Name))
                {
                    return;
                }
                new DescriptorDecorater(new DecoraterContext() { AssemblyDescriptor = Descriptor, Path = path }).DecoraterProcess();

                this.Descriptors.Add(Descriptor);
            }
            catch (LoadAssemblyException ex)
            {
                throw ex;
            }
            catch (FileLoadException ex)
            {
                var returnException = new LoadAssemblyException(ex.Message) { ExceptionType = ExceptionType.LoadFailure, AssemblyName = assName };
                returnException.RTExceptions.Add(new ReflectionTypeLoadSubException() { AssemblyName = assName, Message = ex.Message });
                throw returnException;
            }
            catch (Exception ex)
            {
                if (ex is not FileLoadException && ex is not LoadAssemblyException)
                {
                    var returnException = new LoadAssemblyException(ex.Message) { ExceptionType = ExceptionType.LoadFailure, AssemblyName = assName };
                    returnException.RTExceptions.Add(new ReflectionTypeLoadSubException() { AssemblyName = assName, Message = ex.Message });
                    throw returnException;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void LoadDirectory(string path)
        {
            DirectoryLoadException directoryLoadException = null;
            var directoryInfo = new DirectoryInfo(path);
            var dllFiles = directoryInfo.GetFiles("*.dll");
            var loadNumber = 0;
            foreach (var file in dllFiles)
            {
                try
                {
                    this.Load(file.FullName);
                }
                catch (LoadAssemblyException ex)
                {
                    if (directoryLoadException == null)
                    {
                        directoryLoadException = new DirectoryLoadException();
                    }
                    directoryLoadException.loadAssemblyExceptions.Add(ex as LoadAssemblyException);
                }
                finally
                {
                    loadNumber++;
                    if (OnLoadFinished != null)
                    {
                        if (this.Descriptors.Count > 0)
                        {
                            OnLoadFinished(loadNumber, this.Descriptors.Last(), dllFiles.Count());
                        }
                    }
                }
            }

            if (directoryLoadException != null)
            {
                throw directoryLoadException;
            }
        }

    }
}
