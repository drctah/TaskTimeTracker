using System;
using System.IO;

using Newtonsoft.Json;
using TaskTimeTracker.Client.Contract;

namespace TaskTimeTracker.Client.Data
{
  public abstract class ClientBusinessBase<TInterface, TType> : ITaskTimeTrackerContractObject<TInterface>, ISaveable, ILoadable
    where TInterface : ITaskTimeTrackerContractObject<TInterface>
    where TType : TInterface
  {
    protected JsonSerializer serializer = new JsonSerializer();

    protected virtual void Serialize(TextWriter writer, object value)
    {
      this.serializer.Serialize(writer, value);
    }

    public virtual void Save(TextWriter writer)
    {
      this.serializer.Serialize(writer, this, this.GetType());
    }

    public void Save(Stream stream)
    {
      using (TextWriter writer = new StreamWriter(stream)) {
        this.Save(writer);
      }
    }

    public void Save(string fileName)
    {
      using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read)) {
        this.Save(fs);
      }
    }

    public void Load(string fileName)
    {
      using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)) {
        this.Load(fs);
      }
    }

    public void Load(Stream stream)
    {
      using (TextReader reader = new StreamReader(stream)) {
        this.Load(reader);
      }
    }

    public void Load(TextReader reader)
    {
      LoadInternal(this.serializer, reader);
    }

    protected virtual void LoadInternal(JsonSerializer serializer, TextReader reader)
    {
      serializer.Populate(reader, this);
    }

    public static int Compare(ITaskTimeTrackerContractObject<TInterface> v1, ITaskTimeTrackerContractObject<TInterface> v2)
    {
      bool n1 = object.ReferenceEquals(v1, null);
      bool n2 = object.ReferenceEquals(v2, null);

      if (n1 ^ n2) {
        return n1 ? -1 : 1;
      }

      if (n1) {
        return 0;
      }

      return v1.CompareTo(v2);
    }

    public virtual bool Equals(ITaskTimeTrackerContractObject<TInterface> other)
    {
      return ClientBusinessBase<TInterface, TType>.Compare(this, (ITaskTimeTrackerContractObject<TInterface>)other) == 0;
    }

    public override bool Equals(object obj)
    {
      if (obj is ITaskTimeTrackerContractObject<TInterface>) {
        return ClientBusinessBase<TInterface, TType>.Compare(this, (ITaskTimeTrackerContractObject<TInterface>)obj) == 0;
      }

      return false;
    }

    public virtual int CompareTo(object obj)
    {
      if (obj is ITaskTimeTrackerContractObject<TInterface>) {
        return ClientBusinessBase<TInterface, TType>.Compare(this, (ITaskTimeTrackerContractObject<TInterface>)obj);
      }

      return 1;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override string ToString()
    {
      return "ClientBusinessBase";
    }

    public abstract object Clone();

    public abstract int CompareTo(ITaskTimeTrackerContractObject<TInterface> other);
  }
}
