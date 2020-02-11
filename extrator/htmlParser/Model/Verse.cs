using System;
using System.Collections.Generic;
using System.Text;

namespace htmlParser.Model
{
    public class Verse
    {
        public int number { get; set; }
        public string content { get; set; }
        public LanguageEnum Idiom { get; set; }
    }
}
