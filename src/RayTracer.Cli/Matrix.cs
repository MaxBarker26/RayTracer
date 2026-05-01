namespace RayTracer.Cli;

/// <summary>
/// Represents a matrix of double-precision floating-point numbers.
/// </summary>
public class Matrix
{
    private double[,] _matrix;

    /// <summary>
    /// Gets the number of rows in the matrix.
    /// </summary>
    public int RowCount { get; }

    /// <summary>
    /// Gets the number of columns in the matrix.
    /// </summary>
    public int ColCount { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix"/> class with the specified dimensions.
    /// </summary>
    /// <param name="numRow">The number of rows.</param>
    /// <param name="numColumn">The number of columns.</param>
    public Matrix(int numRow, int numColumn)
    {
        RowCount = numRow;
        ColCount = numColumn;
        _matrix = new double[numRow, numColumn];
    }

    // Matrix indexer
    public double this[int row, int col]
    {
        get => _matrix[row, col];
        set => _matrix[row, col] = value;
    }

    /// <summary>
    /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="Matrix"/>.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="Matrix"/>.</param>
    /// <returns><see langword="true"/> if the specified <see cref="object"/> is equal to the current <see cref="Matrix"/>; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (obj is not Matrix)
            return false;

        Matrix other = (Matrix)obj;

        if (this.RowCount != other.RowCount || this.ColCount != other.ColCount)
            return false;

        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColCount; j++)
            {
                if (!this[i, j].IsNearly(other[i, j]))
                    return false;
            }
        }
        return true;
    }

    ///<summary>
    /// Multiplies the current matrix by another matrix.
    /// Only meant to be used with 4 x 4 matrices.
    ///</summary>
    /// <param name="other">The second matrix in the multiplication.</param>
    /// <returns>A new <see cref="Matrix"/> representing the product of the two matrices.</returns>
    public Matrix Times(Matrix other)
    {
        if (ColCount != other.RowCount)
            throw new InvalidOperationException("Matrix dimensions must agree for multiplication.");

        Matrix product = new Matrix(RowCount, other.ColCount);

        Tuple[] rows = new Tuple[RowCount];
        Tuple[] cols = new Tuple[other.ColCount];

        for (int i = 0; i < 4; i++)
        {
            rows[i] = new Tuple(this[i, 0], this[i, 1], this[i, 2], this[i, 3]);
            cols[i] = new Tuple(other[0, i], other[1, i], other[2, i], other[3, i]);
        }

        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < other.ColCount; j++)
            {
                // Here is the magic line from your dump: product[i, j] = rows[i] * cols[j];
                product[i, j] = rows[i] * cols[j];
            }
        }

        return product;
    }

    /// <summary>
    /// Multiplies this matrix by a tuple.
    /// </summary>
    /// <param name="t">The tuple to multiply the matrix by.</param>
    /// <returns>A new tuple representing the result of the matrix-tuple multiplication.</returns>
    public Tuple Times(Tuple t)
    {
        if (ColCount != 4)
            throw new InvalidOperationException("Matrix dimensions must agree for multiplication.");

        double x = 0;
        double y = 0;
        double z = 0;
        double w = 0;

        for (int i = 0; i < 4; i++)
        {
            Tuple row = new Tuple(this[i, 0], this[i, 1], this[i, 2], this[i, 3]);
            if (i == 0)
            {
                x = row * t;
            }
            else if (i == 1)
            {
                y = row * t;
            }
            else if (i == 2)
            {
                z = row * t;
            }
            else
            {
                w = row * t;
            }
        }

        return new Tuple(x, y, z, w);
    }

    /// <summary>
    /// Multiplies a Matrix by a Tuple.
    /// </summary>
    /// <param name="m">The matrix.</param>
    /// <param name="t">The tuple.</param>
    /// <returns>A new tuple representing the product of the matrix and the tuple.</returns>
    public static Tuple operator *(Matrix m, Tuple t)
    {
        return m.Times(t);
    }

    /// <summary>
    /// Overloads the multiplication operator for two <see cref="Matrix"/> objects.
    /// </summary>
    /// <param name="a">The first matrix in the multiplication.</param>
    /// <param name="b">The second matrix in the multiplication.</param>
    /// <returns>A new <see cref="Matrix"/> representing the product of the two matrices.</returns>
    public static Matrix operator *(Matrix a, Matrix b)
    {
        return a.Times(b);
    }
}
