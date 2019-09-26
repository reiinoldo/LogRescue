using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectToSharedFolder.FileUploadAsync("testeq.log").Wait();
          //  System.Diagnostics.Process.Start("net.exe", @"use K: ""\\my.server.com\Websites\Mail"" / user:MyDomain\admin MySuperSecretPassword").WaitForExit();
          //    System.Diagnostics.Process.Start(“net.exe”, @”use K: / delete”).WaitForExit();
        }
    }
    public class ConnectToSharedFolder : IDisposable
    {

        public string networkPath = @"\\PythonWindows\Log";
        NetworkCredential credentials = new NetworkCredential(@"zamonelli", "Vivaforever666@!");
        public string myNetworkPath = string.Empty;
        readonly string _networkName;

        public ConnectToSharedFolder(string networkName, NetworkCredential credentials)
        {
            _networkName = networkName;

            var netResource = new NetResource
            {
                Scope = ResourceScope.GlobalNetwork,
                ResourceType = ResourceType.Disk,
                DisplayType = ResourceDisplaytype.Share,
                RemoteName = networkName
            };

            var userName = string.IsNullOrEmpty(credentials.Domain)
                ? credentials.UserName
                : string.Format(@"{0}\{1}", credentials.Domain, credentials.UserName);

            var result = WNetAddConnection2(
                netResource,
                credentials.Password,
                userName,
                0);

            if (result != 0)
            {
                throw new Win32Exception(result, "Error connecting to remote share");
            }
        }

        ~ConnectToSharedFolder()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            WNetCancelConnection2(_networkName, 0, true);
        }

        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(NetResource netResource,
            string password, string username, int flags);

        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string name, int flags,
            bool force);

        [StructLayout(LayoutKind.Sequential)]
        public class NetResource
        {
            public ResourceScope Scope;
            public ResourceType ResourceType;
            public ResourceDisplaytype DisplayType;
            public int Usage;
            public string LocalName;
            public string RemoteName;
            public string Comment;
            public string Provider;
        }

        public enum ResourceScope : int
        {
            Connected = 1,
            GlobalNetwork,
            Remembered,
            Recent,
            Context
        };

        public enum ResourceType : int
        {
            Any = 0,
            Disk = 1,
            Print = 2,
            Reserved = 8,
        }

        public enum ResourceDisplaytype : int
        {
            Generic = 0x0,
            Domain = 0x01,
            Server = 0x02,
            Share = 0x03,
            File = 0x04,
            Group = 0x05,
            Network = 0x06,
            Root = 0x07,
            Shareadmin = 0x08,
            Directory = 0x09,
            Tree = 0x0a,
            Ndscontainer = 0x0b
        }

        public static async Task FileUploadAsync(string UploadURL)     
        {
            string networkPathw = @"\\PythonWindows\Log\";
            try
            {
                using (new ConnectToSharedFolder( @"\\PythonWindows\Log", new NetworkCredential(@"Zamonelli", "Vivaforever666@!")))
                {
                    var fileList = Directory.GetDirectories(@"\\PythonWindows\\Log");

                    foreach (var item in fileList)
                    { 
                        if (item.Contains("{ClientDocument}")) 
                        {
                            networkPathw = item; 
                        } 
                    }

                    networkPathw = networkPathw + UploadURL;
                    string teste = "C:\\Log\\teste.txt";
                    File.Copy(teste, networkPathw);

                }
            }
            catch (Exception ex)
            {

            }

           // return null;
        }
    }
}
// https://stackoverflow.com/questions/43173970/map-network-drive-programmatically-in-c-sharp-on-windows-10
//