// Developer Express Code Central Example:
// How to create a custom GridControl that represents columns horizontally in a way similar to the WinForms VerticalGrid control
// 
// This example demonstrates how to create a GridControl descendant with
// horizontally oriented columns.
// To use it, simply add the
// VerticalGridControl.xaml and VerticalGridControl.xaml.cs files in your
// project.
// 
// In addition, this example demonstrates how to customize grid cells
// using data templates.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E4630

using System;
using System.Collections.Generic;
using System.Windows;

namespace dxExample {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        void OnAddButtonClick(object sender, RoutedEventArgs e) {
            grid.ItemsSource = SampleDataRow.CreateRows(10);
        }
    }
}
