using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace mono2.Framework
{
    public static class ParameterManager
    {
        public static List<Parameter> AllParameters = new List<Parameter>();

        public static IDictionary<string, Parameter> parameterMap = new Dictionary<string, Parameter>();


        public static void setParameter( Parameter parameter, double value) 
        {
            parameterMap[parameter.Name].Value = value;
        }

        public static Parameter getParameter(string Name, float Data, bool isInt = false) 
        {
            if (!parameterMap.ContainsKey(Name))
            {
                parameterMap.Add(Name,  new Parameter(Name, Data, isInt) );
                AllParameters.Add(parameterMap[Name]);
            }
            
            return parameterMap[Name];
            
        }
        public static Parameter getParameter(string Name, float Data, double min, double max, bool isInt = false) 
        {
            if (!parameterMap.ContainsKey(Name))
            {
                parameterMap.Add(Name, new Parameter(Name, Data, min, max, isInt));
                AllParameters.Add(parameterMap[Name]);
            }
            return parameterMap[Name];
        }


    }

    public class Parameter
    {
        double data;
        string name;
        double minValue;
        public double maxValue;
        public bool isInt = false;


        public static List<List<double>> AllParameterLists = new List<List<double>>();


        public Parameter(string Name, float Data, bool isInt = false)
        {
            data = Data;
            name = Name;
            minValue = double.MinValue;
            maxValue = double.MaxValue;
            this.isInt = isInt;


            if (File.Exists("AllParameters.json"))
            {
                AllParameterLists = Serializer.ReadFromJsonFile<List<List<double>>>("AllParameters.json");
            }
            load(0);
        }

        public Parameter()
        {

        }

        public Parameter(string Name, float Data, double min, double max, bool isInt = false)
        {
            data = Data;
            name = Name;
            this.isInt = isInt;
            if (!isInt)
            {
                minValue = min;
                maxValue = max;
            }
            else
            {
                minValue = Math.Floor(min);
                maxValue = Math.Floor(max);
            }
        }
        public int intValue
        {
            get
            {
                if (isInt)
                {
                    return (int)data;
                }
                return 0;
            }
        }
        public double Value
        {
            get
            {
                return data;
            }
            set
            {
                if (!isInt)
                {
                    data = value;
                    data = Math.Max(data, minValue);
                    data = Math.Min(data, maxValue);
                }
                else
                {
                    if (value > data)
                    {
                        data = value;
                        data = Math.Ceiling(Math.Min(data, maxValue));
                    }
                    else
                    if (value < data)
                    {
                        data = value;
                        data = Math.Floor(Math.Max(data, minValue));
                    }

                }
            }
        }


        public static void save()
        {
            List<double> parameterValues = new List<double>();
            foreach (var parameter in ParameterManager.AllParameters)
            {
                parameterValues.Add(parameter.Value);
            }
            AllParameterLists.Add(parameterValues);
            Serializer.WriteToJsonFile("AllParameters.json", AllParameterLists);

        }

        public static void load(int index)
        {
            if (AllParameterLists.Count >= 1)
            {
                for (int i = 0; i < ParameterManager.AllParameters.Count; i++)
                {
                    ParameterManager.AllParameters[i].Value = AllParameterLists[index][i];

                }
            }

        }

        public string Name
        {
            get { return name; }
        }
    }

}
