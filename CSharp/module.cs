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

static class Module1
{
    public static void Main()
    {
        refClient orefclient = new refClient();
        Query oQuery = new Query();
        oQuery.priority = Query.epriority.medium;
        oQuery.webkey = "";  // Replace your original key



        Query.refitem orefitem1 = new Query.refitem();
        orefitem1.id = 1000; // Reference ID
        orefitem1.txt = "Lei, Q., et al., Behavior of Flow Through Low-Permeability Reservoirs, Society of Petroleum Engineers, 2008.";

        Query.refitem orefitem2 = new Query.refitem();
        orefitem2.id = 1001; // Reference ID
        orefitem2.txt = "Zhao S &#x0026; Fernald RD 2005 Comprehensive algorithm for quantitative real-time polymerase chain reaction. <i>Journal of Computational Biology</i> <b>12</b> 1047-1064.";



        // Add references (Maximum number of refereces allowed per request is 250)
        oQuery.refs.Add(orefitem1);
        oQuery.refs.Add(orefitem2);

        QueryResult oQueryresult = orefclient.Parse(oQuery);
        Console.WriteLine("Result: " + oQueryresult.Result);
        Console.WriteLine("Remarks: " + oQueryresult.Remarks);

        foreach (refData eachrefitem in oQueryresult.Refs)
        {
            Console.WriteLine("Reference: " + eachrefitem.id);
            System.Text.StringBuilder sbRefXml = new System.Text.StringBuilder();
            sbRefXml.AppendLine("<structured id=\"" + eachrefitem.id + "\" refiduser=\"" + eachrefitem.refmeta.refid + "\" doi=\"" + eachrefitem.refmeta.doi + "\" pmcid=\"" + eachrefitem.refmeta.pmcid + "\" pubmed=\"" + eachrefitem.refmeta.pmid + "\" matchpercent=\"" + eachrefitem.matchpercent + "\" type=\"" + eachrefitem.reftype + "\">");
            foreach (var eachitem in eachrefitem.refs)
            {
                if ((eachitem) is refData.group)
                {
                    refData.group tgroup = eachitem as refData.group;
                    sbRefXml.AppendLine("<group type=\"" + tgroup.type + "\">");
                    foreach (var eachitemgroup in tgroup.refs)
                    {
                        if ((eachitemgroup) is refData.refitem)
                        {
                            refData.refitem trefitem = eachitemgroup as refData.refitem;
                            sbRefXml.AppendLine("<item type=\"" + trefitem.type + "\">" + trefitem.text + "</item>");
                        }
                        else if ((eachitemgroup) is refData.space)
                        {
                            refData.space trefspace = eachitemgroup as refData.space;
                            sbRefXml.AppendLine("<space />");
                        }
                    }
                    sbRefXml.AppendLine("</group>");
                }
                else if ((eachitem) is refData.refitem)
                {
                    refData.refitem trefitem = eachitem as refData.refitem;
                    sbRefXml.AppendLine("<item type=\"" + trefitem.type + "\">" + trefitem.text + "</item>");
                }
                else if ((eachitem) is refData.space)
                {
                    refData.space trefspace = eachitem as refData.space;
                    sbRefXml.AppendLine("<space />");
                }
            }
            sbRefXml.AppendLine("</structured>");
            Console.WriteLine(sbRefXml.ToString());
        }

        Console.WriteLine("--------------END---------------");
        Console.ReadKey();
    }
}
