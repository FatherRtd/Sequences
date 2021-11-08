using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Channels;


namespace Lab1
{
	class MyClass
	{
		public static int Number { get; set; }

	}
	class Program
	{
		static void Main(string[] args)
		{
			NumbersSequenceInt sequence = new NumbersSequenceInt(1000, 0, 100000);
			sequence.CreateIncreasingSequence();

			for (int i = 1000; i <= 512000; i*=2)
			{
				int z = 0;
				for (int k = 0; k < 100; k++)
				{
					int rand = new Random().Next(0, 100000);
					new NumbersSequenceInt(i, -1000000, 1000000);
					sequence.CreateIncreasingSequence();
					//for (int j = 0; j < i; j++)
					//{
					//	if (sequence.Sequence[j] >= rand)
					//	{
					//		sequence.Sequence[j] = rand;
					//		break;
					//	}
					//}

					FindAlgorithms.JumpFindOneLevel(sequence.Sequence, rand);
					z += FindAlgorithms.Count;
				}

				Console.WriteLine("i - " + i);
				Console.WriteLine("Среднее кол-во сравнений - " + z / 10);
			}

			//foreach (var VARIABLE in sequence.Sequence)
			//{
			//	Console.WriteLine(VARIABLE);
			//}

			//Console.WriteLine(FindAlgorithms.InterpolationFind(sequence.Sequence, 50000));
			//Console.WriteLine("Кол-во сравнений - " + FindAlgorithms.Count);
		}
	}
}
