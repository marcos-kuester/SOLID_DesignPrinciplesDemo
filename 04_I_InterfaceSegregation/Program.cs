using static System.Console;

namespace _04_I_InterfaceSegregation
{
    public class Document
    {
        public string Text { get; set; }

        public Document(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return $"{Text}";
        }
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IPhotoCopier : IScanner, IPrinter
    {
        void Copy(Document d);
    }

    public class SimplePrinter : IPrinter
    {
        public void Print(Document d)
        {
            WriteLine($"Print {d}");
        }
    }

    public class SimpleScanner : IScanner
    {
        public void Scan(Document d)
        {
            WriteLine($"Scan {d}");
        }
    }

    public class MultiFunctionalDevice : IPhotoCopier
    {
        public void Copy(Document d)
        {
            WriteLine($"Copy {d}");
        }

        public void Print(Document d)
        {
            WriteLine($"Print {d}");
        }

        public void Scan(Document d)
        {
            WriteLine($"Scan {d}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var txt = @"This is a document"; ;
            var doc = new Document(txt);

            var printer = new SimplePrinter();
            printer.Print(doc);

            var scanner = new SimpleScanner();
            scanner.Scan(doc);

            var multi = new MultiFunctionalDevice();
            multi.Copy(doc);
            multi.Scan(doc);
            multi.Print(doc);

            // Result
            // ------------------------
            // Print This is a document
            // Scan This is a document
            // Copy This is a document
            // Scan This is a document
            // Print This is a document
        }
    }
}
