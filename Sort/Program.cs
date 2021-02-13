using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{
    class Program
    {
        public static void Sort(ref int[] input)
        {
            for (int i = 1; i < input.Length; i++)
            {
                int temp = input[i];
                int j = i-1;
                while(j >= 0 && input[j] > temp)
                {
                    input[j+1] = input[j];
                    j--;
                }
                input[j+1] = temp;
            }
        }
        public static void Output(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Random r = new Random();
            int[] newArray = new int[32];
            for (int i = 0; i < 32; i++)
            {
                newArray[i] = r.Next(100);
            }
            Output(newArray);

            Sort(ref newArray);
            Output(newArray);
        }
    }
}
