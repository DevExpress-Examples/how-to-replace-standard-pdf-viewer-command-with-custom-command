#  How to replace a standard PDF Viewer control command with a custom command

This example shows how to modify the existing PDF Viewer **ZoomIn**/**ZoomOut** command functionality to zoom to a custom zoom factor.

All commands in the PDF Viewer are created using a command provider represented by the <a href="https://documentation.devexpress.com/WPF/DevExpress.Xpf.PdfViewer.PdfCommandProvider.class">PdfCommandProvider</a>  class. You can substitute the default command provider with its descendant to create a custom command in place of the default command.

Follow the steps below.

1. Create a custom command provider class inherited from the <a href="https://documentation.devexpress.com/WPF/DevExpress.Xpf.PdfViewer.PdfCommandProvider.class">PdfCommandProvider</a>  class. You need to override the required members (e.g., the **ZoomInCommandInternal** property) to create an instance of the **DelegateCommand** class.  

The delegate command constructor accepts **Execute** and **CanExecute** delegates (in this example **ZoomIn** and **CanZoomIn** delegates). The delegate command calls these delegates when the command's logic is invoked.

2. Implement the command logic in the **Execute** and **CanExecute** methods (in this example **ZoomIn** and **CanZoomIn** methods).

3. Use the created **CustomPdfCommandProvider** to substitute the default command provider.

See the following files for implementation details:

CS | VB
------------ | -------------
[MainWindow.xaml](./CS/MainWindow.xaml) | [MainWindow.xaml](./VB/MainWindow.xaml)
[MainWindow.xaml.cs](./CS/MainWindow.xaml.cs) | [MainWindow.xaml.vb](./VB/MainWindow.xaml.vb)
