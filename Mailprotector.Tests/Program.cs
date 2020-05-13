using System;

namespace Mailprotector.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Run();

            Console.ReadKey();
        }

        static void Run()
        {
            try
            {
                Mailprotector_Api.MailprotectorApi api = new Mailprotector_Api.MailprotectorApi("https://emailservice.io/", "t02gi3ulqwzRcrdFMyvNj82qHLBa8bhR");
                var result = api.DeleteCustomer(17986);

                //Console.WriteLine(result.Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
