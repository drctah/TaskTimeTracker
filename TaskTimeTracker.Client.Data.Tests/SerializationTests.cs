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
      where T : ClientBusinessBase<TInterface, T>, TInterface, ILoadable, ISaveable, new()
      where TInterface : ITaskTimeTrackerContractObject<TInterface>
    {
      T value2 = new T();
      string data = this.TestSaveable(value1);
      this.TestLoadable(value2, data);
      Assert.AreEqual(0, ClientBusinessBase<TInterface, T>.Compare(value1, value2));
    }

    [TestMethod()]
    public void Task()
    {
      Task v = this.CreateRandomTask();
      this.TestBusinessObject<Task, ITask>(v);
    }

    [TestMethod()]
    public void TaskCollection()
    {
      TaskCollection tasks1 = new TaskCollection();

      for(int cnt = 0; cnt < 10; ++cnt) {
        tasks1.Add(this.CreateRandomTask());
      }

      this.TestBusinessObject<TaskCollection, ITaskCollection>(tasks1);
    }
  }
}
