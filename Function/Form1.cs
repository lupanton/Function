using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Function
{
    public partial class Form1 : Form
    {
        private ViewModel _viewModel;      

        public Form1()
        {
            InitializeComponent();
            button1.Click += delegate
            {
                dataGridView1.DataSource = new object();
                try
                {
                    MyFunction myfunc = new MyFunction()
                    {
                        InitialValue = (Convert.ToDouble(textBox1.Text)),
                        FinalValue = (Convert.ToDouble(textBox2.Text)),
                        ChangeStep = (Convert.ToInt32(comboBox1.Text)),
                        ParameterValue = (Convert.ToDouble(textBox4.Text))
                    };
                    //это вообще неправильно = светить модель во вьюшке, но мне чет надоело, потом разберусь
                    //myfunc.ErrorMessage += (s, e) => { MessageBox.Show($"Значение {e.InitialValue} , или {e.FinalValue}, или {e.ChangeStep}, или {e.ParameterValue} введены неправильно."); };
                    //какой-то метод в MyFunction

                    _viewModel = new ViewModel(myfunc);
                    _viewModel.CalculationsAreMade += (s, e) => { MessageBox.Show($"Расчет для Х = {e.ParameterValue} произведен {DateTime.Now}: \n на интервале от {e.InitialValue} до {e.FinalValue} с шагом {e.ChangeStep}"); };
                    _viewModel.MessageToFile += (s, e) => 
                    {                        
                        FileStream fs = new FileStream(@"F:\\function.txt", FileMode.Append);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.WriteLine($"Расчет для Х = { e.ParameterValue} произведен { DateTime.Now}: \n на интервале от { e.InitialValue} до { e.FinalValue} с шагом { e.ChangeStep}");
                        for (int i = 0; i < e.MyResult.Count(); i++)
                        {
                            sw.WriteLine(e.MyResult[i].ToString());
                        }
                        sw.Close();
                    };
                    _viewModel.CalculateResult();
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "iteration",
                        HeaderText = "Iteration"
                    });
                    //("iteration", "Iteration");
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "n",
                        HeaderText = "N"
                    });
                    //("n", "N");
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "result",
                        HeaderText = "Result"
                    });
                    //("result", "Result");
                    dataGridView1.DataSource = _viewModel.MyResult;

                    textBox1.DataBindings.Add("", _viewModel, "", true);
                    textBox2.DataBindings.Add("", _viewModel, "", true);
                }
                catch
                {
                    MessageBox.Show("Что-то не так");
                }
                
            };
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {            
            //вообще так неправильно - нужно это вычисление иметь только в ViewModel, а здесь только отображение
            double diff = Convert.ToDouble(textBox2.Text) - Convert.ToDouble(textBox1.Text);
            BindingList<double> divider = new BindingList<double>();
            for (int i = 0; i < diff; i++)
            {
                if (diff % i == 0)
                {
                    divider.Add(i);
                }
            }
            comboBox1.DataSource = divider;
        }        
    }
}

