namespace Components
{
    public class RangeValue
    {
        public decimal MinValue { get; set; } = 0;
        public decimal MaxValue { get; set; } = 100000;
        public decimal ClinicallySignificantMinValue { get; set; }
        public decimal ClinicallySignificantMaxValue { get; set; }
        public decimal Value { get; set; }

    }
}