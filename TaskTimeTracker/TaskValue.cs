using TaskTimeTracker.Client.Contract;

namespace TaskTimeTracker.Client {
  class TaskValue : ITask {
    public EditableDateTime CreationTime { get; set; }
    public string Tag { get; set; }

    public TaskValue(Task task) {
      this.Tag = task.Tag;
      this.CreationTime = new EditableDateTime(task.CreationTime);
    }
  }
}