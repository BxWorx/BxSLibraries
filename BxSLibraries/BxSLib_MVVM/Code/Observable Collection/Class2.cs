using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BxSLib_MVVM.Code.Observable_Collection
	{
	class Class2
		{
		}
	}


//[Export]
//		public class LogFeedViewModel : BaseViewModel
//		{
//				[Import]
//				private ILoggingService _logger;
 
//				public LogFeedViewModel()
//				{
//				}
 
//				private ObservableCollection<Log> _logs;
//				public ObservableCollection<Log> Logs
//				{
//						get
//						{
//								if (_logs == null)
//								{
//										_logs = new ObservableCollection<Log>();
 
//										_logger
//												.LogFeed
//												.ObserveOnDispatcher()
//												.Subscribe(l =>
//												{
//														Logs.Add(l);
//												});
//								}
 
//								return _logFeed;
//						}
//				}
 
//		public interface ILoggingService
//		{
//				IObservable<Log> LogFeed { get; }
 
//				void Log(Log log);
 
//				void Log(string message, LogSeverity severity, Exception exception); 
//		}
//public class Log
//	{
//	}