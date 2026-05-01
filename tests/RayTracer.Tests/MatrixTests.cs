using RayTracer.Cli;

namespace RayTracer.Tests;

public class MatrixTests
{
    [Fact]
    public void MatrixIndexer_FillA4x4Matrix_MatrixIsCorrect()
    {
        Matrix m = new Matrix(4, 4);

        for (int i = 0; i < 4; i++)
            m[0, i] = i + 1;
        for (int i = 0; i < 4; i++)
            m[1, i] = i + 5.5;
        for (int i = 0; i < 4; i++)
            m[2, i] = i + 9;
        for (int i = 0; i < 4; i++)
            m[3, i] = i + 13.5;

        Assert.Equal(1, m[0, 0]);
        Assert.Equal(4, m[0, 3]);
        Assert.Equal(7.5, m[1, 2]);
        Assert.Equal(6.5, m[1, 1]);
        Assert.Equal(10, m[2, 1]);
        Assert.Equal(16.5, m[3, 3]);
    }

    [Fact]
    public void Equals_SimilarMatricesAreEqual_ReturnTrue()
    {
        Matrix m1 = new Matrix(2, 2);
        Matrix m2 = new Matrix(2, 2);

        for (int i = 0; i < 2; i++)
        {
            m1[0, i] = i;
            m2[0, i] = i + 0.0000001;
            m1[1, i] = i;
            m2[1, i] = i + 0.0000001;
        }

        Assert.Equal(m1, m2);
    }

    [Fact]
    public void Equals_DifferentMatricesAreNotEqual_ReturnTrue()
    {
        Matrix m1 = new Matrix(2, 2);
        Matrix m2 = new Matrix(2, 2);

        for (int i = 0; i < 2; i++)
        {
            m1[0, i] = i;
            m2[0, i] = i + 0.1;
        }

        Assert.NotEqual(m1, m2);
    }

    [Fact]
    public void Times_4x4MatrixMultiplication_ResultsInCorrectMatrix()
    {
        Matrix _matrixA = new Matrix(4, 4);
        _matrixA[0, 0] = 1;
        _matrixA[0, 1] = 2;
        _matrixA[0, 2] = 3;
        _matrixA[0, 3] = 4;
        _matrixA[1, 0] = 5;
        _matrixA[1, 1] = 6;
        _matrixA[1, 2] = 7;
        _matrixA[1, 3] = 8;
        _matrixA[2, 0] = 9;
        _matrixA[2, 1] = 8;
        _matrixA[2, 2] = 7;
        _matrixA[2, 3] = 6;
        _matrixA[3, 0] = 5;
        _matrixA[3, 1] = 4;
        _matrixA[3, 2] = 3;
        _matrixA[3, 3] = 2;

        Matrix _matrixB = new Matrix(4, 4);
        _matrixB[0, 0] = -2;
        _matrixB[0, 1] = 1;
        _matrixB[0, 2] = 2;
        _matrixB[0, 3] = 3;
        _matrixB[1, 0] = 3;
        _matrixB[1, 1] = 2;
        _matrixB[1, 2] = 1;
        _matrixB[1, 3] = -1;
        _matrixB[2, 0] = 4;
        _matrixB[2, 1] = 3;
        _matrixB[2, 2] = 6;
        _matrixB[2, 3] = 5;
        _matrixB[3, 0] = 1;
        _matrixB[3, 1] = 2;
        _matrixB[3, 2] = 7;
        _matrixB[3, 3] = 8;

        Matrix _expectedProduct = new Matrix(4, 4);
        _expectedProduct[0, 0] = 20;
        _expectedProduct[0, 1] = 22;
        _expectedProduct[0, 2] = 50;
        _expectedProduct[0, 3] = 48;
        _expectedProduct[1, 0] = 44;
        _expectedProduct[1, 1] = 54;
        _expectedProduct[1, 2] = 114;
        _expectedProduct[1, 3] = 108;
        _expectedProduct[2, 0] = 40;
        _expectedProduct[2, 1] = 58;
        _expectedProduct[2, 2] = 110;
        _expectedProduct[2, 3] = 102;
        _expectedProduct[3, 0] = 16;
        _expectedProduct[3, 1] = 26;
        _expectedProduct[3, 2] = 46;
        _expectedProduct[3, 3] = 42;

        Assert.Equal(_expectedProduct, _matrixA * _matrixB);
    }
}
