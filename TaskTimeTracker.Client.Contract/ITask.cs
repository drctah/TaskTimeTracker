﻿using System;

namespace TaskTimeTracker.Client.Contract {
  /// <summary>
  /// Interface for Tasks
  /// </summary>
  public interface ITask : IEquatable<ITask>, IComparable<ITask>, IComparable, ICloneable {
    /// <summary>
    /// Date of Creation
    /// </summary>
    EditableDateTime CreationTime { get; set; }

    /// <summary>
    /// The Tag of the Task
    /// </summary>
    string Tag { get; set; }
  }
}
