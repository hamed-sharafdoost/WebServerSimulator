using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebServerSimulatorThread
{
    interface IWebserverManager
    {
        public void SendToBrowser(byte[] res,string type,Socket socket);
        public void HandleRequest(object socket);
        public (byte[],string) FindFile(string path);
    }
}
