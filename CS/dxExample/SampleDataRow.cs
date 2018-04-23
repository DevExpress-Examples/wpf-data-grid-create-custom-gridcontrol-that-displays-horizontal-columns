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

namespace dxExample {
    class SampleDataRow {
        public int Id { get; set; }
        public string Name {get; set;}
        public DateTime Date { get; set; }
        public bool HasFlag {get; set;}

        public static IList<SampleDataRow> CreateRows(int rowCount) {
            IList<SampleDataRow> result = new List<SampleDataRow>();
            for(int i = 0; i < rowCount; i++) {
                result.Add(new SampleDataRow() {
                    Id = i,
                    Name = String.Format("name {0}", i % 3),
                    Date = DateTime.Now.AddDays(i),
                    HasFlag = (i % 3 == 0),
                });
            }
            return result;
        }
    }
}
