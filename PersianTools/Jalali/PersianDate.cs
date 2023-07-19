using System;
using System.ComponentModel;
using Calendar = System.Globalization.Calendar;
using NumberStyles = System.Globalization.NumberStyles;

namespace PersianTools.Jalali
{
    using Enums;

    //this attribute enables converting to/from this type in wpf and other designing environments
    [TypeConverter(typeof (PersianDateConverter))]
    public struct PersianDate : IComparable<PersianDate>
    {
        private static readonly Calendar calendar = new System.Globalization.PersianCalendar();
        private static readonly DateTime minValue = new DateTime(622, 3, 21);
        private readonly DateTime date;

        #region ctor

        /// <summary>
        /// Initializes a new instance of the PersianDate structure set to the year, month and day parameters.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public PersianDate(int year, int month, int day) : this()
        {
            date = IsValid(year, month, day) ? new DateTime(year, month, day, calendar) : minValue;
        }

        /// <summary>
        /// Initializes a new instance of the PersianDate structure set to the Persian date
        /// equivalent to the date specified in the dateTime parameter.
        /// </summary>
        /// <param name="dateTime"></param>
        public PersianDate(DateTime dateTime) : this()
        {
            date = dateTime >= minValue ? dateTime : minValue;
        }

        /// <summary>
        /// Initializes a new instance of the PersianDate structure set to the Persian date
        /// equivalent to the date and time-of day specified by dateTime and tod parameters.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="tod">time of day</param>
        public PersianDate(DateTime dateTime, TimeSpan tod) : this(dateTime + tod)
        {
        }

        #endregion

        #region static properties & fields

        /// <summary>
        /// The minimum admissible Gregorian time for Persian calendar
        /// </summary>
        public static readonly PersianDate MinValue = new PersianDate(minValue);

        /// <summary>
        /// The maximum admissible value for a DateTime
        /// </summary>
        public static readonly PersianDate MaxValue = new PersianDate(DateTime.MaxValue);

        public static PersianDate Today
        {
            get { return new PersianDate(DateTime.Today); }
        }

        public static PersianDate Now
        {
            get { return new PersianDate(DateTime.Now); }
        }

        #endregion

        #region properties

        /// <summary>
        /// Gets the day of the month represented by this PersianDate instance.
        /// </summary>
        public int Day
        {
            get { return date > minValue ? calendar.GetDayOfMonth(date) : 1; }
        }

        /// <summary>
        /// Gets the month represented by this PersianDate instance.
        /// </summary>
        public int Month
        {
            get { return date > minValue ? calendar.GetMonth(date) : 1; }
        }

        /// <summary>
        /// Gets year represented by this PersianDate instance.
        /// </summary>
        public int Year
        {
            get { return date > minValue ? calendar.GetYear(date) : 1; }
        }

        /// <summary>
        /// Gets the month as PersianMonth represented by this PersianDate instance.
        /// </summary>
        public PersianMonth MonthAsPersianMonth
        {
            get { return (PersianMonth) Month; }
        }

        /// <summary>
        /// Gets the day of the week represented by this PersianDate instance.
        /// </summary>
        public DayOfWeek DayOfWeek
        {
            get { return calendar.GetDayOfWeek(date); }
        }

        /// <summary>
        /// Gets the day of the week as PersianDayOfWeek represented by this PersianDate instance.
        /// </summary>
        public PersianDayOfWeek PersianDayOfWeek
        {
            get { return (PersianDayOfWeek) (int) calendar.GetDayOfWeek(date); }
        }

        /// <summary>
        /// Gets the day of the year represented by this PersianDate instance.
        /// </summary>
        public int DayOfYear
        {
            get { return calendar.GetDayOfYear(date); }
        }

        /// <summary>
        /// Gets the hour value represented by this PersianDate instance.
        /// </summary>
        public int Hour
        {
            get { return date.Hour; }
        }

        /// <summary>
        /// Gets the minute value represented by this PersianDate instance.
        /// </summary>
        public int Minute
        {
            get { return date.Minute; }
        }

        /// <summary>
        /// Gets the second value represented by this PersianDate instance.
        /// </summary>
        public int Second
        {
            get { return date.Second; }
        }

        #endregion

