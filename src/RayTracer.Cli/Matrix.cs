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

    /// <summary>
    /// Computes the transpose of the current matrix.
    /// </summary>
    /// <returns>A new <see cref="Matrix"/> instance representing the transpose of the current matrix.</returns>
    /// <remarks>
    /// The transpose of a matrix is obtained by swapping its rows and columns.
    /// If the original matrix has dimensions m x n, its transpose will have dimensions n x m.
    /// </remarks>
    public Matrix Transpose()
    {
        Matrix transposed = new Matrix(RowCount, ColCount);
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColCount; j++)
            {
                transposed[j, i] = this[i, j];
            }
        }

        return transposed;
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

    /// <summary>
    /// Creates an identity matrix of the specified size.
    /// An identity matrix is a square matrix with ones on the main diagonal and zeros elsewhere.
    /// </summary>
    /// <param name="size">The dimension of the square identity matrix (e.g., for a 3x3 matrix, size would be 3).</param>
    /// <returns>A new <see cref="Matrix"/> instance representing the identity matrix.</returns>
    public static Matrix Identity(int size)
    {
        Matrix identity = new Matrix(size, size);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i == j)
                {
                    identity[i, j] = 1;
                }
                else
                {
                    identity[i, j] = 0;
                }
            }
        }
        return identity;
    }

    /// <summary>
    /// Creates a 4x4 identity matrix.
    /// An identity matrix is a square matrix with ones on the main diagonal and zeros elsewhere.
    /// When an identity matrix multiplies another matrix, the other matrix remains unchanged.
    /// </summary>
    /// <returns>A new 4x4 identity <see cref="Matrix"/>.</returns>
    public static Matrix Identity()
    {
        Matrix identity = new Matrix(4, 4);

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i == j)
                {
                    identity[i, j] = 1;
                }
                else
                {
                    identity[i, j] = 0;
                }
            }
        }
        return identity;
    }

    /// <summary>
    /// Calculates the determinant of a matrix.
    /// </summary>
    /// <param name="m">The matrix for which to calculate the determinant.</param>
    /// <returns>The determinant of the matrix.</returns>
    public static double FindDeterminant(Matrix m)
    {
        if (m.RowCount == 2 && m.ColCount == 2)
        {
            return m[0, 0] * m[1, 1] - m[1, 0] * m[0, 1];
        }

        double determinant = 0;
        for (int i = 0; i < m.RowCount; i++)
        {
            determinant += m[0, i] * m.Cofactor(0, i);
        }
        return determinant;
    }

    /// <summary>
    /// Creates a translation matrix.
    /// </summary>
    /// <param name="x">The translation amount along the x-axis.</param>
    /// <param name="y">The translation amount along the y-axis.</param>
    /// <param name="z">The translation amount along the z-axis.</param>
    /// <returns>A 4x4 translation matrix.</returns>
    public static Matrix Translation(double x, double y, double z)
    {
        Matrix translation = Identity(4);
        translation[0, 3] = x;
        translation[1, 3] = y;
        translation[2, 3] = z;
        return translation;
    }

    public static Matrix Scaling(double x, double y, double z)
    {
        Matrix scaling = Identity(4);
        scaling[0, 0] = x;
        scaling[1, 1] = y;
        scaling[2, 2] = z;
        return scaling;
    }

    /// <summary>
    /// Creates a 4x4 rotation matrix around the X-axis.
    /// </summary>
    /// <param name="radians">The angle of rotation in radians.</param>
    /// <returns>A new <see cref="Matrix"/> instance representing the X-axis rotation.</n
    public static Matrix RotationX(double radians)
    {
        Matrix rotation = Matrix.Identity(4);
        rotation[1, 1] = Math.Cos(radians);
        rotation[2, 1] = Math.Sin(radians);
        rotation[1, 2] = -Math.Sin(radians);
        rotation[2, 2] = Math.Cos(radians);
        return rotation;
    }

    public static Matrix RotationY(double radians)
    {
        Matrix rotation = Matrix.Identity(4);
        rotation[0, 0] = Math.Cos(radians);
        rotation[0, 2] = Math.Sin(radians);
        rotation[2, 0] = -Math.Sin(radians);
        rotation[2, 2] = Math.Cos(radians);
        return rotation;
    }

    public static Matrix RotationZ(double radians)
    {
        Matrix rotation = Matrix.Identity(4);
        rotation[0, 0] = Math.Cos(radians);
        rotation[1, 0] = Math.Sin(radians);
        rotation[0, 1] = -Math.Sin(radians);
        rotation[1, 1] = Math.Cos(radians);
        return rotation;
    }

    /// <summary>
    /// Creates a shearing transformation matrix.
    /// </summary>
    /// <param name="xy">The shearing factor for the X-coordinate based on Y (x' = x + y * xy).</param>
    /// <param name="xz">The shearing factor for the X-coordinate based on Z (x' = x + z * xz).</param>
    /// <param name="yx">The shearing factor for the Y-coordinate based on X (y' = y + x * yx).</param>
    /// <param name="yz">The shearing factor for the Y-coordinate based on Z (y' = y + z * yz).</param>
    /// <param name="zx">The shearing factor for the Z-coordinate based on X (z' = z + x * zx).</param>
    /// <param name="zy">The shearing factor for the Z-coordinate based on Y (z' = z + y * zy).</param>
    /// <returns>A new <see cref="Matrix"/> representing the shearing transformation.</returns>
    public static Matrix Shearing(double xy, double xz, double yx, double yz, double zx, double zy)
    {
        Matrix shearing = Matrix.Identity(4);
        shearing[0, 1] = xy;
        shearing[0, 2] = xz;
        shearing[1, 2] = yz;
        shearing[1, 0] = yx;
        shearing[2, 0] = zx;
        shearing[2, 1] = zy;

        return shearing;
    }

    /// <summary>
    /// Creates a view matrix looking from one point to another.
    /// </summary>
    /// <param name="from">The position of the camera (eye point).</param>
    /// <param name="to">The point the camera is looking at (target point).</param>
    /// <param name="up">The world's up vector, used to orient the camera. Does not have to be exact or normalized.</param>
    /// <returns>A view matrix that transforms world coordinates to view coordinates.</n`returns>
    public static Matrix View(Point from, Point to, Vector up)
    {
        Matrix orientation = new(4, 4);
        Vector forward = (to - from).Normalized;
        Vector left = forward.Cross(up.Normalized);
        Vector trueUp = left.Cross(forward);
        orientation[0, 0] = left.X;
        orientation[0, 1] = left.Y;
        orientation[0, 2] = left.Z;
        orientation[0, 3] = 0;
        orientation[1, 0] = trueUp.X;
        orientation[1, 1] = trueUp.Y;
        orientation[1, 2] = trueUp.Z;
        orientation[1, 3] = 0;
        orientation[2, 0] = -(forward.X);
        orientation[2, 1] = -(forward.Y);
        orientation[2, 2] = -(forward.Z);
        orientation[2, 3] = 0;
        orientation[3, 0] = 0;
        orientation[3, 1] = 0;
        orientation[3, 2] = 0;
        orientation[3, 3] = 1;

        return orientation * Translation(-(from.X), -(from).Y, -(from).Z);
    }

    //Creates a translation matrix to orient an object in world space based on its position
    //relative to the camera.
    //IMPORTANT: Due to the x axis of the cameras view transformation being a left vector, a positive x value
    // will move an object LEFT when it is applied to an objects transformation matrix.
    // +y moves an object up, -y down, +z will move an object further from the camera, -z closer.
    public static Matrix CameraRelativeTranslation(Camera cam, double x, double y, double z)
    {
        Vector localVector = new(x, y, z);
        Vector worldVector = (cam.Transform.Invert() * localVector).ToVector();
        Matrix relativeTransform = Matrix.Translation(worldVector.X, worldVector.Y, worldVector.Z);
        return relativeTransform;
    }

    public static Matrix CameraRelativeRotationX(Camera cam, double angle)
    {
        Matrix viewRotation = ExtractCameraRotation(cam);
        Matrix cameraRotation = viewRotation.Transpose();
        //possible cause for concern but leave this mismatch for now
        return cameraRotation * RotationY(angle) * viewRotation;
    }

    public static Matrix CameraRelativeRotationY(Camera cam, double angle)
    {
        Matrix viewRotation = ExtractCameraRotation(cam);
        Matrix cameraRotation = viewRotation.Transpose();
        //possible cause for concern but leave this mismatch for now
        return cameraRotation * RotationZ(angle) * viewRotation;
    }

    public static Matrix CameraRelativeRotationZ(Camera cam, double angle)
    {
        Matrix viewRotation = ExtractCameraRotation(cam);
        Matrix cameraRotation = viewRotation.Transpose();
        return cameraRotation * RotationX(angle) * viewRotation;
        //possible cause for concern but leave this mismatch for now
    }

    //Helper method returns the camera's transformation matrix with the translation collumn zeroed out
    private static Matrix ExtractCameraRotation(Camera cam)
    {
        Matrix view = new(4, 4);
        for (int i = 0; i < cam.Transform.RowCount; i++)
        {
            for (int j = 0; j < cam.Transform.ColCount; j++)
            {
                view[i, j] = cam.Transform[i, j];
            }
        }
        view[0, 3] = 0;
        view[1, 3] = 0;
        view[2, 3] = 0;

        return view;
    }

    /// <summary>
    /// Creates a submatrix by removing the specified row and column from the current matrix.
    /// </summary>
    /// <param name="row">The zero-based index of the row to remove.</param>
    /// <param name="col">The zero-based index of the column to remove.</param>
    // <returns>A new matrix that is a submatrix of the current matrix, with the specified row and column removed.</returns>
    public Matrix Submatrix(int row, int col)
    {
        Matrix sub = new(RowCount - 1, ColCount - 1);
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColCount; j++)
            {
                //subRow and subCol keep track of the correct row and column to place a value in the sub matrix.
                int subRow = i;
                int subCol = j;
                if (i == row)
                    continue;
                if (j == col)
                    continue;
                if (i > row)
                {
                    subRow--;
                }
                if (j > col)
                {
                    subCol--;
                }

                sub[subRow, subCol] = this[i, j];
            }
        }
        return sub;
    }

    /// <summary>
    /// Calculates the minor of the matrix at the specified row and column.
    /// </summary>
    /// <param name="row">The zero-based row index.</param>
    /// <param name="col">The zero-based column index.</param>
    /// <returns>The minor value at the given row and column.</returns>
    public double Minor(int row, int col)
    {
        return FindDeterminant(this.Submatrix(row, col));
    }

    /// <summary>
    /// Calculates the cofactor of a specific element in the matrix.
    /// The cofactor is the minor multiplied by (-1)^(row + col).
    /// </summary>
    /// <param name="row">The zero-based row index of the element.</param>
    /// <param name="col">The zero-based column index of the element.</param>
    /// <returns>The cofactor of the element at the specified row and column.</returns>
    public double Cofactor(int row, int col)
    {
        if ((row + col) % 2 == 0)
        {
            return Minor(row, col);
        }
        else
            return -(Minor(row, col));
    }

    /// <summary>
    /// Checks if the matrix is invertible.
    /// </summary>
    /// <returns><c>true</c> if the matrix is invertible (its determinant is not zero); otherwise, <c>false</c>.</returns>
    public bool IsInvertible()
    {
        if (FindDeterminant(this) == 0)
            return false;
        return true;
    }

    public Matrix Invert()
    {
        double determinant = FindDeterminant(this);
        Matrix inverted = new(RowCount, ColCount);
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColCount; j++)
            {
                inverted[j, i] = this.Cofactor(i, j) / determinant;
            }
        }

        return inverted;
    }
}
