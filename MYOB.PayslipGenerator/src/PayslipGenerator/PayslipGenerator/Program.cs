using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PayslipGenerator.BusinessLogic;
using PayslipGenerator.BusinessLogic.Services;
using PayslipGenerator.BusinessLogic.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace PayslipGenerator
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            var inputArgs = Console.ReadLine();
            Console.WriteLine(GeneratePayslip(host.Services, inputArgs));

            return host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                services.AddScoped<IIncomeTaxCalculator, IncomeTaxCalculatorService>()
                        .AddScoped<IGrossIncomeCalculator, GrossIncomeCalculatorService>()
                        .AddScoped<IPayslipDetails, PayslipDetailsService>());
        }

        public static string GeneratePayslip(IServiceProvider services, string inputArguments)
        {
            try
            {
                using var serviceScope = services.CreateScope();
                var provider = serviceScope.ServiceProvider;

                var payslipGenerator = provider.GetRequiredService<IPayslipDetails>();
                var employeeDetails = DataValidator.IsInputStringValid(inputArguments);
                return payslipGenerator.GetMonthlyPayslip(employeeDetails);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}