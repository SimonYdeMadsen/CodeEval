using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;

namespace CityBlocksFlyover
{
    class Program
    {
        //IGNORE 
        //http://stackoverflow.com/questions/33538527/display-a-image-in-a-console-application

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateFile(
            string lpFileName,
            int dwDesiredAccess,
            int dwShareMode,
            IntPtr lpSecurityAttributes,
            int dwCreationDisposition,
            int dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetCurrentConsoleFont(
            IntPtr hConsoleOutput,
            bool bMaximumWindow,
            [Out][MarshalAs(UnmanagedType.LPStruct)]ConsoleFontInfo lpConsoleCurrentFont);

        [StructLayout(LayoutKind.Sequential)]
        internal class ConsoleFontInfo
        {
            internal int nFont;
            internal Coord dwFontSize;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct Coord
        {
            [FieldOffset(0)]
            internal short X;
            [FieldOffset(2)]
            internal short Y;
        }

        private const int GENERIC_READ = unchecked((int)0x80000000);
        private const int GENERIC_WRITE = 0x40000000;
        private const int FILE_SHARE_READ = 1;
        private const int FILE_SHARE_WRITE = 2;
        private const int INVALID_HANDLE_VALUE = -1;
        private const int OPEN_EXISTING = 3;


        private static Size GetConsoleFontSize()
        {
            // getting the console out buffer handle
            IntPtr outHandle = CreateFile("CONOUT$", GENERIC_READ | GENERIC_WRITE,
                FILE_SHARE_READ | FILE_SHARE_WRITE,
                IntPtr.Zero,
                OPEN_EXISTING,
                0,
                IntPtr.Zero);
            int errorCode = Marshal.GetLastWin32Error();
            if (outHandle.ToInt32() == INVALID_HANDLE_VALUE)
            {
                throw new IOException("Unable to open CONOUT$", errorCode);
            }

            ConsoleFontInfo cfi = new ConsoleFontInfo();
            if (!GetCurrentConsoleFont(outHandle, false, cfi))
            {
                throw new InvalidOperationException("Unable to get font information.");
            }

            return new Size(cfi.dwFontSize.X, cfi.dwFontSize.Y);
        }

        public static void displayImage()
        {
            Point location = new Point(85, 1);
            Size imageSize = new Size(30, 15); // desired image size in characters


            string path = "city-illustration.jpg";

            using (Graphics g = Graphics.FromHwnd(GetConsoleWindow()))
            {
                using (Image image = Image.FromFile(path))
                {
                    Size fontSize = GetConsoleFontSize();

                    // translating the character positions to pixels
                    Rectangle imageRect = new Rectangle(
                        location.X * fontSize.Width,
                        location.Y * fontSize.Height,
                        imageSize.Width * fontSize.Width,
                        imageSize.Height * fontSize.Height);
                    g.DrawImage(image, imageRect);
                }
            }

        }


        // END IGNORE 


        static void Main(string[] args)
        {

            displayImage();
            
            using (StreamReader reader = File.OpenText(args[0]))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == null) continue;

                    string[] lineArray = line.Replace("(", "").Replace(")", "").Split(' ');

                    string[] streets = lineArray[0].Split(',');
                    string[] avenues = lineArray[1].Split(',');

                    //foreach (string str in avenues) Console.WriteLine(str);

                    double streetsMax = int.Parse(streets[streets.Length-1]);
                    double avenuesMax = int.Parse(avenues[avenues.Length-1]);

                    Console.WriteLine("xMax : " + streetsMax + " and yMax: " + avenuesMax);

                    Console.WriteLine("streets 1: "+ streets[1]);
                    Console.WriteLine("avenue 1: " + streets[1]);

                    int cityBlocks = 0;
                    for (int i = 0; i < streets.Length; i++)
                    {
                        if (i == streets.Length - 1) continue; //temp

                        int streetValue = int.Parse(streets[i]);

                        double avenueValue = avenuesMax / streetsMax * streetValue;
                        Console.WriteLine("line intersection with street " + i + ": " + streetValue +", " + avenueValue);

                        int j = 0;
                        while (int.Parse(avenues[j]) < (int)Math.Floor(avenueValue))
                        {
                            Console.WriteLine("first: "+ int.Parse(avenues[j]) + " second: "+ (int)Math.Floor(avenueValue));
                            j++;
                        }

                        Console.WriteLine("j : " + j);
                        // avenues[j] is now first avenue GT floor(avenueValue)
                        
                        
                        //find the rest of the avenues, in case of a steep slope
                        while (int.Parse(avenues[j]) < (int)Math.Ceiling(avenueValue)  )
                        {
                            j++;
                            cityBlocks+=2; //two city blocks adjacent to 
                        }

                        

                    }

                    Console.WriteLine("CityBlocks: "+ cityBlocks);
                }
        }
    }
}
