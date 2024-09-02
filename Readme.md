<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128649200/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4630)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# WPF Data Grid - Create a Custom GridControl that Displays Horizontal Columns

This example demonstrates how to create a [GridControl](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.GridControl) class descendant with horizontally-oriented columns (similar to the [WinForms Vertical Grid Control](https://docs.devexpress.com/WindowsForms/2449/controls-and-libraries/vertical-grid)).

![image](https://user-images.githubusercontent.com/65009440/225031485-ffb424e3-7b38-4f32-9870-5a1f11cecd67.png)

This functionality is not available out of the box, so we used [unbound columns](https://docs.devexpress.com/WPF/6124/controls-and-libraries/data-grid/grid-view-data-layout/columns-and-card-fields/unbound-columns) to get/set cell values and customized row appearance ([Modify Theme Resources](https://docs.devexpress.com/WPF/403598/common-concepts/themes/customize-devexpress-theme-resources)).

If you want to use this custom solution in your real application, first check if you can implement one of the following solutions instead:

1. Change the structure of your `ItemsSource` collection to use the regular [Data Grid](https://docs.devexpress.com/WPF/6084/controls-and-libraries/data-grid).
2. If you wish to show properties of a single object, use the [Property Grid](https://docs.devexpress.com/WPF/15640/controls-and-libraries/property-grid).

## Files to Review

* [VerticalGridControl.cs](./CS/dxExample/VGrid/VerticalGridControl.cs) (VB: [VerticalGridControl.vb](./VB/dxExample/VGrid/VerticalGridControl.vb))
* [VerticalGridControlResources.xaml](./CS/dxExample/VGrid/VerticalGridControlResources.xaml) (VB: [VerticalGridControlResources.xaml](./VB/dxExample/VGrid/VerticalGridControlResources.xaml))
* [MainWindow.xaml](./CS/dxExample/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/dxExample/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/dxExample/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/dxExample/MainWindow.xaml.vb))
* [SampleDataRow.cs](./CS/dxExample/SampleDataRow.cs) (VB: [SampleDataRow.vb](./VB/dxExample/SampleDataRow.vb))

## Documentation

* [Modify Theme Resources](https://docs.devexpress.com/WPF/403598/common-concepts/themes/customize-devexpress-theme-resources)
* [Unbound Columns](https://docs.devexpress.com/WPF/6124/controls-and-libraries/data-grid/grid-view-data-layout/columns-and-card-fields/unbound-columns)
* [Assign Editors to Cells](https://docs.devexpress.com/WPF/401011/controls-and-libraries/data-grid/data-editing-and-validation/modify-cell-values/assign-an-editor-to-a-cell)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-data-grid-create-custom-gridcontrol-that-displays-horizontal-columns&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-data-grid-create-custom-gridcontrol-that-displays-horizontal-columns&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
