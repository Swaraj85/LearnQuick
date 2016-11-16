using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AsyncLibrary
{
    public class TaskDemo
    {
        public TaskDemo() { }

        public string SimpleUsageOfTaskContinueWith(int timeToSleep)
        {
            //This method demonstrates the usage of "continue"
            var taskForLoadingmarketData = Task.Run(() =>
            {
                Console.WriteLine("Going to load data...");
                Thread.Sleep(timeToSleep);
                Console.WriteLine("Data loaded");
                return "login";
            });

            taskForLoadingmarketData.ContinueWith(t =>
            {
                Console.WriteLine("Continue with started...");
            });

            return taskForLoadingmarketData.Result;
        }

        public string SimpleUsageOfTaskContinueWithAndException(int timeToSleep, 
            Action<string> actionToExecute)
        {
            //This method demonstrates the usage of "continue"
            var taskForLoadingmarketData = Task.Run(() =>
            {
                Console.WriteLine("Going to load data...");
                Thread.Sleep(timeToSleep);
                actionToExecute("ss");
                Console.WriteLine("Data loaded");
                return "Login";
            });

            taskForLoadingmarketData.ContinueWith(t =>
            {
                /* "IsFaulted" is true if exception is thrown before entering the continuation
                 */
                if (!t.IsFaulted)
                    Console.WriteLine("Continue with started...");
            });

            return taskForLoadingmarketData.Result;
        }
    }
}
