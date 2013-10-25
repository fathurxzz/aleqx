using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Penetron.Utils
{
    public static class Transliterator
    {
        private static Dictionary<char, string> _mReplacementRules;
        private static StringBuilder _mBuilder = new StringBuilder();

        static Transliterator()
        {
            InitializeReplacementRules();
        }

        public static string Transliterate(string str)
        {
            _mBuilder.Clear();

            foreach (char ch in str.ToCharArray())
            {
                string transCh = ch.ToString();

                char loweredCh = char.ToLower(ch);

                if (_mReplacementRules.ContainsKey(loweredCh))
                {
                    transCh = _mReplacementRules[loweredCh];

                    if (char.IsUpper(ch) && transCh != "")
                    {
                        transCh = transCh.Replace(transCh[0], char.ToUpper(transCh[0]));
                    }
                }

                _mBuilder.Append(transCh);
            }

            return _mBuilder.ToString();
        }

        private static void InitializeReplacementRules()
        {
            _mReplacementRules = new Dictionary<char, string>();
            _mReplacementRules['а'] = "a";
            _mReplacementRules['б'] = "b";
            _mReplacementRules['в'] = "v";
            _mReplacementRules['г'] = "g";
            _mReplacementRules['д'] = "d";
            _mReplacementRules['е'] = "e";
            _mReplacementRules['ё'] = "yo";
            _mReplacementRules['ж'] = "zh";
            _mReplacementRules['з'] = "z";
            _mReplacementRules['и'] = "i";
            _mReplacementRules['й'] = "j";
            _mReplacementRules['к'] = "k";
            _mReplacementRules['л'] = "l";
            _mReplacementRules['м'] = "m";
            _mReplacementRules['н'] = "n";
            _mReplacementRules['о'] = "o";
            _mReplacementRules['п'] = "p";
            _mReplacementRules['р'] = "r";
            _mReplacementRules['с'] = "s";
            _mReplacementRules['т'] = "t";
            _mReplacementRules['у'] = "u";
            _mReplacementRules['ф'] = "f";
            _mReplacementRules['х'] = "h";
            _mReplacementRules['ц'] = "c";
            _mReplacementRules['ч'] = "ch";
            _mReplacementRules['ш'] = "sh";
            _mReplacementRules['щ'] = "sh";
            _mReplacementRules['ь'] = "";
            _mReplacementRules['ы'] = "y";
            _mReplacementRules['ъ'] = "";
            _mReplacementRules['э'] = "e";
            _mReplacementRules['ю'] = "yu";
            _mReplacementRules['я'] = "ya";
            _mReplacementRules[' '] = "-";
            _mReplacementRules['/'] = "";
            _mReplacementRules['\\'] = "";
            _mReplacementRules['*'] = "";
            _mReplacementRules[':'] = "";
            _mReplacementRules['"'] = "";
            _mReplacementRules['>'] = "";
            _mReplacementRules['<'] = "";
            _mReplacementRules['|'] = "";
        }
    }
}