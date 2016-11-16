using System;
using System.Text;

namespace TaskTimeTracker.Client.Data.Tests
{
  internal class TestHelper
  {
    public string GetRandomString(Random random, int length)
    {
      StringBuilder sb = new StringBuilder();
      for (int cnt = 0; cnt < length; ++cnt) {
        char c = (char)random.Next(0x20, 0x7f);
        sb.Append(c);
      }

      return sb.ToString();
    }

    public DateTime GetRandomDateTime(Random random)
    {
      long ticks = 0;
      for (int cnt = 0; cnt < 7; ++cnt) {
        int v = random.Next(0, 256);
        ticks <<= 8;
        ticks |= (byte)v;
      }

      return new DateTime(ticks, DateTimeKind.Utc);
    }
  }
}
