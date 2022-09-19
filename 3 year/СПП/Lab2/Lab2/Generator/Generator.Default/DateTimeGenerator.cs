using System;
using System.Globalization;

namespace Generator.Default
{
    public sealed class DateTimeGenerator : ValueGenerator
    {
        public DateTimeGenerator() : base(typeof(DateTime)) { }

        public override object Generate(Type type, GeneratorContext context)
        {
            int year = this.RandRange(1900, 2022);
            int month = this.RandRange(1, 12);
            int day = this.RandRange(1, 28);
            int hour = this.RandRange(0, 23);
            int minute = this.RandRange(0, 59);
            int second = this.RandRange(0, 59);
            int millisecond = this.RandRange(0, 999);
            var calendar = new GregorianCalendar();
            var dateTimeKind = DateTimeKind.Utc;
            return new DateTime(year, month, day, hour, minute, second,
                millisecond, calendar, dateTimeKind);
        }
    }
}
