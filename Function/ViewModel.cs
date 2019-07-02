using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Function
{
    public interface IViewModel
    {
        int Number { get; set; }
    }

    class MyEventArgs : EventArgs
    {
        public double InitialValue { get; set; }
        public double FinalValue { get; set; }
        public int ChangeStep { get; set; }
        public double ParameterValue { get; set; }
        public BindingList<object> MyResult { get; set; }

        public MyEventArgs(double initialValue, double finalValue, int changeStep, double parameterValue, BindingList<object> myResult)
        {                    
            InitialValue = initialValue;
            FinalValue = finalValue;
            ChangeStep = changeStep;
            ParameterValue = parameterValue;
            MyResult = myResult;
        }
    } 

    class ViewModel : INotifyPropertyChanged
    {
        public event EventHandler<MyEventArgs> MessageToFile;

        public event EventHandler<MyEventArgs> CalculationsAreMade;        

        private readonly MyFunction _myFunction;
        
        public double InitialValue { get; set; }
        public double FinalValue { get; set; }
        public int ChangeStep { get; set; }
        public double ParameterValue { get; set; }        
        
        private BindingList<object> myResult;
        public BindingList<object> MyResult
        {
            get
            {
                return myResult;
            }
            set
            {
                if (value.Count() != 0)
                {
                    myResult = value;
                    RaisePropertyChanged();
                }
            }
        }
        
        public ViewModel() { }
        
        public ViewModel(MyFunction myFunction)
        {

            _myFunction = myFunction;            
            InitialValue = myFunction.InitialValue;
            FinalValue = myFunction.FinalValue;
            ChangeStep = myFunction.ChangeStep;
            ParameterValue = myFunction.ParameterValue;
        }

        private void _myFunction_ErrorMessage(object sender, EventArgs e)
        {
            MessageBox.Show("Ошибка приваивания.");
        } 

        public void CalculateResult()
        {            
            CalculationsAreMade?.Invoke(this, new MyEventArgs(InitialValue, FinalValue, ChangeStep, ParameterValue, MyResult));
            MyResult = _myFunction.GetMyResults();
            MessageToFile?.Invoke(this, new MyEventArgs(InitialValue, FinalValue, ChangeStep, ParameterValue, MyResult));            
        }        
        // Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
