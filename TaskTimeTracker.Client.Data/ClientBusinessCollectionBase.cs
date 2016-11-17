using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TaskTimeTracker.Client.Contract;
using System.Linq;

namespace TaskTimeTracker.Client.Data
{
  public class ClientBusinessCollectionBase<TICollection, TCollection, TIElement, TElement> : ClientBusinessBase<TICollection, TCollection>, ITaskTimeTrackerContractObjectCollection<TICollection, TIElement>
    where TICollection : ITaskTimeTrackerContractObject<TICollection>
    where TCollection : TICollection
    where TIElement : ITaskTimeTrackerContractObject<TIElement>
    where TElement : ITaskTimeTrackerContractObject<TIElement>
  {
    private IList<ITaskTimeTrackerContractObject<TIElement>> elements;

    public int Count { get { return this.elements.Count; } }

    public bool IsReadOnly { get { return false; } }

    public ClientBusinessCollectionBase()
    {
      this.elements = new List<ITaskTimeTrackerContractObject<TIElement>>();
    }

    public void Add(TIElement item)
    {
      if (item == null) {
        throw new ArgumentNullException("item");
      }

      this.elements.Add(item);
    }

    public void AddRange(IEnumerable<TIElement> items)
    {
      if (items != null) {
        foreach (TIElement e in items) {
          if (e != null) {
            this.Add(e);
          }
        }
      }
    }

    public void Clear()
    {
      this.elements.Clear();
    }

    public bool Contains(TIElement item)
    {
      if (item == null) {
        return false;
      }

      return this.elements.Contains(item);
    }

    public void CopyTo(TIElement[] array, int arrayIndex)
    {
      for (int cnt = 0; cnt < this.elements.Count; ++cnt) {
        array[arrayIndex + cnt] = (TIElement)this.elements[cnt];
      }
    }

    public IEnumerator<TIElement> GetEnumerator()
    {
      return this.elements.Cast<TIElement>().GetEnumerator();
    }

    public bool Remove(TIElement item)
    {
      if (item == null) {
        return false;
      }

      return this.elements.Remove(item);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.elements.GetEnumerator();
    }

    protected override void LoadInternal(JsonSerializer serializer, TextReader reader)
    {
      TElement[] values = (TElement[])serializer.Deserialize(reader, typeof(TElement[]));
      this.elements.Clear();
      foreach (TElement e in values) {
        this.elements.Add((ITaskTimeTrackerContractObject<TIElement>)e);
      }
    }

    public override object Clone()
    {
      Type t = this.GetType();
      ClientBusinessCollectionBase<TICollection, TCollection, TIElement, TElement> copy = (ClientBusinessCollectionBase<TICollection, TCollection, TIElement, TElement>)Activator.CreateInstance(t);
      copy.AddRange(this);
      return copy;
    }

    public override int CompareTo(ITaskTimeTrackerContractObject<TICollection> other)
    {
      IEnumerator enu1 = this.GetEnumerator();
      IEnumerator enu2 = this.GetEnumerator();
      int result = 0;

      while (true) {
        bool n1 = enu1.MoveNext();
        bool n2 = enu2.MoveNext();

        if ((n1 ^ n2) || !n1) {
          result = 0;
          break;
        }

        result = ((IComparable)enu1.Current).CompareTo(enu2.Current);
        if (result != 0) {
          break;
        }
      }

      return result;
    }
  }
}
