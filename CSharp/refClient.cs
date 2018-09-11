using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;


public class refClient
{
    private string BaseUrl = "https://pubquick.co.uk/";

    public QueryResult Parse(Query orefquery)
    {
        try
        {
            using (System.Net.Http.HttpClient wclient = new System.Net.Http.HttpClient())
            {
                wclient.BaseAddress = new Uri(BaseUrl);
                wclient.DefaultRequestHeaders.Accept.Clear();
                wclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                wclient.Timeout = TimeSpan.FromMinutes(30);

                var requestcontent = new StringContent(JsonConvert.SerializeObject(orefquery), System.Text.Encoding.UTF8, "application/json");
                // Dim postTask = wclient.PostAsJsonAsync("api/refparser", orefquery)
                var postTask = wclient.PostAsync("api/refparser", requestcontent);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    Newtonsoft.Json.JsonConverter[] converters = new[] { new RefItemJsonConverter() };
                    QueryResult qresult = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryResult>(readTask.Result, new Newtonsoft.Json.JsonSerializerSettings() { Converters = converters });
                    if (qresult != null && qresult.Refs.Count > 0)
                        return qresult;
                    else
                        return null;
                }
                else
                    return null;
            }
        }
        catch (Exception ex)
        {
        }
        return null;
    }
}

public class RefItemJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        if (objectType == typeof(refData.refitems))
            return true;
        return false;
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);

        if (jo("et") != null)
        {
            switch (jo("et").Value<string>)
            {
                case "grp":
                    {
                        return jo.ToObject<refData.group>(serializer);
                    }

                case "ri":
                    {
                        return jo.ToObject<refData.refitem>(serializer);
                    }

                case "sp":
                    {
                        return jo.ToObject<refData.space>(serializer);
                    }

                default:
                    {
                        break;
                    }
            }
        }
        else
            return jo.ToObject<refData.refitem>(serializer);

        return null;
    }

    public override bool CanWrite
    {
        get
        {
            return false;
        }
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}




public class QueryResult
{
    public bool Result { get; set; } = false;
    public string Remarks { get; set; } = string.Empty;
    public int timetaken { get; set; }
    public List<refData> Refs { get; set; } = new List<refData>();
}

public class refData
{
    public crefinfo refmeta = new crefinfo();
    public List<refitems> refs = new List<refitems>();

    public string result { get; set; }
    public int matchpercent { get; set; } = 0;
    public string id { get; set; } = string.Empty;
    public long rid { get; set; } = 0;
    public int seq { get; set; } = 0;
    public int timetaken { get; set; } = 0;
    public string reftype { get; set; } = string.Empty;
    public class crefinfo
    {
        public string refid { get; set; } = string.Empty;
        public string doi { get; set; } = string.Empty;
        public string pmid { get; set; } = string.Empty;
        public string pmcid { get; set; } = string.Empty;
        public string pii { get; set; } = string.Empty;
        public string isbn { get; set; } = string.Empty;
        public string pissn { get; set; } = string.Empty;
        public string eissn { get; set; } = string.Empty;
    }

    public abstract class refitems
    {
    }
    public class group : refitems
    {
        public string et { get; } = "grp";
        public string type { get; set; } = string.Empty;
        public List<refitems> refs = new List<refitems>();
    }
    public class refitem : refitems
    {
        public string et { get; } = "ri";
        public string type { get; set; } = string.Empty;
        public string text { get; set; } = string.Empty;
    }
    public class space : refitems
    {
        public string et { get; } = "sp";
        public string type { get; set; } = "space";
        public string text { get; set; } = " ";
    }
}

public class Query
{
    public enum epriority
    {
        low = 1,
        medium = 2,
        high = 3
    }
    public string webkey { get; set; } = string.Empty;
    public epriority priority { get; set; } = epriority.medium;
    public string batchname { get; set; } = string.Empty;
    public bool Demomode { get; set; } = false;
    public List<refitem> refs { get; set; } = new List<refitem>();


    public class refitem
    {
        public string id { get; set; } = string.Empty;
        public long rid { get; set; } = 0;
        public int seq { get; set; } = 0;
        public string txt { get; set; } = string.Empty;

        public refData RefData { get; set; }
    }
}
