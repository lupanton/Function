using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function
{
    //класс, который записывает информацию о возникающих ошибках в log (где-то подглядел и везде теперь пихаю)
    public static class Log
    {
        private static object sync = new object();
        public static void Write(Exception ex)
        {
            try
            {
                // Путь .\\Log
                string pathToLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
                if (!Directory.Exists(pathToLog))
                    Directory.CreateDirectory(pathToLog); // Создаем директорию, если нужно
                //тмя файла на сегодня
                string filename = Path.Combine(pathToLog, string.Format($"{AppDomain.CurrentDomain.FriendlyName}_{DateTime.Now}.log"));
                //текстовая строка, которая записывается в файл
                string fullText = string.Format($"[{DateTime.Now}] [HResult: {ex.HResult}] - {ex.Message}\r\n");
                lock (sync)
                {
                    File.AppendAllText(filename, fullText, Encoding.GetEncoding("Windows-1251"));
                }
            }
            catch
            {
                // Перехватываем все и ничего не делаем
            }
        }
    }
        class MyFunctionAttribute
    {
        public BindingList<double> DividerT { get; set; }

        double initialValue;
        public bool IsValidDouble(double x)
        {
            if (x > -1000 && x < 1000 )
                return true;
            else
                return false;
        }
        public double InitialValue
        {
            get
            {
                return initialValue;
            }
            set
            {
                if (!IsValidDouble(value))
                {
                    Log.Write(new ArgumentException("Wrong value initialValue.", "initialValue"));
                    throw new ArgumentException("Wrong value initialValue.", "initialValue");
                }
                initialValue = value;
            }
        }
        double finalValue;
        public double FinalValue
        {
            get
            {
                return finalValue;
            }
            set
            {
                if (!IsValidDouble(value))
                {
                    Log.Write(new ArgumentException("Wrong value finalValue.", "finalValue"));
                    throw new ArgumentException("Wrong value finalValue.", "finalValue");
                } 
                finalValue = value;
            }
        }
        int changeStep;
        public bool IsValidInt(int x)
        {
            if (x.GetTypeCode() == TypeCode.Int32)
                return true;
            else
                return false;
        }
        public int ChangeStep
        {
            get
            {
                return changeStep;
            }
            set
            {
                if (!IsValidInt(value))
                {
                    Log.Write(new ArgumentException("Wrong value changeStep.", "changeStep"));
                    throw new ArgumentException("Wrong value changeStep.", "changeStep");
                }
                changeStep = value;
            }
        }
        double parameterValue;
        public double ParameterValue
        {
            get
            {
                return parameterValue;
            }
            set
            {
                if (!IsValidDouble(value))
                {
                    Log.Write(new ArgumentException("Wrong value parameterValue.", "parameterValue"));
                    throw new ArgumentException("Wrong value parameterValue.", "parameterValue");
                }
                parameterValue = value;
            }
        }        

        public MyFunctionAttribute() { }

        public MyFunctionAttribute(double initialValue, double finalValue, int changeStep, double parameterValue)
        {
            if (!IsValidDouble(initialValue))
                throw new ArgumentException("Wrong value initialValue.", "initialValue");
            if (!IsValidDouble(finalValue))                         
                throw new ArgumentException("Wrong value finalValue.", "finalValue");
            if (!IsValidInt(changeStep))
                throw new ArgumentException("Wrong value changeStep.", "changeStep");
            if (!IsValidDouble(parameterValue))
                throw new ArgumentException("Wrong value parameterValue.", "parameterValue");            
            InitialValue = initialValue;
            FinalValue = finalValue;
            ChangeStep = changeStep;
            ParameterValue = parameterValue;          
        }
    }
}