        /// <summary>
        /// Checks whether the given date is a valid date
        /// </summary>
        public static bool IsValid(int year, int month, int day)
        {
            try
            {
                return new DateTime(year, month, day, calendar) >= DateTime.MinValue;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsLeapYear(int year)
        {
            return calendar.IsLeapYear(year);
        }

        public static int DaysInMonth(int year, int month)
        {
            return calendar.GetDaysInMonth(year, month);
        }

        /// <summary>
        /// Converts the specified string representation of a Persian date to its equivalent PersianDate value.
        /// </summary>
        /// <param name="persianDateString"></param>
        /// <returns></returns>
        public static PersianDate Parse(string persianDateString)
        {
            int y, m, d;
            const NumberStyles style = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite;
            try
            {
                string[] parts = persianDateString.Split('/');
                y = int.Parse(parts[0], style);
                m = int.Parse(parts[1], style);
                d = int.Parse(parts[2], style);
            }
            catch
            {
                throw new ArgumentException("The date string must be in the form y/m/d");
            }
            try
            {
                return new PersianDate(y, m, d);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid Date Value: ", ex);
            }
        }

        /// <summary>
        /// Converts the specified string representation of a Persian date to its equivalent PersianDate value.
        /// </summary>
        /// <param name="presianDateString"></param>
        /// <param name="result">
        /// If the conversion succeeds, this parameter will contain the PersianDate value equivalent to the
        /// Persian date specified in the first parameter, otherwise it will have the value of date 1/1/1
        /// </param>
        /// <returns>true if the s parameter was converted successfully; otherwise, false.</returns>
        public static bool TryParse(string presianDateString, out PersianDate result)
        {
            try
            {
                result = Parse(presianDateString);
                return true;
            }
            catch
            {
                result = MinValue;
                return false;
            }
        }

        /// <summary>
        /// returns a new PersianDate resulting from adding the days specified to the current PersianDate
        /// </summary>
        /// <param name="days">number of days to be added to the current PersianDate</param>
        public PersianDate AddDays(int days)
        {
            return new PersianDate(this.date.AddDays(days));
        }

        /// <summary>
        /// Converts the PersianDate value to its DateTime equivalent.
        /// </summary>
        /// <returns></returns>
        public DateTime ToDateTime()
        {
            return this.date;
        }

        /// <summary>
        /// Returns the long date String representation of the PersianDate Value.
        /// </summary>
        /// <returns></returns>
        public string ToLongDateString()
        {
            return string.Format("‏" + "{3}، {2} {1} {0}", Year, MonthAsPersianMonth, Day, PersianDayOfWeek);
        }

        #region IComparable<PersianDate> Members

        public int CompareTo(PersianDate that)
        {
            return this.date.CompareTo(that.date);
        }

        #endregion

        #region Comparison operators

        public static bool operator <(PersianDate x, PersianDate y)
        {
            return x.date < y.date;
        }

        public static bool operator <=(PersianDate x, PersianDate y)
        {
            return x.date <= y.date;
        }

        public static bool operator ==(PersianDate x, PersianDate y)
        {
            return x.date == y.date;
        }

        public static bool operator >=(PersianDate x, PersianDate y)
        {
            return x.date >= y.date;
        }

        public static bool operator >(PersianDate x, PersianDate y)
        {
            return x.date > y.date;
        }

        public static bool operator !=(PersianDate x, PersianDate y)
        {
            return x.date != y.date;
        }

        public bool Equals(PersianDate other)
        {
            return this.date.Equals(other.date);
        }

        #endregion

        #region Arithmetic operators

        public static PersianDate operator +(PersianDate persianDate, TimeSpan ts)
        {
            try
            {
                return new PersianDate(persianDate.date + ts);
            }
            catch (Exception ex)
            {
                throw new OverflowException("The resulting date of the addition is outside of acceptable range.", ex);
            }
        }

        public static PersianDate operator -(PersianDate persianDate, TimeSpan ts)
        {
            try
            {
                return new PersianDate(persianDate.date - ts);
            }
            catch (Exception ex)
            {
                throw new OverflowException("The resulting date of the addition is outside of acceptable range.", ex);
            }
        }

        public static TimeSpan operator -(PersianDate x, PersianDate y)
        {
            return x.date - y.date;
        }

        #endregion

        #region overrides

        /// <summary>
        /// Returns the String representation of the PersianDate value.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}/{1}/{2}", Year, Month, Day);
        }

        public override bool Equals(object obj)
        {
            return obj is PersianDate && Equals((PersianDate) obj);
        }

        public override int GetHashCode()
        {
            return date.GetHashCode();
        }

        #endregion
    }
}
