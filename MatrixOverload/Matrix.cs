using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixOverload
{
    class Matrix
    {
        #region Constructors
        protected int sequence;
        public double[,] element;
        public Matrix(int sequence = 1)
        {
            this.sequence = sequence;
            this.element = new double[sequence, sequence];
        }
        public Matrix(int sequence,double value)
        {
            this.sequence = sequence;
            Matrix newMatrix = new Matrix(sequence);
            for(int i = 0;i<sequence;++i)
            {
                newMatrix[i, i] = value;
            }
        }
        public Matrix(int sequence = 1, params double[] elements)
        {
            this.sequence = sequence;
            this.element = new double[sequence, sequence];
            if (elements.Length <= sequence * sequence)
            {
                for (int i = 0; i < elements.Length; ++i)
                {
                    this.element[i / sequence, i % sequence] = elements[i];
                }
            }
        }
        #endregion
        #region Checking for false accesse
        bool IsIndexTrue(int row, int col)
        {
            if (row >= this.sequence || row < 0)
            {
                return false;
            }
            if (col >= this.sequence || col < 0)
            {
                return false;
            }
            return true;
        }
        #endregion
        #region Indexer

        public double this[int row, int col]
        {
            get
            {
                if (IsIndexTrue(row, col))
                {
                    return this.element[row, col];
                }
                //error need to be handeled
                else return 0;
            }
            set
            {
                if(IsIndexTrue(row,col))
                {
                    this.element[row, col] = value;
                }
            }

        }
        #endregion

        #region Create Unit Matrix
        public static Matrix CreateUnitMatrix(int sequence)
        {
            Matrix UnitMatrix = new Matrix(sequence);
            for (int i = 0; i < sequence; ++i)
            {
                UnitMatrix.element[i, i] = 1;
            }
            return UnitMatrix;
        }
        #endregion
        #region Overloading
        #region Methmetical Operetions 
        #region  Unary -
        public static Matrix operator -(Matrix matrix1)
        {
            Matrix returnMatrix = new Matrix(matrix1.sequence);
            for (int i = 0; i < matrix1.sequence; ++i)
            {
                for (int j = 0; j < matrix1.sequence; ++j)
                {
                    returnMatrix.element[i, j] = (-1) * matrix1.element[i, j];
                }
            }
            return returnMatrix;
        }
        #endregion
        #region Binary +
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            Matrix returnMatrix = new Matrix(matrix1.sequence);
            for (int i = 0; i < matrix1.sequence; ++i)
            {
                for (int j = 0; j < matrix1.sequence; ++j)
                {
                    returnMatrix.element[i, j] = matrix1.element[i, j] +
                    ((i < matrix2.sequence && j < matrix2.sequence) ? (matrix2.element[i, j]) : 0.0);
                }
            }
            return returnMatrix;
        }
        #endregion
        #region Binary -
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            return matrix1 + (-matrix2);
        }
        public static Matrix operator -(double number, Matrix matrix)
        {
            return number * CreateUnitMatrix(matrix.sequence) + (-matrix);
        }
        #endregion
        #region Binary *
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.sequence != matrix2.sequence)
            {
                return null;
            }
            Matrix returnMatrix = new Matrix(matrix1.sequence);
            for (int row = 0; row < matrix1.sequence; ++row)
            {
                for (int col = 0; col < matrix1.sequence; ++col)
                {

                    for (int i = 0; i < matrix1.sequence; ++i)
                    {
                        returnMatrix.element[row, col] +=
                         matrix1.element[row, i] * matrix2.element[i, col];
                    }
                }
            }
            return returnMatrix;
        }
        public static Matrix operator *(double number, Matrix matrix)
        {
            Matrix newMatr = new Matrix(matrix.sequence);
            newMatr = matrix;
            for (int i = 0; i < matrix.sequence; ++i)
            {
                for (int j = 0; j < matrix.sequence; ++j)
                {
                    newMatr.element[i, j] *= number;
                }
            }
            return newMatr;
        }
        public static Matrix operator *(Matrix matrix, double number)
        {
            return number * matrix;
        }
        #endregion
        #endregion
        #region Logic
        #region Unary !
        public static bool operator !(Matrix matrix)
        {
            if (matrix) return false;
            else return true;
        }
        #endregion
        #region false
        public static bool operator false(Matrix matrix)
        {
            for (int i = 0; i < matrix.sequence; ++i)
            {
                for (int j = 0; j < matrix.sequence; ++j)
                {
                    if (matrix.element[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
        #region true
        public static Boolean operator true(Matrix matrix)
        {
            for (int i = 0; i < matrix.sequence; ++i)
            {
                for (int j = 0; j < matrix.sequence; ++j)
                {
                    if (matrix.element[i, j] != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
        #region |
        public static Matrix operator |(Matrix matrix1, Matrix matrix2)
        {
            for (int i = 0; i < matrix1.sequence; ++i)
            {
                for (int j = 0; j < matrix1.sequence; ++j)
                {
                    if (matrix1.element[i, j] != 0 || matrix2.element[i, j] != 0)
                    {
                        return matrix1;
                    }
                }
            }
            return new Matrix();

        }
        #endregion
        #endregion
        #endregion

        #region Determinate
        /// <summary>
        /// 
        /// </summary>
        /// <returns>determine of this matrix</returns>
        public double Determinate()
        {
            double[,] tmpMatrix = new double[this.sequence, this.sequence];
            for (int i = 0; i < this.sequence; ++i)
            {
                for (int j = 0; j < this.sequence; ++j)
                {
                    tmpMatrix[i, j] = this.element[i, j];
                }
            }
            double determinate = 1;
            for (int row = 0; row < sequence - 1; ++row)
            {
                determinate *= tmpMatrix[row, row];
                for (int rowToNull = 1; rowToNull < sequence - row; ++rowToNull)
                {
                    double k = tmpMatrix[row + rowToNull, row] / tmpMatrix[row, row];
                    for (int cols = row; cols < sequence; ++cols)
                    {
                        tmpMatrix[row + rowToNull, cols] -= (k * tmpMatrix[row, cols]);
                    }
                }
            }
            determinate *= tmpMatrix[sequence - 1, sequence - 1];
            return determinate;
        }
        #endregion

        #region Inverse Matrix
        public Matrix InverseMatrix()
        {

            Matrix inverseMatrix = new Matrix(this.sequence);
            if (sequence == 1)
            {
                inverseMatrix.element[0, 0] = this.element[0, 0];
                return inverseMatrix;
            }
            double Determinant = this.Determinate();
            if (Determinant == 0)
            {
                return null;
            }
            for (int i = 0; i < sequence; ++i)
            {
                for (int j = 0; j < sequence; ++j)
                {

                    inverseMatrix.element[i, j] = (((i + j) % 2) != 0 ? -1 : 1) * (1 / Determinant) * this.FindAddtionMatrWithoutSign(i, j).Determinate();
                }
            }
            return inverseMatrix;
        }
        #endregion

        #region Find Addtion Of Matr Element Withou Sign(Not usigned)
        public Matrix FindAddtionMatrWithoutSign(int row, int col)
        {
            Matrix matrAddionWithoutSign = new Matrix(this.sequence - 1);
            for (int i = 0, iForThis = 0; i < sequence - 1; ++i, ++iForThis)
            {
                if (row == iForThis)
                {
                    ++iForThis;
                }
                if (iForThis >= this.sequence)
                {
                    break;
                }
                for (int j = 0, jForThis = 0; j < sequence - 1; ++j, ++jForThis)
                {

                    if (col == jForThis)
                    {
                        ++jForThis;
                    }
                    if (jForThis >= this.sequence)
                    {
                        break;
                    }
                    matrAddionWithoutSign.element[i, j] = this.element[iForThis, jForThis];
                }
            }
            return matrAddionWithoutSign;
        }
        #endregion

        #region Show Matrix
        public void Show()
        {
            for (int i = 0; i < this.sequence; ++i)
            {
                for (int j = 0; j < this.sequence; ++j)
                {
                    Console.Write(this.element[i, j] + " ");

                }
                Console.Write("\n");
            }
        }
        #endregion

    }
}
