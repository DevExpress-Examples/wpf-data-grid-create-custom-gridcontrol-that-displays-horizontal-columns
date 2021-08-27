<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128649200/16.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4630)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/dxExample/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/dxExample/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/dxExample/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/dxExample/MainWindow.xaml.vb))
* [SampleDataRow.cs](./CS/dxExample/SampleDataRow.cs) (VB: [SampleDataRow.vb](./VB/dxExample/SampleDataRow.vb))
* [VerticalGridControl.cs](./CS/dxExample/VGrid/VerticalGridControl.cs) (VB: [VerticalGridControl.vb](./VB/dxExample/VGrid/VerticalGridControl.vb))
* [VerticalGridControlResources.xaml](./CS/dxExample/VGrid/VerticalGridControlResources.xaml) (VB: [VerticalGridControlResources.xaml](./VB/dxExample/VGrid/VerticalGridControlResources.xaml))
<!-- default file list end -->
# How to create a custom GridControl that represents columns horizontally in a way similar to the WinForms VerticalGrid control

This example demonstrates how to create a `GridControl` class descendant with horizontally oriented columns. Please note that this functionality is not available out of the box, so we used [unbound columns](https://docs.devexpress.com/WPF/6124/controls-and-libraries/data-grid/grid-view-data-layout/columns-and-card-fields/unbound-columns) to get/set cell values and customized row appearance (see: [T361488](https://www.devexpress.com/Support/Center/p/T361488.aspx)).

Before using this custom solution in your real application, please check if you can use one of the following solutions instead:

1. Change the structure of your `ItemsSource` collection so that you can use the regular`GridControl`.
2. If you wish to show properties of a single object, use the [Property Grid](https://docs.devexpress.com/WPF/15640/controls-and-libraries/property-grid).

