namespace BxSLib_MVVM
{
	using System.Runtime.CompilerServices;
	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	public abstract class ViewModelBase : NotifyPropertyChangedBase
		//INotifyPropertyChanged
		{

			#region **[Definitions]**
					
				protected string	cc_myName;

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods: Internal]**

				protected void SetName([CallerMemberName] string CallerName = null)
					{
						this.cc_myName = CallerName;
					}

			#endregion

		}
}
