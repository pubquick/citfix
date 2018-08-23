
Module Module1

	Sub Main()
		Dim orefclient As New refClient
		Dim oQuery As New Query()
		oQuery.priority = Query.epriority.medium
		oQuery.webkey = ""  'Replace your original key



		Dim orefitem1 As New Query.refitem
		orefitem1.id = 1000 'Reference ID
		orefitem1.txt = "Lei, Q., et al., Behavior of Flow Through Low-Permeability Reservoirs, Society of Petroleum Engineers, 2008."

		Dim orefitem2 As New Query.refitem
		orefitem2.id = 1001 'Reference ID
		orefitem2.txt = "Zhao S &#x0026; Fernald RD 2005 Comprehensive algorithm for quantitative real-time polymerase chain reaction. <i>Journal of Computational Biology</i> <b>12</b> 1047-1064."



		'Add references
		oQuery.refs.Add(orefitem1)
		oQuery.refs.Add(orefitem2)

		Dim oQueryresult As QueryResult = orefclient.Parse(oQuery)
		Console.WriteLine("Result: " & oQueryresult.Result)
		Console.WriteLine("Remarks: " & oQueryresult.Remarks)

		For Each eachrefitem As refData In oQueryresult.Refs
			Console.WriteLine("Reference: " & eachrefitem.id)
			Dim sbRefXml As New Text.StringBuilder
			sbRefXml.AppendLine("<structured id=""" & eachrefitem.id & """ refiduser=""" & eachrefitem.refmeta.refid & """ doi=""" & eachrefitem.refmeta.doi & """ pmcid=""" & eachrefitem.refmeta.pmcid & """ pubmed=""" & eachrefitem.refmeta.pmid & """ matchpercent=""" & eachrefitem.matchpercent & """ type=""" & eachrefitem.reftype & """>")
			For Each eachitem In eachrefitem.refs
				If TypeOf (eachitem) Is refData.group Then
					Dim tgroup As refData.group = TryCast(eachitem, refData.group)
					sbRefXml.AppendLine("<group type=""" & tgroup.type & """>")
					For Each eachitemgroup In tgroup.refs
						If TypeOf (eachitemgroup) Is refData.refitem Then
							Dim trefitem As refData.refitem = TryCast(eachitemgroup, refData.refitem)
							sbRefXml.AppendLine("<item type=""" & trefitem.type & """>" & trefitem.text & "</item>")
						ElseIf TypeOf (eachitemgroup) Is refData.space Then
							Dim trefspace As refData.space = TryCast(eachitemgroup, refData.space)
							sbRefXml.AppendLine("<space />")
						End If
					Next
					sbRefXml.AppendLine("</group>")
				ElseIf TypeOf (eachitem) Is refData.refitem Then
					Dim trefitem As refData.refitem = TryCast(eachitem, refData.refitem)
					sbRefXml.AppendLine("<item type=""" & trefitem.type & """>" & trefitem.text & "</item>")
				ElseIf TypeOf (eachitem) Is refData.space Then
					Dim trefspace As refData.space = TryCast(eachitem, refData.space)
					sbRefXml.AppendLine("<space />")
				End If
			Next
			sbRefXml.AppendLine("</structured>")
			Console.WriteLine(sbRefXml.ToString)
		Next

		Console.WriteLine("--------------END---------------")
		Console.ReadKey()
	End Sub

End Module
