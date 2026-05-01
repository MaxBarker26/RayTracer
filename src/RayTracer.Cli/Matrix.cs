namespace RayTracer.Cli;

public class Matrix
{
    private double[,] _matrix;
    public int RowCount { get; }
    public int ColCount { get; }

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
    /// Only meant to be used with 4 x 4 matrices.
    ///</summary>
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

    public static Matrix operator *(Matrix a, Matrix b)
    {
        return a.Times(b);
    }
}
