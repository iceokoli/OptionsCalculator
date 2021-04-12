using System;
using MathNet.Numerics.Distributions;

namespace BlackScholes
{
    class EuroOptionCalculator
    {   
        static public Tuple <double[], double[]> PayOff(EuroOption option, double[] prices)
        {
            double[] call = new double[prices.Length];
            double[] put = new double[prices.Length];

            for(int i = 0; i < prices.Length; i++)
            {
                call[i] = Math.Max(prices[i] - option.strike,0);
                put[i] = Math.Max(option.strike - prices[i], 0);
            }

            return new Tuple<double[], double[]> (call, put);
        }

        static public Tuple<double[], double[]> GetOptionValue(EuroOption option, double[] cPrices, double riskFree, double vol, double time)
        {
            double[] call = new double[cPrices.Length];
            double[] put = new double[cPrices.Length];
            Tuple <double[], double[]> d = CalculateD(cPrices, option.strike, vol, riskFree, option.expiry, time);
            
            for(int i = 0; i < cPrices.Length; i++)
            {   
                call[i] = cPrices[i]*Normal.CDF(0,1,d.Item1[i]) - option.strike*Math.Exp(-riskFree*(option.expiry - time))*Normal.CDF(0,1,d.Item2[i]);
                put[i] = -cPrices[i]*Normal.CDF(0,1,-d.Item1[i]) + option.strike*Math.Exp(-riskFree*(option.expiry - time))*Normal.CDF(0,1,-d.Item2[i]);
            }
            
            return new Tuple<double[], double[]> (call, put);
        }

        static Tuple<double[], double[]> CalculateD(double[] S, double E, double sigma, double r, double T, double t)
        {
            double[] dOne = new double[S.Length];
            double[] dTwo = new double[S.Length];

            for(int i = 0; i < S.Length; i++)
            {
                double num = Math.Log(S[i]/ E) + (r + 0.5*Math.Pow(sigma,2))*(T - t);
                double denom = sigma*Math.Sqrt(T - t);

                dOne[i] = num /denom;
                dTwo[i] = dOne[i] - sigma*(T - t);
            }

            return new Tuple<double[], double[]> (dOne , dTwo);
        }
    }
}