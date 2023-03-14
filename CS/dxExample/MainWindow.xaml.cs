using System.Windows;

namespace dxExample {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            grid.ItemsSource = SampleDataRow.CreateRows(10);
        }
    }
}
