using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Lab1
{

	class Program
	{
		public static int c = 0;

		static int cond(int x, int y)
		{
			c++;
			if (x > y)
				return 1;
			else if (x == y)
				return 0;
			else return -1;
		}

		static void Main(string[] args)
		{
			int max = 1000000;
			int min = 0;
			long t = 0;

			NumbersSequenceInt sequence;
			List<int> list = new List<int>();
			Stopwatch sw = new Stopwatch();

			for (int i = 5000; i <= 50000; i+=5000)
			{
				c = 0;
				sequence = new NumbersSequenceInt(i, min, max);
				sequence.CreateSawtoothSequence(100);
				sw.Start();
				Array.Sort<int>(sequence.Sequence,cond);
				sw.Stop();
				t = sw.ElapsedMilliseconds;
				Console.WriteLine($"N = {i}, Время сортировки = {t} Кол-во сравнений = {c}");
				Console.WriteLine("-------------");
				sw.Reset();
			}


		}
	}
}
