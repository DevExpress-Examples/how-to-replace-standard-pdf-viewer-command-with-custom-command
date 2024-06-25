<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/144967341/17.2.6%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T830541)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
#  How to replace a standard PDF Viewer control command with a custom command

This example shows how to modify the existing PDF Viewer command functionality to zoom to a custom zoom factor.

All commands in the PDF Viewer are created using a command provider represented by the <a href="https://documentation.devexpress.com/WPF/DevExpress.Xpf.PdfViewer.PdfCommandProvider.class">PdfCommandProvider</a>  class. You can substitute the default command provider with its descendant to create a custom command in place of the default command.

Follow the steps below.

1. Create a custom command provider class inherited from the <a href="https://documentation.devexpress.com/WPF/DevExpress.Xpf.PdfViewer.PdfCommandProvider.class">PdfCommandProvider</a>  class. You need to override the required members (e.g., the **ZoomInCommandInternal** property) to create an instance of the **DelegateCommand** class.  

The delegate command constructor accepts **ZoomIn** and **CanZoomIn** delegates. The delegate command calls these delegates when the command's logic is invoked.

2. Implement the command logic in the **ZoomIn** and **CanZoomIn** methods.

3. Use the created **CustomPdfCommandProvider** to substitute the default command provider.

See the following files for implementation details:

CS | VB
------------ | -------------
[MainWindow.xaml](./CS/MainWindow.xaml) | [MainWindow.xaml](./VB/MainWindow.xaml)
[MainWindow.xaml.cs](./CS/MainWindow.xaml.cs) | [MainWindow.xaml.vb](./VB/MainWindow.xaml.vb)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=how-to-replace-standard-pdf-viewer-command-with-custom-command&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=how-to-replace-standard-pdf-viewer-command-with-custom-command&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
