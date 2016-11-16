using System;
using System.IO;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TaskTimeTracker.Client.Contract;

namespace TaskTimeTracker.Client.Data.Tests
{
  [TestClass()]
  public class SerializationTests
  {
    private Random random;

    private TestHelper helper;

    public SerializationTests()
    {
      this.random = new Random();
      this.helper = new TestHelper();
    }

    private Task CreateRandomTask()
    {
      Task task = new Task();
      task.CreationTime = new EditableDateTime(this.helper.GetRandomDateTime(this.random));
      task.Tag = this.helper.GetRandomString(this.random, 32);
      return task;
    }

    private string TestSaveable(ISaveable saveable)
    {
      StringBuilder sb = new StringBuilder();
      using (TextWriter tw = new StringWriter(sb)) {
        saveable.Save(tw);
      }

      return sb.ToString();
    }

    private void TestLoadable(ILoadable loadable, string data)
    {
      using (TextReader reader = new StringReader(data)) {
        loadable.Load(reader);
      }
    }

    private void TestBusinessObject<T, TInterface>(T value1)
      where T : ClientBusinessBase<TInterface>, ILoadable, ISaveable, new()
      where TInterface : ITaskTimeTrackerContractObject<TInterface>
    {
      T value2 = new T();
      string data = this.TestSaveable(value1);
      this.TestLoadable(value2, data);
      Assert.AreEqual(0, ClientBusinessBase<TInterface>.Compare(value1, value2));
    }

    [TestMethod()]
    public void Task()
    {
      Task v = this.CreateRandomTask();
      this.TestBusinessObject<Task, ITask>(v);

      //StringBuilder sb = new StringBuilder();
      //using (TextWriter tw = new StringWriter(sb)) {
      //  task1.Save(tw);
      //}

      //string text = sb.ToString();

      //Task task2 = new Task();
      //using (TextReader reader = new StringReader(text)) {
      //  task2.Load(reader);
      //}

      //Assert.AreEqual(0, Data.Task.Compare(task1, task2));
    }

    [TestMethod()]
    public void TaskCollection()
    {
      TaskCollection tasks1 = new TaskCollection();

      tasks1.Add(this.CreateRandomTask());
      tasks1.Add(this.CreateRandomTask());

      //this.TestBusinessObject<TaskCollection, ITaskCollection>(tasks1);

      //StringBuilder sb = new StringBuilder();
      //using (TextWriter tw = new StringWriter(sb)) {
      //  tasks1.Save(tw);
      //}

      //string text = sb.ToString();

      //TaskCollection tasks2 = new TaskCollection();
      //using (TextReader reader = new StringReader(text)) {
      //  tasks2.Load(reader);
      //}

      //Assert.AreEqual(0, Data.TaskCollection.Compare(tasks1, tasks2));
    }
  }
}
