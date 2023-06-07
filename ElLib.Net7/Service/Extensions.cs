namespace ElLib.Net7.Service
{
    public static class Extensions
    {
        public static string CutController(this string str)
        { 
            return str.Replace("Controller", "");
        }
    }
}
