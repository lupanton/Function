using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function
{
    class MyFunction : MyFunctionAttribute
    {
        public BindingList<object> MyResult { get; set; }

        public MyFunction() { }

        public MyFunction(BindingList<object> myResult, double initialValue, double finalValue, int changeStep, double parameterValue)
            :base(initialValue, finalValue, changeStep, parameterValue)
        {
            MyResult = myResult;
        }       

        public BindingList<object> GetMyResults() 
        {
            int count = 1;
            BindingList<object> myResult = new BindingList<object>();
            for (int i = 1; i < Convert.ToDouble(FinalValue) - Convert.ToDouble(InitialValue); i += ChangeStep)
            {
                myResult.Add(new
                {
                    Iteration = count,
                    N = i,
                    Result = Math.Round(Math.Pow(ParameterValue, i) / ((i) * (i)), 2)                  
                });
                count++;
                if (i > 99)
                    break;                
            }    
            MyResult = myResult;
            
            return myResult;
        }
    }
}
