using System;

namespace TaskTimeTracker.Client.Contract
{
  /// <summary>
  /// Interface for Tasks
  /// </summary>
  public interface ITask : ITaskTimeTrackerContractObject<ITask>
  {
    /// <summary>
    /// Date of Creation
    /// </summary>
    EditableDateTime CreationTime { get; set; }

    /// <summary>
    /// The Tag of the Task
    /// </summary>
    string Tag { get; set; }
  }

  /// <summary>
  /// 
  /// </summary>
  public interface ITaskCollection : ITaskTimeTrackerContractObjectCollection<ITaskCollection, ITask>
  {
  }
}
