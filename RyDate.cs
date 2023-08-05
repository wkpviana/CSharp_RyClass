using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RahimyGroup
{
    public class RyDate
    {
        public static bool IsGregorianLeapYear(int GregorianYear)
        {
            if ((GregorianYear % 4 == 0) && ((GregorianYear % 100 != 0) || (GregorianYear % 400 == 0)))
                return true;
            else
                return false;
        }
        public static bool IsJalaliLeapYear(int JalaliYear)
        {
            int[] LeapSet = { 1, 5, 9, 13, 17, 22, 26, 30 };
            if (LeapSet.Contains(JalaliYear % 33))
                return true;
            else
                return false;
        }

        public static string Gregorian2Jalali(int GregorianYear, int GregorianMonth, int GregorianDay)
        {
            int I = 1;
            int DayOfYear = 0;
            int M = 0;
            int[] GMonthDays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            string Result = "";
            string sTemp = "";

            /* Special Years */
            if ((GregorianYear % 400) == 384)
                M = 1;
            else
                M = 0;

            GregorianDay = GregorianDay - M;
            if (GregorianDay == 0)
            {
                GregorianMonth = GregorianMonth - 1;
                if (GregorianMonth == 0)
                {
                    GregorianYear = GregorianYear - 1;
                    GregorianMonth = 12;
                }
                GregorianDay = GMonthDays[GregorianMonth];
            }

            /* Day of Year */
            while (I < GregorianMonth)
            {
                DayOfYear += GMonthDays[I];
                I++;
            }
            DayOfYear += GregorianDay;
            if (IsGregorianLeapYear(GregorianYear) && (GregorianMonth > 2))
                DayOfYear++;

            /* Month and Day */
            if (DayOfYear <= 79)
            {
                if ((GregorianYear - 1 % 4) == 0)
                    DayOfYear = DayOfYear + 11;
                else
                {
                    DayOfYear = DayOfYear + 10;
                    GregorianYear = GregorianYear - 622;
                }
                if (DayOfYear % 30 == 0)
                {
                    GregorianMonth = (DayOfYear / 30) + 9;
                    GregorianDay = 30;
                }
                else
                {
                    GregorianMonth = (DayOfYear / 30) + 10;
                    GregorianDay = DayOfYear % 30;
                }
            }
            else
            {
                GregorianYear = GregorianYear - 621;
                DayOfYear = DayOfYear - 79;
                if (DayOfYear <= 186)
                {
                    if (DayOfYear % 31 == 0)
                    {
                        GregorianMonth = DayOfYear / 31;
                        GregorianDay = 31;
                    }
                    else
                    {
                        GregorianMonth = (DayOfYear / 31) + 1;
                        GregorianDay = DayOfYear % 31;
                    }
                }
                else
                {
                    DayOfYear = DayOfYear - 186;
                    if (DayOfYear % 30 == 0)
                    {
                        GregorianMonth = (DayOfYear / 30) + 6;
                        GregorianDay = 30;
                    }
                    else
                    {
                        GregorianMonth = (DayOfYear / 30) + 7;
                        GregorianDay = DayOfYear % 30;
                    }
                }
            }

            Result = GregorianYear.ToString() + "/";
            sTemp = "00" + GregorianMonth.ToString();
            Result += sTemp.Substring(sTemp.Length - 2, 2) + "/";
            sTemp = "00" + GregorianDay.ToString();
            Result += sTemp.Substring(sTemp.Length - 2, 2);
            return Result;
        }

        public static string Gregorian2Jalali(DateTime GregorianDate)
        {
            return Gregorian2Jalali(GregorianDate.Year, GregorianDate.Month, GregorianDate.Day);
        }

        public static int CheckLeap(int iValue, bool Leap)
        {
            if (iValue == 1 && Leap)
                return 1;
            else
                return 0;
        }

        public static DateTime Jalali2Gregorian(int JalaliYear, int JalaliMonth, int JalaliDay)
        {
            int[] GDayInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int[] JDayInMonth = { 31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 29 };
            int JYear = 0;
            int JMonth = 0;
            int JDay = 0;
            int GYear = 0;
            int GMonth = 0;
            int GDay = 0;
            Int64 JDayNo = 0;
            Int64 GDayNo = 0;
            int I;
            bool Leap = true;

            JYear = JalaliYear - 979;
            JMonth = JalaliMonth - 1;
            JDay = JalaliDay - 1;
            JDayNo = 365 * JYear + (JYear / 33) * 8 + (((JYear % 33) + 3) / 4);
            I = 1;
            while (I <= JMonth)
            {
                JDayNo += JDayInMonth[I];
                I++;
            }

            JDayNo = JDayNo + JDay;
            GDayNo = JDayNo + 79;
            GYear = (int)(1600 + 400 * (GDayNo / 146097));
            GDayNo = GDayNo % 146097;

            Leap = true;
            if (GDayNo >= 36525)
            {
                GDayNo--;
                GYear = (int)(GYear + 100 * (GDayNo / 36524));
                GDayNo = GDayNo % 36524;
                if (GDayNo >= 365)
                    GDayNo++;
                else
                    Leap = false;
            }

            GYear = (int)(GYear + 4 * (GDayNo / 1461));
            GDayNo = GDayNo % 1461;
            if (GDayNo >= 366)
            {
                Leap = false;
                GDayNo--;
                GYear = (int)(GYear + (GDayNo / 365));
                GDayNo = GDayNo % 365;
            }

            I = 0;
            while (GDayNo >= (GDayInMonth[I + 1] + CheckLeap(I, Leap)))
            {
                GDayNo = GDayNo - (GDayInMonth[I + 1] + CheckLeap(I, Leap));
                I++;
            }
            GMonth = I + 1;
            GDay = (int)(GDayNo + 1);
            return new DateTime(GYear, GMonth, GDay);
        }

        public static DateTime Jalali2Gregorian(string JalaliDate)
        {
            return Jalali2Gregorian(int.Parse(JalaliDate.Substring(0, 4)), int.Parse(JalaliDate.Substring(5, 2)),
                int.Parse(JalaliDate.Substring(8, 2)));
        }

        public static int JalaliDaysInYear(int JalaliYear, int JalaliMonth, int JalaliDay)
        {
            if (JalaliMonth <= 6)
                return ((JalaliMonth * 31) + JalaliDay);
            else
                return (186 + ((JalaliMonth - 6 - 1) * 30) + JalaliDay);
        }

        public static int JalaliDaysInYear(string JalaliDate)
        {
            return JalaliDaysInYear(int.Parse(JalaliDate.Substring(0, 4)), int.Parse(JalaliDate.Substring(5, 2)),
                int.Parse(JalaliDate.Substring(8, 2)));
        }

        public static string JalaliDateDayName(int JalaliYear, int JalaliMonth, int JalaliDay)
        {
            switch ((int)Jalali2Gregorian(JalaliYear, JalaliMonth, JalaliDay).DayOfWeek)
            {
                case 1:
                    return "دوشنبه";
                    break;
                case 2:
                    return "سه شنبه";
                    break;
                case 3:
                    return "چهارشنبه";
                    break;
                case 4:
                    return "پنجشنبه";
                    break;
                case 5:
                    return "جمعه";
                    break;
                case 6:
                    return "شنبه";
                    break;
                case 7:
                    return "یکشنبه";
                    break;
                default:
                    return "";
            }
        }

        public static string JalaliDateDayName(string JalaliDate)
        {
            return JalaliDateDayName(int.Parse(JalaliDate.Substring(0, 4)), int.Parse(JalaliDate.Substring(5, 2)), int.Parse(JalaliDate.Substring(8, 2)));
        }

        public static string JalaliMonthName(int JalaliMonth)
        {
            switch (JalaliMonth)
            {
                case 1:
                    return "فروردین";
                    break;
                case 2:
                    return "اردیبهشت";
                    break;
                case 3:
                    return "خرداد";
                    break;
                case 4:
                    return "تیر";
                    break;
                case 5:
                    return "مرداد";
                    break;
                case 6:
                    return "شهریور";
                    break;
                case 7:
                    return "مهر";
                    break;
                case 8:
                    return "آبان";
                    break;
                case 9:
                    return "آذر";
                    break;
                case 10:
                    return "دی";
                    break;
                case 11:
                    return "بهمن";
                    break;
                case 12:
                    return "اسفند";
                    break;
                default:
                    return "";
            }
        }

        public static string JalaliMonthName(string JalaliDate)
        {
            return JalaliMonthName(int.Parse(JalaliDate.Substring(5, 2)));
        }

        public static string JalaliYearName(int JalaliYear)
        {
            switch (JalaliYear % 12)
            {
                case 0:
                    return "مار";
                    break;
                case 1:
                    return "اسب";
                    break;
                case 2:
                    return "گوسفند";
                    break;
                case 3:
                    return "میمون";
                    break;
                case 4:
                    return "مرغ";
                    break;
                case 5:
                    return "سگ";
                    break;
                case 6:
                    return "خوک";
                    break;
                case 7:
                    return "موش";
                    break;
                case 8:
                    return "گاو";
                    break;
                case 9:
                    return "پلنگ";
                    break;
                case 10:
                    return "خرگوش";
                    break;
                case 11:
                    return "نهنگ";
                    break;
                default:
                    return "";
            }
        }

        public static string JalaliYearName(string JalaliDate)
        {
            return JalaliYearName(int.Parse(JalaliDate.Substring(0, 4)));
        }

        public static string JalaliTodayShortDate()
        {
            return Gregorian2Jalali(DateTime.Now);
        }

        public static string JalaliTodayLongDate()
        {
            string sTemp = Gregorian2Jalali(DateTime.Now);
            return JalaliDateDayName(sTemp) + " " + sTemp.Substring(8, 2) + " " + JalaliMonthName(sTemp) + " " + sTemp.Substring(0, 4);
        }

        public static string JalaliFirstDayNameOfYear(int JalaliYear)
        {
            return JalaliDateDayName(JalaliYear, 1, 1);
        }

        public static string JalaliLastDayNameOfYear(int JalaliYear)
        {
            if (IsJalaliLeapYear(JalaliYear))
                return JalaliDateDayName(JalaliYear, 12, 30);
            else
                return JalaliDateDayName(JalaliYear, 12, 29);
        }

        public static string JalaliIncYear(int JalaliYear, int JalaliMonth, int JalaliDay, int NumberOfYear)
        {
            return Gregorian2Jalali(Jalali2Gregorian(JalaliYear, JalaliMonth, JalaliDay).AddYears(NumberOfYear));
        }
        public static string JalaliIncYear(string JalaliDate, int NumberOfYear)
        {
            return JalaliIncYear(int.Parse(JalaliDate.Substring(0, 4)), int.Parse(JalaliDate.Substring(5, 2)), int.Parse(JalaliDate.Substring(8, 2)), NumberOfYear);
        }

        public static string JalaliIncMonth(int JalaliYear, int JalaliMonth, int JalaliDay, int NumberOfMonth)
        {
            return Gregorian2Jalali(Jalali2Gregorian(JalaliYear, JalaliMonth, JalaliDay).AddMonths(NumberOfMonth));
        }

        public static string JalaliIncMonth(string JalaliDate, int NumberOfMonth)
        {
            return JalaliIncMonth(int.Parse(JalaliDate.Substring(0, 4)), int.Parse(JalaliDate.Substring(5, 2)), int.Parse(JalaliDate.Substring(8, 2)), NumberOfMonth);
        }

        public static string JalaliIncDay(int JalaliYear, int JalaliMonth, int JalaliDay, int NumberOfDay)
        {
            return Gregorian2Jalali(Jalali2Gregorian(JalaliYear, JalaliMonth, JalaliDay).AddDays(NumberOfDay));
        }

        public static string JalaliIncDay(string JalaliDate, int NumberOfDay)
        {
            return JalaliIncDay(int.Parse(JalaliDate.Substring(0, 4)), int.Parse(JalaliDate.Substring(5, 2)), int.Parse(JalaliDate.Substring(8, 2)), NumberOfDay);
        }

        public static string JalaliIncWeek(int JalaliYear, int JalaliMonth, int JalaliDay, int NumberOfWeek)
        {
            return Gregorian2Jalali(Jalali2Gregorian(JalaliYear, JalaliMonth, JalaliDay).AddDays(NumberOfWeek * 7));
        }

        public static string JalaliIncWeek(string JalaliDate, int NumberOfWeek)
        {
            return JalaliIncWeek(int.Parse(JalaliDate.Substring(0, 4)), int.Parse(JalaliDate.Substring(5, 2)), int.Parse(JalaliDate.Substring(8, 2)), NumberOfWeek);
        }

        public static string JalaliYesterday(int JalaliYear, int JalaliMonth, int JalaliDay)
        {
            return JalaliIncDay(JalaliYear, JalaliMonth, JalaliDay, -1);
        }

        public static string JalaliYesterday(string JalaliDate)
        {
            return JalaliYesterday(int.Parse(JalaliDate.Substring(0, 4)), int.Parse(JalaliDate.Substring(5, 2)), int.Parse(JalaliDate.Substring(8, 2)));
        }

        public static string JalaliYesterdayName(int JalaliYear, int JalaliMonth, int JalaliDay)
        {
            return JalaliDateDayName(JalaliYesterday(JalaliYear, JalaliMonth, JalaliDay));
        }

        public static string JalaliYesterdayName(string JalaliDate)
        {
            return JalaliDateDayName(int.Parse(JalaliDate.Substring(0, 4)), int.Parse(JalaliDate.Substring(5, 2)), int.Parse(JalaliDate.Substring(8, 2)));
        }

        public static string JalaliTomorrow(int JalaliYear, int JalaliMonth, int JalaliDay)
        {
            return JalaliIncDay(JalaliYear, JalaliMonth, JalaliDay, 1);
        }

        public static string JalaliTomorrow(string JalaliDate)
        {
            return JalaliTomorrow(int.Parse(JalaliDate.Substring(0, 4)), int.Parse(JalaliDate.Substring(5, 2)), int.Parse(JalaliDate.Substring(8, 2)));
        }

        public static string JalaliTomorrowName(int JalaliYear, int JalaliMonth, int JalaliDay)
        {
            return JalaliDateDayName(JalaliTomorrow(JalaliYear, JalaliMonth, JalaliDay));
        }
        public static string JalaliTomorrowName(string JalaliDate)
        {
            return JalaliTomorrowName(int.Parse(JalaliDate.Substring(0, 4)), int.Parse(JalaliDate.Substring(5, 2)), int.Parse(JalaliDate.Substring(8, 2)));
        }

        public static Int64 JalaliYearSpan(string JalaliStartDate, string JalaliEndDate)
        {
            return Int64.Parse((Jalali2Gregorian(JalaliEndDate) - Jalali2Gregorian(JalaliStartDate)).TotalDays.ToString()) / 365;
        }

        public static Int64 JalaliMonthSpan(string JalaliStartDate, string JalaliEndDate)
        {
            return Int64.Parse((Jalali2Gregorian(JalaliEndDate) - Jalali2Gregorian(JalaliStartDate)).TotalDays.ToString()) / 30;
        }

        public static Int64 JalaliDaySpan(string JalaliStartDate, string JalaliEndDate)
        {
            return Int64.Parse((Jalali2Gregorian(JalaliEndDate) - Jalali2Gregorian(JalaliStartDate)).TotalDays.ToString()) / 365;
        }

        public static Int64 JalaliWeekSpan(string JalaliStartDate, string JalaliEndDate)
        {
            return Int64.Parse((Jalali2Gregorian(JalaliEndDate) - Jalali2Gregorian(JalaliStartDate)).TotalDays.ToString()) / 7;
        }
    }
}