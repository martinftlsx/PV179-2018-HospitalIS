using HospitalISDBContext;
using System.Linq;

namespace HospitalIS.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new HospitalISDbContext())
            {
                System.Console.WriteLine(dbContext.Users.First().Name);
                System.Console.WriteLine(dbContext.Users.First().Id);
                System.Console.WriteLine(dbContext.Users.First(p => p.Name == "Rick").Surname);
                System.Console.WriteLine(dbContext.Users.First(p => p.Name == "Rick").Id);
            }
        }
    }
}
