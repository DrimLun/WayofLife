﻿//using Android.App;
//using Android.Content;
//using Android.OS;
//using OperationCanceledException = Android.OS.OperationCanceledException;

//namespace WayofLife.Platforms.Android.Resources
//{
//    [Service]
//    internal class AndroidExpiryService : Service
//    {
//        CancellationTokenSource _cts;
//        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10001;

//        public override IBinder OnBind(Intent intent)
//        {
//            return null;
//        }

//        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
//        {
//            _cts = new CancellationTokenSource();

//            Notification notification = new NotificationHelper().GetServiceStartedNotification();
//            StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);

//            Task.Run(() =>
//            {
//                try
//                {
//                    //var locShared = new GetLocationService();
//                    //locShared.Run(_cts.Token).Wait();
//                }
//                catch (OperationCanceledException)
//                {
//                }
//                finally
//                {
//                    if (_cts.IsCancellationRequested)
//                    {
//                        //var message = new StopServiceMessage();
//                    }
//                }
//            }, _cts.Token);

//            return StartCommandResult.Sticky;
//        }

//        public override void OnDestroy()
//        {
//            if (_cts != null)
//            {
//                _cts.Token.ThrowIfCancellationRequested();
//                _cts.Cancel();
//            }
//            base.OnDestroy();
//        }
//    }
//}
