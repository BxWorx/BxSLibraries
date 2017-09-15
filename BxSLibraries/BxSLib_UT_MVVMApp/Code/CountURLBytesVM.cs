namespace BxSLib_UT_MVVMApp
{
	using System.Windows.Input;
	using BxSLib_MVVM;
	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	internal sealed class CountUrlBytesVM
		{
			internal CountUrlBytesVM(MainWindowVM parent, string url, IAsyncCommand command)
				{
					this.LoadingMessage	= $"Loading ({ url })...";
					this.Command				= command;
					this.RemoveCommand	= new DelegateCommand(() => parent.Operations.Remove(this));
				}

			public string					LoadingMessage	{ get; private set; }

			public IAsyncCommand	Command					{ get; private set; }

			public ICommand				RemoveCommand		{ get; private set; }

		}

}
