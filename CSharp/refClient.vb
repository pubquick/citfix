Imports System.Collections.Concurrent
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net


Public Class refClient
	Dim BaseUrl As String = "https://pubquick.co.uk/"

	Public Function Parse(orefquery As Query) As QueryResult
		Try
			Using wclient As New Net.Http.HttpClient()
				wclient.BaseAddress = New Uri(BaseUrl)
				wclient.DefaultRequestHeaders.Accept.Clear()
				wclient.DefaultRequestHeaders.Accept.Add(New Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"))
				wclient.Timeout = TimeSpan.FromMinutes(30)

				Dim requestcontent = New StringContent(JsonConvert.SerializeObject(orefquery), System.Text.Encoding.UTF8, "application/json")
				'Dim postTask = wclient.PostAsJsonAsync("api/refparser", orefquery)
				Dim postTask = wclient.PostAsync("api/refparser", requestcontent)
				postTask.Wait()
				Dim result = postTask.Result
				If result.IsSuccessStatusCode Then
					Dim readTask = result.Content.ReadAsStringAsync()
					readTask.Wait()
					Dim converters As Newtonsoft.Json.JsonConverter() = {New RefItemJsonConverter()}
					Dim qresult As QueryResult = Newtonsoft.Json.JsonConvert.DeserializeObject(Of QueryResult)(readTask.Result, New Newtonsoft.Json.JsonSerializerSettings() With {.Converters = converters})
					If qresult IsNot Nothing AndAlso qresult.Refs.Count > 0 Then
						Return qresult
					Else
						Return Nothing
					End If
				Else
					Return Nothing
				End If
			End Using
		Catch ex As Exception
			'ExceptionHandling(ex)
		End Try
		Return Nothing
	End Function
End Class

Public Class RefItemJsonConverter
	Inherits JsonConverter
	Public Overrides Function CanConvert(ByVal objectType As Type) As Boolean
		If objectType = GetType(refData.refitems) Then
			Return True
		End If
		Return False
	End Function

	Public Overrides Function ReadJson(ByVal reader As JsonReader, ByVal objectType As Type, ByVal existingValue As Object, ByVal serializer As JsonSerializer) As Object
		Dim jo As JObject = JObject.Load(reader)

		If jo("et") IsNot Nothing Then
			Select Case jo("et").Value(Of String)
				Case "grp" : Return jo.ToObject(Of refData.group)(serializer)
				Case "ri" : Return jo.ToObject(Of refData.refitem)(serializer)
				Case "sp" : Return jo.ToObject(Of refData.space)(serializer)
				Case Else
			End Select
		Else
			Return jo.ToObject(Of refData.refitem)(serializer)
		End If

		Return Nothing
	End Function

	Public Overrides ReadOnly Property CanWrite As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overrides Sub WriteJson(ByVal writer As JsonWriter, ByVal value As Object, ByVal serializer As JsonSerializer)
		Throw New NotImplementedException()
	End Sub
End Class




Public Class QueryResult
	Public Property Result As Boolean = False
	Public Property Remarks As String = String.Empty
	Public Property timetaken As Integer
	Public Property Refs As New List(Of refData)
End Class

Public Class refData
	Public refmeta As New crefinfo
	Public refs As New List(Of refitems)

	Public Property result As String
	Public Property matchpercent As Integer = 0
	Public Property id As String = String.Empty
	Public Property rid As Long = 0
	Public Property seq As Integer = 0
	Public Property timetaken As Integer = 0
	Public Property reftype As String = String.Empty
	Public Class crefinfo
		Public Property refid As String = String.Empty
		Public Property doi As String = String.Empty
		Public Property pmid As String = String.Empty
		Public Property pmcid As String = String.Empty
		Public Property pii As String = String.Empty
		Public Property isbn As String = String.Empty
		Public Property pissn As String = String.Empty
		Public Property eissn As String = String.Empty
	End Class

	Public MustInherit Class refitems

	End Class
	Public Class group : Inherits refitems
		Public ReadOnly Property et As String = "grp"
		Public Property type As String = String.Empty
		Public refs As New List(Of refitems)
	End Class
	Public Class refitem : Inherits refitems
		Public ReadOnly Property et As String = "ri"
		Public Property type As String = String.Empty
		Public Property text As String = String.Empty
	End Class
	Public Class space : Inherits refitems
		Public ReadOnly Property et As String = "sp"
		Public Property type As String = "space"
		Public Property text As String = " "
	End Class
End Class
Public Class Query
	Public Enum epriority
		low = 1
		medium = 2
		high = 3
	End Enum
	Public Property webkey As String = String.Empty
	Public Property priority As epriority = epriority.medium
	Public Property batchname As String = String.Empty
	Public Property Demomode As Boolean = False
	Public Property refs As New List(Of refitem)


	Public Class refitem
		Public Property id As String = String.Empty
		Public Property seq As Integer = 0
		Public Property txt As String = String.Empty

		Public Property RefData As refData
	End Class
End Class
