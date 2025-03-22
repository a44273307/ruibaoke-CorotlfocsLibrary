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
namespace testapp
{
    public partial class Form1 : Form
    {
         private TextBox textBox;
        public Form1()
        {
            InitializeComponent();

             // 指定 TextBox 的位置和大小
            int x = 50;
            int y = 650;
            int width = 800;
            int height = 150;

            // 创建 TextBox 控件
            textBox = new TextBox();
            textBox.Multiline = true;
            textBox.Location = new System.Drawing.Point(x, y);
            textBox.Size = new System.Drawing.Size(width, height);
            this.Controls.Add(textBox);

            
        }
       
    private TextBox input1;
    private TextBox input2;
    private TextBox input3;
  
    private TextBox output1;
    private TextBox output2;
    private TextBox output3;
    

    private void Input_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
    }
    private void showlog(string s)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        if (textBox != null)
            {
                textBox.AppendText(timestamp+s + Environment.NewLine);
            }
        // this.textBox1.AppendText("hhh");
         //this.textBox1.Text ="hhh";
     
    }
    private void CreateControlButtons()
    {
            // 按钮配置列表（按钮名称，显示文本）
            var buttonConfigs = new List<Tuple<string, string>>()
{
    new Tuple<string, string>("controlTopDiskRotation", "控制上圆盘旋转到指定孔位"),
    new Tuple<string, string>("controlBottomDiskRotation", "控制下圆盘旋转到指定孔位"),
    new Tuple<string, string>("controlfocusRotation", "控制聚焦位置"),
    new Tuple<string, string>("controlapertureRotation", "控制光圈位置"),
    new Tuple<string, string>("readInfo", "读取状态信息"),
    new Tuple<string, string>("setkey", "设置控制读取的为编码器值"),
    // new Tuple<string, string>("readBottomDiskInfo", "读取下圆盘状态信息（参数同上圆盘）"),
    // new Tuple<string, string>("readfocusInfo", "读取聚焦机构状态信息（参数同上圆盘）"),
    // new Tuple<string, string>("readapertureInfo", "读取光圈机构状态信息（参数同上圆盘）"),
    };

    // 创建按钮并设置属性
    int verticalPosition = 10;
    // 遍历除最后一个元素外的所有元素
    for (int i = 0; i < buttonConfigs.Count - 1; i++)
    {
        var config = buttonConfigs[i];
        var btn = new Button
        {
            Name = config.Item1,
            Text = config.Item2,
            Location = new Point(20, verticalPosition),
            Size = new Size(280, 40),
            Font = new Font("Microsoft YaHei", 10F)
        };
        btn.Click += Button_ClickHandler;
        
        Controls.Add(btn);
        verticalPosition += 50;
    }

    // 手动创建并设置最后一个按钮的位置
    var lastConfig = buttonConfigs[buttonConfigs.Count - 1];
    var lastBtn = new Button
    {
        Name = lastConfig.Item1,
        Text = lastConfig.Item2,
        Location = new Point(600, 500), // 手动设置位置
        Size = new Size(280, 40),
        Font = new Font("Microsoft YaHei", 10F)
    };
    lastBtn.Click += Button_ClickHandler;
    Controls.Add(lastBtn);
    }
   
        private void Form1_Load(object sender, EventArgs e)
        {
           TcpReadinfo.logger= showlog;
           InitializeComponent();
           CreateControlButtons();

           this.Text = "测试控制";
     

        Label label1 = new Label() { Text = "控制的输入:", Left = 500, Top = 10 };
        input1 = new TextBox() { Left = 600, Top = 10, Width = 100 };
        input1.KeyPress += Input_KeyPress;


        Label label4 = new Label() { Text = "输出:", Left = 500, Top = 40 };
        output1 = new TextBox() { Left = 600, Top = 40, Width = 100 };





      Label label2 = new Label() { Text = "readifno的输入1", Left = 500, Top = 220 };
        input2 = new TextBox() { Left = 600, Top = 220, Width = 100 };
        this.Controls.Add(input2);
        
        this.Controls.Add( new Label() { Text = "readifno的输入2", Left = 500, Top = 220+30 });
        input3 = new TextBox() { Left = 600, Top = 220+30, Width = 100 };
        this.Controls.Add(input3);
        
        
        this.Controls.Add( new Label() { Text = "输出1:", Left = 500, Top = 220+30+30 });
        output2 = new TextBox() { Left = 600, Top = 220+30+30, Width = 100 };
     

        this.Controls.Add( new Label() { Text = "输出2:", Left = 500, Top = 220+30+30+30 });
        output3 = new TextBox() { Left = 600, Top = 220+30+30+30, Width = 100 };
           this.Controls.Add(output3);
        // Label label3 = new Label() { Text = "输入3:", Left = 500, Top = 70 };
        // input3 = new TextBox() { Left = 600, Top = 40, Width = 100 };
        // input3.KeyPress += Input_KeyPress;

        



    

        this.Controls.Add(label1);
        this.Controls.Add(input1);

        this.Controls.Add(input2);

   

        this.Controls.Add(output1);
        this.Controls.Add(label4);
        this.Controls.Add(output2);

        this.Controls.Add(label2);
        //      this.Controls.Add(label3);
        }
        
    private void Button_ClickHandler(object sender, EventArgs e)
    {
        var button = sender as Button;
           
        if(button?.Name=="readInfo")
        {
             if (int.TryParse(input2.Text, out int num2) && int.TryParse(input3.Text, out int num3))
            {

            }
            else
            {
                MessageBox.Show("请输入有效的数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }
            int out3;
            int out2=TcpReadinfo.readinform(num2,num3,out out3);
            output2.Text= out2.ToString();
            output3.Text = out3.ToString();
                return; 
        }
        if(button?.Name=="setkey")
        {
            TcpReadinfo.IsUsemm=0;
              return; 
        }


            // 这里添加对应功能的实现逻辑
            // MessageBox.Show($"点击按钮：{button?.Name}\n对应功能还未做gui：{button?.Text}");
            if (int.TryParse(input1.Text, out int num1))
            {

            }
            else
            {
            MessageBox.Show("请输入有效的数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return ;
        }

        if(button?.Name=="controlTopDiskRotation")
        {
           output1.Text= TcpReadinfo.controlTopDiskRotation(num1).ToString();
           return;  
        }
        if(button?.Name=="controlBottomDiskRotation")
        {
           output1.Text= TcpReadinfo.controlBottomDiskRotation(num1).ToString();
            return; 
        }
        if(button?.Name=="controlapertureRotation")
        {
           output1.Text= TcpReadinfo.controlapertureRotation(num1).ToString();
            return; 
        }

        if(button?.Name=="controlfocusRotation")
        {
           output1.Text= TcpReadinfo.controlfocusRotation(num1).ToString();
            return; 
        }

        


    }

   
    }
}
