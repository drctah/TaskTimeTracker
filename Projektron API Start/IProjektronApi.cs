using System.Security;

namespace ConsoleApplication1 {
  interface IProjektronApi {
    string InstanceName { get; }

    string FirstName { get; }

    string LastName { get; }

    bool Logedin { get; }

    bool Login(string userName, SecureString password);

    void GetApointments();

    void DayEffortRecording();

    void Presence();
  }
}