namespace BxSLib_MVVM
{
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Runtime.CompilerServices;
	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
		{

			#region **[INotifyPropertyChanged Members]**

				public event PropertyChangedEventHandler PropertyChanged;

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods: Internal]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected bool SetProperty<T>(								ref		T				storage	,
																														T				value		,
																				[CallerMemberName]	string	propertyName = null)
					{
						if (EqualityComparer<T>.Default.Equals(storage, value)) return false;
						storage = value;
						PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
						return true;
					}

			#endregion

		}
}
