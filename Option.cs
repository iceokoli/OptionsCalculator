namespace BlackScholes
{
    class Option
    {
        public double strike;
        public double expiry;
        public double dividend;

    }

    class EuroOption: Option
    {
        public bool earlyExpiry = false;
        public EuroOption(double aStrike, double aExpiry, double aDividend)
        {
            strike = aStrike;
            expiry = aExpiry;
            dividend = aDividend;
        }
    }
}