using System.Reflection;
using System.Reflection.Metadata;
using UITool;
using UTTool.Core;

namespace UITool.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new UTTool.Core.UTToolLoader().Load(@"C:\P4Source\GSS\Trunk\MMSSVC\Web\Services\ACHTransfer\V1\Business\ACHTransfer.Controllers\bin\Debug\net6.0\ACHTransfer.Controllers.dll");
            //new UTTool.Core.UTToolLoader().LoadDirectory(@"C:\P4Source\GSS\Trunk\MMSSVC\Web\Services\ACHTransfer\V1\Business\ACHTransfer.Controllers\bin\Debug\net6.0");
        }


    }
}