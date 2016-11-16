using System;
using TaskTimeTracker.Client.Contract;

namespace TaskTimeTracker.Client.Data
{
  public class Task : ClientBusinessBase<ITask>
  {
    public EditableDateTime CreationTime { get; set; }

    public string Tag { get; set; }

    public Task()
      : this(DateTime.MinValue, string.Empty)
    {
    }

    public Task(DateTime time)
      : this(time, string.Empty)
    {
    }

    public Task(string tag)
      : this(DateTime.Now, tag)
    {
    }

    public Task(DateTime time, string tag)
    {
      this.CreationTime = new EditableDateTime(time);
      this.Tag = tag;
    }

    public Task(ITask source)
    {
      this.CreationTime = new EditableDateTime(source.CreationTime);
      this.Tag = source.Tag;
    }

    public static int Compare(ITask v1, ITask v2)
    {
      bool n1 = object.ReferenceEquals(v1, null);
      bool n2 = object.ReferenceEquals(v2, null);

      if (n1 ^ n2) {
        return n1 ? -1 : 1;
      }

      if (n1) {
        return 0;
      }

      int result = string.Compare(v1.Tag, v2.Tag);
      if (result != 0) {
        return result;
      }

      n1 = object.ReferenceEquals(v1.CreationTime, null);
      n2 = object.ReferenceEquals(v2.CreationTime, null);

      if (n1 ^ n2) {
        return n1 ? -1 : 1;
      }

      if (n1) {
        return 0;
      }

      result = v1.CreationTime.ToDateTime().CompareTo(v2.CreationTime.ToDateTime());
      return result;
    }

    public override string ToString()
    {
      if (this.CreationTime == null) {
        return "empty task";
      }

      return string.Format("task {0}"
        , this.CreationTime
        );
    }

    public override int GetHashCode()
    {
      int hash = 0;

      if (!string.IsNullOrEmpty(this.Tag)) {
        hash = this.Tag.GetHashCode();
      }

      if (this.CreationTime != null) {
        hash ^= this.CreationTime.GetHashCode();
      }

      return hash;
    }

    //public override bool Equals(object obj)
    //{
    //  if (obj is ITask) {
    //    return Task.Compare(this, (ITask)obj) == 0;
    //  }

    //  return false;
    //}

    //public int CompareTo(object obj)
    //{
    //  if (obj is ITask) {
    //    return Task.Compare(this, (ITask)obj);
    //  }

    //  return 1;
    //}

    //public int CompareTo(ITask other)
    //{
    //  return Task.Compare(this, other);
    //}

    //public bool Equals(ITask other)
    //{
    //  return Task.Compare(this, other) == 0;
    //}

    public override object Clone()
    {
      return new Task((ITask)this);
    }

    public override int CompareTo(ITaskTimeTrackerContractObject<ITask> other)
    {
      Task obj = other as Task;
      if (object.ReferenceEquals(obj, null)) {
        return 1;
      }

      int result = string.Compare(this.Tag, obj.Tag, StringComparison.OrdinalIgnoreCase);
      if (result != 0) {
        return result;
      }

      bool n1 = object.ReferenceEquals(this.CreationTime, null);
      bool n2 = object.ReferenceEquals(obj.CreationTime, null);

      if (n1 ^ n2) {
        return n1 ? -1 : 1;
      }

      if (n1) {
        return 0;
      }

      return this.CreationTime.ToDateTime().CompareTo(obj.CreationTime.ToDateTime());
    }
  }

  public class TaskCollection : ClientBusinessCollectionBase<ITaskCollection, ITask>
  {
  }
}
