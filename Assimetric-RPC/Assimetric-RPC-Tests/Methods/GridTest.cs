using Assimetric_RPC.Methods;

namespace Assimetric_RPC_Tests.Methods
{
    public class GridTest
    {
        public static List<object[]> GridObjects => new()
        {
            new object[]
            {
                2,
                2,
                new List<bool> 
                { 
                    true, true, 
                    true, false
                } 
            },
            new object[]
            {
                5,
                5,
                new List<bool> 
                { 
                    true, true, true, true, true,
                    true, false, false, false, false,
                    true, false, false, false, false,
                    true, false, false, false, false,
                    true, false, false, false, false,
                }
            }
        };

        [Theory]
        [MemberData(nameof(GridObjects))]
        public void BuildSchedule(int n, int m, List<bool> expectedSchedule)
        {
            // Arrange

            // Act
            Grid grid = new Grid(n, m);

            // Assert
            Assert.Equal(expectedSchedule.Count, grid.ScheduleSize);
            Assert.Equal(expectedSchedule, grid.Schedule);
            Assert.Equal(n * m, grid.ScheduleSize);
            Assert.Equal(n + m - 1, grid.ActiveSlotsCount);
        }
    }
}
