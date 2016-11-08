using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TaskTimeTracker.Client.Ui.ConfigurationWindow {
  public class ConfigurationWindowViewModel : INotifyPropertyChanged {
    private string _keyOneString;
    private string _keyTwoString;
    private string _keyThreeString;
    private Key _keyOne;
    private Key _keyTwo;
    private Key _keyThree;

    public event PropertyChangedEventHandler PropertyChanged;

    public string KeyOneString {
      get { return this._keyOneString; }
      set {
        this._keyOneString = value;
        OnPropertyChanged(nameof(this.KeyOneString));
      }
    }

    public Key KeyOne {
      get { return this._keyOne; }
      set {
        this._keyOne = value;
        OnPropertyChanged(nameof(this.KeyOne));
      }
    }

    public string KeyTwoString {
      get { return this._keyTwoString; }
      set {
        this._keyTwoString = value;
        OnPropertyChanged(nameof(this.KeyTwoString));
      }
    }

    public Key KeyTwo {
      get { return this._keyTwo; }
      set {
        this._keyTwo = value;
        OnPropertyChanged(nameof(this.KeyTwo));
      }
    }

    public string KeyThreeString {
      get { return this._keyThreeString; }
      set {
        this._keyThreeString = value;
        OnPropertyChanged(nameof(this.KeyThreeString));
      }
    }

    public Key KeyThree {
      get { return this._keyThree; }
      set {
        this._keyThree = value;
        OnPropertyChanged(nameof(this.KeyThree));
      }
    }

    public void SetKey(string senderName, Key key) {
      if (String.Compare(senderName, "KeyOneBox", StringComparison.Ordinal) == 0) {
        this.KeyOne = key;
        this.KeyOneString = key.ToString();
      }else if (String.Compare(senderName, "KeyTwoBox", StringComparison.Ordinal) == 0) {
        this.KeyTwo = key;
        this.KeyTwoString = key.ToString();
      } else if (String.Compare(senderName, "KeyThreeBox", StringComparison.Ordinal) == 0) {
        this.KeyThree = key;
        this.KeyThreeString = key.ToString();
      }
    }
    
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
