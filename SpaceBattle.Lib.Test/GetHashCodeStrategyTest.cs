using System;
using System.IO;
using System.Collections.Generic;

using Xunit;
using Moq;

using Hwdtech;
using Hwdtech.Ioc;

namespace BattleSpace.Lib.Test {
    public class GetHashCodeStrategyTest {
        [Fact]
        public void equalHashCodesForIdenticalObjectsTest() {

            var list1 = new List<Type>() {
                typeof(ArgumentException),
                typeof(IOException)
            };

            var list2 = new List<Type>() {
                typeof(IOException),
                typeof(ArgumentException)
            };

            var getHashCodeStrategy = new GetHashCodeStrategy();

            Assert.Equal(getHashCodeStrategy.ExecuteStrategy(list1), getHashCodeStrategy.ExecuteStrategy(list2));
        }

        [Fact]
        public void notEqualHashCodeForNotIdenticalObjects() {
            var list1 = new List<Type>() {
                typeof(ArgumentException),
                typeof(IOException)
            };

            var list2 = new List<Type>() {
                typeof(IOException)
            };

            var getHashCodeStrategy = new GetHashCodeStrategy();

            Assert.NotEqual(getHashCodeStrategy.ExecuteStrategy(list1), getHashCodeStrategy.ExecuteStrategy(list2));
        }

        [Fact]
        public void EqualHashCodeForDifferentPermutationsOfObjectsTest() {
            var list1 = new List<Type>() {
                typeof(ArgumentException),
                typeof(IOException)
            };

            var list2 = new List<Type>() {
                typeof(IOException),
                typeof(ArgumentException)
            };

            var getHashCodeStrategy = new GetHashCodeStrategy();

            Assert.Equal(getHashCodeStrategy.ExecuteStrategy(list1), getHashCodeStrategy.ExecuteStrategy(list2));
        }
    }
}
