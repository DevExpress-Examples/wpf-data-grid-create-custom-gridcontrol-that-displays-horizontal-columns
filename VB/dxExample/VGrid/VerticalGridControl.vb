Imports DevExpress.Data
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Linq
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data

Namespace dxExample.VGrid
	Public Class VerticalGridControl
		Inherits GridControl

		Public Property Rows() As VerticalRowsCollection
		Public Property AutoPopulateRows() As Boolean
			Get
				Return DirectCast(GetValue(AutoPopulateRowsProperty), Boolean)
			End Get
			Set(ByVal value As Boolean)
				SetValue(AutoPopulateRowsProperty, value)
			End Set
		End Property
		Public Shadows Property ItemsSource() As Object
			Get
				Return DirectCast(GetValue(ItemsSourceProperty), Object)
			End Get
			Set(ByVal value As Object)
				SetValue(ItemsSourceProperty, value)
			End Set
		End Property
		Public Sub New()
			AddHandler CustomUnboundColumnData, AddressOf OnCustomUnboundColumnData
			InitializeRowsCollection()
		End Sub
		Private Sub InitializeRowsCollection()
			Rows = New VerticalRowsCollection()
			AddHandler Rows.CollectionChanged, AddressOf OnRowsCollectionChanged
		End Sub
		Private Sub UpdateGridColumns()
'INSTANT VB NOTE: The variable itemsSource was renamed since Visual Basic does not handle local variables named the same as class members well:
			Dim itemsSource_Renamed As ICollection = TryCast(ItemsSource, ICollection)
			If itemsSource_Renamed Is Nothing Then
				Columns.Clear()
				Return
			End If
			Dim columnsCount As Integer = itemsSource_Renamed.Count
			If Columns.Count = columnsCount Then
				Return
			End If
			Columns.BeginUpdate()
			Dim delta As Integer = columnsCount - Columns.Count
			If columnsCount > Columns.Count Then
				For i As Integer = Columns.Count To columnsCount - 1
					Columns.Add(New GridColumn() With {
						.FieldName = i.ToString(),
						.UnboundType = UnboundColumnType.Object
					})
				Next i
			Else
				For i As Integer = Columns.Count - 1 To columnsCount Step -1
					Columns.RemoveAt(i)
				Next i
			End If
			Columns.EndUpdate()
		End Sub
		Private Sub UpdateItemsSourceCollectionChangedSubscription(ByVal oldSource As Object, ByVal newSource As Object)
			If TypeOf oldSource Is INotifyCollectionChanged Then
				RemoveHandler DirectCast(oldSource, INotifyCollectionChanged).CollectionChanged, AddressOf OnItemsSourceCollectionChanged
			End If
			If TypeOf newSource Is INotifyCollectionChanged Then
				AddHandler DirectCast(newSource, INotifyCollectionChanged).CollectionChanged, AddressOf OnItemsSourceCollectionChanged
			End If
		End Sub
		Private Sub PopulateRows()
			If Not (TypeOf ItemsSource Is IEnumerable) Then
				Return
			End If
			Dim firstItem = DirectCast(ItemsSource, IEnumerable).Cast(Of Object)().FirstOrDefault()
			If firstItem Is Nothing Then
				Return
			End If
			Dim newRows = TypeDescriptor.GetProperties(firstItem).Cast(Of PropertyDescriptor)().Select(Function(p) New VerticalRow() With {.RowName = p.Name})
			For Each row In newRows
				Rows.Add(row)
			Next row
		End Sub
		Private Sub OnRowsCollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
			MyBase.ItemsSource = Rows
		End Sub
		Private Shared Overloads Sub OnItemsSourceChanged(ByVal sender As Object, ByVal e As DependencyPropertyChangedEventArgs)
			Dim grid = DirectCast(sender, VerticalGridControl)
			grid.UpdateGridColumns()
			If grid.AutoPopulateRows Then
				grid.PopulateRows()
			End If
			grid.UpdateItemsSourceCollectionChangedSubscription(e.OldValue, e.NewValue)
		End Sub
		Private Sub OnItemsSourceCollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
			If e.Action = NotifyCollectionChangedAction.Add OrElse e.Action = NotifyCollectionChangedAction.Remove Then
				UpdateGridColumns()
			End If
		End Sub
		Private Sub OnCustomUnboundColumnData(ByVal sender As Object, ByVal e As GridColumnDataEventArgs)
