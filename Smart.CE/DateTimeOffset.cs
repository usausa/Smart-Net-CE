namespace Smart
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    /// <summary>
    ///
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public struct DateTimeOffset : IComparable, IComparable<DateTimeOffset>, IEquatable<DateTimeOffset>
    {
        internal const Int64 MaxOffset = TimeSpan.TicksPerHour * 14;

        internal const Int64 MinOffset = -MaxOffset;

        public static readonly DateTimeOffset MinValue = new DateTimeOffset(DateTime.MinValue.Ticks, TimeSpan.Zero);

        public static readonly DateTimeOffset MaxValue = new DateTimeOffset(DateTime.MaxValue.Ticks, TimeSpan.Zero);

        private readonly DateTime dateTime;

        private readonly short offsetMinutes;

        private static short ValidateOffset(TimeSpan offset)
        {
            var ticks = offset.Ticks;

            if (ticks % TimeSpan.TicksPerMinute != 0)
            {
                throw new ArgumentException("offset precision", "offset");
            }
            if (ticks < MinOffset || ticks > MaxOffset)
            {
                throw new ArgumentOutOfRangeException("offset");
            }

            return (short)(offset.Ticks / TimeSpan.TicksPerMinute);
        }

        private static DateTime ValidateDate(DateTime dateTime, TimeSpan offset)
        {
            var utcTicks = dateTime.Ticks - offset.Ticks;

            if (utcTicks < DateTime.MinValue.Ticks || utcTicks > DateTime.MaxValue.Ticks)
            {
                throw new ArgumentOutOfRangeException("offset");
            }

            return new DateTime(utcTicks, DateTimeKind.Unspecified);
        }

        public DateTimeOffset(long ticks, TimeSpan offset)
        {
            this.offsetMinutes = ValidateOffset(offset);
            this.dateTime = ValidateDate(new DateTime(ticks), offset);
        }

        public DateTimeOffset(DateTime dateTime)
        {
            var offset = dateTime.Kind != DateTimeKind.Utc ? TimeZone.CurrentTimeZone.GetUtcOffset(dateTime) : new TimeSpan(0);
            this.offsetMinutes = ValidateOffset(offset);
            this.dateTime = ValidateDate(dateTime, offset);
        }

        public DateTimeOffset(DateTime dateTime, TimeSpan offset)
        {
            if (dateTime.Kind == DateTimeKind.Local)
            {
                if (offset != TimeZone.CurrentTimeZone.GetUtcOffset(dateTime))
                {
                    throw new ArgumentException("offset local mismatch", "offset");
                }
            }
            else if (dateTime.Kind == DateTimeKind.Utc)
            {
                if (offset != TimeSpan.Zero)
                {
                    throw new ArgumentException("offset UTC mismatch");
                }
            }
            this.offsetMinutes = ValidateOffset(offset);
            this.dateTime = ValidateDate(dateTime, offset);
        }

        public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, TimeSpan offset)
        {
            this.offsetMinutes = ValidateOffset(offset);
            this.dateTime = ValidateDate(new DateTime(year, month, day, hour, minute, second), offset);
        }

        public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, int millisecond, TimeSpan offset)
        {
            this.offsetMinutes = ValidateOffset(offset);
            this.dateTime = ValidateDate(new DateTime(year, month, day, hour, minute, second, millisecond), offset);
        }

        public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, TimeSpan offset)
        {
            this.offsetMinutes = ValidateOffset(offset);
            this.dateTime = ValidateDate(new DateTime(year, month, day, hour, minute, second, millisecond, calendar), offset);
        }

        public static DateTimeOffset Now
        {
            get { return new DateTimeOffset(DateTime.Now); }
        }

        public static DateTimeOffset UtcNow
        {
            get { return new DateTimeOffset(DateTime.UtcNow); }
        }

        public DateTime DateTime
        {
            get { return ClockDateTime; }
        }

        public DateTime UtcDateTime
        {
            get { return DateTime.SpecifyKind(this.dateTime, DateTimeKind.Utc); }
        }

        public DateTime LocalDateTime
        {
            get { return UtcDateTime.ToLocalTime(); }
        }

        public DateTimeOffset ToOffset(TimeSpan offset)
        {
            return new DateTimeOffset((this.dateTime + offset).Ticks, offset);
        }

        private DateTime ClockDateTime
        {
            get { return new DateTime((this.dateTime + Offset).Ticks, DateTimeKind.Unspecified); }
        }

        public DateTime Date
        {
            get { return ClockDateTime.Date; }
        }

        public int Day
        {
            get { return ClockDateTime.Day; }
        }

        public DayOfWeek DayOfWeek
        {
            get { return ClockDateTime.DayOfWeek; }
        }

        public int DayOfYear
        {
            get { return ClockDateTime.DayOfYear; }
        }

        public int Hour
        {
            get { return ClockDateTime.Hour; }
        }

        public int Millisecond
        {
            get
            {
                return ClockDateTime.Millisecond;
            }
        }

        public int Minute
        {
            get { return ClockDateTime.Minute; }
        }

        public int Month
        {
            get { return ClockDateTime.Month; }
        }

        public TimeSpan Offset
        {
            get { return new TimeSpan(0, this.offsetMinutes, 0); }
        }

        public int Second
        {
            get { return ClockDateTime.Second; }
        }

        public long Ticks
        {
            get { return ClockDateTime.Ticks; }
        }

        public long UtcTicks
        {
            get { return UtcDateTime.Ticks; }
        }

        public TimeSpan TimeOfDay
        {
            get { return ClockDateTime.TimeOfDay; }
        }

        public int Year
        {
            get { return ClockDateTime.Year; }
        }

        public DateTimeOffset Add(TimeSpan timeSpan)
        {
            return new DateTimeOffset(ClockDateTime.Add(timeSpan), Offset);
        }

        public DateTimeOffset AddDays(double days)
        {
            return new DateTimeOffset(ClockDateTime.AddDays(days), Offset);
        }

        public DateTimeOffset AddHours(double hours)
        {
            return new DateTimeOffset(ClockDateTime.AddHours(hours), Offset);
        }

        public DateTimeOffset AddMilliseconds(double milliseconds)
        {
            return new DateTimeOffset(ClockDateTime.AddMilliseconds(milliseconds), Offset);
        }

        public DateTimeOffset AddMinutes(double minutes)
        {
            return new DateTimeOffset(ClockDateTime.AddMinutes(minutes), Offset);
        }

        public DateTimeOffset AddMonths(int months)
        {
            return new DateTimeOffset(ClockDateTime.AddMonths(months), Offset);
        }

        public DateTimeOffset AddSeconds(double seconds)
        {
            return new DateTimeOffset(ClockDateTime.AddSeconds(seconds), Offset);
        }

        public DateTimeOffset AddTicks(long ticks)
        {
            return new DateTimeOffset(ClockDateTime.AddTicks(ticks), Offset);
        }

        public DateTimeOffset AddYears(int years)
        {
            return new DateTimeOffset(ClockDateTime.AddYears(years), Offset);
        }

        public static int Compare(DateTimeOffset first, DateTimeOffset second)
        {
            return DateTime.Compare(first.UtcDateTime, second.UtcDateTime);
        }

        int IComparable.CompareTo(Object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is DateTimeOffset))
            {
                throw new ArgumentException("must be DateTimeOffset");
            }

            var objUtc = ((DateTimeOffset)obj).UtcDateTime;
            var utc = UtcDateTime;
            if (utc > objUtc)
            {
                return 1;
            }
            if (utc < objUtc)
            {
                return -1;
            }
            return 0;
        }

        public int CompareTo(DateTimeOffset other)
        {
            var otherUtc = other.UtcDateTime;
            var utc = UtcDateTime;
            if (utc > otherUtc)
            {
                return 1;
            }
            if (utc < otherUtc)
            {
                return -1;
            }
            return 0;
        }

        public override bool Equals(Object obj)
        {
            if (obj is DateTimeOffset)
            {
                return UtcDateTime.Equals(((DateTimeOffset)obj).UtcDateTime);
            }
            return false;
        }

        public bool Equals(DateTimeOffset other)
        {
            return UtcDateTime.Equals(other.UtcDateTime);
        }

        public bool EqualsExact(DateTimeOffset other)
        {
            return ClockDateTime == other.ClockDateTime && Offset == other.Offset && ClockDateTime.Kind == other.ClockDateTime.Kind;
        }

        public static bool Equals(DateTimeOffset first, DateTimeOffset second)
        {
            return DateTime.Equals(first.UtcDateTime, second.UtcDateTime);
        }

        public static DateTimeOffset FromFileTime(long fileTime)
        {
            return new DateTimeOffset(DateTime.FromFileTime(fileTime));
        }

        public override int GetHashCode()
        {
            return UtcDateTime.GetHashCode();
        }

        public TimeSpan Subtract(DateTimeOffset value)
        {
            return UtcDateTime.Subtract(value.UtcDateTime);
        }

        public DateTimeOffset Subtract(TimeSpan value)
        {
            return new DateTimeOffset(ClockDateTime.Subtract(value), Offset);
        }

        public long ToFileTime()
        {
            return UtcDateTime.ToFileTime();
        }

        public DateTimeOffset ToLocalTime()
        {
            return new DateTimeOffset(UtcDateTime.ToLocalTime());
        }

        public DateTimeOffset ToUniversalTime()
        {
            return new DateTimeOffset(UtcDateTime);
        }

        public static implicit operator DateTimeOffset(DateTime dateTime)
        {
            return new DateTimeOffset(dateTime);
        }

        public static DateTimeOffset operator +(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
        {
            return new DateTimeOffset(dateTimeOffset.ClockDateTime + timeSpan, dateTimeOffset.Offset);
        }

        public static DateTimeOffset operator -(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
        {
            return new DateTimeOffset(dateTimeOffset.ClockDateTime - timeSpan, dateTimeOffset.Offset);
        }

        public static TimeSpan operator -(DateTimeOffset left, DateTimeOffset right)
        {
            return left.UtcDateTime - right.UtcDateTime;
        }

        public static bool operator ==(DateTimeOffset left, DateTimeOffset right)
        {
            return left.UtcDateTime == right.UtcDateTime;
        }

        public static bool operator !=(DateTimeOffset left, DateTimeOffset right)
        {
            return left.UtcDateTime != right.UtcDateTime;
        }

        public static bool operator <(DateTimeOffset left, DateTimeOffset right)
        {
            return left.UtcDateTime < right.UtcDateTime;
        }

        public static bool operator <=(DateTimeOffset left, DateTimeOffset right)
        {
            return left.UtcDateTime <= right.UtcDateTime;
        }

        public static bool operator >(DateTimeOffset left, DateTimeOffset right)
        {
            return left.UtcDateTime > right.UtcDateTime;
        }

        public static bool operator >=(DateTimeOffset left, DateTimeOffset right)
        {
            return left.UtcDateTime >= right.UtcDateTime;
        }
    }
}
