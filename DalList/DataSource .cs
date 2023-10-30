
namespace Dal;

internal static class DataSource
{
    internal static List<DO.Dependency> Dependencies { get; } = new();
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Task> Tasks { get; } = new();
    internal static class Config//class of running numbers
    {
        internal const int startTaskId = 1;
        private static int nextTaskId = startTaskId;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId
        {
            get => nextTaskId++;
        }
        internal const int startDependencyId = 1;
        private static int nextDependencyId = startDependencyId;
        internal static int NextDependencyId
        {
            get => nextDependencyId++;
        }

    }
}
