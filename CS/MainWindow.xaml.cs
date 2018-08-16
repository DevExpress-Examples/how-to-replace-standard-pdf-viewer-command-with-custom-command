using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.PdfViewer;

namespace DXSample {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
    }

    public class CustomPdfCommandProvider : PdfCommandProvider {

        protected override ICommand NextPageCommandInternal {
            get {
                return new CustomNextPageCommand(base.NextPageCommandInternal);
            }
        }

        protected override ICommand PreviousPageCommandInternal {
            get {
                return new CustomPreviousPageCommand(base.PreviousPageCommandInternal);
            }
        }
    }
}
