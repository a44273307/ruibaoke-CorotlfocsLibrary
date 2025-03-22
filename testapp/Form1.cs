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
        public Form1()
        {
            InitializeComponent();
        }
        
           private TextBox input1;
    private TextBox input2;
    private TextBox input3;
    private TextBox output1;
    private TextBox output2;
    private Button calculateButton;

    private void Input_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    private void CreateControlButtons()
    {
        // 按钮配置列表（按钮名称，显示文本）
        var buttonConfigs = new List<Tuple<string, string>>() 
        {
            new Tuple<string, string>("controlTopDiskRotation", "控制上圆盘旋转到指定孔位"),
            new Tuple<string, string>("controlBottomDiskRotation", "控制下圆盘旋转到指定孔位"),
            new Tuple<string, string>("readTopDiskInfo", "读取上圆盘状态信息"),
            new Tuple<string, string>("readBottomDiskInfo", "读取下圆盘状态信息（参数同上圆盘）"),
            new Tuple<string, string>("readfocusInfo", "读取聚焦机构状态信息（参数同上圆盘）"),
            new Tuple<string, string>("readapertureInfo", "读取光圈机构状态信息（参数同上圆盘）"),
            new Tuple<string, string>("controlapertureRotation", "控制光圈位置"),
            new Tuple<string, string>("controlfocusRotation", "控制聚焦位置")
        };

        // 创建按钮并设置属性
        int verticalPosition = 10;
        foreach (var config in buttonConfigs)
        {
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
    }
    private void CalculateButton_Click(object sender, EventArgs e)
    {
        if (int.TryParse(input1.Text, out int num1) && int.TryParse(input2.Text, out int num2))
        {
            
            
        }
        else
        {
            MessageBox.Show("请输入有效的数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

        private void Form1_Load(object sender, EventArgs e)
        {
           InitializeComponent();
           CreateControlButtons();

           this.Text = "输入计算器";
     

        Label label1 = new Label() { Text = "输入1:", Left = 500, Top = 10 };
        input1 = new TextBox() { Left = 600, Top = 10, Width = 100 };
        input1.KeyPress += Input_KeyPress;

        Label label2 = new Label() { Text = "输入2:", Left = 500, Top = 40 };
        input2 = new TextBox() { Left = 600, Top = 40, Width = 100 };
        input2.KeyPress += Input_KeyPress;

        Label label3 = new Label() { Text = "输入3:", Left = 500, Top = 70 };
        input3 = new TextBox() { Left = 600, Top = 40, Width = 100 };
        input3.KeyPress += Input_KeyPress;

        

        Label label4 = new Label() { Text = "输出1:", Left = 500, Top = 100 };
        output1 = new TextBox() { Left = 600, Top = 100, Width = 100, ReadOnly = true };

        Label label5 = new Label() { Text = "输入3:", Left = 500, Top = 70 };
        output2 = new TextBox() { Left = 600, Top = 130, Width = 100};

        this.Controls.Add(label1);
        this.Controls.Add(input1);
        this.Controls.Add(label2);
        this.Controls.Add(input2);
        this.Controls.Add(calculateButton);
        this.Controls.Add(label3);
        this.Controls.Add(output1);
        this.Controls.Add(label4);
        this.Controls.Add(output2);
        }
        
    private void Button_ClickHandler(object sender, EventArgs e)
    {
        var button = sender as Button;
        // 这里添加对应功能的实现逻辑
        // MessageBox.Show($"点击按钮：{button?.Name}\n对应功能还未做gui：{button?.Text}");
        if (int.TryParse(input1.Text, out int num1) && int.TryParse(input2.Text, out int num2)
        && int.TryParse(input3.Text, out int num3))
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

        if(button?.Name=="controlfocusRotation")
        {
            int ans=TcpReadinfo.readinform(num1,num2,out num3);
            output1.Text= ans.ToString();
            output2.Text = num3.ToString();
                return; 
        }


    }


    }
}
