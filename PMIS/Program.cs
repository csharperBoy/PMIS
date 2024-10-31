using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.Map.Concrete;
using Microsoft.Extensions.DependencyInjection;
using PMIS.Forms;
namespace PMIS
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());
        }

        private static void ConfigureServices(IServiceCollection services)
        {
           // services.AddSingleton<AbstractGenericMapHandler, GenericAutoMapHandler>();
            services.AddTransient<AbstractGenericLogHandler, GenericAutoMapHandler>();
            services.AddTransient<AbstractGenericMapHandler, GenericAutoMapHandler>();
            services.AddTransient<AbstractGenericMapHandler, GenericAutoMapHandler>();
            services.AddTransient<AbstractGenericMapHandler, GenericAutoMapHandler>();
            services.AddTransient<AbstractGenericMapHandler, GenericAutoMapHandler>();
            services.AddTransient<AbstractGenericMapHandler, GenericAutoMapHandler>();
            services.AddTransient<AbstractGenericMapHandler, GenericAutoMapHandler>();
            services.AddTransient<AbstractGenericMapHandler, GenericAutoMapHandler>();
            services.AddTransient<AbstractGenericMapHandler, GenericAutoMapHandler>();

        }
    }
}