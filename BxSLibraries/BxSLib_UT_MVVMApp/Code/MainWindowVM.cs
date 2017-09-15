namespace BxSLib_UT_MVVMApp
{
	using BxSLib_MVVM;
	using System.Collections.ObjectModel;
	using System.Windows.Input;
	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	internal class MainWindowVM : ViewModelBase
		{

			private string _url;

			public MainWindowVM()
				{
					Url										= "http://www.example.com/";
					Operations						= new ObservableCollection<CountUrlBytesVM>();
					CountUrlBytesCommand	= new DelegateCommand(
						() =>
							{
								var countBytes	= AsyncCommand.Create(token => StaticService.CountBytesInUrlAsync(Url, token));
								var operaton		=	new CountUrlBytesVM(this, Url, countBytes);
								//.........................................
								countBytes.Execute(null);
								Operations.Add(operaton);
							}
						);
				}

			public NotifyTaskCompletion<int> UrlByteCount
				{
					get;
					private set;
				}

			public string Url
				{
						get { return _url; }
						set	{ if ( this.SetProperty( ref _url, value ) ) { }; }
				}

			public ObservableCollection<CountUrlBytesVM>	Operations						{ get; private set; }
			public ICommand																CountUrlBytesCommand	{ get; private set; }

		}
}
