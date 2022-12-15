namespace BattleSpace.Lib.Test;

public class VectorTest {
    [Fact]
    public void ConstructorVectorTest() {
        var vector = new Vector(1, 2, 3);
        var vector_verify = new Vector(1, 2, 3);

        Assert.Equal(vector, vector_verify);
    }

    [Fact]
    public void GetHashCodeVectorTest() {
        var vector_1 = new Vector(1, 2, 3);
        var vector_2 = new Vector(4, 5, 6);

        var hash_1 = vector_1.GetHashCode();
        var hash_2 = vector_2.GetHashCode();

        bool result = hash_1 != hash_2;

        Assert.True(result, "Hash code generator doesn`t work");
    }

    [Fact]
    public void EqualsVectorTest() {
        var vector = new Vector(1, 2, 3);
        var vector_verify = new Vector(1);

        bool result = vector.Equals(vector_verify);

        Assert.True(result, "Vector is not equals");
    }

    [Fact]
    public void ToStringVectorTest() {
        var vector = new Vector(1, 2, 3);
        var toStringVector = "Vector (1, 2, 3)";

        bool result = toStringVector == vector.ToString();

        Assert.False(result, "Vector string is equal");
    }

    [Fact]
    public void AdditiveVectorTest1() {
        var vector_1 = new Vector(1, 2, 3);
        var vector_2 = new Vector(4, 5, 6);
        var vector_verify = new Vector(5, 7, 9);

        bool result = vector_1 + vector_2 == vector_verify;

        Assert.True(result, "Vectors additive doesn`t work");
    } 

    [Fact]
    public void IndexVectorTest() {
        var vector = new Vector(1, 2, 3);
        var index = 0;

        bool getResult = 1 == vector[index];
        Assert.True(getResult, "Getter doesn`t work");

        vector[index] = 5;
        bool setResult = 5 == vector[index];
        Assert.True(setResult, "Setter doesn`t work");
    }

    [Fact]
    public void AdditiveVectorTest2(){
        var vector_1 = new Vector(1, 2, 3);
        var vector_2 = new Vector(4, 5, 6, 7);

        Assert.Throws<System.ArgumentException>(() => vector_1 + vector_2);
    }

    [Fact]
    public void DifferenceVectorTest1(){
        var vector_1 = new Vector(1, 2, 3);
        var vector_2 = new Vector(4, 5, 6, 7);

        Assert.Throws<System.ArgumentException>(() => vector_1 - vector_2);
    }

    [Fact]
    public void DifferenceVectorTest2() {
        var vector_1 = new Vector(1, 2, 3);
        var vector_2 = new Vector(4, 5, 6);
        var vector_verify = new Vector(-3, -3, -3);

        bool result = vector_1 - vector_2 == vector_verify;

        Assert.True(result, "Vector difference doesn`t work");
    }

    [Fact]
    public void LessOperatorTest1(){
        var vector = new Vector(1, 2, 3);
        var vector2 = new Vector(1, 2, 3);

        bool result = vector < vector2;

        Assert.False(result, "Vector is less than vector2");
    }

    [Fact]
    public void LessOperatorTest2(){
        var vector = new Vector(1, 2, 3);
        var vector2 = new Vector(1, 2, 3, 4);

        bool result = vector < vector2;

        Assert.True(result, "Vector is less than vector2");
    }

    [Fact]
    public void LessOperatorTest4(){
        var vector = new Vector(1, 5, 3);
        var vector2 = new Vector(1, 2, 3);

        bool result = vector < vector2;

        Assert.False(result, "Vector is less than vector2");
    }

    [Fact]
    public void LessOperatorTest3(){
        var vector = new Vector(1, 2, 3);
        var vector2 = new Vector(1, 2);

        bool result = vector < vector2;

        Assert.False(result, "Vector is less than vector2");
    }

    [Fact]
    public void NotEqualTest1(){
        var vector = new Vector(1, 2, 3);
        var vector2 = new Vector(1, 2, 3);

        bool result = vector != vector2;

        Assert.False(result, "Vector is not equal to vector2");
    }

    [Fact]
    public void NotEqualTest2(){
        var vector = new Vector(1, 2, 3, 4);
        var vector2 = new Vector(1, 2, 3);

        bool result = vector != vector2;

        Assert.True(result, "Vector is not equal to vector2");
    }


    [Fact]
    public void EqualTest2(){
        var vector = new Vector(4, 2, 3);
        var vector2 = new Vector(1, 2, 3);

        bool result = vector == vector2;

        Assert.False(result, "Vector is equal to vector2");
    }

    [Fact]
    public void EqualTest1(){
        var vector = new Vector(1, 2, 3);
        var vector2 = new Vector(1, 2, 3);

        bool result = vector == vector2;

        Assert.True(result, "Vector is equal to vector2");
    }

    [Fact]
    public void MultiplicationTest(){
        var vector = new Vector(1, 2, 3);
        int n = 3;

        var mult = n*vector;
        bool result = mult == new Vector(3, 6, 9);

        Assert.True(result, "mult is equal to n * vector");
    }      

    [Fact]
    public void GreaterOperatorTest(){
        var vector = new Vector(1, 2, 3, 4);
        var vector2 = new Vector(1, 2, 3);

        bool result = vector > vector2;

        Assert.True(result, "Vector is more than vector2");
    }
}
