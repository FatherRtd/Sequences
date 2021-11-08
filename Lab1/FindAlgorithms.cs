using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection.Metadata;
using System.Text;

namespace Lab1
{
	class FindAlgorithms
	{
		public static int Count { get; private set; }

		public static int JumpFindOneLevel(int[] Sequence,int key)
		{
			Count = 0;
			int jumpDelta = (int)Math.Sqrt((double)Sequence.Length); // Значение прыжка
			int min = 0, max = 0; // Границы диапазона поиска
			while (min < Sequence.Length && Sequence[min] <= key) // Вычисление границ
			{
				max = Math.Min(Sequence.Length - 1, min + jumpDelta);
				Count++;
				if (Sequence[min] <= key && Sequence[max] >= key) // Если ключ в диапазоне, то выходим из цикла поиска
					break;
				min += jumpDelta;
			}

			if (min >= Sequence.Length || Sequence[min] > key) // Проверка на выход левой границы за пределы массива
				return -1;

			max = Math.Min(Sequence.Length - 1, max); // Перепроверка правой границы на выход за пределы массива
			int i = min; // Индекс левой границы
			while (i <= max && Sequence[i] <= key) // Цикл поиска в диапазоне
			{
				Count++;
				if (Sequence[i] == key) // Если ключ и элемент массива совпали, то возвращаем найденное значение
					return Sequence[i];
				i++; // Увеличение левой границы
			}

			return -1; // Возвращаемое значение по умолчанию (если не нашли ключ)
		}

		public static int JumpFindTwoLevels(int[] Sequence, int key)
		{
			Count = 0;
			int jumpDelta = (int)Math.Sqrt((double)Sequence.Length); // Значение прыжка
			int min = 0, max = 0; // Границы диапазона поиска
			while (min < Sequence.Length && Sequence[min] <= key) // Вычисление границ
			{
				max = Math.Min(Sequence.Length - 1, min + jumpDelta);
				if (Sequence[min] <= key && Sequence[max] >= key) // Если ключ в диапазоне, то выходим из цикла поиска
					break;
				min += jumpDelta;
			}

			if (min >= Sequence.Length || Sequence[min] > key) // Проверка на выход левой границы за пределы массива
				return -1;

			max = Math.Min(Sequence.Length - 1, max); // Проверка правой границы на выход за пределы массива
			int i = min; // Индекс левой границы
			int deltaSize = (int)Math.Sqrt(max - min + 1);
			while (i <= max)// Цикл поиска в диапазоне
			{
				int newMax = Math.Min(i + deltaSize,max); //вычисление правой границы в пределах диапазона
				while (i <= newMax) // Цикл поиска в диапазоне
				{
					Count++;
					if (Sequence[i] == key)
						return Sequence[i];
					i++;
				}
			}

			return -1; // Возвращаемое значение по умолчанию (если не нашли ключ)
		}

		public static int BinaryFind(int[] Sequence, int key)
		{
			Count = 0;
			int min = 0, max = Sequence.Length - 1;
			while (min <= max && key >= Sequence[min] && key <= Sequence[max])
			{
				int avg = (min + max ) / 2 + Convert.ToInt32((min + max) % 2 != 0); // Вычисление среднего значения
				Count += 1;
				if (Sequence[avg] == key)
					return Sequence[avg];

				Count += 1;
				if (key < Sequence[avg]) 
					max = avg - 1; // смещение правой границы
				else 
					min = avg + 1;  // смещение левой границы
			}
			return -1;
		}

		public static int InterpolationFind(int[] Sequence, int key)
		{
			Count = 0;
			int min = 0, max = Sequence.Length - 1; // Начальные левая и правая границы
			while (min <= max && key >= Sequence[min] && key <= Sequence[max]) // Цикл поиска
			{
				int avg = min + (key - Sequence[min]) * (max - min) / (Sequence[max] - Sequence[min]); // Вычисление приблизительного значения
				Count += 1;
				if (Sequence[avg] == key)
					return Sequence[avg];

				Count += 1;
				if (Sequence[avg] < key)
					min = avg + 1; // смещение левой границы
				else
					max = avg - 1; // смещение правой границы
			}
			return -1;
		}
	}
}
