using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace _05_D_DependencyInversion
{
    public enum LogType
    {
        Information, Warning, Error
    }

    public interface ILogger
    {
        void Log(LogType logType, string message);
    }

    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }

    public interface IProductRepository : IRepository<Product>
    {
        void AddProduct(Product product);
    }

    public class AppLogger : ILogger
    {
        public void Log(LogType logType, string message)
        {
            var logTypeString = Enum.GetName(typeof(LogType), logType);
            WriteLine($"[{DateTime.Now}] {logTypeString}: {message}");
        }
    }

    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }
    }

    public class ProductRepository : IProductRepository
    {
        private static List<Product> _products = new List<Product>();

        public ProductRepository()
        {
            // sample data
            _products.Add(new Product { Id = 1, Name = "A", Price = 9.99m });
            _products.Add(new Product { Id = 2, Name = "B", Price = 8.99m });
            _products.Add(new Product { Id = 3, Name = "C", Price = 7.99m });
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await Task.Run(() => _products);
        }

        public Product GetById(int id)
        {
            return _products.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await Task.Run(() => _products.FirstOrDefault(x => x.Id == id));
        }
    }

    public class ProductController
    {
        private readonly IProductRepository _productRepo;
        private ILogger _logger;

        public ProductController(ILogger logger, IProductRepository productRepo)
        {
            _logger = logger;
            _productRepo = productRepo;
        }

        public IEnumerable<Product> GetAll()
        {
            _logger.Log(LogType.Information, "Product Controller GetAll() Called");
            return _productRepo.GetAll();
        }
    }

    public class ProductView
    {
        public void PrintProducts(IEnumerable<Product> products)
        {
            foreach (var item in products)
            {
                WriteLine($"{item.Id} {item.Name} {item.Price}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // IoC
            ILogger loger = new AppLogger();
            IProductRepository productRepo = new ProductRepository();

            var productController = new ProductController(loger, productRepo);
            var productView = new ProductView();
            productView.PrintProducts(productController.GetAll());

            // Result
            // ---------------------------------------------------------------------
            // [13/09/2020 16:06:13] Information: Product Controller GetAll() Called
            // 1 A 9,99
            // 2 B 8,99
            // 3 C 7,99
        }
    }
}
