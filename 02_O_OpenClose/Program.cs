using System.Collections.Generic;
using static System.Console;

namespace _02_O_OpenClose
{
    class Program
    {
        public enum Color
        {
            Red, Green, Blue
        }

        public enum Size
        {
            Small, Medium, Large
        }

        public class Product
        {
            private string _name;
            private Color _color;
            private Size _size;

            public Product(string name, Color color, Size size)
            {
                _name = name;
                _color = color;
                _size = size;
            }

            public Color GetColor()
            {
                return _color;
            }

            public Size GetSize()
            {
                return _size;
            }

            public string GetName()
            {
                return _name;
            }
        }

        public interface ISpecification<T>
        {
            bool IsSatisfied(T t);
        }

        public interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
        }

        public class ColorSpecification : ISpecification<Product>
        {
            private Color _color;
            public ColorSpecification(Color color)
            {
                _color = color;
            }

            public bool IsSatisfied(Product t)
            {
                return _color == t.GetColor();
            }
        }

        public class SizeSpecification : ISpecification<Product>
        {
            private Size _size;
            public SizeSpecification(Size size)
            {
                _size = size;
            }
            public bool IsSatisfied(Product t)
            {
                return _size == t.GetSize();
            }
        }

        public class ProductFilter : IFilter<Product>
        {
            public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
            {
                foreach (var item in items)
                {
                    if (spec.IsSatisfied(item))
                        yield return item;
                }
            }
        }

        public class AndSpecification<T> : ISpecification<T>
        {
            private ISpecification<T> _spec1;
            private ISpecification<T> _spec2;

            public AndSpecification(ISpecification<T> spec1, ISpecification<T> spec2)
            {
                _spec1 = spec1;
                _spec2 = spec2;
            }
            public bool IsSatisfied(T t)
            {
                return _spec1.IsSatisfied(t) && _spec2.IsSatisfied(t);
            }
        }

        static void Main(string[] args)
        {
            var prodA = new Product("A", Color.Blue, Size.Small);
            var prodB = new Product("B", Color.Red, Size.Medium);
            var prodC = new Product("C", Color.Red, Size.Large);
            var prodD = new Product("D", Color.Blue, Size.Small);

            var products = new Product[] { prodA, prodB, prodC, prodD };

            var filter = new ProductFilter();

            WriteLine("Red products:");
            var redProdFilter = filter.Filter(products, new ColorSpecification(Color.Red));
            foreach (var item in redProdFilter)
            {
                WriteLine($"{item.GetName()} {item.GetColor()} {item.GetSize()}");
            }

            WriteLine("Blue and Small products:");
            var blueAndSmallProdFilter = filter.Filter(products,
                new AndSpecification<Product>(
                    new ColorSpecification(Color.Blue), 
                    new SizeSpecification(Size.Small)
                )
            );
            foreach (var item in blueAndSmallProdFilter)
            {
                WriteLine($"{item.GetName()} {item.GetColor()} {item.GetSize()}");
            }
        }
    }

}
