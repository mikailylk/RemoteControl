using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RemoteControl
{
    class KeyCommandsValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // Check if input is greater than 2 chars or empty
            string val = value.ToString();
            if (value.ToString() == string.Empty || val.Count<char>() < 2)
            {
                return new ValidationResult(false, "Bitte Wert in HEX (2-stellig) eingeben!");
            }
            if(val.Count<char>() > 2)
            {
                return new ValidationResult(false, "HEX-Wert muss 2-stellig sein!");
            }

            // HEX-Characters
            List<char> hexvalues = new List<char>();
            hexvalues.Add('0');
            hexvalues.Add('1');
            hexvalues.Add('2');
            hexvalues.Add('3');
            hexvalues.Add('4');
            hexvalues.Add('5');
            hexvalues.Add('6');
            hexvalues.Add('7');
            hexvalues.Add('8');
            hexvalues.Add('9');
            hexvalues.Add('A');
            hexvalues.Add('B');
            hexvalues.Add('C');
            hexvalues.Add('D');
            hexvalues.Add('E');
            hexvalues.Add('F');

            int counter = 0;
            // Check if HEX-Letters are right
            foreach (char letter in val)
            {
                foreach (char hexletters in hexvalues)
                {
                   if (letter.ToString().ToUpper() == hexletters.ToString().ToUpper())
                    {
                        counter++;
                    }
                }
            }
            if (counter > 0)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Eingabe falsch (keine HEX-Struktur)!");
            }

        }
    }
}
