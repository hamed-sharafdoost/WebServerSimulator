using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebServerSimulator
{
    public class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(5050);
            string path = "C:\\Rootserver\\Images\\eye.jpg";
            string msg = "HTTP / 1.1 200 OK \n Content-Type : image/jpg \r\n\r\n";
            var file = File.OpenRead(path);
            listener.Start();
            Socket conn = null;
            int last = 0;
            byte[] data = new byte[1024];
            byte[] res = new byte[file.Length];
            byte[] header = new byte[Encoding.UTF8.GetBytes(msg).Length];
            byte[] result = new byte[res.Length + header.Length];
            while(true)
            {
                conn = listener.AcceptSocket();
                if (conn.Receive(data, SocketFlags.None) > 0)
                {
                    Console.WriteLine(Encoding.UTF8.GetString(data));
                    file.Read(res,0,res.Length);
                    
                    header = Encoding.UTF8.GetBytes(msg);
                    for (int i = 0; i < header.Length;i++)
                    {
                        result[i] += header[i];
                    }
                    last = header.Length;
                    for (int j = 0; j < res.Length;j++)
                    {
                        result[last] += res[j];
                        last++;
                    }
                    conn.Send(result);
                    conn.Close();
                }
            }
        }
    }
}