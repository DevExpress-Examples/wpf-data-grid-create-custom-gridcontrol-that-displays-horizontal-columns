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
