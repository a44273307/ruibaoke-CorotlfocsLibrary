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
         private List<Button> buttonList = new List<Button>();
        private System.Collections.Generic.List<TextBox> inputList = new System.Collections.Generic.List<TextBox>();

        public Form1()
        {
            InitializeComponent();
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
        }
        private void allkeydeal(int num)
        {
            timer1.Enabled=true;
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
           var ans= TcpReadinfo.controlTopDiskRotation(num1);
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
           var ans= TcpReadinfo.controlBottomDiskRotation(num1);
           if (ans!=0)
           {
             MessageBox.Show("操作失败，结果"+ans.ToString());
           }
           return; 
        }

        private void button3_Click(object sender, EventArgs e)
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
             var ans=TcpReadinfo.controlfocusRotation(num1);
             if (ans!=0)
           {
             MessageBox.Show("操作失败，结果"+ans.ToString());
           }
        }

        private void button4_Click(object sender, EventArgs e)
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
              var ans=TcpReadinfo.controlapertureRotation(num1);
             if (ans!=0)
           {
             MessageBox.Show("操作失败，结果"+ans.ToString());
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
            var ans= TcpReadinfo.controlTopDiskRotation(1);
           if (ans!=0)
           {
             MessageBox.Show("操作失败，结果"+ans.ToString());
           }
           ans= TcpReadinfo.controlBottomDiskRotation(3);
           if (ans!=0)
           {
             MessageBox.Show("操作失败，结果"+ans.ToString());
           }
        }
        int num=0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            int  num1;
            int  ans;
            ans=TcpReadinfo.readinform(1,2,out num1);
            inputList[6].Text=num1.ToString();

            ans=TcpReadinfo.readinform(2,2,out num1);
            inputList[8].Text=num1.ToString();

            ans=TcpReadinfo.readinform(3,2,out num1);
            inputList[10].Text=num1.ToString();

            ans=TcpReadinfo.readinform(4,2,out num1);
            inputList[11].Text=num1.ToString();
            
            // num++;
            // inputList[6].Text=num.ToString();
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }
    }
}