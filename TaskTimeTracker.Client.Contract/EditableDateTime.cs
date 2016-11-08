using System;

namespace TaskTimeTracker.Client.Contract {
  /// <summary>
  /// TODO VALIDATORS
  /// </summary>
  public class EditableDateTime {
    private int _year;
    private int _month;
    private int _day;
    private int _hour;
    private int _minute;
    private int _second;
    private int _millisecond;

    public int Year {
      get { return _year; }
      set {
        if (value > DateTime.MaxValue.Year || value < DateTime.MinValue.Year) {
          throw new InvalidOperationException();
        }

        _year = value;
      }
    }

    public int Month {
      get { return _month; }
      set {
        if (value > 12 || value < 1) {
          throw new InvalidOperationException();
        }

        _month = value;
      }
    }

    public int Day {
      get { return _day; }
      set {
        if (value > DateTime.DaysInMonth(this.Year, this.Month) || value < 1) {
          throw new InvalidOperationException();
        }

        _day = value;
      }
    }

    public int Hour {
      get { return _hour; }
      set {
        if (value > 24 || value < 0) {
          throw new InvalidOperationException();
        }

        _hour = value;
      }
    }

    public int Minute {
      get { return _minute; }
      set {
        if (value > 60 || value < 0) {
          throw new InvalidOperationException();
        }

        _minute = value;
      }
    }

    public int Second {
      get { return _second; }
      set {
        if (value > 60 || value < 0) {
          throw new InvalidOperationException();
        }

        _second = value;
      }
    }

    public int Millisecond {
      get { return _millisecond; }
      set {
        if (value > 1000 || value < 0) {
          throw new InvalidOperationException();
        }

        _millisecond = value;
      }
    }

    public EditableDateTime(EditableDateTime editableDateTime) {
      this.Year = editableDateTime.Year;
      this.Month = editableDateTime.Month;
      this.Day = editableDateTime.Day;
      this.Hour = editableDateTime.Hour;
      this.Minute = editableDateTime.Minute;
      this.Second = editableDateTime.Second;
      this.Millisecond = editableDateTime.Millisecond;
    }

    public EditableDateTime(DateTime dateTime) {
      this.Year = dateTime.Year;
      this.Month = dateTime.Month;
      this.Day = dateTime.Day;
      this.Hour = dateTime.Hour;
      this.Minute = dateTime.Minute;
      this.Second = dateTime.Second;
      this.Millisecond = dateTime.Millisecond;
    }

    public DateTime ToDateTime() {
      return new DateTime(this.Year, this.Month, this.Day, this.Hour, this.Minute, this.Second, this.Millisecond);
    }

    public static implicit operator EditableDateTime(DateTime dateTime) {
      return new EditableDateTime(dateTime);
    }

    public static implicit operator DateTime(EditableDateTime editableDateTime) {
      return editableDateTime.ToDateTime();
    }
  }
}
