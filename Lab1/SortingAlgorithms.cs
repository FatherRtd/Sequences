using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Lab1
{
	class SortingAlgorithms
	{
		public long ComparisonsCount { get; private set; }
		public long SortingTime { get; private set; }
		public delegate bool IsSwap(int a, int b);
		private Stopwatch SWatch;

		public SortingAlgorithms()
		{
			SWatch = new Stopwatch();
		}

		private void Swap(int[] array, int firstId, int secondId) => (array[firstId], array[secondId]) = (array[secondId], array[firstId]);

		public void FlagBubbleSort(int[] array, IsSwap cond)
		{
			ComparisonsCount = 0;
			SortingTime = 0;
			SWatch.Start();
			bool flag;
			while (true)
			{
				flag = true;
				for (int i = 0; i < array.Length - 1; i++)
				{
					if (cond(array[i], array[i + 1]))
					{
						Swap(array, i, i + 1);
						flag = false;
					}
					ComparisonsCount++;
				}
				if (flag)
					break;
			}
			SWatch.Stop();
			SortingTime = SWatch.ElapsedMilliseconds;
			SWatch.Reset();
		}

		public void LastIndexBubbleSort(int[] array, IsSwap cond)
		{
			ComparisonsCount = 0;
			SortingTime = 0;
			SWatch.Start();
			int lastIndex, j, Low = 0;
			while (Low < array.Length - 1)
			{
				for (lastIndex = j = array.Length - 1; j > Low; j--)
				{
					if (cond(array[j - 1], array[j]))
					{
						Swap(array, j - 1, j);
						lastIndex = j;
					}
					ComparisonsCount++;
				}
				Low = lastIndex;
			}
			SWatch.Stop();
			SortingTime = SWatch.ElapsedMilliseconds;
			SWatch.Reset();
		}

		public void PyramidalSort(int[] array, IsSwap cond)
		{
			ComparisonsCount = 0;
			SortingTime = 0;
			SWatch.Start();
			int i;
			if (array.Length > 1)
			{
				for (i = (array.Length - 1) / 2; i >= 0; i--)
					Shift(array, i, array.Length - 1, cond);
				for (i = array.Length - 1; i >= 1; i--)
				{
					Swap(array, 0, i);
					Shift(array, 0, i - 1, cond);
				}
			}
			SWatch.Stop();
			SortingTime = SWatch.ElapsedMilliseconds;
			SWatch.Reset();
		}

		private void Shift(int[] array, int low, int high, IsSwap cond)
		{
			int index = 2 * low + 1;
			while (index <= high)
			{
				if (index < high && cond(array[index + 1], array[index])) index++;
				if (cond(array[low], array[index])) break;
				ComparisonsCount+=2;
				Swap(array, index, low);
				low = index;
				index = 2 * low + 1;
			}
		}

		public void MSDSort(int[] array)
		{
			ComparisonsCount = 0;
			SortingTime = 0;
			SWatch.Start();
			int i, mask = 0;
			for (i = 0; i < array.Length; i++) mask |= array[i];
			for (i = 31; i >= 1; i--)
				if ((mask & (1 << i)) != 0)
					break;
			MSRadix(array, 0, array.Length - 1, 1 << i);
			SWatch.Stop();
			SortingTime = SWatch.ElapsedMilliseconds;
			SWatch.Reset();
		}

		private void MSRadix(int[] array, int low, int high, int mask)
		{
			int i;
			if (mask > 0 && low < high)
			{
				i = RadixPartition(array, low, high, mask);
				MSRadix(array, low, i - 1, mask >> 1);
				MSRadix(array, i, high, mask >> 1);
			}
			ComparisonsCount++;
		}

		private int RadixPartition(int[] array, int low, int high, int mask)
		{
			int i = low - 1, j = high + 1;
			for (; ; )
			{
				while (!((mask & array[++i]) != 0) && i < high) ;
				while (((mask & array[--j]) != 0) && j > low) ;
				if (i >= j) break;
				Swap(array, i, j);
			}

			if (i == j && i == high) i++;
			return i;
		}
	}
}
