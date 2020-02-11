using System;
using System.Collections.Generic;
using System.IO;
using HtmlAgilityPack;
using htmlParser.Model;
using htmlParser.Util;

namespace htmlParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var booksPT = BookParser.getBooks("bom_pt", LanguageEnum.Portuguese);
            var booksEN = BookParser.getBooks("bom_en", LanguageEnum.English);
            var booksES = BookParser.getBooks("bom_es", LanguageEnum.Spanish);

            string workingDir = @$"{AppContext.BaseDirectory}output\";
            string htmlTemplate = BookParser.htmlTemplate;

            #region [Criar o indice principal]

            string htmlTemplateIndex = BookParser.htmlTemplateIndex;
            string booksLinks = "";

            foreach (var book in booksPT)
            {
                booksLinks += $"<a href=\"{book.position}-{book.Name.Trim().Replace(" ", "-").ToLower()}.html\" title=\"{book.Name}\">{book.Name}</><br/>{Environment.NewLine}";
            }

            htmlTemplateIndex = htmlTemplateIndex.Replace("{content}", booksLinks);

            StreamWriter sw2 = new StreamWriter($@"{workingDir}index.html");
            sw2.Write(htmlTemplateIndex);
            sw2.Close();

            #endregion


            #region [criar os versos]
            foreach (var book in booksPT)
            {
                string content = htmlTemplate;
                string verses = "";
                content = content.Replace("{title}", book.Name);
                
                foreach(var verse in book.Verses)
                {
                    var verseEN = booksEN.Find(x => x.position == book.position).Verses.Find(x=> x.number == verse.number);
                    var verseESN = booksES.Find(x => x.position == book.position).Verses.Find(x => x.number == verse.number);

                    verses += $"<p>{verse.content}</p>{Environment.NewLine}";
                    verses += $"<p>{verseEN.content}</p>{Environment.NewLine}";
                    verses += $"<p>{verseESN.content}</p>{Environment.NewLine}";
                }

                content = content.Replace("{content}", verses);

                StreamWriter sw = new StreamWriter($@"{workingDir}{book.position}-{book.Name.Trim().Replace(" ","-").ToLower()}.html");
                sw.Write(content);
                sw.Close();
            }
            #endregion


        }
    }
}
