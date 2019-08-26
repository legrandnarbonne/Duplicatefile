using Microsoft.Win32.TaskScheduler;

namespace duplicateFile
{
    public static class SchedulerTrigger
    {
        public enum TriggerTypes { AllDays, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday, FirstDayOfMonth, OneTime };

        public static Trigger getTrigger(int triggerType)
        {
            var tt = (TriggerTypes)triggerType;

            switch (tt)
            {
                case TriggerTypes.AllDays:
                    return new DailyTrigger { DaysInterval = 1 };

                case TriggerTypes.Monday:
                    return new WeeklyTrigger { DaysOfWeek = DaysOfTheWeek.Monday };

                case TriggerTypes.Tuesday:
                    return new WeeklyTrigger { DaysOfWeek = DaysOfTheWeek.Tuesday };

                case TriggerTypes.Wednesday:
                    return new WeeklyTrigger { DaysOfWeek = DaysOfTheWeek.Wednesday };

                case TriggerTypes.Thursday:
                    return new WeeklyTrigger { DaysOfWeek = DaysOfTheWeek.Thursday };

                case TriggerTypes.Friday:
                    return new WeeklyTrigger { DaysOfWeek = DaysOfTheWeek.Friday };

                case TriggerTypes.Saturday:
                    return new WeeklyTrigger { DaysOfWeek = DaysOfTheWeek.Saturday };

                case TriggerTypes.Sunday:
                    return new WeeklyTrigger { DaysOfWeek = DaysOfTheWeek.Sunday };

                case TriggerTypes.FirstDayOfMonth:
                    return new MonthlyTrigger { };

                case TriggerTypes.OneTime:
                    return new TimeTrigger { };
            }

            return null;
        }

        public static TriggerTypes getFromTrigger(Trigger trigger)
        {
            switch (trigger.TriggerType)
            {
                case TaskTriggerType.Daily:
                    return TriggerTypes.AllDays;

                case TaskTriggerType.Weekly:
                    switch (((WeeklyTrigger)trigger).DaysOfWeek)
                    {
                        case DaysOfTheWeek.Monday:
                            return TriggerTypes.Monday;

                        case DaysOfTheWeek.Friday:
                            return TriggerTypes.Friday;

                        case DaysOfTheWeek.Saturday:
                            return TriggerTypes.Saturday;

                        case DaysOfTheWeek.Sunday:
                            return TriggerTypes.Sunday;

                        case DaysOfTheWeek.Thursday:
                            return TriggerTypes.Thursday;

                        case DaysOfTheWeek.Tuesday:
                            return TriggerTypes.Tuesday;

                        case DaysOfTheWeek.Wednesday:
                            return TriggerTypes.Wednesday;
                    }
                    break;

                case TaskTriggerType.Monthly:
                    return TriggerTypes.FirstDayOfMonth;

                case TaskTriggerType.Time:
                    return TriggerTypes.OneTime;
            }

            return TriggerTypes.AllDays;
        }
    }
}