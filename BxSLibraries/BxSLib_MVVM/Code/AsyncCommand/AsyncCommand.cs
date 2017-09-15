namespace BxSLib_MVVM
{
	using System;
	using System.ComponentModel;
	using System.Runtime.CompilerServices;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Windows.Input;


	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	public class AsyncCommand<TResult> : AsyncCommandBase
		//, INotifyPropertyChanged
		{
			#region **[Definitions]**

				private readonly	Func<CancellationToken, Task<TResult>>	_command;
				private readonly	CancelAsyncCommand											_cancelCommand;
				private						NotifyTaskCompletion<TResult>						_execution;
				//.................................................
				//public event PropertyChangedEventHandler PropertyChanged;

			#endregion
			//___________________________________________________________________________________________
			#region **[Constructors]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public AsyncCommand(Func<CancellationToken, Task<TResult>> command)
					{
						_command				= command;
						_cancelCommand	= new CancelAsyncCommand();
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Properties]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public NotifyTaskCompletion<TResult> Execution
					{
										get { return _execution; }
						private set { if (this.SetProperty( ref _execution, value)) { } }
							//{
							//		_execution = value;
							//		OnPropertyChanged();
							//}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ICommand CancelCommand
					{
						get { return _cancelCommand; }
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods: Exposed]**


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override bool CanExecute( object parameter )
					{
						return Execution == null || Execution.IsCompleted;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override async Task ExecuteAsync( object parameter )
					{
						_cancelCommand.NotifyCommandStarting();
						Execution = new NotifyTaskCompletion<TResult>( _command(_cancelCommand.Token) );
						RaiseCanExecuteChanged();
						await Execution.TaskCompletion;
						_cancelCommand.NotifyCommandFinished();
						RaiseCanExecuteChanged();
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods: Internal]**

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
				//	{
				//		PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
				//	}

			#endregion
			//===========================================================================================
			private sealed class CancelAsyncCommand : ICommand
				{
					private CancellationTokenSource _cts = new CancellationTokenSource();
					private bool										_commandExecuting;

					public CancellationToken Token { get { return _cts.Token; } }

					public void NotifyCommandStarting()
						{
							_commandExecuting = true;
							if (!_cts.IsCancellationRequested)
									return;
							_cts = new CancellationTokenSource();
							RaiseCanExecuteChanged();
						}

					public void NotifyCommandFinished()
						{
							_commandExecuting = false;
							RaiseCanExecuteChanged();
						}

					bool ICommand.CanExecute(object parameter)
						{
							return _commandExecuting && !_cts.IsCancellationRequested;
						}

					void ICommand.Execute(object parameter)
						{
							_cts.Cancel();
							RaiseCanExecuteChanged();
						}

					public event EventHandler CanExecuteChanged
						{
							add			{ CommandManager.RequerySuggested += value; }
							remove	{ CommandManager.RequerySuggested -= value; }
						}

					private void RaiseCanExecuteChanged()
						{
							CommandManager.InvalidateRequerySuggested();
						}
				}

		}

}
