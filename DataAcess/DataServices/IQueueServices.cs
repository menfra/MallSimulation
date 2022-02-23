using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcess.DataServices
{
    interface IQueueServices
    {
        /// <summary>
        /// This method consumes messages inside a queue.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Consume<T>();

        /// <summary>
        /// This method produces messages to a queue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tObject"></param>
        void Produce<T>(T tObject);
    }
}
