using SDLCSimulator_Data.Enums;

namespace SDLCSimulator_BusinessLogic.Helpers
{
    public static class ErrorRateGetter
    {
        public static decimal GetErrorRate(ErrorRateEnum errorRate)
        {
            return errorRate switch
            {
                ErrorRateEnum.EasyErrorRate =>
                    0.5m,
                ErrorRateEnum.MediumErrorRate =>
                    1.0m,
                ErrorRateEnum.HardErrorRate =>
                    1.5m,
                _ => 0
            };
        }
    }
}
