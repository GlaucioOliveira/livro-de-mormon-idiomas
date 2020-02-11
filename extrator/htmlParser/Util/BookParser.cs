using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HtmlAgilityPack;
using htmlParser.Model;

namespace htmlParser.Util
{
    public static class BookParser
    {
        
        public static string htmlTemplate
        {
            get
            {
                return File.ReadAllText(@$"{AppContext.BaseDirectory}assets\template.html");
            }
        }

        public static string htmlTemplateIndex
        {
            get
            {
                return File.ReadAllText(@$"{AppContext.BaseDirectory}assets\template_index.html");
            }
        }

        public static List<Book> getBooks(string filename, LanguageEnum language)
        {
            List<Book> books = new List<Book>();

            var lines = File.ReadLines(@$"{AppContext.BaseDirectory}assets\{filename}.txt");

            int curBook = 1;
            foreach (var line in lines)
            {
                var bookNameSeparator = line.IndexOf("\t");
                var bookname = line.Substring(0, bookNameSeparator);
                var bookHtmlContent = line.Substring(bookNameSeparator + 1).Replace("^N", Environment.NewLine);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(bookHtmlContent);

                HtmlNode divBodyBlock = doc.DocumentNode.SelectNodes("//div[@class='body-block']")[0];
                removeChildNodes(divBodyBlock);

                var divBodyBlockText = divBodyBlock.InnerText.Replace("\t", "");

                Book book = new Book() { Name = bookname, position = curBook };

                int curVerse = 1;
                foreach (var linha in divBodyBlockText.Split(Environment.NewLine))
                {
                    if (linha == "") continue;

                    Verse verse = new Verse();
                    verse.number = curVerse;
                    verse.Idiom = language;
                    verse.content = $"{curVerse} - {linha.Substring(linha.IndexOf(" ") + 1)}";

                    book.Verses.Add(verse);

                    curVerse++;
                }

                books.Add(book);
                curBook++;
            }

            return books;
        }

        static void removeChildNodes(HtmlNode node, string tag = "sup")
        {
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                if (node.ChildNodes[i].Name == tag)
                {
                    node.ChildNodes.RemoveAt(i);
                }
                else
                {
                    removeChildNodes(node.ChildNodes[i], tag);
                }
            }
        }
    }
}
