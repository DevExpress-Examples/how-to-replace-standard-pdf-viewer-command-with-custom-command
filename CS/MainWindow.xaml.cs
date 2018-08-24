using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Xpf.DocumentViewer;
using DevExpress.Xpf.PdfViewer;

namespace DXSample {
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
        }
    }

    public class CustomPdfCommandProvider : PdfCommandProvider {
        readonly List<double> factors = new List<double> { 0.15, 0.3, 0.45, 1, 1.25, 1.5, 2, 5 };
        public PdfViewerControl Control { get { return DocumentViewer as PdfViewerControl; } }

        ICommand zoomInCommandInternal;
        protected override ICommand ZoomInCommandInternal {
            get { return zoomInCommandInternal ?? (zoomInCommandInternal = new DelegateCommand(ZoomIn, CanZoomIn)); }
        }

        ICommand zoomOutCommandInternal;
        protected override ICommand ZoomOutCommandInternal {
            get { return zoomOutCommandInternal ?? (zoomOutCommandInternal = new DelegateCommand(ZoomOut, CanZoomOut)); }
        }

        void ZoomIn() {
            if (Control == null) return;

            foreach (var zoomFactor in factors) {
                if (Control.ZoomFactor < zoomFactor) {
                    Control.ZoomFactor = zoomFactor;
                    break;
                }
            }
        }

        bool CanZoomIn() {
            return Control != null && Control.Document != null && Control.ZoomFactor < factors[factors.Count - 1];
        }

        void ZoomOut() {
            if (Control == null) return;

            for (int i = factors.Count - 1; i >= 0; i--) {
                if (factors[i] < Control.ZoomFactor) {
                    Control.ZoomFactor = factors[i];
                    break;
                }
            }
        }

        bool CanZoomOut() {
            return Control != null && Control.Document != null && Control.ZoomFactor > factors[0];
        }

        protected override ICommand CreateZoomModeAndZoomFactorItem(string dllName) {

            var items = CreateZoomModeAndFactorsItems();
            CommandCheckItems setZoomModeAndFactor = new CommandCheckItems {
                Caption = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandZoomCaption),
                Hint = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandZoomDescription),
                Group = DocumentViewerLocalizer.GetString(DocumentViewerStringId.ZoomRibbonGroupCaption),
                Command = new DelegateCommand(() => { }, () => items.Any(x => x.CanExecute(null))),
                Items = items,
                SmallGlyph = UriHelper.GetUri(dllName, @"\Images\Zoom_16x16.png"),
                LargeGlyph = UriHelper.GetUri(dllName, @"\Images\Zoom_32x32.png"),
            };
            return setZoomModeAndFactor;
        }

        ObservableCollection<CommandToggleButton> CreateZoomModeAndFactorsItems() {

            ObservableCollection<CommandToggleButton> zoomModeAndFactorsItems = new ObservableCollection<CommandToggleButton>();

            DelegateCommand<double> setZoomFactorCommand = new DelegateCommand<double>(x => {
                SetZoomFactorCommandInternal.Execute(x);
                UpdateZoomCommand();
            }, x => SetZoomFactorCommandInternal.CanExecute(x));


            zoomModeAndFactorsItems.Add(new CommandSetZoomFactorAndModeItem {
                Caption = "15%",
                Command = new CommandWrapper(() => setZoomFactorCommand),
                ZoomFactor = 0.15,
                GroupIndex = 1
            });

            zoomModeAndFactorsItems.Add(new CommandSetZoomFactorAndModeItem {
                Caption = "30%",
                Command = new CommandWrapper(() => (setZoomFactorCommand)),
                ZoomFactor = 0.3,
                GroupIndex = 1
            });

            zoomModeAndFactorsItems.Add(new CommandSetZoomFactorAndModeItem {
                Caption = "45%",
                Command = new CommandWrapper(() => setZoomFactorCommand),
                ZoomFactor = 0.45,
                GroupIndex = 1
            });

            zoomModeAndFactorsItems.Add(new CommandSetZoomFactorAndModeItem {
                Caption = "100%",
                Command = new CommandWrapper(() => setZoomFactorCommand),
                ZoomFactor = 1,
                GroupIndex = 1
            });

            zoomModeAndFactorsItems.Add(new CommandSetZoomFactorAndModeItem {
                Caption = "125%",
                Command = new CommandWrapper(() => setZoomFactorCommand),
                ZoomFactor = 1.25,
                GroupIndex = 1
            });

            zoomModeAndFactorsItems.Add(new CommandSetZoomFactorAndModeItem {
                Caption = "150%",
                Command = new CommandWrapper(() => setZoomFactorCommand),
                ZoomFactor = 1.5,
                GroupIndex = 1
            });

            zoomModeAndFactorsItems.Add(new CommandSetZoomFactorAndModeItem {
                Caption = "200%",
                Command = new CommandWrapper(() => setZoomFactorCommand),
                ZoomFactor = 2,
                GroupIndex = 1
            });

            zoomModeAndFactorsItems.Add(new CommandSetZoomFactorAndModeItem {
                Caption = "500%",
                Command = new CommandWrapper(() => setZoomFactorCommand),
                ZoomFactor = 5,
                GroupIndex = 1
            });
            return zoomModeAndFactorsItems;
        }

        void UpdateZoomCommand() {
            CommandCheckItems zoomCommand = ZoomCommand as CommandCheckItems;
            if (zoomCommand == null) return;
            zoomCommand.UpdateCheckState(UpdateZoomFactorCheckState);
        }
    }
}


