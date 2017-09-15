namespace BxSLib_UT_MVVMApp
{
	using System.Windows;
	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	public partial class MainWindow : Window
		{
			public MainWindow()
				{
					DataContext	= new MainWindowVM();
					InitializeComponent();
				}
		}
}
