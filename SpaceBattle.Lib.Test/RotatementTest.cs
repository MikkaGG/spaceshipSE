using Moq;
using Xunit;

namespace BattleSpace.Lib.Test;
public class RotatementTest {
    [Fact]
    public void ChangeAngleTest() {
        var rotate_test = new Mock<IRotatable>();
        rotate_test.Setup(m => m.Angle).Returns(45);
        rotate_test.Setup(m => m.AngleVelocity).Returns(90);
        
        var rotate_command = new RotateCommand(rotate_test.Object);
        rotate_command.Execute();
        rotate_test.VerifySet(m => m.Angle = 135);
    }

    [Fact]
    public void UnreadableAngleTest(){
        var rotate_test = new Mock<IRotatable>();
        rotate_test.SetupGet(m => m.Angle).Throws<Exception>();
        rotate_test.SetupGet(m => m.AngleVelocity).Returns(90);

        RotateCommand command = new RotateCommand(rotate_test.Object);
        Assert.Throws<Exception>(() => command.Execute());
    }

    [Fact]
    public void UnreadableAngleVelocityTest(){
        var rotate_test = new Mock<IRotatable>();
        rotate_test.SetupProperty(m => m.Angle, 45);
        rotate_test.SetupGet(m => m.AngleVelocity).Throws<Exception>();

        RotateCommand command = new RotateCommand(rotate_test.Object);
        Assert.Throws<Exception>(() => command.Execute());
    }

    [Fact]
    public void ImmutableAngleTest(){
        var rotate_test = new Mock<IRotatable>();
        rotate_test.SetupProperty(m => m.Angle, 45);
        rotate_test.SetupGet(m => m.AngleVelocity).Returns(90);
        rotate_test.SetupSet(m => m.Angle = It.IsAny<int>()).Throws<Exception>();   
        
        RotateCommand command = new RotateCommand(rotate_test.Object);
        Assert.Throws<Exception>(() => command.Execute());
    }


}
