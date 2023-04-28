using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

namespace WebServerSimulatorThread
{
    public class Program
    {
        static void Main(string[] args)
        {
            int requests = 0;
            TcpListener listener = new TcpListener(IPAddress.Parse("192.168.1.52"),5050);
            listener.Start();
            while (true)
            {
                requests++;
                WebServerManager manager = new WebServerManager();
                Socket conn = listener.AcceptSocket();
                Thread thread = new Thread(manager.HandleRequest);
                thread.Name = $"Request {requests}";
                Console.WriteLine(thread.Name);
                thread.Start(conn);
            }
        }
    }
}
