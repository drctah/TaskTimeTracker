using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ConsoleApplication1 {
  class ProjectRon {

    /// <summary>
    /// Gets the console secure password.
    /// </summary>
    /// <returns></returns>
    private static SecureString GetConsoleSecurePassword() {
      SecureString pwd = new SecureString();
      while (true) {
        ConsoleKeyInfo i = Console.ReadKey(true);
        if (i.Key == ConsoleKey.Enter) {
          break;
        } else if (i.Key == ConsoleKey.Backspace) {
          pwd.RemoveAt(pwd.Length - 1);
          Console.Write("\b \b");
        } else {
          pwd.AppendChar(i.KeyChar);
          Console.Write("*");
        }
      }
      return pwd;
    }

    public static int _Main(string[] args) {
      IProjektronApi projektron = new ProjektronApi("", false);

      string userName = "";
      string readAllText = File.ReadAllText("", Encoding.UTF7);
      SecureString password = readAllText.ToSecureString();

      if (projektron.Login(userName, password)) {
        Console.WriteLine($"Wilkommen {projektron.FirstName} {projektron.LastName} Archiv: {projektron.InstanceName}");
      }

      projektron.Presence();

      return 0;
    }
  }

  static class Extensions {
    public static SecureString ToSecureString(this string Source) {
      if (string.IsNullOrWhiteSpace(Source))
        return null;
      else {
        SecureString Result = new SecureString();
        foreach (char c in Source.ToCharArray())
          Result.AppendChar(c);
        return Result;
      }
    }

    public static string ToStandartString(this SecureString secureString) {
      if (secureString == null)
        throw new ArgumentNullException("secureString");

      IntPtr unmanagedString = IntPtr.Zero;
      try {
        unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
        return Marshal.PtrToStringUni(unmanagedString);
      } finally {
        Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
      }
    }
  }
}
