public static class Utilities
{
    public static string FormatSecondsAsTiming(float seconds)
    {
        return FormatSecondsAsTiming(seconds, false);
    }

    public static string FormatSecondsAsTiming(float seconds, bool shortform)
    {
        int numHours = (int)seconds / 3600;
        int numMinutes = ((int)seconds / 60) % 60;
        float numSeconds = seconds % 60;

        string formattedTime = "";

        if (shortform)
        {
            formattedTime += numHours != 0 ? numHours + "h " : "";
            formattedTime += numMinutes != 0 ? numMinutes + "m " : "";
            formattedTime += numSeconds != 0 ? numSeconds.ToString("0.00") + "s" : "";
        }
        else
        {
            formattedTime += numHours != 0 ? numHours + " Hours " : "";
            formattedTime += numMinutes != 0 ? numMinutes + " Min " : "";
            formattedTime += numSeconds != 0 ? numSeconds.ToString("0.00") + " Seconds" : "";
        }

        return formattedTime;
    }
}
