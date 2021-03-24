using System;

namespace BSX.FG.Ejemplo.Cliente
{
    internal class Program
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        private static void CrearCliente(ClienteFG clienteFG)
        {
            Console.WriteLine("\n** Creando un cliente");
            var result = clienteFG.CrearCliente(new ClienteSimpleModel()
            {
                CorreoContacto = "contacto@bisimplex.com",
                Nombre = Guid.NewGuid().ToString(),
                NombreContacto = "Nombre",
                RFC = "XEXX010101000"
            });

            if (!result.Success)
            {
                result.DisplayToConsole();
                Console.ReadLine();

                return;
            }

            Console.WriteLine("El identificador del cliente recién creado es: {0}", result.Content);
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadLine();
        }

        private static void Main(string[] args)
        {
            var api_url = "https://test.facturagorila.com";
            var api_key = "iSG/buBwYJfoVMroLV6zRSJXyd3WNF6ZOVZ84eC6nhqqP0E4bxevIV1vV1nsKZYfv+dg/c+2r4Q=";
            var nombre_usuario = "bsxtest@bisimplex.com";
            var contrasenia = "bisimplex";

            var clienteFG = new ClienteFG();
            clienteFG.Inicializar(api_url, api_key, nombre_usuario, contrasenia);

            MostrarClientes(clienteFG);
            //CrearCliente(clienteFG);
            MostrarDocumentos(clienteFG);
        }

        private static void MostrarClientes(ClienteFG clienteFG)
        {
            Console.WriteLine("\n** Consultando la lista de clientes");
            var result = clienteFG.ObtenerClientes("xexx");

            if (!result.Success)
            {
                result.DisplayToConsole();
                Console.ReadLine();

                return;
            }

            int index = 0;
            foreach (var item in result.Content)
            {
                Console.WriteLine($"{++index}: {item.ClienteId} {item.RFC} {item.Nombre}");
            }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadLine();
        }

        private static void MostrarDocumentos(ClienteFG clienteFG)
        {
            Console.WriteLine("\n** Consultando la lista de documentos");
            var result = clienteFG.ObtenerDocumentos("XAXX010101000");

            if (!result.Success)
            {
                result.DisplayToConsole();
                Console.ReadLine();

                return;
            }

            int index = 0;
            foreach (var item in result.Content)
            {
                Console.WriteLine($"{++index}: Borrador: {item.Borrador} {item.RfcReceptor} {item.UUID} {item.FolioCompleto} {item.Receptor}");
            }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadLine();
        }
    }
}