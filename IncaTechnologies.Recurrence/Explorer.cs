namespace IncaTechnologies.Recurrence
{
    /// <summary>
    /// Contains function to navigate a <see cref="IRecurrent"/> data structure.
    /// </summary>
    public static class Explorer
    {
        /// <summary>
        /// <para>Finds the greatest parent of a recurrent object.</para>
        /// <example>
        /// In the example below 'x' will be of type <see cref="IMinutely"/>.
        /// By calling <see cref="GetRoot(IRecurrent)"/> 'a' will be of type <see cref="IMonthly"/> since the base recurrence of 'x' is a monthly recurrence.
        /// <code>
        /// var x = Occurs.EveryMonth().Day(15).Hour(12);
        /// var a = x.FindAncestor();
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="recurrent">The recurrent child.</param>
        /// <returns>The greatest parent of the recurrent data structure.</returns>
        public static IRecurrent GetRoot(this IRecurrent recurrent)
        {
            // Damn you .NET standard 2.0
            var ancestor = recurrent;
            while (recurrent != null)
            {
                if (recurrent is Secondly secondly)
                {
                    ancestor = secondly;
                    recurrent = secondly.Minutely;
                }
                else if (recurrent is Minutely minutely)
                {
                    ancestor = minutely;
                    recurrent = minutely.Hourly;
                }
                else if (recurrent is Hourly hourly)
                {
                    ancestor = hourly;
                    recurrent = hourly.Daily;
                }
                else if (recurrent is Daily daily)
                {
                    ancestor = daily;
                    recurrent = daily.Weekly is null
                        ? daily.Monthly as IRecurrent
                        : daily.Weekly;
                }
                else if (recurrent is Weekly weekly)
                {
                    ancestor = weekly;
                    recurrent = weekly.Monthly;
                }
                else if (recurrent is Monthly monthly)
                {
                    ancestor = monthly;
                    recurrent = monthly.Yearly;
                }
                else if (recurrent is Yearly yearly)
                {
                    ancestor = yearly;
                    recurrent = null;
                }
                else
                {
                    recurrent = null;
                }
            }

            return ancestor;
        }
    }
}