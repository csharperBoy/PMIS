using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service
{
    using System;

    using System;

    namespace ShapeExample
    {
        // تعریف اینترفیس  
        public interface IDrawing
        {
            void Draw(); // متد برای رسم شکل  
        }

        // تعریف کلاس ابسترکت که از اینترفیس ارث‌بری می‌کند  
        public abstract class Shape : IDrawing
        {
            public abstract double Area(); // متد ابسترکت برای محاسبه مساحت  

            // پیاده‌سازی متد اینترفیس  
            public virtual void Draw()
            {
                Console.WriteLine("Drawing a shape.");
            }
        }

        // کلاس دایره که از کلاس ابسترکت Shape ارث‌بری می‌کند  
        public class Circle : Shape
        {
            public double Radius { get; set; }

            public Circle(double radius)
            {
                Radius = radius;
            }

            // پیاده‌سازی متد ابسترکت برای محاسبه مساحت  
            public override double Area()
            {
                return Math.PI * Radius * Radius;
            }

            // می‌توانیم متد Draw را در اینجا هم پیاده‌سازی کنیم یا به استفاده از پیاده‌سازی والد ادامه دهیم  
            public override void Draw()
            {
                Console.WriteLine($"Drawing a Circle with radius: {Radius}");
            }
        }

        // کلاس مستطیل که از کلاس ابسترکت Shape ارث‌بری می‌کند  
        public class Rectangle : Shape
        {
            public double Width { get; set; }
            public double Height { get; set; }

            public Rectangle(double width, double height)
            {
                Width = width;
                Height = height;
            }

            // پیاده‌سازی متد ابسترکت برای محاسبه مساحت  
            public override double Area()
            {
                return Width * Height;
            }

            // می‌توانیم متد Draw را در اینجا هم پیاده‌سازی کنیم  
            public override void Draw()
            {
                Console.WriteLine($"Drawing a Rectangle with width: {Width} and height: {Height}");
            }
        }

        class Program
        {
            static void DrawShapes(List<Shape> shapes)
            {
                foreach (Shape shape in shapes)
                {
                    shape.Draw(); // اینجا متد Draw به صورت چندریختی اجرا می‌شود  
                    Console.WriteLine($"Area: {shape.Area()}");
                }
            }
            static void Main(string[] args)
            {
                // ایجاد نمونه‌هایی از دایره و مستطیل  
                Shape circle = new Circle(5);
                Shape rectangle = new Rectangle(4, 6);

                // محاسبه و نمایش مساحت  
                Console.WriteLine($"Circle Area: {circle.Area()}");
                Console.WriteLine($"Rectangle Area: {rectangle.Area()}");

                // رسم اشکال  
                circle.Draw();
                rectangle.Draw();

                List<Shape> shapes = new List<Shape>
                {
                    new Circle(5),
                    new Rectangle(4, 6)
                };

                DrawShapes(shapes); // همان متد Draw برای هر نوع شکل فراخوانی می‌شود  
            }
        }
    }
}
