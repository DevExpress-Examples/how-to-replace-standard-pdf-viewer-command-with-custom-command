#  How to replace a standard PDF Viewer control command with a custom command

This example shows how to modify the existing PDF Viewer command functionality.

All commands in the PDF Viewer are created using a command provider represented by the <a href="https://documentation.devexpress.com/WPF/DevExpress.Xpf.PdfViewer.PdfCommandProvider.class">PdfCommandProvider</a>  class. You can substitute the default command provider with its descendant to create a custom command in place of the default command.

Follow the steps below.

1. Create a custom command class (e.g., **CustomNextPageCommand**) that implements the **ICommand** interface. This interface exposes two methods: **CanExecute**, and **Execute**, and the **CanExecuteChanged** event that you need to implement.

2. Create a custom command provider class inherited from the <a href="https://documentation.devexpress.com/WPF/DevExpress.Xpf.PdfViewer.PdfCommandProvider.class">PdfCommandProvider</a>  class. You need to override the required members (e.g., the **NextPageCommandInternal** property) to create an instance of the custom command class. So, instead of the default command, the custom command will be used by PdfViewerControl. 

3. Use the created **CustomPdfCommandProvider** to substitute the default command provider, for example, to add the built-in NextPage item to PdfViewerControl's Ribbon.

<br/>
