namespace EdTechApp.Models
{
    public static class Logger
    {
        private static TextWriter writer = Console.Out;
        public static bool EnableConsole { get; set; } = true;

        public static void WriteLine(string message)
        {
            if (writer != null)
            {
                try { writer.WriteLine(message); } 
                catch (ObjectDisposedException) { }
            }
            if (EnableConsole)
                Console.WriteLine(message);
        }

        public static void SetWriter(TextWriter tw)
        {
            writer = tw;
        }
    }
}