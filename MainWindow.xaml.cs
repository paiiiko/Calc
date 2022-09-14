using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace calc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty DisplayProperty =
            DependencyProperty.Register("Display", 
            typeof(string), 
            typeof(MainWindow));

        public static readonly DependencyProperty NumberProperty = 
            DependencyProperty.Register("Number",
            typeof(string),
            typeof(MainWindow),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(Newermind), new CoerceValueCallback(CoerceNumber)));

        public static readonly DependencyProperty ResultProprty =
            DependencyProperty.Register("Result",
            typeof(string),
            typeof(MainWindow),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(Newermind2), new CoerceValueCallback(CoerceResult)));

        private static void Newermind(DependencyObject d, DependencyPropertyChangedEventArgs e) {}
        private static void Newermind2(DependencyObject d, DependencyPropertyChangedEventArgs e) { }
        private static object CoerceNumber(DependencyObject d, object baseValue)
        {
            string coerse = (string)baseValue;
            int counter = 10;
            if (coerse == null) return null;
            if (coerse.Contains('-')) counter++;
            if (coerse.Contains('.')) counter++;
            if (coerse.Length > counter) return coerse.Remove(counter);
            else return baseValue;
        }
        private static object CoerceResult(DependencyObject d, object baseValue)
        {
            string coerse = (string)baseValue;
            if(coerse != null && coerse.Contains('.')) coerse = coerse.TrimEnd('0');

            return coerse;
        }

        public string Display
        {
            get { return (string)GetValue(DisplayProperty); } 
            set { SetValue(DisplayProperty, value); }
        }
        public string Number
        {
            get { return (string)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }
        public string Result
        {
            get { return (string)GetValue(ResultProprty); }
            set { SetValue(ResultProprty, value); }
        }
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Percent_Click(object sender, RoutedEventArgs e)
        {
            double percent;

            if (Display == null)
            {
                Number = null;
                return;
            }

            if (Display.Contains('='))
            {
                Clean_Click(sender, e); 
                return;
            }

            if (Display.Contains('*') || Display.Contains('\u00F7'))
            {
                if (number.Visibility == Visibility.Visible)
                {
                    percent = Convert.ToDouble(Number.Replace('.', ',')) / 100;
                    Number = percent.ToString().Replace(',', '.');
                }
                if (result.Visibility == Visibility.Visible)
                {
                    percent = Convert.ToDouble(Result.Replace('.', ',')) / 100;
                    Number = percent.ToString().Replace(',', '.');
                    Result = null;
                    number.Visibility = Visibility.Visible;
                }
            }

            string[] operand = Display.Split(' ');

            if (Display.Contains('-') || Display.Contains('+'))
            {
                if (number.Visibility == Visibility.Visible)
                {
                    percent = (Convert.ToDouble(Number.Replace('.', ',')) / 100) * Convert.ToDouble(operand[0].Replace('.',','));
                    Number = percent.ToString().Replace(',', '.');
                }
                if (result.Visibility == Visibility.Visible)
                {
                    percent = (Convert.ToDouble(Result.Replace('.', ',')) / 100) * Convert.ToDouble(operand[0].Replace('.', ','));
                    Number = percent.ToString().Replace(',', '.');
                    Result = null;
                    number.Visibility = Visibility.Visible;
                }
            }

        }
        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            if (result.Visibility == Visibility.Visible)
            {
                if (Display != null)
                {
                    if (Display.Contains('=')) Display = null;
                    Result = (Math.Sqrt(Convert.ToDouble(Result.Replace('.', ','))).ToString()).Replace(',','.');
                }
            }
            else if (number.Visibility == Visibility.Visible)
            {
                Number = (Math.Sqrt(Convert.ToDouble(Number.Replace('.', ','))).ToString()).Replace(',', '.');
            }
        }

        private void Square_Click(object sender, RoutedEventArgs e)
        {
            if (result.Visibility == Visibility.Visible)
            {
                if (Display != null)
                {
                    if (Display.Contains('=')) Display = null;
                    Result = ((Convert.ToDouble(Result.Replace('.', ',')) * Convert.ToDouble(Result.Replace('.', ','))).ToString()).Replace(',', '.');
                }
            }
            else if (number.Visibility == Visibility.Visible)
            {
                Number = ((Convert.ToDouble(Number.Replace('.', ',')) * Convert.ToDouble(Number.Replace('.', ','))).ToString()).Replace(',', '.');
            }
        }

        private void PartOfWhole_Click(object sender, RoutedEventArgs e)
        {
            double partOfWhole;

            if (result.Visibility == Visibility.Visible)
            {
                if (Result == "0")
                {
                    Result = "Деление на ноль невозможно";
                    return;
                }
                if(Display != null)
                {
                    if (Display.Contains('=')) Display = null;
                    partOfWhole = 1 / Convert.ToDouble(Result.Replace('.', ','));
                    Result = partOfWhole.ToString().Replace(',', '.');
                }
            }
            else if (number.Visibility == Visibility.Visible)
            {
                if (Number == "0" || Number == null || Number == "0.")
                {
                    Result = "Деление на ноль невозможно";
                    return;
                }
                partOfWhole = 1 / Convert.ToDouble(Number.Replace('.', ','));
                Number = partOfWhole.ToString().Replace(',', '.');
            }
        }

        private void CleanEntry_Click(object sender, RoutedEventArgs e)
        {
            Number = null;
            Result = null;
            number.Visibility = Visibility.Visible;
        }

        private void Clean_Click(object sender, RoutedEventArgs e)
        {
            Number = null;
            Display = null;
            Result= null;
            number.Visibility = Visibility.Visible;
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (number.Visibility == Visibility.Visible)
            {
                if (Display != null && Display.Contains('=')) Display = null;
                if (Number == null) return;
                int backspace = Number.Length - 1;
                Number = Number.Remove(backspace);
                if (Number.Length == 1 & Number.Contains('-')) Number = null;
                if (Number == "") Number = null;
            }
            else Display = null;
        }
        private void Division_Click(object sender, RoutedEventArgs e)
        {
            string division = " " + '\u00F7'.ToString() + " ";
            
            if (number.Visibility == Visibility.Visible)
            {
                if (Number == null) Number = "0";
                Number = Number.TrimEnd('.');
                number.Visibility = Visibility.Hidden;
                result.Visibility = Visibility.Visible;
                if (Display != null && Result == null)
                {
                    Display += Number;
                    Display = Display.Replace('\u00F7', '/');
                    Result = new DataTable().Compute(Display, null).ToString().Replace(',', '.');
                    Display = Display.Replace('/', '\u00F7');
                    Display = Result + division;
                    Number = null;
                    return;
                }
                if (Display != null && Result != null && Number != null)
                {
                    result.Visibility = Visibility.Visible;
                    Result = Number;
                    Number = null;
                    Display = Result + division;
                    return;
                }
                Display += Number + division;
                Result = Number;
                Number = null;
                return;
            }
            else if (result.Visibility == Visibility.Visible)
            {
                if (Display != null)
                {
                    if (Display.Contains('='))
                    {
                        Display = Result + division;
                        return;
                    }
                    if (Display.Contains(division)) return;
                    string[] operand = Display.Split(' ');
                    operand[1] = division;
                    Display = String.Join("", operand);
                }
                else Display = Result + division;
            }
        }
        private void Multiplication_Click(object sender, RoutedEventArgs e)
        {
            string multiplication = " * ";

            if (number.Visibility == Visibility.Visible)
            {
                if (Number == null) Number = "0";
                Number = Number.TrimEnd('.');
                number.Visibility = Visibility.Hidden;
                result.Visibility = Visibility.Visible;
                if (Display != null && Result == null)
                {
                    Display += Number;
                    Result = new DataTable().Compute(Display, null).ToString().Replace(',', '.');
                    Display = Result + multiplication;
                    Number = null;
                    return;
                }
                if (Display != null && Result != null && Number != null)
                {
                    result.Visibility = Visibility.Visible;
                    Result = Number;
                    Number = null;
                    Display = Result + multiplication;
                    return;
                }
                //if (Display != null && )
                Display += Number + multiplication;
                Result = Number;
                Number = null;
                return;
            }
            else if (result.Visibility == Visibility.Visible)
            {
                if (Display != null)
                {
                    if (Display.Contains('='))
                    {
                        Display = Result + multiplication;
                        return;
                    }
                    if (Display.Contains(multiplication)) return;
                    string[] operand = Display.Split(' ');
                    operand[1] = multiplication;
                    Display = String.Join("", operand);
                }
                else Display = Result + multiplication;
            }
        }
        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            string minus = " - ";

            if (number.Visibility == Visibility.Visible)
            {
                if (Number == null) Number = "0";
                Number = Number.TrimEnd('.');
                number.Visibility = Visibility.Hidden;
                result.Visibility = Visibility.Visible;
                if (Display != null && Result == null)
                {
                    Display += Number;
                    Result = new DataTable().Compute(Display, null).ToString().Replace(',', '.');
                    Display = Result + minus;
                    Number = null;
                    return;
                }
                if (Display != null && Result != null && Number != null)
                {
                    result.Visibility = Visibility.Visible;
                    Result = Number;
                    Number = null;
                    Display = Result + minus;
                    return;
                }
                Display += Number + minus;
                Result = Number;
                Number = null;
                return;
            }
            else if (result.Visibility == Visibility.Visible)
            {
                if (Display != null)
                {
                    if (Display.Contains('='))
                    {
                        Display = Result + minus;
                        return;
                    }
                    if (Display.Contains(minus)) return;
                    string[] operand = Display.Split(' ');
                    operand[1] = minus;
                    Display = String.Join("", operand);
                }
                else Display = Result + minus;
            }
        }
        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            string plus = " + ";

            if (number.Visibility == Visibility.Visible)
            {
                if (Number == null) Number = "0";
                Number = Number.TrimEnd('.');
                number.Visibility = Visibility.Hidden;
                result.Visibility = Visibility.Visible;
                if (Display != null && Result == null)
                {
                    Display += Number;
                    Result = new DataTable().Compute(Display, null).ToString().Replace(',', '.');
                    Display = Result + plus;
                    Number = null;
                    return;
                }
                if (Display != null && Result != null && Number != null)
                {
                    result.Visibility = Visibility.Visible;
                    Result = Number;
                    Number = null;
                    Display = Result + plus;
                    return;
                }
                Display += Number + plus;
                Result = Number;
                Number = null;
                return;
            }
            else if (result.Visibility == Visibility.Visible)
            {
                if (Display != null)
                {
                    if (Display.Contains('='))
                    {
                        Display = Result + plus;
                        return;
                    }
                    if (Display.Contains(plus)) return;
                    string[] operand = Display.Split(' ');
                    operand[1] = plus;
                    Display = String.Join("", operand);
                }
                else Display = Result + plus;
            }
        }
        private void One_Click(object sender, RoutedEventArgs e)
        {
            string one = "1";
            if (Number == "0")
            {
                Number = one;
                return;
            }
            if (Display != null && Display.Contains('=')) Clean_Click(sender, e);
            number.Visibility = Visibility.Visible;
            result.Visibility = Visibility.Hidden;
            Number += one;
            Result = null;
        }
        private void Two_Click(object sender, RoutedEventArgs e)
        {
            string two = "2";
            if (Number == "0")
            {
                Number = two;
                return;
            }
            if (Display != null && Display.Contains('=')) Clean_Click(sender, e);
            number.Visibility = Visibility.Visible;
            result.Visibility = Visibility.Hidden;
            Number += two;
            Result = null;
        }
        private void Three_Click(object sender, RoutedEventArgs e)
        {
            string three = "3";
            if (Number == "0")
            {
                Number = three;
                return;
            }
            if (Display != null && Display.Contains('=')) Clean_Click(sender, e);
            number.Visibility = Visibility.Visible;
            result.Visibility = Visibility.Hidden;
            Number += three;
            Result = null;
        }
        private void Four_Click(object sender, RoutedEventArgs e)
        {
            string four = "4";
            if (Number == "0")
            {
                Number = four;
                return;
            }
            if (Display != null && Display.Contains('=')) Clean_Click(sender, e);
            number.Visibility = Visibility.Visible;
            result.Visibility = Visibility.Hidden;
            Number += four;
            Result = null;
        }
        private void Fife_Click(object sender, RoutedEventArgs e)
        {
            string fife = "5";
            if (Number == "0")
            {
                Number = fife;
                return;
            }
            if (Display != null && Display.Contains('=')) Clean_Click(sender, e);
            number.Visibility = Visibility.Visible;
            result.Visibility = Visibility.Hidden;
            Number += fife;
            Result = null;
        }
        private void Six_Click(object sender, RoutedEventArgs e)
        {
            string six = "6";
            if (Number == "0")
            {
                Number = six;
                return;
            }
            if (Display != null && Display.Contains('=')) Clean_Click(sender, e);
            number.Visibility = Visibility.Visible;
            result.Visibility = Visibility.Hidden;
            Number += six;
            Result = null;
        }
        private void Seven_Click(object sender, RoutedEventArgs e)
        {
            string seven = "7";
            if (Number == "0")
            {
                Number = seven;
                return;
            }
            if (Display != null && Display.Contains('=')) Clean_Click(sender, e);
            number.Visibility = Visibility.Visible;
            result.Visibility = Visibility.Hidden;
            Number += seven;
            Result = null;
        }
        private void Eight_Click(object sender, RoutedEventArgs e)
        {
            string eight = "8";
            if (Number == "0")
            {
                Number = eight;
                return;
            }
            if (Display != null && Display.Contains('=')) Clean_Click(sender, e);
            number.Visibility = Visibility.Visible;
            result.Visibility = Visibility.Hidden;
            Number += eight;
            Result = null;
        }
        private void Nine_Click(object sender, RoutedEventArgs e)
        {
            string nine = "9";
            if (Number == "0") 
            {
                Number = nine;
                return;
            }
            if (Display != null && Display.Contains('=')) Clean_Click(sender, e);
            number.Visibility = Visibility.Visible;
            result.Visibility = Visibility.Hidden;
            Number += nine;
            Result = null;
        }
        private void Zero_Click(object sender, RoutedEventArgs e)
        {
            string zero = "0";
            number.Visibility = Visibility.Visible;
            result.Visibility = Visibility.Hidden;
            if (result.Visibility == Visibility.Visible)
            {
                if (Display != null && Display.Contains('=')) Clean_Click(sender, e);
                else
                {
                    Result = null;
                    Number = zero;
                } 
            }
            if (Number == null || Number == zero) return;
            Number += zero;
        }
        private void SignChange_Click(object sender, RoutedEventArgs e)
        {
            string signChange = "-";

            if (number.Visibility == Visibility.Visible)
            {
                if (Display != null && Display.Contains('=')) Display = Number;
                if (Number == null || Number == "0") return;
                if (Number.Contains(signChange))
                {
                    Number = Number.Remove(0, 1);
                    if (Display == Number.Insert(0, signChange)) Display = Number;
                }
                else
                {
                    Number = Number.Insert(0, signChange);
                    if (Display == Number.Remove(0, 1)) Display = Number;
                }
            }
            else if (result.Visibility == Visibility.Visible)
            {
                number.Visibility = Visibility.Visible;
                result.Visibility = Visibility.Hidden;
                Number = Result;
                SignChange_Click(sender, e);
            }
        }
        private void DecimalSeparator_Click(object sender, RoutedEventArgs e)
        {
            string decimalSeparator = ".";
            if (result.Visibility == Visibility.Visible)
            {
                result.Visibility = Visibility.Hidden;
                number.Visibility = Visibility.Visible;
                if (Display != null && Display.Contains('='))
                {
                    Clean_Click(sender, e);
                    Number = "0.";
                }
                else 
                {
                    CleanEntry_Click(sender, e);
                    Number = "0.";
                }
            }
            if (Number == null) Number = "0.";
            else if (Number.Contains('.')) return;
            else Number += decimalSeparator;
        }
        private void Equal_Click(object sender, RoutedEventArgs e) 
        {
            string equal = " =";

            if (Display == null && Number == null)
            {
                Display = "0 =";
                Result = "0";
                result.Visibility = Visibility.Visible;
                number.Visibility = Visibility.Hidden;
            }
            if (number.Visibility == Visibility.Visible)
            {
                Number = Number.TrimEnd('.');
                Display += Number + equal;
                Number = null;
                Display = Display.Replace('\u00F7', '/');
                Result = new DataTable().Compute(Display.Replace('=', '\n'), null).ToString().Replace(',', '.');
                Display = Display.Replace('/', '\u00F7');
                result.Visibility = Visibility.Visible;
                number.Visibility = Visibility.Hidden;
                return;
            }

            string[] operand = Display.Split(' ');

            if (result.Visibility == Visibility.Visible)
            {
                if (Display.Contains('='))
                {
                    operand[0] = Result;
                    Display = String.Join(' ', operand);
                }
                else Display += Result + equal;
                Display = Display.Replace('\u00F7', '/');
                Result = new DataTable().Compute(Display.Replace('=', '\n'), null).ToString().Replace(',', '.');
                Display = Display.Replace('/', '\u00F7');
            }
        }
    }
}