using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Channels;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;


namespace Lab1
{
	public class Benchmark
	{
		private SortingAlgorithms sortingAlgorithms = new SortingAlgorithms();
		private NumbersSequenceInt sequence = new NumbersSequenceInt(10000, -100000, 100000);

		[Benchmark]
		public void MSDSort()
		{
			sortingAlgorithms.MSDSort(sequence.Sequence);
		}

		[Benchmark]
		public void PyramidalSort()
		{
			sortingAlgorithms.PyramidalSort(sequence.Sequence, (i, j) => i > j);
		}

		[Benchmark]
		public void BubbleSort()
		{
			sortingAlgorithms.LastIndexBubbleSort(sequence.Sequence, (i, j) => i > j);
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			BenchmarkRunner.Run<Benchmark>();
		}
	}
}
