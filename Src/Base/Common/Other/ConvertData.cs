using FluentValidation.Results;
using Newtonsoft.Json;
using System.Data;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Common.Other
{
    public static class ConvertData
    {

        public static List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex)
                        { throw new Exception(ex.Message, ex.InnerException); }
                    }
                }
                return objT;
            }).ToList();
        }

        public static string ConvertTimeSpanToString(TimeSpan hour)
        {
            if (hour != null)
            {
                if (hour.Hours < 10 && hour.Minutes < 10)
                    return $"0{hour.Hours}:0{hour.Minutes}";
                else if (hour.Hours >= 10 && hour.Minutes < 10)
                    return $"{hour.Hours}:0{hour.Minutes}";
                else if (hour.Hours < 10 && hour.Minutes >= 10)
                    return $"0{hour.Hours}:0{hour.Minutes}";
                else
                    return $"{hour.Hours}:{hour.Minutes}";
            }

            return string.Empty;
        }


        public static string CastValidationFailureTostring(List<ValidationFailure> failures)
        {
            string _Message = "";
            foreach (var item in failures)
                _Message += item.ErrorMessage;
            return _Message;
        }


        public static DataTable ConvertDynamicListToDataTable(List<dynamic> list)
        {
            var json = JsonConvert.SerializeObject(list);
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, typeof(DataTable));
            return dt;
        }


        public static string ConvertObjectParaJSon<T>(T obj)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, obj);
                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return jsonString;
            }
            catch
            {
                throw;
            }
        }



        public static string ConvertDateToString(string value)
        {
            return DateTime.TryParse(value, out var date) ? date.ToString("yyyy-MM-dd") : value;
        }


    }

}
