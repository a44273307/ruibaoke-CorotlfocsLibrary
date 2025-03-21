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

            TcpClientExample.testxx();
            return a + b;
        }
    }

    class TcpClientExample
    {
        private static string serverIp = "127.0.0.1"; // 服务器 IP 地址
    private static int port = 5000; // 服务器端口

    static int SendAndReceiveData(byte[] sendData, out byte[] trspData)
    {
        trspData = new byte[sendData.Length];
        
        try
        {
            using (TcpClient client = new TcpClient(serverIp, port))
            using (NetworkStream stream = client.GetStream())
            {
                Console.WriteLine("已连接到服务器");
                
                // 发送数据到服务器
                stream.Write(sendData, 0, sendData.Length);
                Console.WriteLine("发送数据: " + BitConverter.ToString(sendData));
                
                // 读取服务器响应，等待 1000ms
                stream.ReadTimeout = 1000000;
                try
                {
                    int bytesRead = stream.Read(trspData, 0, trspData.Length);
                    if (bytesRead == sendData.Length &&     trspData[8] == (byte)trspData.Take(8).Sum(b => (int)b))
                    {
                        Console.WriteLine("收到校验通过数据: " + BitConverter.ToString(trspData));
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine("收到校验不通过数据: " + BitConverter.ToString(trspData));
                        return -1;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("1000ms 超时未收到数据");
                    return -2;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("错误: " + e.Message);
            return -1;
        }
        }
        // 控制旋转 上圆盘第x号孔
        public static int  controlTopDiskRotation(int diskNumber)
        {
             byte[] sendData = { 0x01, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x04 };
            
            sendData[2]=(byte)diskNumber;
            sendData[8] = (byte)sendData.Take(8).Sum(b => (int)b);
            byte[] trspData=sendData;
            return SendAndReceiveData(sendData, out trspData);
        }
        // 控制旋转 下圆盘第x号孔
        public static int  controlBottomDiskRotation(int diskNumber)
        {
             byte[] sendData = { 0x01, 0x01, 0x01, 0x02, 0x00, 0x00, 0x00, 0x00, 0x04 };
            
            sendData[2]=(byte)diskNumber;
            sendData[8] = (byte)sendData.Take(8).Sum(b => (int)b);
            byte[] trspData=sendData;
            return SendAndReceiveData(sendData, out trspData);
        }
         // 光圈位置控制 取值范围0-------1500000
        public static int  controlapertureRotation(int diskNumber)
        {
             byte[] sendData = { 0x01, 0x01, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x04 };
            
           // 将 diskNumber 作为 32 位整数存入 sendData，并按照大端序存储
            sendData[4] = (byte)((diskNumber >> 24) & 0xFF);
            sendData[5] = (byte)((diskNumber >> 16) & 0xFF);
            sendData[6] = (byte)((diskNumber >> 8) & 0xFF);
            sendData[7] = (byte)(diskNumber & 0xFF);

            // 重新计算校验和（第 9 个字节存储前 8 个字节的和）
            sendData[8] = (byte)sendData.Take(8).Sum(b => (int)b);
            
            byte[] trspData=sendData;
            return SendAndReceiveData(sendData, out trspData);
        }
        // 聚焦位置控制 取值范围0-------1500000
        public static int  controlfocusRotation(int diskNumber)
        {
             byte[] sendData = { 0x01, 0x01, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x04 };
            
           // 将 diskNumber 作为 32 位整数存入 sendData，并按照大端序存储
            sendData[4] = (byte)((diskNumber >> 24) & 0xFF);
            sendData[5] = (byte)((diskNumber >> 16) & 0xFF);
            sendData[6] = (byte)((diskNumber >> 8) & 0xFF);
            sendData[7] = (byte)(diskNumber & 0xFF);

            // 重新计算校验和（第 9 个字节存储前 8 个字节的和）
            sendData[8] = (byte)sendData.Take(8).Sum(b => (int)b);
            
            byte[] trspData=sendData;
            return SendAndReceiveData(sendData, out trspData);
        }
        
        // 信息读取
        // type
        //  1 上圆盘信息读取
        //  2 下圆盘信息
        //  3 聚焦信息读取
        //  4 光圈信息读取
        //  order:   
        // 02  实时位置
        // 03  目标位置
        // 04 复位完成标志 
        // 05 运行电流


        public static int  readinform(int type,int order,out int ans)
        {
            byte[] sendData = { 0x01, 0x02, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x04 };
            sendData[1]=(byte)order;
            sendData[3]=(byte)type;
            // 重新计算校验和（第 9 个字节存储前 8 个字节的和）
            sendData[8] = (byte)sendData.Take(8).Sum(b => (int)b);
            byte[] trspData=sendData;
            int result=SendAndReceiveData(sendData, out trspData);
            if(result!=0)
            {
                ans = 0;
                return result;
            }
            ans = ((trspData[4] & 0xFF) << 24) 
                | ((trspData[5] & 0xFF) << 16) 
                | ((trspData[6] & 0xFF) << 8) 
                | (trspData[7] & 0xFF);
            Console.WriteLine("读取到数据: " + ans.ToString());
            return 0;
            
        }


        public static int testxx()
        {
            //controlfocusRotation(256  * 256 * 3+ 256  * 4+5);
            int ans;
            readinform(1,1,out ans);
            return controlTopDiskRotation(1);
        }
    }
}
