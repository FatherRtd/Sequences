using System;
using System.Diagnostics;

namespace Lab1
{
	public class NumbersSequenceInt
	{
		public int Size { get; set; }
		public int MinValue { get; set; }
		public int MaxValue { get; set; }
		public int[] Sequence { get; set; }
		public long GenerationTime { get; set; }
		readonly Random _random = new Random(DateTime.Now.Second);
		private Stopwatch sw { get; set; }

		public NumbersSequenceInt(int size, int minValue, int maxValue)
		{
			Size = size;
			Sequence = new int[Size];
			MinValue = minValue;
			MaxValue = maxValue;
			sw = new Stopwatch();
		}

		public void CreateIncreasingSequence()
		{
			sw.Start();
			int intervalDelta = (MaxValue - MinValue) / Size;
			int min = MinValue;
			for (int i = 0; i < Size; i++, min+= intervalDelta) 
				Sequence[i] = _random.Next(min, min + intervalDelta);
			sw.Stop();
			GenerationTime = sw.ElapsedMilliseconds;
		}

		public void CreateDecreasingSequence()
		{
			sw.Start();
			int intervalDelta = (MaxValue - MinValue) / Size;
			int max = MaxValue;
			for (int i = 0; i < Size; i++, max-= intervalDelta)
				Sequence[i] = _random.Next(max- intervalDelta, max);
			sw.Stop();
			GenerationTime = sw.ElapsedMilliseconds;
		}

		public void CreateRandomSequence()
		{
			sw.Start();
			for (int i = 0; i < Size; i++)
				Sequence[i] = _random.Next(MinValue, MaxValue);
			sw.Stop();
			GenerationTime = sw.ElapsedMilliseconds;
		}

		public void CreateSteppedSequence(int delta)
		{
			sw.Start();
			int staticRange = (delta * 70) / 100;
			int increasingRange = delta - staticRange;
			int intervalsCount = Size / delta + Convert.ToInt32(Size % delta != 0);
			int increasingDelta = (MaxValue-MinValue) / (increasingRange*intervalsCount);
			int min = MinValue;
			int j = 0;

			for (int i = 0; i < intervalsCount; i++)
			{
				for (j = i * delta; j < delta * i + increasingRange && j < Size; j++, min += increasingDelta)
					Sequence[j] = _random.Next(min, min + increasingDelta);

				for (; j < delta * i + delta && j < Size; j++)
				{
					Sequence[j] = _random.Next(min - (MaxValue-MinValue)/Size, min+(MaxValue - MinValue) / Size);
				}
			}
			sw.Stop();
			GenerationTime = sw.ElapsedMilliseconds;
		}

		public void CreateSawtoothSequence(int delta)
		{
			sw.Start(); // Включение таймера
			int increasingRange = delta * 80 / 100; // 80% чисел в заданном промежутке будет возрастать
			int decreasingRange = delta - increasingRange; // 20% чисел в заданном промежутке будет убывать
			int increasingDelta = (MaxValue - MinValue) / increasingRange; // Средний диапазон роста
			int decreasingDelta = (MaxValue - MinValue) / decreasingRange; // Средний диапазон убывания
			int intervalsCount = Size / delta + Convert.ToInt32(Size % delta != 0); // Кол-во интервалов
			int j = 0; // Счётчик для массива
			int min = MinValue; // Начальная точка для генерации
			for (int i = 0; i < intervalsCount; i++) // Цикл в котором генерируются последовательности в промежутках
			{
				// В первом цикле генерируется возрастающая часть промежутка delta
				for (j = i*delta; j < delta*i+increasingRange && j < Size; j++, min += increasingDelta)
					Sequence[j] = _random.Next(min, min + increasingDelta); // Генерация следующего элемента

				// Во втором цикле генерируется убывающая часть промежутка delta
				for (; j < delta * i + delta && j < Size; j++, min -= decreasingDelta)
					Sequence[j] = _random.Next(min-decreasingDelta, min); // Генерация следующего элемента
			}
			sw.Stop(); //Выключение таймера
			GenerationTime = sw.ElapsedMilliseconds; // Запись времени генерации
		}

		public void CreateSinSequence(int delta)
		{
			sw.Start();
			int intervalsCount = Size / delta + Convert.ToInt32(Size % delta != 0);
			for(int i = 0; i < intervalsCount; i++)
			{
				double amplitude = (MaxValue - MinValue);
				double min = 2 * i * Math.PI;
				double max = 2 * (i + 1) * Math.PI;
				double intervalDelta = (max - min) / delta;
				for (int j = i * delta; j < delta * (i + 1) && j < Size; j++, min += intervalDelta)
				{
					double randX = _random.NextDouble() * intervalDelta + min;
					Sequence[j] = (int)(amplitude / 2 * Math.Sin(randX));
				}
			}
			sw.Stop();
			GenerationTime = sw.ElapsedMilliseconds;
		}

		public void CreateQuaziOrderedSequence()
		{
			sw.Start();
			int min = MinValue;
			int intervalDelta = (MaxValue - MinValue) / Size;
			for (int i = 0; i < Size; i++, min += intervalDelta)
				Sequence[i] = _random.Next(min, min + intervalDelta);
			for (int i = 0; i < Size - Size/10; i++)
			{
				if (i % 10 == 0 && i != 0)
				{
					int tmp = Sequence[i];
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
