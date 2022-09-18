namespace ThirdParty.ControlledRandom
{
    public interface IRandomService
    {
        float value { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from">included</param>
        /// <param name="to">excluded</param>
        /// <returns></returns>
        int Range(int from, int to);
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from">included</param>
        /// <param name="to">excluded</param>
        /// <returns></returns>
        int Next(int from, int to);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxValue">included</param>
        /// <returns></returns>
        int Next(int maxValue);

        void SetSeed(int newSeed);
    }
}