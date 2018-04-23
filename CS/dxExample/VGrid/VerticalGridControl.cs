using DevExpress.Data;
using DevExpress.Xpf.Grid;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace dxExample.VGrid
{
    public class VerticalGridControl : GridControl
    {
        public VerticalRowsCollection Rows { get; set; }
        public bool AutoPopulateRows
        {
            get { return (bool)GetValue(AutoPopulateRowsProperty); }
            set { SetValue(AutoPopulateRowsProperty, value); }
        }
        public new object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public VerticalGridControl()
        {
            CustomUnboundColumnData += OnCustomUnboundColumnData;
            InitializeRowsCollection();
        }
        void InitializeRowsCollection()
        {
            Rows = new VerticalRowsCollection();
            Rows.CollectionChanged += OnRowsCollectionChanged;
        }
        void UpdateGridColumns()
        {
            ICollection itemsSource = ItemsSource as ICollection;
            if (itemsSource == null)
            {
                Columns.Clear();
                return;
            }
            int columnsCount = itemsSource.Count;
            if (Columns.Count == columnsCount) return;
            Columns.BeginUpdate();
            int delta = columnsCount - Columns.Count;
            if (columnsCount > Columns.Count)
            {
                for (int i = Columns.Count; i < columnsCount; i++)
                {
                    Columns.Add(new GridColumn() { FieldName = i.ToString(), UnboundType = UnboundColumnType.Object });
                }
            }
            else
            {
                for (int i = Columns.Count - 1; i >= columnsCount; i--)
                {
                    Columns.RemoveAt(i);
                }
            }
            Columns.EndUpdate();
        }
        void UpdateItemsSourceCollectionChangedSubscription(object oldSource, object newSource)
        {
            if(oldSource is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)oldSource).CollectionChanged -= OnItemsSourceCollectionChanged;
            }
            if (newSource is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)newSource).CollectionChanged += OnItemsSourceCollectionChanged;
            }
        }
        void PopulateRows()
        {
            if (!(ItemsSource is IEnumerable)) return;
            var firstItem = ((IEnumerable)ItemsSource).Cast<object>().FirstOrDefault();
            if (firstItem == null) return;
            var newRows = TypeDescriptor.GetProperties(firstItem)
                .Cast<PropertyDescriptor>()
                .Select(p => new VerticalRow() { RowName = p.Name });
            foreach (var row in newRows)
            {
                Rows.Add(row);
            }
        }
        void OnRowsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.ItemsSource = Rows;
        }
        static void OnItemsSourceChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var grid = (VerticalGridControl)sender;
            grid.UpdateGridColumns();
            if (grid.AutoPopulateRows)
            {
                grid.PopulateRows();
            }
            grid.UpdateItemsSourceCollectionChangedSubscription(e.OldValue, e.NewValue);
        }
        void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Remove)
            {
                UpdateGridColumns();
            }
        }
        void OnCustomUnboundColumnData(object sender, GridColumnDataEventArgs e)
        {
            var itemsSource = ItemsSource as IList;
            if (itemsSource == null) return;
            var row = Rows[e.ListSourceRowIndex];
            var item = itemsSource[Convert.ToInt32(e.Column.FieldName)];
            var targetProperty = TypeDescriptor.GetProperties(item).Cast<PropertyDescriptor>().FirstOrDefault(p => p.Name == row.RowName);
            if (targetProperty == null) return;
            if (e.IsGetData)
            {
                e.Value = targetProperty.GetValue(item);
            }
            if (e.IsSetData)
            {
                targetProperty.SetValue(item, e.Value);
            }
        }

        public static readonly DependencyProperty AutoPopulateRowsProperty = DependencyProperty.Register("AutoPopulateRows", typeof(bool), typeof(VerticalGridControl), new PropertyMetadata(false));
        public static new readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(object), typeof(VerticalGridControl), new PropertyMetadata(null, OnItemsSourceChanged));
    }
    public class BottomIndicatorRowVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Count() < 2)
                return Visibility.Collapsed;
            if (!((values[0] is int) && (values[1] is int)))
                return Visibility.Collapsed;
            return ((int)values[0]) > ((int)values[1]) ? Visibility.Visible : Visibility.Collapsed;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class DefaultCellTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var row = (VerticalRow)((EditGridCellData)item).RowData.Row;
            return (row.CellTemplate != null) ? row.CellTemplate : base.SelectTemplate(item, container);
        }
    }
    public class VerticalRow : DependencyObject
    {
        public string RowName { get; set; }
        public DataTemplate CellTemplate
        {
            get { return (DataTemplate)GetValue(CellTemplateProperty); }
            set { SetValue(CellTemplateProperty, value); }
        }

        public static readonly DependencyProperty CellTemplateProperty = DependencyProperty.Register("CellTemplate", typeof(DataTemplate), typeof(VerticalRow), new PropertyMetadata(null));
        public static VerticalRow FromPropertyInfo(PropertyInfo info)
        {
            return new VerticalRow() { RowName = info.Name };
        }
    }
    public class VerticalRowsCollection : ObservableCollection<VerticalRow>
    {
        protected override void InsertItem(int index, VerticalRow item)
        {
            int existsIndex = IndexOf(item.RowName);
            if (existsIndex > -1)
            {
                if (Items[existsIndex].CellTemplate != null) return;
                Items[existsIndex].CellTemplate = item.CellTemplate;
                return;
            }
            base.InsertItem(index, item);
        }

        int IndexOf(string rowName)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].RowName == rowName) return i;
            }
            return -1;
        }
    }
}
