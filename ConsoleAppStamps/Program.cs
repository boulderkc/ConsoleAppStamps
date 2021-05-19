using System;
using System.Collections.Generic;

namespace ConsoleAppStamps
{
    class Program
    {

        static void Main(string[] args)
        {
            int userCents = 0; 
            do
            {
                Console.WriteLine("Enter an integer total amount in cents greater than or equal to seven for which you will need stamps. Enter zero or just close this window to exit.");

                bool tryParseResult = int.TryParse(Console.ReadLine(), out userCents);

                if (!tryParseResult)
                {
                    Console.WriteLine("Please enter an integer value.\n");
                    userCents = 1; 
                }
                else if (userCents == 0)
                {
                    Environment.Exit(0);
                }
                else if (userCents < 7)
                {
                    Console.WriteLine("Please enter a value equal to or greather than 7.\n");
                    userCents = 1;
                }
                else
                {
                    string result = "";
                    StampsNeededObj thisAmountStamps = CalculateStampsNeededThisAmount(userCents);
                    result = "For " + userCents + " cents total, you need: " + thisAmountStamps.FourCentStampsNeeded + " four cent stamps, ";
                    result += thisAmountStamps.FiveCentStampsNeeded + " five cent stamps, and ";
                    result += thisAmountStamps.SevenCentStampsNeeded + " seven cent stamps.\n";
                    Console.WriteLine(result);
                }

            } while (userCents != 0);
            
            if (userCents == 0)
            {
                Environment.Exit(0);
            }


            StampsNeededObj CalculateStampsNeededThisAmount(int totalCents)
            {
                StampsNeededObj stampsNeeded = new StampsNeededObj { FourCentStampsNeeded = 0, FiveCentStampsNeeded = 0, SevenCentStampsNeeded = 0 };

                List<StampsNeededObj> provenBaseStamps = new List<StampsNeededObj>();
                provenBaseStamps.Add(new StampsNeededObj { FourCentStampsNeeded = 2, FiveCentStampsNeeded = 0, SevenCentStampsNeeded = 0 }); //8
                provenBaseStamps.Add(new StampsNeededObj { FourCentStampsNeeded = 1, FiveCentStampsNeeded = 1, SevenCentStampsNeeded = 0 }); //9
                provenBaseStamps.Add(new StampsNeededObj { FourCentStampsNeeded = 0, FiveCentStampsNeeded = 2, SevenCentStampsNeeded = 0 }); //10
                provenBaseStamps.Add(new StampsNeededObj { FourCentStampsNeeded = 1, FiveCentStampsNeeded = 0, SevenCentStampsNeeded = 1 }); //11
                provenBaseStamps.Add(new StampsNeededObj { FourCentStampsNeeded = 3, FiveCentStampsNeeded = 0, SevenCentStampsNeeded = 0 }); //12
                provenBaseStamps.Add(new StampsNeededObj { FourCentStampsNeeded = 2, FiveCentStampsNeeded = 1, SevenCentStampsNeeded = 0 }); //13
                provenBaseStamps.Add(new StampsNeededObj { FourCentStampsNeeded = 0, FiveCentStampsNeeded = 0, SevenCentStampsNeeded = 2 }); //14

                foreach (var provenStamps in provenBaseStamps)
                {
                    if (totalCents == provenStamps.TotalAmountProperty)
                        return provenStamps; 
                }

                if (totalCents % 7 == 0)
                {
                    return new StampsNeededObj { FourCentStampsNeeded = 0, FiveCentStampsNeeded = 0, SevenCentStampsNeeded = totalCents / 7 }; 
                }

                if (totalCents % 7 == 4)
                {
                    return new StampsNeededObj { FourCentStampsNeeded = 1, FiveCentStampsNeeded = 0, SevenCentStampsNeeded = totalCents / 7 };
                }

                if (totalCents % 7 == 5)
                {
                    return new StampsNeededObj { FourCentStampsNeeded = 0, FiveCentStampsNeeded = 1, SevenCentStampsNeeded = totalCents / 7 };
                }

                // We've returned all of the easy ones. Now calculate the answers that require a
                // combination of the 'provenBaseStamps' values to add to a multiple of 7.
                int maxSevens = (totalCents - (totalCents % 7))/7;
                int amountWithOneLessSevenThanMax = maxSevens * 7 - 7;
                foreach (var provenStamps in provenBaseStamps)
                {
                    if (amountWithOneLessSevenThanMax + provenStamps.TotalAmountProperty == totalCents)
                    {
                        StampsNeededObj modifiedStamps = provenStamps;
                        modifiedStamps.SevenCentStampsNeeded = modifiedStamps.SevenCentStampsNeeded + (maxSevens -1);
                        return modifiedStamps;
                    }
                }

                return stampsNeeded;
            }
        }
    }

    public class StampsNeededObj
    {
        public int FourCentStampsNeeded { get; set; }
        public int FiveCentStampsNeeded { get; set; }
        public int SevenCentStampsNeeded { get; set; }

        public int TotalAmountProperty
        {
            get { 
                return (FourCentStampsNeeded * 4) + (FiveCentStampsNeeded * 5) + (SevenCentStampsNeeded * 7); 
            }
        }

    }
}
