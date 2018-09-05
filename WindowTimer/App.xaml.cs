using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices; //Dyrektywa potrzebna do użycia niezbędnych nam metod
using System.Text;
using System.Windows;

namespace WindowTimer
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Stack applnames = new Stack();

        public static Hashtable applhash = new Hashtable();

        public static DateTime applfocustime; //data wstawienia ostatniego okna

        public static string appltitle;

        public static TimeSpan timeInterval; //ostatni przedział czasu

        public static string appName, prevvalue; //prevvalue- pełna nazwa ostatniego okna



        /// <summary>
        /// Deklaracja funkcji Windows API
        /// </summary>
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        //This Function is used to get Active process ID
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 GetWindowThreadProcessId(IntPtr handle, out Int32 processId);

        /// <summary>
        /// Funkcja zwracająca nazwę aktualnego okna
        /// </summary>
        /// <returns>Zwraca "" gdy nie uda się utworzyć uchwytu, "unknown" gdy nazwa jest zbyt krótka lub długa</returns>
        public static string GetActiveWindow()
        {
            const int nChars = 256;
            IntPtr handle;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();

            if (handle.Equals(IntPtr.Zero)) return "";

            int intLength = GetWindowText(handle, Buff, nChars); //Do Buff wrzucamy nazwę aktualnego okna

            if ((intLength <= 0) || (intLength > Buff.Length)) return "unknown";

            return Buff.ToString();
        }

        public static Int32 GetWindowProcessID(IntPtr handle)

        {
            //This Function is used to get Active process ID...

            Int32 id;

            GetWindowThreadProcessId(handle, out id);

            return id;

        }

        public static void Monitoring()
        {
            try

            {


                bool isNewAppl = false;

                IntPtr handle = GetForegroundWindow();

                Int32 id = GetWindowProcessID(handle);

                Process p = Process.GetProcessById(id);

                appName = p.ProcessName;

                appltitle = GetActiveWindow();

                string fullName = appltitle;

                if (!applnames.Contains(fullName) && fullName != "MainWindow")

                {

                    applnames.Push(fullName);

                    applhash.Add(fullName, 0);

                    isNewAppl = true;

                }

                if (prevvalue != (fullName))

                {

                    IDictionaryEnumerator en = applhash.GetEnumerator();

                    timeInterval = DateTime.Now.Subtract(applfocustime);

                    while (en.MoveNext())

                    {

                        if (en.Key.ToString() == prevvalue)

                        {

                            double prevseconds = Convert.ToDouble(en.Value);

                            applhash.Remove(prevvalue);

                            applhash.Add(prevvalue, ((int)timeInterval.TotalSeconds + (int)prevseconds)); //Aktualizujemy czas

                            break;

                        }

                    }

                    prevvalue = fullName; //Przypisujemy nową wartość

                    applfocustime = DateTime.Now;

                }

                if (isNewAppl)

                    applfocustime = DateTime.Now;

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message + ":" + ex.StackTrace);

            }
        }
    }
}
