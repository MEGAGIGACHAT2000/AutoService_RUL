using System;
using System.Collections.Generic;
using System.IO;
using calc.interfaces;
using Newtonsoft.Json;

namespace calc.models
{
    public class CalculatorModel : CalculatorInterface{
        private List<string> history = new List<string>();
        private const string HistoryFilePath = "history.json";

        public CalculatorModel()
        {
            LoadHistory();
        }

        public double Add(double a, double b)
        {
            double result = a + b;
            AddToHistory($"{a} + {b} = {result}");
            return result;
        }

        public double Subtract(double a, double b)
        {
            double result = a - b;
            AddToHistory($"{a} - {b} = {result}");
            return result;
        }

        public double Multiply(double a, double b)
        {
            double result = a * b;
            AddToHistory($"{a} * {b} = {result}");
            return result;
        }

        public double Divide(double a, double b)
        {
            if (b == 0) throw new DivideByZeroException();
            double result = a / b;
            AddToHistory($"{a} / {b} = {result}");
            return result;
        }

        private void AddToHistory(string entry)
        {
            history.Add(entry);
            SaveHistory();
        }

        private void LoadHistory()
        {
            if (File.Exists(HistoryFilePath))
            {
                string json = File.ReadAllText(HistoryFilePath);
                history = JsonConvert.DeserializeObject<List<string>>(json);
            }
        }

        private void SaveHistory()
        {
            string json = JsonConvert.SerializeObject(history);
            File.WriteAllText(HistoryFilePath, json);
        }
    }
}
