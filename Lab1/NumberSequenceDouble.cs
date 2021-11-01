using System;
using System.Diagnostics;

namespace Lab1
{
	public class NumbersSequenceDouble
	{
		public int Size { get; set; }
		public double MinValue { get; set; }
		public double MaxValue { get; set; }
		public double[] Sequence { get; set; }
		public long GenerationTime { get; set; }
		readonly Random _random = new Random(DateTime.Now.Second);
		private Stopwatch sw { get; set; }

		public NumbersSequenceDouble(int size, double minValue, double maxValue)
		{
			Size = size;
			Sequence = new double[Size];
			MinValue = minValue;
			MaxValue = maxValue;
			sw = new Stopwatch();
		}

		private double GetRandomDouble(double min, double max)
		{
			return _random.NextDouble() * (max - min) + min;
		}

		public void CreateIncreasingSequence()
		{
			sw.Start();
			double intervalDelta = (MaxValue - MinValue) / Size;
			double min = MinValue;
			for (int i = 0; i < Size; i++, min += intervalDelta)
				Sequence[i] = GetRandomDouble(min, min + intervalDelta);
			sw.Stop();
			GenerationTime = sw.ElapsedMilliseconds;
		}

		public void CreateDecreasingSequence()
		{
			sw.Start();
			double intervalDelta = (MaxValue - MinValue) / Size;
			double max = MaxValue;
			for (int i = 0; i < Size; i++, max -= intervalDelta)
				Sequence[i] = GetRandomDouble(max - intervalDelta, max);
			sw.Stop();
			GenerationTime = sw.ElapsedMilliseconds;
		}

		public void CreateRandomSequence()
		{
			sw.Start();
			for (int i = 0; i < Size; i++)
				Sequence[i] = GetRandomDouble(MinValue, MaxValue);
			sw.Stop();
			GenerationTime = sw.ElapsedMilliseconds;
		}

		public void CreateSteppedSequence(int delta)
		{
			sw.Start();
			int staticRange = (delta * 70) / 100;
			int increasingRange = delta - staticRange;
			int intervalsCount = Size / delta + Convert.ToInt32(Size % delta != 0);
			double increasingDelta = (MaxValue - MinValue) / (increasingRange * intervalsCount);
			double min = MinValue;
			int j = 0;

			for (int i = 0; i < intervalsCount; i++)
			{
				for (j = i * delta; j < delta * i + increasingRange && j < Size; j++, min += increasingDelta)
					Sequence[j] = GetRandomDouble(min, min + increasingDelta);

				for (; j < delta * i + delta && j < Size; j++)
				{
					Sequence[j] = GetRandomDouble(min - (MaxValue - MinValue) / Size, min + (MaxValue - MinValue) / Size);
				}
			}
			sw.Stop();
			GenerationTime = sw.ElapsedMilliseconds;
		}

		public void CreateSawtoothSequence(int delta)
		{
			sw.Start();
			int increasingRange = delta * 80 / 100;
			int decreasingRange = delta - increasingRange;
			double increasingDelta = (MaxValue - MinValue) / increasingRange;
			double decreasingDelta = (MaxValue - MinValue) / decreasingRange;
			int intervalsCount = Size / delta + Convert.ToInt32(Size % delta != 0);
			int j = 0;
			double min = MinValue;
			for (int i = 0; i < intervalsCount; i++)
			{
				for (j = i * delta; j < delta * i + increasingRange && j < Size; j++, min += increasingDelta)
					Sequence[j] = GetRandomDouble(min, min + increasingDelta);


				for (; j < delta * i + delta && j < Size; j++, min -= decreasingDelta)
					Sequence[j] = GetRandomDouble(min - decreasingDelta, min);
			}
			sw.Stop();
			GenerationTime = sw.ElapsedMilliseconds;
		}

		public void CreateSinSequence(int delta)
		{
			sw.Start();
			int intervalsCount = Size / delta + Convert.ToInt32(Size % delta != 0);
			for (int i = 0; i < intervalsCount; i++)
			{
				double amplitude = (MaxValue - MinValue);
				double min = 2 * i * Math.PI;
				double max = 2 * (i + 1) * Math.PI;
				double intervalDelta = (max - min) / delta;
				for (int j = i * delta; j < delta * (i + 1) && j < Size; j++, min += intervalDelta)
				{
					double randX = _random.NextDouble() * intervalDelta + min;
					Sequence[j] = amplitude / 2 * Math.Sin(randX);
				}
			}
			sw.Stop();
			GenerationTime = sw.ElapsedMilliseconds;
		}

		public void CreateQuaziOrderedSequence()
		{
			sw.Start();
			double min = MinValue;
			double intervalDelta = (MaxValue - MinValue) / Size;
			for (int i = 0; i < Size; i++, min += intervalDelta)
				Sequence[i] = GetRandomDouble(min, min + intervalDelta);
			for (int i = 0; i < Size - Size / 10; i++)
			{
				if (i % 10 == 0 && i != 0)
				{
					double tmp = Sequence[i];
					int j = _random.Next(i, i + Size / 10);
					Sequence[i] = Sequence[j];
					Sequence[j] = tmp;
				}
			}
			sw.Stop();
			GenerationTime = sw.ElapsedMilliseconds;
		}
	}
}
