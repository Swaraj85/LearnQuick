using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleBootstrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskCompletionSource<int> tcs= new TaskCompletionSource<int>();

            try
            {
                WaitForAllToCompleteAsync().ContinueWith(t =>
                {
                    if(t.IsFaulted)
                        Console.WriteLine(t.Exception);
                });
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.InnerException);
            }
            
            //t4.ContinueWith(t =>
            //{
            //    Console.WriteLine($"Status:{t.Status} Cancelled:{t.IsCanceled} Completed:{t.IsCompleted}");
            //});

            Console.WriteLine($"Waiting on the main Thread: {Thread.CurrentThread.ManagedThreadId}");
            Console.ReadLine();
        }

        private static async Task WaitForAllToCompleteAsync()
        {
            Task t1 = Task.Run(() => { Thread.Sleep(1000); });
            Task t2 = Task.Run(() => { Thread.Sleep(2000); throw new AccessViolationException();});
            Task t3 = Task.Run(() => { Thread.Sleep(3000); throw new NullReferenceException();});
            
            //await Task.WhenAll(t1, t2, t3);

            List<Exception> excep= new List<Exception>();
            await Task.Factory.ContinueWhenAll( new List<Task>() { t1, t2, t3 }.ToArray(), (t) =>
            {
                foreach (var task in t)
                {
                    if(task.IsFaulted)
                        excep.Add(task.Exception.InnerException);
                }
            } );
            if(excep.Count>0)
                throw new AggregateException(excep);
        }
    }
}
