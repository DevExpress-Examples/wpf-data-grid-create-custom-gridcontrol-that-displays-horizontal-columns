' Developer Express Code Central Example:
' How to create a custom GridControl that represents columns horizontally in a way similar to the WinForms VerticalGrid control
' 
' This example demonstrates how to create a GridControl descendant with
' horizontally oriented columns.
' To use it, simply add the
' VerticalGridControl.xaml and VerticalGridControl.xaml.cs files in your
' project.
' 
' In addition, this example demonstrates how to customize grid cells
' using data templates.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E4630

Imports System
Imports System.Collections.Generic

Namespace dxExample
	Friend Class SampleDataRow
		Public Property Id() As Integer
		Public Property Name() As String
		Public Property [Date]() As Date
		Public Property HasFlag() As Boolean

		Public Shared Function CreateRows(ByVal rowCount As Integer) As IList(Of SampleDataRow)
			Dim result As IList(Of SampleDataRow) = New List(Of SampleDataRow)()
			For i As Integer = 0 To rowCount - 1
				result.Add(New SampleDataRow() With {.Id = i, .Name = String.Format("name {0}", i Mod 3), .Date = Date.Now.AddDays(i), .HasFlag = (i Mod 3 = 0)})
			Next i
			Return result
		End Function
	End Class
End Namespace
