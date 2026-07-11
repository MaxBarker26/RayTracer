namespace RayTracer.Tests;

using RayTracer.Cli;

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

    [Fact]
    public void Times_4x4MatrixMultipliedWithTuple_ResultsInCorrectTuple()
    {
        Matrix matrix = new Matrix(4, 4);
        matrix[0, 0] = 1;
        matrix[0, 1] = 2;
        matrix[0, 2] = 3;
        matrix[0, 3] = 4;
        matrix[1, 0] = 5;
        matrix[1, 1] = 6;
        matrix[1, 2] = 7;
        matrix[1, 3] = 8;
        matrix[2, 0] = 9;
        matrix[2, 1] = 8;
        matrix[2, 2] = 7;
        matrix[2, 3] = 6;
        matrix[3, 0] = 5;
        matrix[3, 1] = 4;
        matrix[3, 2] = 3;
        matrix[3, 3] = 2;

        Tuple tuple = new(1, 2, 3, 1);

        Tuple expected = new(18, 46, 52, 24);

        Assert.Equal(expected, matrix * tuple);
    }

    [Fact]
    public void Times_MatrixMultipliedWithIdentityMatrix_ResultsInTheSameMatrix()
    {
        Matrix matrix = new Matrix(4, 4);
        matrix[0, 0] = 1;
        matrix[0, 1] = 2;
        matrix[0, 2] = 3;
        matrix[0, 3] = 4;
        matrix[1, 0] = 5;
        matrix[1, 1] = 6;
        matrix[1, 2] = 7;
        matrix[1, 3] = 8;
        matrix[2, 0] = 9;
        matrix[2, 1] = 8;
        matrix[2, 2] = 7;
        matrix[2, 3] = 6;
        matrix[3, 0] = 5;
        matrix[3, 1] = 4;
        matrix[3, 2] = 3;
        matrix[3, 3] = 2;

        Matrix identity = Matrix.Identity(4);

        Assert.Equal(matrix, matrix * identity);
    }

    [Fact]
    public void Tranpose_TheIdentityMatrixTransposedIsStillTheIdentityMatrix_TransposedIDMatrixEqualsIdMatrix()
    {
        Matrix identity = Matrix.Identity(4);

        Matrix transposed = identity.Transpose();

        Assert.Equal(identity, transposed);
    }

    [Fact]
    public void Transpose_2x2MatrixRowsAndColumnsSwap_TransposeReturnsNewTransposedMatrix()
    {
        Matrix m = new(2, 2);
        m[0, 0] = 1;
        m[0, 1] = 2;
        m[1, 0] = 3;
        m[1, 1] = 4;

        Matrix expected = new(2, 2);

        expected[0, 0] = 1;
        expected[0, 1] = 3;
        expected[1, 0] = 2;
        expected[1, 1] = 4;

        Matrix transposed = m.Transpose();

        Assert.Equal(expected, transposed);
    }

    [Fact]
    public void Determinant_FindDeterminantOf2x2Matrix_DeterminantIsExpected()
    {
        Matrix m = new(2, 2);
        m[0, 0] = 1;
        m[0, 1] = 5;
        m[1, 0] = -3;
        m[1, 1] = 2;

        double determinant = Matrix.FindDeterminant(m);
        Assert.Equal(17, determinant);
    }

    [Fact]
    public void Submatrix_3x3SubmatrixBecomes2x2_ReturnsExpected2x2SubMatrix()
    {
        Matrix m = new(3, 3);
        m[0, 0] = 4;
        m[0, 1] = 1;
        m[0, 2] = 5;
        m[1, 0] = -7;
        m[1, 1] = -3;
        m[1, 2] = 2;
        m[2, 0] = 4;
        m[2, 1] = 99;
        m[2, 2] = -9;

        Matrix expected = new(2, 2);
        expected[0, 0] = m[1, 0];
        expected[0, 1] = m[1, 1];
        expected[1, 0] = m[2, 0];
        expected[1, 1] = m[2, 1];

        Assert.Equal(expected, m.Submatrix(0, 2));
    }

    [Fact]
    public void Submatrix_4x4SubmatrixRemoveMiddleRowsAndColumns_Resulting3x3SubmatrixIsCorrect()
    {
        Matrix matrix = new Matrix(4, 4);
        matrix[0, 0] = 1;
        matrix[0, 1] = 2;
        matrix[0, 2] = 3;
        matrix[0, 3] = 4;
        matrix[1, 0] = 5;
        matrix[1, 1] = 6;
        matrix[1, 2] = 7;
        matrix[1, 3] = 8;
        matrix[2, 0] = 9;
        matrix[2, 1] = 8;
        matrix[2, 2] = 7;
        matrix[2, 3] = 6;
        matrix[3, 0] = 5;
        matrix[3, 1] = 4;
        matrix[3, 2] = 3;
        matrix[3, 3] = 2;

        Matrix expected = new Matrix(3, 3);
        expected[0, 0] = 1;
        expected[0, 1] = 2;
        expected[0, 2] = 4;
        expected[1, 0] = 9;
        expected[1, 1] = 8;
        expected[1, 2] = 6;
        expected[2, 0] = 5;
        expected[2, 1] = 4;
        expected[2, 2] = 2;

        Assert.Equal(expected, matrix.Submatrix(1, 2));
    }

    [Fact]
    public void Minor_FindMinorOf3x3Matrix_MinorIsDeterminantOfSubmatrix()
    {
        Matrix m = new(3, 3);
        m[0, 0] = 4;
        m[0, 1] = 1;
        m[0, 2] = 5;
        m[1, 0] = -7;
        m[1, 1] = -3;
        m[1, 2] = 2;
        m[2, 0] = 4;
        m[2, 1] = 99;
        m[2, 2] = -9;

        double minor = Matrix.FindDeterminant(m.Submatrix(0, 2));

        Assert.Equal(minor, m.Minor(0, 2));
    }

    [Fact]
    public void Cofactor_FindCofactorsOf3x3Matrix_CofactorSignIsCorrect()
    {
        Matrix m = new(3, 3);
        m[0, 0] = 4;
        m[0, 1] = 1;
        m[0, 2] = 5;
        m[1, 0] = -7;
        m[1, 1] = -3;
        m[1, 2] = 2;
        m[2, 0] = 4;
        m[2, 1] = 99;
        m[2, 2] = -9;

        double positiveCofactor = Matrix.FindDeterminant(m.Submatrix(0, 2));
        double negativeCofactor = -(Matrix.FindDeterminant(m.Submatrix(0, 1)));

        Assert.Equal(positiveCofactor, m.Cofactor(0, 2));
        Assert.Equal(negativeCofactor, m.Cofactor(0, 1));
    }

    [Fact]
    public void Determinant_DeterminantOf3x3Matrix_DeterminantIsCorrect()
    {
        Matrix matrix = new Matrix(3, 3);
        matrix[0, 0] = 1;
        matrix[0, 1] = 2;
        matrix[0, 2] = 6;
        matrix[1, 0] = -5;
        matrix[1, 1] = 8;
        matrix[1, 2] = -4;
        matrix[2, 0] = 2;
        matrix[2, 1] = 6;
        matrix[2, 2] = 4;

        Assert.Equal(56, matrix.Cofactor(0, 0));
        Assert.Equal(12, matrix.Cofactor(0, 1));
        Assert.Equal(-46, matrix.Cofactor(0, 2));
        Assert.Equal(-196, Matrix.FindDeterminant(matrix));
    }

    [Fact]
    public void Determinant_DeterminantOf4x4Matrix_DeterminantIsCorrect()
    {
        Matrix matrix = new Matrix(4, 4);
        matrix[0, 0] = -2;
        matrix[0, 1] = -8;
        matrix[0, 2] = 3;
        matrix[0, 3] = 5;
        matrix[1, 0] = -3;
        matrix[1, 1] = 1;
        matrix[1, 2] = 7;
        matrix[1, 3] = 3;
        matrix[2, 0] = 1;
        matrix[2, 1] = 2;
        matrix[2, 2] = -9;
        matrix[2, 3] = 6;
        matrix[3, 0] = -6;
        matrix[3, 1] = 7;
        matrix[3, 2] = 7;
        matrix[3, 3] = -9;

        Assert.Equal(690, matrix.Cofactor(0, 0));
        Assert.Equal(447, matrix.Cofactor(0, 1));
        Assert.Equal(210, matrix.Cofactor(0, 2));
        Assert.Equal(51, matrix.Cofactor(0, 3));
        Assert.Equal(-4071, Matrix.FindDeterminant(matrix));
    }

    [Fact]
    public void IsInvertible_TestIfAMatrixIsInvertible_ReturnTrue()
    {
        Matrix matrix = new Matrix(4, 4);
        matrix[0, 0] = 6;
        matrix[0, 1] = 4;
        matrix[0, 2] = 4;
        matrix[0, 3] = 4;
        matrix[1, 0] = 5;
        matrix[1, 1] = 5;
        matrix[1, 2] = 7;
        matrix[1, 3] = 6;
        matrix[2, 0] = 4;
        matrix[2, 1] = -9;
        matrix[2, 2] = 3;
        matrix[2, 3] = -7;
        matrix[3, 0] = 9;
        matrix[3, 1] = 1;
        matrix[3, 2] = 7;
        matrix[3, 3] = -6;

        Assert.Equal(-2120, Matrix.FindDeterminant(matrix));

        Assert.True(matrix.IsInvertible());
    }

    [Fact]
    public void IsInvertible_TestIfAMatrixIsInvertible_ReturnFalse()
    {
        Matrix matrix = new Matrix(4, 4);
        matrix[0, 0] = -4;
        matrix[0, 1] = 2;
        matrix[0, 2] = -2;
        matrix[0, 3] = -3;
        matrix[1, 0] = 9;
        matrix[1, 1] = 6;
        matrix[1, 2] = 2;
        matrix[1, 3] = 6;
        matrix[2, 0] = 0;
        matrix[2, 1] = -5;
        matrix[2, 2] = 1;
        matrix[2, 3] = -5;
        matrix[3, 0] = 0;
        matrix[3, 1] = 0;
        matrix[3, 2] = 0;
        matrix[3, 3] = 0;

        Assert.Equal(0, Matrix.FindDeterminant(matrix));

        Assert.False(matrix.IsInvertible());
    }

    [Fact]
    public void Invert_InvertingA4x4Matrix_ResultIsExpectedInverse()
    {
        Matrix matrix = new Matrix(4, 4);
        matrix[0, 0] = -5;
        matrix[0, 1] = 2;
        matrix[0, 2] = 6;
        matrix[0, 3] = -8;
        matrix[1, 0] = 1;
        matrix[1, 1] = -5;
        matrix[1, 2] = 1;
        matrix[1, 3] = 8;
        matrix[2, 0] = 7;
        matrix[2, 1] = 7;
        matrix[2, 2] = -6;
        matrix[2, 3] = -7;
        matrix[3, 0] = 1;
        matrix[3, 1] = -3;
        matrix[3, 2] = 7;
        matrix[3, 3] = 4;

        Matrix matrix2 = new Matrix(4, 4);
        matrix2[0, 0] = 0.21805;
        matrix2[0, 1] = 0.45113;
        matrix2[0, 2] = 0.24060;
        matrix2[0, 3] = -0.04511;
        matrix2[1, 0] = -0.80827;
        matrix2[1, 1] = -1.45677;
        matrix2[1, 2] = -0.44361;
        matrix2[1, 3] = 0.52068;
        matrix2[2, 0] = -0.07895;
        matrix2[2, 1] = -0.22368;
        matrix2[2, 2] = -0.05263;
        matrix2[2, 3] = 0.19737;
        matrix2[3, 0] = -0.52256;
        matrix2[3, 1] = -0.81391;
        matrix2[3, 2] = -0.30075;
        matrix2[3, 3] = 0.30639;

        Assert.Equal(532, Matrix.FindDeterminant(matrix));
        Assert.Equal(-160, matrix.Cofactor(2, 3));
        Assert.Equal((double)-160 / 532, matrix2[3, 2], 0.0001);
        Assert.Equal(matrix2[3, 2], matrix.Invert()[3, 2], 0.0001);

        Assert.Equal(matrix.Cofactor(2, 0) / 532, matrix2[0, 2], 0.0001);

        Assert.Equal(matrix2, matrix.Invert());
    }

    [Fact]
    public void Invert_AdditionalInversionTests_InvertedIsCorrect()
    {
        Matrix matrixA = new Matrix(4, 4);
        matrixA[0, 0] = 8;
        matrixA[0, 1] = -5;
        matrixA[0, 2] = 9;
        matrixA[0, 3] = 2;
        matrixA[1, 0] = 7;
        matrixA[1, 1] = 5;
        matrixA[1, 2] = 6;
        matrixA[1, 3] = 1;
        matrixA[2, 0] = -6;
        matrixA[2, 1] = 0;
        matrixA[2, 2] = 9;
        matrixA[2, 3] = 6;
        matrixA[3, 0] = -3;
        matrixA[3, 1] = 0;
        matrixA[3, 2] = -9;
        matrixA[3, 3] = -4;

        Matrix inverseA = new Matrix(4, 4);
        inverseA[0, 0] = -0.15385;
        inverseA[0, 1] = -0.15385;
        inverseA[0, 2] = -0.28205;
        inverseA[0, 3] = -0.53846;
        inverseA[1, 0] = -0.07692;
        inverseA[1, 1] = 0.12308;
        inverseA[1, 2] = 0.02564;
        inverseA[1, 3] = 0.03077;
        inverseA[2, 0] = 0.35897;
        inverseA[2, 1] = 0.35897;
        inverseA[2, 2] = 0.43590;
        inverseA[2, 3] = 0.92308;
        inverseA[3, 0] = -0.69231;
        inverseA[3, 1] = -0.69231;
        inverseA[3, 2] = -0.76923;
        inverseA[3, 3] = -1.92308;

        Matrix matrixB = new Matrix(4, 4);
        matrixB[0, 0] = 9;
        matrixB[0, 1] = 3;
        matrixB[0, 2] = 0;
        matrixB[0, 3] = 9;
        matrixB[1, 0] = -5;
        matrixB[1, 1] = -2;
        matrixB[1, 2] = -6;
        matrixB[1, 3] = -3;
        matrixB[2, 0] = -4;
        matrixB[2, 1] = 9;
        matrixB[2, 2] = 6;
        matrixB[2, 3] = 4;
        matrixB[3, 0] = -7;
        matrixB[3, 1] = 6;
        matrixB[3, 2] = 6;
        matrixB[3, 3] = 2;

        Matrix inverseB = new Matrix(4, 4);
        inverseB[0, 0] = -0.04074;
        inverseB[0, 1] = -0.07778;
        inverseB[0, 2] = 0.14444;
        inverseB[0, 3] = -0.22222;
        inverseB[1, 0] = -0.07778;
        inverseB[1, 1] = 0.03333;
        inverseB[1, 2] = 0.36667;
        inverseB[1, 3] = -0.33333;
        inverseB[2, 0] = -0.02901;
        inverseB[2, 1] = -0.14630;
        inverseB[2, 2] = -0.10926;
        inverseB[2, 3] = 0.12963;
        inverseB[3, 0] = 0.17778;
        inverseB[3, 1] = 0.06667;
        inverseB[3, 2] = -0.26667;
        inverseB[3, 3] = 0.33333;

        Assert.Equal(inverseA, matrixA.Invert());
        Assert.Equal(inverseB, matrixB.Invert());
    }

    [Fact]
    public void ViewTransformationMatrix_TransformationMatrixForDefaultView_EqualsIdentityMatrix()
    {
        Point from = new(0, 0, 0);
        Point to = new(0, 0, -1);
        Vector up = new(0, 1, 0);
        Matrix t = Matrix.View(from, to, up);
        Assert.Equal(Matrix.Identity(), t);
    }

    [Fact]
    public void ViewTransformationMatrix_TransformationMatrixPositiveZTo_EqualsNegativeScalingMatrix()
    {
        Point from = new(0, 0, 0);
        Point to = new(0, 0, 1);
        Vector up = new(0, 1, 0);
        Matrix t = Matrix.View(from, to, up);
        //This test should result in the mirror image accross
        //z axis, the same as reflection (negative scaling).
        Assert.Equal(Matrix.Scaling(-1, 1, -1), t);
    }

    [Fact]
    public void ViewTransformationMatrix_TransformationMatrixWorldMoves_WorldTranslatesOppositeOfEyePosition()
    {
        Point from = new(0, 0, 8);
        Point to = new(0, 0, 0);
        Vector up = new(0, 1, 0);
        Matrix t = Matrix.View(from, to, up);
        //This test should result in the world being translated opposite from the eye's from position
        Assert.Equal(Matrix.Translation(0, 0, -8), t);
    }

    [Fact]
    public void ViewTransformationMatrix_ArbitraryTransformation_MatrixEqualsExpected()
    {
        Point from = new(1, 3, 2);
        Point to = new(4, -2, 8);
        Vector up = new(1, 1, 0);
        Matrix t = Matrix.View(from, to, up);
        Matrix expected = new Matrix(4, 4);
        expected[0, 0] = -0.50709;
        expected[0, 1] = 0.50709;
        expected[0, 2] = 0.67612;
        expected[0, 3] = -2.36643;
        expected[1, 0] = 0.76772;
        expected[1, 1] = 0.60609;
        expected[1, 2] = 0.12122;
        expected[1, 3] = -2.82843;
        expected[2, 0] = -0.35857;
        expected[2, 1] = 0.59761;
        expected[2, 2] = -0.71714;
        expected[2, 3] = 0.00000;
        expected[3, 0] = 0.00000;
        expected[3, 1] = 0.00000;
        expected[3, 2] = 0.00000;
        expected[3, 3] = 1.00000;
        Assert.Equal(expected, t);
    }

    [Fact]
    public void CameraRelativeTranslation_MoveObjectLeftRelativeToCamera_ObjectIsExpected()
    {
        Camera c = new(160, 120, Math.PI / 2);
        c.Transform = Matrix.View(new(0, 0, 0), new(1, 0, 0), new(0, 1, 0));
        Sphere s = new("s");
        s.TransformMatrix = Matrix.CameraRelativeTranslation(c, 5, 0, 0) * s.TransformMatrix;

        Assert.Equal(Matrix.Translation(0, 0, 5), s.TransformMatrix);
    }

    [Fact]
    public void CameraRelativeTranslation_MoveObjectRightRelativeToCamera_ObjectIsExpected()
    {
        Camera c = new(160, 120, Math.PI / 2);
        c.Transform = Matrix.View(new(0, 0, 0), new(1, 0, 0), new(0, 1, 0));
        Sphere s = new("s");
        s.TransformMatrix = Matrix.CameraRelativeTranslation(c, -5, 0, 0) * s.TransformMatrix;

        Assert.Equal(Matrix.Translation(0, 0, -5), s.TransformMatrix);
    }

    [Fact]
    public void CameraRelativeRotation_XAxisPitch_MatchesWorldZ()
    {
        Camera c = new(160, 120, Math.PI / 2);
        c.Transform = Matrix.View(new(0, 0, 0), new(1, 0, 0), new(0, 1, 0));

        Matrix result = Matrix.CameraRelativeRotationX(c, Math.PI / 2);

        Assert.Equal(Matrix.RotationZ(Math.PI / 2), result);
    }

    [Fact]
    public void CameraRelativeRotation_YAxisYaw_MatchesWorldY()
    {
        Camera c = new(160, 120, Math.PI / 2);
        c.Transform = Matrix.View(new(0, 0, 0), new(1, 0, 0), new(0, 1, 0));

        Matrix result = Matrix.CameraRelativeRotationY(c, Math.PI / 2);

        Assert.Equal(Matrix.RotationY(Math.PI / 2), result);
    }

    [Fact]
    public void CameraRelativeRotation_ZAxisRoll_MatchesInvertedWorldX()
    {
        Camera c = new(160, 120, Math.PI / 2);
        c.Transform = Matrix.View(new(0, 0, 0), new(1, 0, 0), new(0, 1, 0));

        Matrix result = Matrix.CameraRelativeRotationZ(c, Math.PI / 2);

        Assert.Equal(Matrix.RotationX(-Math.PI / 2), result);
    }
}
