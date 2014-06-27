using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ThreadManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            foreach (Thread item in TManager.DctThread.Values)
            {
                item.Resume();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            foreach (Thread item in TManager.DctThread.Values)
            {
                item.Suspend();
            }
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            new TManager(10, 10);

            for (int i = 0; i < TManager.ThreadSize; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback((b) =>
                {
                    TManager.AddThread(Thread.CurrentThread);
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(10000); // 操作
                    Console.WriteLine("Second:" + Thread.CurrentThread.ManagedThreadId);
                }),
                    null);
            }
        }
    }

    public class TManager
    {
        private static IDictionary<string, Thread> _dctThread;

        private static object _objectLock = new object();

        public static int ThreadSize { get; set; }

        static TManager()
        {
            _dctThread = new Dictionary<string, Thread>();
        }


        public static IDictionary<string, Thread> DctThread
        {
            get
            {
                return _dctThread;
            }
        }

        public TManager(int size, int maxSize)
        {
            ThreadSize = size;
            ThreadPool.SetMaxThreads(maxSize, maxSize);
        }


        public static void AddThread(Thread thread)
        {
            lock (_objectLock)
            {
                if (!_dctThread.ContainsKey(thread.ManagedThreadId.ToString()))
                {
                    _dctThread.Add(thread.ManagedThreadId.ToString(), thread);
                }
            }
        }
    }
}
