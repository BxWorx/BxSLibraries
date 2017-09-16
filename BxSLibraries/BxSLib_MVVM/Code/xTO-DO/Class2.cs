using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BxSLib_MVVM.Code
	{
	class Class2
		{
		}
	}

//=================================================================================================

//static AutoResetEvent MeasureMessageEvent = new AutoResetEvent(true);

//private static void HandleMeasurementMessage(IMessage<MessageEnvelope> msg)
//{
//    /* Do a bunch of stuff to msg */

//    // Wait for exclusive access
//    MeasureMessageEvent.WaitOne();

//    EventHubDataBatch.Push(eventHubDatum);

//    if(EventHubDataBatch.Count == 100)
//    {
//       /* Send them off*/
//       EventHubDatabatch.Clear();
//    }

//    // Release exclusive access
//    MeasureMessageEvent.Set();
//}

//=================================================================================================

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