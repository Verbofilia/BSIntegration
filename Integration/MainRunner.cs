using Autofac;
using BaselinkerConnector;
using Common.Interfaces;
using Common.Services;
using SubiektConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Integration
{
    public class MainRunner
    {
        private readonly IContainer _container;
        public MainRunner()
        {
            _container = GetContainer();
        }

        private IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<BaselinkerClient>()
                .As<ISourceConnector>()
                .InstancePerLifetimeScope();
            builder.RegisterType<SubiektClient>()
                .As<ITargetConnector>()
                .InstancePerLifetimeScope();
            builder.RegisterType<MigratorService>()
                .As<IMigrator>()
                .InstancePerLifetimeScope();
            return builder.Build();
        }

        public void RunOnce()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Inicjalizacja...");
            var migrator = _container.Resolve<IMigrator>();
            Console.WriteLine("Test polaczen...");
            var res = migrator.TestConnections();
            if (!res.IsSuccess)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Blad polaczenia:{res.ErrorMessage}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Koniec pracy, wciśnij enter.");
                Console.ReadLine();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Test polaczen udany");
            int orderId;
            bool success = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Podaj numer zamowienia do przeniesiena (int):");
                var entry = Console.ReadLine();
                if (!int.TryParse(entry, out orderId) || orderId<=0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Niepoprawny numer zamowienia");
                }
                else
                {
                    success = true;
                }


            } while (!success);

            var migrationResult = migrator.MigrateOrder(orderId).Result;
            if (migrationResult.IsSuccess)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Migracja zakonczona sukcesem");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Podczas migracji wystapil blad:{migrationResult.ErrorMessage}");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Koniec pracy, wciśnij enter.");
            Console.ReadLine();
            return;
        }
    }
}
