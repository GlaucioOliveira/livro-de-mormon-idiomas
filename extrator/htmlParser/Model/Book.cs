using System;
using System.Collections.Generic;
using System.Text;

namespace htmlParser.Model
{
    public class Book
    {
        public Book()
        {
            Verses = new List<Verse>();
        }

        public int position { get; set; }
        public string Name { get; set; }

        public List<Verse> Verses { get; set; }
    }
}
