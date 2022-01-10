// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Hello, World!");

//var args = Environment.GetCommandLineArgs();
//var ip = args?.Length > 0 ? args[0] : "127.0.0.1";
var ip = args?.Length > 0 ? args[0] : "101.200.215.219";
var port = args?.Length > 1 ? int.Parse(args[1]) : 6996;

var ss = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
var ipa = IPAddress.Parse(ip);
ss.Connect(ipa, port);

var buffer = Encoding.UTF8.GetBytes(Environment.MachineName);

try
{
    ss.Send(buffer);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

buffer = new byte[2048];

using var ms = new MemoryStream();
using var sr = new StreamReader(ms);
while (true)
{
    var r = ss.Receive(buffer);
    if (r == 0)
    {
        break;
    }
    else if (r == 2048)
    {
        ms.Write(buffer, 0, r);
    }
    else
    {
        ms.Write(buffer, 0, r);
        break;
    }
}

ms.Seek(0, SeekOrigin.Begin);

while (true)
{
    var str = sr.ReadLine();
    if (string.IsNullOrEmpty(str))
    {
        break;
    }

    Console.WriteLine(str);
}