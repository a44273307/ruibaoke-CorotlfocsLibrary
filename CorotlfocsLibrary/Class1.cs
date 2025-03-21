using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System;
using System.Net.Sockets;
using System.Text;



namespace CorotlfocsLibrary
{
    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }

    class TcpClientExample
    {
        static void Main(string[] args)
        {
            string serverIp = "127.0.0.1"; // 服务器 IP 地址
            int port = 5000; // 服务器端口

            try
            {
                using (TcpClient client = new TcpClient(serverIp, port))
                using (NetworkStream stream = client.GetStream())
                {
                    Console.WriteLine("已连接到服务器");

                    // 发送数据到服务器
                    string message = "Hello, Server!";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("发送: " + message);

                    // 接收服务器响应
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("收到: " + response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("错误: " + e.Message);
            }
        }
    }
}
