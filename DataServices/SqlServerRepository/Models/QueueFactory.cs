using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.SqlServerRepository.Models
{
    public static class QueueFactory<T>
        where T : new()
    {
        private static Queue<T> _queue = new Queue<T>();
        private static int N = 10;
        private static object locker = new object();
        static QueueFactory()
        {
            Fill();
        }

        static void Fill()
        {
            while(_queue.Count < N)
            {
                _queue.Enqueue(new T());
            }
        }
        public static T GetNew()
        {
            T t;
            if (_queue.Count < N / 2)
                N++; // Shortage
            else if (_queue.Count < N / 4)
                N += 4; // Severe Shortage
            else if (_queue.Count == 0)
                N *= 2; // Error

            lock (locker)
            {
                if (_queue.Count > 0)
                {
                    t = _queue.Dequeue();
                    Task.Run(Fill);
                    return t;
                }
            }

            t = new();
            Task.Run(Fill);
            return t;
        }
    }
}
