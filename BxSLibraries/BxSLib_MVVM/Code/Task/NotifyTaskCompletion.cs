namespace BxSLib_MVVM
{
	using System;
	using System.ComponentModel;
	using System.Threading.Tasks;
	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	public sealed class NotifyTaskCompletion<TResult> : NotifyPropertyChangedBase
		{
			#region **[Definitions]**

 				//public event PropertyChangedEventHandler PropertyChanged;

				private TResult	_result;


			#endregion
			//___________________________________________________________________________________________
			#region **[Constructors]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
 				public NotifyTaskCompletion(Task<TResult> task)
					{
   					this.Task = task;
   					if (!task.IsCompleted)
							{
								this.TaskCompletion = WatchTaskAsync();
							}
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Properties]**

 				public Task<TResult>	Task		{ get; private set; }
 				//public TResult				Result	{ get { return	(this.Task.Status == TaskStatus.RanToCompletion) ? this.Task.Result : default(TResult); } }
 				public TaskStatus			Status	{ get { return	this.Task.Status; } }
				//.................................................
				public Task	TaskCompletion	{ get; private set; }
				//.................................................
 				public bool IsCompleted			{ get { return	this.Task.IsCompleted; } }
 				public bool IsNotCompleted	{ get { return	!this.Task.IsCompleted; } }
 				//public bool	IsSuccessful		{ get { return	this.Task.Status == TaskStatus.RanToCompletion; } }
 				public bool	IsCanceled			{ get { return	this.Task.IsCanceled; } }
 				public bool	IsFaulted				{ get { return	this.Task.IsFaulted; } }
				//.................................................
 				public AggregateException	Exception				{ get { return	this.Task.Exception; } }
 				public Exception					InnerException	{ get { return	(Exception == null) ?	null : Exception.InnerException; } }
				//.................................................
 				public string	ErrorMessage { get { return	(InnerException == null) ? null : InnerException.Message; } }

				private bool _isSuccessful;

		 		public bool	IsSuccessful
					{
										get { return	_isSuccessful; }
						private set { if (this.SetProperty(ref _isSuccessful, value)) { } }
					}

 				public TResult	Result
					{
										get { return	_result; }
						private set { if (SetProperty( ref _result, value)) { }; }
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods: Private]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async Task WatchTaskAsync()
					{
   					try		{	await this.Task; }
   					catch	{	}
						//.............................................
   					//var propertyChanged = PropertyChanged;
   					//if (propertyChanged == null)	return;

   					//propertyChanged(this, new PropertyChangedEventArgs("Status"));
   					//propertyChanged(this, new PropertyChangedEventArgs("IsCompleted"));
   					//propertyChanged(this, new PropertyChangedEventArgs("IsNotCompleted"));

   					if (this.Task.IsCanceled)
							{
     						//propertyChanged(this, new PropertyChangedEventArgs("IsCanceled"));
							}
   					else if (this.Task.IsFaulted)
							{
     						//propertyChanged(this, new PropertyChangedEventArgs("IsFaulted"));
     						//propertyChanged(this, new PropertyChangedEventArgs("Exception"));
     						//propertyChanged(this,	new PropertyChangedEventArgs("InnerException"));
     						//propertyChanged(this, new PropertyChangedEventArgs("ErrorMessage"));
							}	
   					else
							{
								if (this.Task.Status == TaskStatus.RanToCompletion)
									{
										this.Result	= this.Task.Result;
										this.IsSuccessful	= true;
									}
								else
									{
										this.Result	= default(TResult);
										this.IsSuccessful	= false;
									}
     						//propertyChanged(this,	new PropertyChangedEventArgs("IsSuccessful"));
     						//propertyChanged(this, new PropertyChangedEventArgs("Result"));
							}
					}

			#endregion

		}
}


