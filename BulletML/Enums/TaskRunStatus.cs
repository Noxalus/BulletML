namespace BulletML.Enums
{
    /// <summary>
    /// These are used for tasks during runtime.
    /// </summary>
    public enum TaskRunStatus
    {
        Continue, // Keep parsing this task
        End, // This task is finished parsing
        Stop // This task is paused
    }
}