'INSTANT VB NOTE: The variable itemsSource was renamed since Visual Basic does not handle local variables named the same as class members well:
			Dim itemsSource_Renamed = TryCast(ItemsSource, IList)
			If itemsSource_Renamed Is Nothing Then
				Return
			End If
			Dim row = Rows(e.ListSourceRowIndex)
			Dim item = itemsSource_Renamed(Convert.ToInt32(e.Column.FieldName))
			Dim targetProperty = TypeDescriptor.GetProperties(item).Cast(Of PropertyDescriptor)().FirstOrDefault(Function(p) p.Name = row.RowName)
			If targetProperty Is Nothing Then
				Return
			End If
			If e.IsGetData Then
				e.Value = targetProperty.GetValue(item)
			End If
			If e.IsSetData Then
				targetProperty.SetValue(item, e.Value)
			End If
		End Sub

		Public Shared ReadOnly AutoPopulateRowsProperty As DependencyProperty = DependencyProperty.Register("AutoPopulateRows", GetType(Boolean), GetType(VerticalGridControl), New PropertyMetadata(False))
		Public Shared Shadows ReadOnly ItemsSourceProperty As DependencyProperty = DependencyProperty.Register("ItemsSource", GetType(Object), GetType(VerticalGridControl), New PropertyMetadata(Nothing, AddressOf OnItemsSourceChanged))
	End Class
	Public Class BottomIndicatorRowVisibilityConverter
		Implements IMultiValueConverter

		Public Function Convert(ByVal values() As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IMultiValueConverter.Convert
			If values.Count() < 2 Then
				Return Visibility.Collapsed
			End If
			If Not ((TypeOf values(0) Is Integer) AndAlso (TypeOf values(1) Is Integer)) Then
				Return Visibility.Collapsed
			End If
			Return If((DirectCast(values(0), Integer)) > (DirectCast(values(1), Integer)), Visibility.Visible, Visibility.Collapsed)
		End Function
		Public Function ConvertBack(ByVal value As Object, ByVal targetTypes() As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
	Public Class DefaultCellTemplateSelector
		Inherits DataTemplateSelector

		Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
			Dim row = DirectCast(DirectCast(item, EditGridCellData).RowData.Row, VerticalRow)
			Return If(row.CellTemplate IsNot Nothing, row.CellTemplate, MyBase.SelectTemplate(item, container))
		End Function
	End Class
	Public Class VerticalRow
		Inherits DependencyObject

		Public Property RowName() As String
		Public Property CellTemplate() As DataTemplate
			Get
				Return DirectCast(GetValue(CellTemplateProperty), DataTemplate)
			End Get
			Set(ByVal value As DataTemplate)
				SetValue(CellTemplateProperty, value)
			End Set
		End Property

		Public Shared ReadOnly CellTemplateProperty As DependencyProperty = DependencyProperty.Register("CellTemplate", GetType(DataTemplate), GetType(VerticalRow), New PropertyMetadata(Nothing))
		Public Shared Function FromPropertyInfo(ByVal info As PropertyInfo) As VerticalRow
			Return New VerticalRow() With {.RowName = info.Name}
		End Function
	End Class
	Public Class VerticalRowsCollection
		Inherits ObservableCollection(Of VerticalRow)

		Protected Overrides Sub InsertItem(ByVal index As Integer, ByVal item As VerticalRow)
			Dim existsIndex As Integer = IndexOf(item.RowName)
			If existsIndex > -1 Then
				If Items(existsIndex).CellTemplate IsNot Nothing Then
					Return
				End If
				Items(existsIndex).CellTemplate = item.CellTemplate
				Return
			End If
			MyBase.InsertItem(index, item)
		End Sub

		Private Overloads Function IndexOf(ByVal rowName As String) As Integer
			For i As Integer = 0 To Items.Count - 1
				If Items(i).RowName = rowName Then
					Return i
				End If
			Next i
			Return -1
		End Function
	End Class
End Namespace
