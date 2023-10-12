using System;
using System.Collections.Generic;

using Xunit;
using Moq;

using Hwdtech;
using Hwdtech.Ioc;

namespace BattleSpace.Lib.Test {
    public class RegisterAnExceptionHandlerTest {
        public RegisterAnExceptionHandlerTest() {
            new InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

            var handler = new Mock<IHandler>();
            var tree = new Dictionary<int, IHandler>() {
                {0, handler.Object},
                {1, handler.Object},
                {3, handler.Object},
                {666, handler.Object},
                {999, handler.Object}
            };
            var treeExceptionCreateStrategy = new Mock<IStrategy>();
            treeExceptionCreateStrategy.Setup(m => m.ExecuteStrategy()).Returns(tree);
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.ExceptionHandlingTree.Get", (object[] args) => (treeExceptionCreateStrategy.Object.ExecuteStrategy(args))).Execute();

            var returnHashStrategy = new GetHashCodeStrategy();
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.HashCode.Get", (object[] args) => (returnHashStrategy.ExecuteStrategy(args))).Execute();
        }

        [Fact]
        public void registerAnExceptionHandlerTest() {
            var handler = new Mock<IHandler>();
            var list = new List<Type>() {
                typeof(MoveCommand)
            };
            var cmd = new RegisterAnExceptionHandler(list, handler.Object);

            cmd.Execute();

            Assert.Equal(6, IoC.Resolve<IDictionary<int, IHandler>>("Game.ExceptionHandlingTree.Get").Count);
        }
    }
}
