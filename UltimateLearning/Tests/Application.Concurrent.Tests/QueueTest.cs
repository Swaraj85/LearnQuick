using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Concurrent.Tests
{
    [TestClass]
    public class QueueTest
    {
        [TestMethod]
        public void Check_Simple_Queue()
        {
            var orders = new Queue<string>();
            PlaceOrder(orders, "Swaraj");
            PlaceOrder(orders, "Cutie");

            foreach (var order in orders)
            {
                Console.WriteLine(order);
            }
        }

        [TestMethod]
        public void Check_Simple_Multithreaded_Queue()
        {
            var orders = new ConcurrentQueue<string>();
            var task1 = Task.Run(() => PlaceOrder(orders, "Swaraj"));
            var task2 = Task.Run(() => PlaceOrder(orders, "Cutie"));

            Task.WaitAll(task1, task2);

            foreach (var order in orders)
            {
                Console.WriteLine(order);
            }
        }

        private void PlaceOrder(Queue<string> orders, string customerName)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1);
                orders.Enqueue($"{customerName} wants {i + 1} shirts");
            }
        }

        private void PlaceOrder(ConcurrentQueue<string> orders, string customerName)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1);
                orders.Enqueue($"{customerName} wants {i + 1} shirts");
            }
        }
    }
}
