namespace ThirdParty.StringHashCalculator
{
    public static class HashCalculator
    {
        public static int KeyHash(this string source)
        {
            return source.GetHashCode();
        }
    }
}