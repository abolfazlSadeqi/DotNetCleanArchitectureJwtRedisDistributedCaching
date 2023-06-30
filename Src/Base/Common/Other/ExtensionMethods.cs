using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Common.Other
{
    public sealed class ExtensionMethods
    {

       
        public string GetOnlyNumbers(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var numbers = text.Where(char.IsDigit).ToArray();
            if (numbers == null || numbers.Length == 0)
                return string.Empty;

            return new string(numbers);
        }





        public string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        public string RemoveQuotationMarks(string value)
        {
            if (!string.IsNullOrWhiteSpace(value) && value.EndsWith("\'") || value.EndsWith("\""))
                return value.Replace("\'", "").Replace("\"", "").Trim();
            return value;
        }


       



        public void ValidItensArray()
        {
            var list = new List<int>() { 4, 6, 7, 8, 34, 33, 11 };
            var isTrue = list.TrueForAll(n => n > 0);
            Console.WriteLine("VAlidate = {0}", isTrue);
        }



        public string ConvertImageToBase64(string pImage)
        {
            var imagePath = Path.GetFileName(pImage);
            var imageArray = File.ReadAllBytes(imagePath);

            return string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(imageArray));
        }

        public string GetColorHex(string[] arrColorUsed)
        {
            string color = string.Empty;
            while (true)
            {
                color = GetColorHex();
                if (arrColorUsed.Any(x => x != color))
                    break;
            }

            return color;
        }

        public string GetColorHex()
        {
            Random random = new Random();
            int r = random.Next(0, 255);
            int g = random.Next(0, 255);
            int b = random.Next(0, 255);
            int a = random.Next(0, 255);
            string cor = string.Format("#{0}", Color.FromArgb(a, r, g, b).Name.ToUpper().Substring(0, 6));
            return cor;
        }

        public string GetColorRgb(string[] arrColorUsed)
        {
            string color = string.Empty;
            while (true)
            {
                color = GetColorRgb().ToString();
                if (arrColorUsed.Any(x => x != color))
                    break;
            }

            return color;
        }

        public Color GetColorRgb()
        {
            Random rnd = new Random();
            Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            return randomColor;
        }

        public DateTime LastDayPreviousMonth(string currentMonth, string currentYear)
        {
            DateTime firstMonthDay = DateTime.Parse(string.Format("{0}/{1}/{2}", "01", currentMonth, currentYear));
            DateTime lastMonthDay = firstMonthDay.AddDays(-1);
            return lastMonthDay;
        }

        public DateTime LastDayCurrentMonth(string currentMonth, string currentYear)
        {
            DateTime firstMonthDay = DateTime.Parse(string.Format("{0}/{1}/{2}", "01", currentMonth, currentYear));
            DateTime firstMonthNextDay = firstMonthDay.AddMonths(1);
            DateTime lastMonthDay = firstMonthNextDay.AddDays(-1);
            return lastMonthDay;
        }

        private static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            return table;
        }

        public List<string> CloneList(List<string> list)
        {
            return list.GetRange(0, list.Count);
        }

       


        public string[] getLocalDriversFromMachine()
        {
            return Environment.GetLogicalDrives();
        }

        private XmlDocument removeXmlDeclaration(XmlDocument doc)
        {
            var declarations = doc.ChildNodes.OfType<XmlNode>().Where(x => x.NodeType == XmlNodeType.XmlDeclaration).ToList();
            declarations.ForEach(x => doc.RemoveChild(x));
            return doc;
        }



        public string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }



        public static DateTime GetNextUtilDay(DateTime dateTime)
        {
            try
            {
                while (true)
                {
                    if (dateTime.DayOfWeek == DayOfWeek.Saturday)
                        dateTime = dateTime.AddDays(2);
                    else if (dateTime.DayOfWeek == DayOfWeek.Sunday)
                        dateTime = dateTime.AddDays(1);

                    return dateTime;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }


        public static double MilesToKm(double miles) => Math.Round(miles * 1.609, 3);

        public static double KmToMiles(double km) => Math.Round(km / 1.609, 3);

        public static string FormatStringBase64ToString(string text)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(text));
        }



        private bool IsBinaryString(string binaryContent) => Regex.IsMatch(binaryContent, "^[01]+$");


        public bool IsValidNumber(int number) => number is >= 0 and <= 100;

        public bool IsZeroOrOne(int number) => number is 0 or 1;

        public bool IsvalidObject(object obj) => obj is not null;

        public bool IsValidSeparator(char c) => c is >= 'a' and <= 'z' or >= 'A' and <= 'Z' or '.' or ',';


    }
}