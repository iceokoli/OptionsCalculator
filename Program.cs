using System;

namespace BlackScholes
{
    class Program
    {
            static double strikePrice;
            static double dividendYield;
            static double riskFreeRate;
            static double expiry; 
            static double vol;
            static double[] initialPrices;
            static double elapsedTime;

        static void PrettyPrintArray(double [] array)
        {   
            string result = "[";
            foreach (var item in array)
            {
                result = result + String.Format(" {0},", item);
            }
            result = result.Remove(result.Length -1 , 1) + "]";
            System.Console.WriteLine(result);
        }

        static void GetInput()
        {
            string val;

            Console.WriteLine("This a Black Scholes Option Calculator for European options \n");
            
            Console.Write("What is the strike price? ");
            val = Console.ReadLine();
            strikePrice = Convert.ToDouble(val);
            
            Console.Write("When does the option expire? ");
            val = Console.ReadLine();
            expiry = Convert.ToDouble(val);

            Console.Write("What is the volatility of the underlying? ");
            val = Console.ReadLine();
            vol = Convert.ToDouble(val);

            Console.Write("What is the dividend yield of the underlying? ");
            val = Console.ReadLine();
            dividendYield = Convert.ToDouble(val);

            Console.Write("What is the current risk free rate? ");
            val = Console.ReadLine();
            riskFreeRate = Convert.ToDouble(val);

            Console.Write("For what initail stock prices? ");
            val = Console.ReadLine();
            string[] sep = val.Split(',');
            initialPrices = Array.ConvertAll<string, double>(sep, Double.Parse);

            Console.Write("How much time has elapsed? ");
            val = Console.ReadLine();
            elapsedTime = Convert.ToDouble(val);

        }
        static void Main(string[] args)
        {   
            
            GetInput();

            // Instantiate european option
            EuroOption option = new EuroOption(strikePrice, expiry, dividendYield);
            
            // Calculate payoff for different prices at expiry
            Tuple <double[], double[]> payoff = EuroOptionCalculator.PayOff(
                option,
                initialPrices
            );

            System.Console.Write("PayOff for Call Option: ");
            PrettyPrintArray(payoff.Item1);
            System.Console.Write("PayOff for Put Option: ");
            PrettyPrintArray(payoff.Item2);

            // Calculate option value for different Initial Stock Prices
            Tuple <double[], double[]> value = EuroOptionCalculator.GetOptionValue(
                option, 
                initialPrices,
                riskFreeRate,
                vol,
                elapsedTime
            );

            System.Console.Write("Value for Call Option: ");
            PrettyPrintArray(value.Item1);
            System.Console.Write("Value for Put Option: ");
            PrettyPrintArray(value.Item2);

        }
    }
}
