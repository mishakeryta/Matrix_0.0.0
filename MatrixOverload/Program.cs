using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixOverload
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Matrix matrix2 = new Matrix(3,3,2,1,2,-1,1,1,5,0);
            int sequence = Convert.ToInt32(Console.ReadLine());
            Matrix matrix1 = new Matrix(sequence);
            for (int i = 0; i < sequence; ++i)
            {
                for (int j = 0; j < sequence; ++j)
                {
                    matrix1.element[i, j] = Convert.ToDouble(Console.ReadLine());
                }
            }
           
            //Matrix matrix3 = matrix1 * matrix2;
            matrix1.Show();
            (matrix1.InverseMatrix()).Show();
            Console.WriteLine(matrix1.Determinate());
           
            
        }
    }
}
