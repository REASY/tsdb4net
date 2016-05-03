using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public class Tsdb
    {
        private readonly int MAX_DEGREE = 51;
        private IBPlusTree<long, InternalMeasurement>[] _trees;
        private ConcurrentQueue<Measurement> _queue;
        private ManualResetEventSlim _hasAnyMeasurementEvent;
        private Task _treeFiller;
        private CancellationTokenSource _treeFillerCancellation;
        private bool _isStarted = false;

        public int NumOfSensors { get; private set; }

        public Tsdb(int numOfSensors)
        {
            if (numOfSensors <= 0)
                throw new ArgumentOutOfRangeException("numOfSensors", numOfSensors, "must be > 0");
            NumOfSensors = numOfSensors;
        }
        public void Initialize()
        {
            _trees = new IBPlusTree<long, InternalMeasurement>[NumOfSensors];
            //Parallel.For(0, NumOfSensors, new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount }, (i) =>
            //{
            //for (int i = 0; i < NumOfSensors; i++)
            //{
            //    _trees[i] = new BPlusTreeRW<long, InternalMeasurement>(MAX_DEGREE);
            //}
            //});
            _queue = new ConcurrentQueue<Measurement>();
            _hasAnyMeasurementEvent = new ManualResetEventSlim(false);
            _treeFillerCancellation = new CancellationTokenSource();
        }
        public void Start()
        {
            if (_isStarted) return;
            _treeFiller = Task.Factory.StartNew(() => TreeFiller());
            _isStarted = true;
        }
        public void Stop()
        {
            if (!_isStarted) return;
            _isStarted = false;
            _treeFillerCancellation.Cancel();
            _treeFiller.Wait();
        }
        public void Write(Measurement m)
        {
            if (!_isStarted) return;
            _queue.Enqueue(m);
            _hasAnyMeasurementEvent.Set();
        }
        public bool ReadByCurrentTime(long sensorId, out Measurement m)
        {
            Debug.Assert(sensorId < _trees.Length);
            m = new Measurement();
            var tree = _trees[sensorId];
            if (tree == null) return false;
            InternalMeasurement im;
            if (!tree.TryFindExactOrSmaller(DateTimeOffset.UtcNow.Ticks, out im)) return false;
            m = Measurement.New(sensorId, im.Time, im.Value, im.Tag, im.User);
            return true;
        }
        public List<Measurement> ReadAllByCurrentTime()
        {
            List<Measurement> result = new List<Measurement>();
            for(int i = 0; i < NumOfSensors; i++)
            {
                Measurement m;
                if (ReadByCurrentTime(i, out m)) result.Add(m);
            }
            return result;
        }
        public bool ReadByTimeInterval(long sensorId, long begin, long end, out List<Measurement> measurements)
        {
            measurements = null;
            var tree = _trees[sensorId];
            if (tree == null) return false;
            var rangeResult = tree.FindRange(begin, end);
            var result = new List<Measurement>(rangeResult.Count);
            rangeResult.ForEach(x => result.Add(Measurement.New(sensorId, x.Time, x.Value, x.Tag, x.User)));
            measurements = result;
            return true;
        }
        public long GetNumberOfMeasurements()
        {
            long result = 0;
            Array.ForEach(_trees, (x) =>
            {
                if (x != null)
                    result += x.Count;
            });
            return result;
        }
        private void TreeFiller()
        {
            Measurement m;
            while (!_treeFillerCancellation.IsCancellationRequested)
            {
                _hasAnyMeasurementEvent.Wait();
                while (_queue.TryDequeue(out m))
                {
                    //ThreadPool.UnsafeQueueUserWorkItem(WriteWaitCallback, m);
                    WriteToTree(m);
                }
            }
            while (_queue.TryDequeue(out m))
            {
                WriteToTree(m);
            }
        }
        private void WriteToTree(Measurement m)
        {
            Debug.Assert(m.Id < _trees.Length);
            var tree = _trees[m.Id];
            if (tree == null)
                tree = _trees[m.Id] = new BPlusTreeRW<long, InternalMeasurement>(MAX_DEGREE);
            tree.Insert(m.Time, InternalMeasurement.From(m));
        }
        private void WriteWaitCallback(object obj)
        {
            Measurement m = (Measurement)obj;
            WriteToTree(m);
        }
    }
}
