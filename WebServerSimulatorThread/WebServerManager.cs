using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebServerSimulatorThread
{
    public class WebServerManager : IWebserverManager
    {
        const string basepath = "C:\\Rootserver";
        List<string> vpaths = new List<string>() { "/Data/index.html", "/Data/about.html", "/Images/eye.jpg", "/Images/moon.jpg", "/Data/style.css","/Images/space.jpg" };
        public (byte[], string) FindFile(string content)
        {
            string relpath = vpaths.Find(x => content.Contains(x));

            string abpath = basepath + relpath.Replace("/", "\\");
            FileStream file = File.OpenRead(abpath);
            byte[] rc = new byte[(int)file.Length];
            file.Read(rc, 0, (int)file.Length);
            file.Close();
            return (rc, relpath);
        }

        public void HandleRequest(object socket)
        {
            byte[] buffer = new byte[20000];
            Socket conn = socket as Socket;
            while (conn.Connected)
            {
                if (conn.Receive(buffer, SocketFlags.None) > 0)
                {
                    string msg = Encoding.UTF8.GetString(buffer);
                    Console.WriteLine(msg);
                    (byte[] res, string type) = FindFile(msg);
                    SendToBrowser(res, type, conn);
                }
            }
        }

        public void SendToBrowser(byte[] res, string type, Socket socket)
        {
            Response rep = new Response();
            rep.StatusLine = "HTTP/1.1 200 OK";
            rep.date = $"Date: {DateTime.Today} GMT";
            rep.Content_length = $"Content-Length : {res.Length}";
            if (res != null && type.Contains("html"))
            {
                rep.Content_type = "Content-Type : text/html";
                rep.Msgbody = res;
                socket.Send(rep.GetRes());
                socket.Close();
            }
            else if (res != null && type.Contains("css"))
            {
                rep.Content_type = "Content-Type : text/css";
                rep.Msgbody = res;
                socket.Send(rep.GetRes());
                socket.Close();
            }
            else if (res != null && type.Contains("jpg"))
            {
                rep.Content_type = "Content - Type : image/jpg";
                rep.Msgbody = res;
                socket.Send(rep.GetRes());
                socket.Close();
            }

        }
    }
}
