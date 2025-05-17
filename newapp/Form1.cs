using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CorotlfocsLibrary;
using System.Diagnostics;
namespace newapp
{
    public partial class Form1 : Form
    {
          // 定义两个Timer及独立的状态计数器
        private System.Windows.Forms.Timer timerButton1;
        private System.Windows.Forms.Timer timerButton2;
         private List<Button> buttonList = new List<Button>();
        private System.Collections.Generic.List<TextBox> inputList = new System.Collections.Generic.List<TextBox>();

        public Form1()
        {
            InitializeComponent();
            TcpReadinfo.readini();
            buttonList.Add(button1);
            buttonList.Add(button2);
            buttonList.Add(button3);
            buttonList.Add(button4);
            buttonList.Add(button5);
            buttonList.Add(button6);


            inputList.Add(textBox1);
            inputList.Add(textBox2);
            inputList.Add(textBox3);
            inputList.Add(textBox4);
            inputList.Add(textBox5);
            inputList.Add(textBox6);
            inputList.Add(textBox7);
            inputList.Add(textBox8);
            inputList.Add(textBox9);
            inputList.Add(textBox10);
            inputList.Add(textBox11);
            inputList.Add(textBox12);
            for (int i = 0; i < 4; i++)
            {
                buttonList[i].Text="确认执行";
            }
            buttonList[4].Text="ip设置";
            buttonList[5].Text="归零";

                    // 初始化按钮1的Timer（间隔100ms）
            timerButton1 = new System.Windows.Forms.Timer();
            timerButton1.Interval = 600;
            timerButton1.Tick += TimerButton1_Tick;

            // 初始化按钮2的Timer（间隔200ms）
            timerButton2 = new System.Windows.Forms.Timer();
            timerButton2.Interval = 300;
            timerButton2.Tick += TimerButton2_Tick;

            inputList[3].Text="1";
            inputList[5].Text="1";

            
            textBox13.Text="0~"+TcpReadinfo.maxfocsSet.ToString();
            textBox14.Text="0~"+TcpReadinfo.maxguangSet.ToString();


            textBox13.ReadOnly = true; // 让TextBox变为只读状态
            textBox14.ReadOnly = true; // 让TextBox变为只读状态

            
        }
        private  int focsset;
        private  int focsimesset;
        private  int focsimesrun;
        private  bool focstozero=false;
        private void TimerButton1_Tick(object sender, EventArgs e)
        {
            int  num1;
            int  ans;
            ans=TcpReadinfo.readinform(4,2,out num1);
            inputList[10].Text=num1.ToString();
            if(focstozero==false)
            {
                // 正到位
                if (num1==focsset)
                {
                    focsimesrun++;
                    if(focsimesrun==focsimesset)
                    {
                        timerButton1.Stop();
                        button3.Enabled=true;
                        return;
                    }
                    // 反向走
                    focstozero=true;
                }
                else
                {
                    TcpReadinfo.controlfocusRotation(focsset);
                }
            }
            if (focstozero==true)
            {
                // 到0位值
                if (num1==0)
                {
                    // 反向走
                    focstozero=false;
                }
                else
                {
                    TcpReadinfo.controlfocusRotation(0);
                }
            }
        }
private  int rotatset;
        private  int rotatimesset;
        private  int rotatimesrun;
        private  bool rotattozero=false;
        private void TimerButton2_Tick(object sender, EventArgs e)
        {
            int  num1;
            int  ans;
            ans=TcpReadinfo.readinform(3,2,out num1);
            inputList[11].Text=num1.ToString();
            if(rotattozero==false)
            {
                // 正到位
                if (num1==rotatset)
                {
                    rotatimesrun++;
                    if(rotatimesrun==rotatimesset)
                    {
                        timerButton2.Stop();
                        button4.Enabled=true;
                        return;
                    }
                    // 反向走
                    rotattozero=true;
                }
                else
                {
                    TcpReadinfo.controlapertureRotation(rotatset);
                }
            }
            if (rotattozero==true)
            {
                // 到0位值
                if (num1==0)
                {
                    // 反向走
                    rotattozero=false;
                }
                else
                {
                    TcpReadinfo.controlapertureRotation(0);
                }
            }
        }
        private void allkeydeal(int num)
        {
            // timer1.Enabled=true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            int i=0;
            allkeydeal(i);
           if (! int.TryParse(inputList[i].Text, out int num1))
            {
            MessageBox.Show("请输入有效的数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return ;
             }
           var ans= TcpReadinfo.controlBottomDiskRotation(num1);
           if (ans!=0)
           {
             MessageBox.Show("操作失败，结果"+ans.ToString());
           }
           return;  
      
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i=1;
            allkeydeal(i);
           if (! int.TryParse(inputList[i].Text, out int num1))
            {
            MessageBox.Show("请输入有效的数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return ;
             }
           var ans= TcpReadinfo.controlTopDiskRotation(num1);
           if (ans!=0)
           {
             MessageBox.Show("操作失败，结果"+ans.ToString());
           }
           return; 
        }

        private async void  button3_Click(object sender, EventArgs e)
        {
            int i=2;
            allkeydeal(i);
           if (! int.TryParse(inputList[i].Text, out int num1))
            {
            MessageBox.Show("请输入有效的数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return ;
             }
             if (! int.TryParse(inputList[i+1].Text, out int num2))
            {
            MessageBox.Show("请输入有效的数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return ;
             }
             if(num1<0 || num1 > TcpReadinfo.maxfocsSet)
             {
                MessageBox.Show("请输入范围内的数字！");
                return ;
             }
             bool beixiselect=false;
            if(ch.Checked==true)
            {
                beixiselect=true;
            }
            if(beixiselect==true)
            {
                if (! int.TryParse(textBox16.Text, out int beixi))
                {
                MessageBox.Show("背隙请输入有效的数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
                }
                if(beixi<0 || beixi > 15000)
                {
                    MessageBox.Show("背隙请输入范围内的数字！");
                    return ;
                }
            }


             var ans=TcpReadinfo.controlfocusRotation(num1);
             if (ans!=0)
           {
             MessageBox.Show("操作失败，结果"+ans.ToString());
             return ;
           }
           button3.Enabled=false;
           int runtimes=0;
           for(int time=0;time<10000;time++)
           {
               
                TcpReadinfo.controlfocusRotation(num1);
                while(1>0)
                {
                     // 异步等待 1 秒（不阻塞 UI 线程）
                    await Task.Delay(1000);
                    if(gfocs== num1)
                    {
                        break;
                    }
                }
                runtimes++;
                if(runtimes==num2)
                {
                   
                    button3.Enabled=true;
                     break;
                }
                
                    

                TcpReadinfo.controlfocusRotation(0);
                while(1>0)
                {
                     // 异步等待 1 秒（不阻塞 UI 线程）
                    await Task.Delay(1000);
                    if(gfocs== 0)
                    {
                        break;
                    }
                    
                }
           }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
             int i=4;
             allkeydeal(i);
           if (! int.TryParse(inputList[i].Text, out int num1))
            {
            MessageBox.Show("请输入有效的数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return ;
             }
             if (! int.TryParse(inputList[i+1].Text, out int num2))
            {
            MessageBox.Show("请输入有效的数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return ;
             }
            bool beixiselect=false;
            if(checkBox1.Checked==true)
            {
                beixiselect=true;
            }
            if(beixiselect==true)
            {
                if (! int.TryParse(textBox15.Text, out int beixi))
                {
                MessageBox.Show("背隙请输入有效的数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
                }
                if(beixi<0 || beixi > 15000)
                {
                    MessageBox.Show("背隙请输入范围内的数字！");
                    return ;
                }
            }
             

            if(num1<0 || num1 > TcpReadinfo.maxguangSet)
             {
                MessageBox.Show("请输入范围内的数字！");
                return ;
             }
              var ans=TcpReadinfo.controlapertureRotation(num1);
             if (ans!=0)
           {
             MessageBox.Show("操作失败，结果"+ans.ToString());
             return ;
           }
           button4.Enabled=false;
           int runtimes=0;
           for(int time=0;time<10000;time++)
           {
               
                TcpReadinfo.controlapertureRotation(num1);
                while(1>0)
                {
                     // 异步等待 1 秒（不阻塞 UI 线程）
                    await Task.Delay(1000);
                    if(groat== num1)
                    {
                        break;
                    }
                }
                runtimes++;
                if(runtimes==num2)
                {
                   
                    button4.Enabled=true;
                     break;
                }
                
                    

                TcpReadinfo.controlapertureRotation(0);
                while(1>0)
                {
                     // 异步等待 1 秒（不阻塞 UI 线程）
                    await Task.Delay(1000);
                    if(groat== 0)
                    {
                        break;
                    }
                    
                }
           }
        
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "http://"+TcpReadinfo.serverIp+":80";
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开网址时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
              return; 
        }
        private void button6_Click(object sender, EventArgs e)
        {
            int i = 10;
            allkeydeal(i);
            var ans= TcpReadinfo.controlBottomDiskRotation(1);
           if (ans!=0)
           {
             MessageBox.Show("操作失败，结果"+ans.ToString());
           }
           ans= TcpReadinfo.controlTopDiskRotation(3);
           if (ans!=0)
           {
             MessageBox.Show("操作失败，结果"+ans.ToString());
           }
           ans= TcpReadinfo.controlapertureRotation(0);
           ans= TcpReadinfo.controlfocusRotation(0);

            timer2.Enabled=true;
        }
        
        private  int numtoindex(int input)
        {
            input=-input;
            return input/30720;
        }
        private  void showmessage()
        {
            int  num1;
            int  ans;
            ans=TcpReadinfo.readinform(2,2,out num1);
            inputList[7].Text=num1.ToString();
            inputList[6].Text=numtoindex(num1).ToString();
            // inputList[6].Text=

            ans=TcpReadinfo.readinform(1,2,out num1);
            inputList[9].Text=num1.ToString();
            inputList[8].Text=numtoindex(num1).ToString();
        }
        private   int gfocs=0;
        private   int groat=0;
        private  void showmessage2()
        {
            int  num1;
            int  ans;
            ans=TcpReadinfo.readinform(4,2,out num1);
            inputList[10].Text=num1.ToString();
            gfocs=num1;


            ans=TcpReadinfo.readinform(3,2,out num1);
            inputList[11].Text=num1.ToString();
            groat=num1;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            showmessage();
            showmessage2();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if(gfocs ==0  && groat == 0)
            {
                timer2.Enabled=false;
                this.button6.BackColor = System.Drawing.Color.Green;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }
    }
}