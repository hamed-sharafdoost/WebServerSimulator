using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerSimulatorThread
{
    public class Response
    {
        public string StatusLine { get; set; }
        public string date { get; set; }
        public string Content_type {get; set;}
        public string Content_length { get; set; }
        public byte[] Msgbody { get; set; }

        public byte[] GetRes()
        {
            byte[] header = Encoding.UTF8.GetBytes(StatusLine + "\n" + date + "\n" + Content_type + "\n" + Content_length + "\r\n\r\n");
            var hh = header.Concat(Msgbody).ToArray();
            return hh;
        }
    }
}
