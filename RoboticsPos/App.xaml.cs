using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoboticsPos.Data.Repositories;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using AutoMapper;
using RoboticsPos.Services;
using RoboticsPos.UI;

namespace RoboticsPos
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost AppHost { get; set; }

        private String path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RoboticsPos");


        public App()
        {
             if (!Directory.Exists(path))
             {
                Directory.CreateDirectory(path);
            }
            
            if (!File.Exists(Variablies.StaticVariablies.DataBaseName))
             {
                File.Create(Variablies.StaticVariablies.DataBaseName); ;
            }


            using var dbcontext = new AppDbContext();             
            dbcontext.Database.Migrate();

            AppHost = Host.CreateDefaultBuilder().ConfigureServices((_, services) =>

            {
                services.AddDbContext<AppDbContext>();
                services.AddTransient<MainWindow>();
                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IUserService, UserService>();
                
                services.AddScoped<IProductRepository, ProductRepository>();
                services.AddScoped<IProductService, ProductService>();
                
                services.AddScoped<ICheckPrinterRepository, CheckPrinterRepository>();
                services.AddScoped<ICheckPrinterService, CheckPrinterService>();
              
                services.AddScoped<IClientRepository, ClientRepository>();
                services.AddScoped<IClientService, ClientService>();

                services.AddScoped<IEmployeeRepository, EmployeeRepository>();
                services.AddScoped<EmployeeService>();
                services.AddScoped<IDiscountService, DiscountService>();
                services.AddScoped<IDiscountRepository, DiscountRepository>();
                services.AddScoped<ICompanyRepository, CompanyRepository>();
                services.AddScoped<ICompanyService, CompanyService>();
                services.AddScoped<ICategoryRepository, CetegoryRepository>();
                services.AddScoped<ICategoryService,CategoryService>();
                services.AddScoped<IShopRepository, ShopRepository >();
                services.AddScoped<IShopService, ShopService>();
                
                services.AddAutoMapper(typeof(IMapper));
                services.AddTransient<MainWindow>();
                services.AddTransient<LoginPage>();
                services.AddTransient<PinKodPage>();
                services.AddTransient<MenyuPage>();
                services.AddTransient<KassaPage>();
                services.AddTransient<XodimCrudPage>();
                services.AddTransient<XodimCreatePage>();
                services.AddTransient<StoreControl>();
                services.AddTransient<ProductCreatePage>();
                services.AddTransient<ProductListPage>();
                services.AddTransient<CheckForm>();
                services.AddTransient<CheckCreatePage>();
                services.AddTransient<ClientForm>();
                services.AddTransient<ClientCreatePagae>();
                services.AddTransient<ChegirmaPage>();

            }
            ).Build();

        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();

            var startupform = AppHost.Services.GetRequiredService<MainWindow>();

            startupform.Show();
            base.OnStartup(e);

        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            base.OnExit(e);
        }
    }

}
