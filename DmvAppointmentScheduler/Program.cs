using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DmvAppointmentScheduler
{
    class Program
    {
        public static Random random = new Random();
        public static List<Appointment> appointmentList = new List<Appointment>();
        static void Main(string[] args)
        {
            CustomerList customers = ReadCustomerData();
            TellerList tellers = ReadTellerData();
            Calculation(customers, tellers);
            OutputTotalLengthToConsole();

        }
        private static CustomerList ReadCustomerData()
        {
            string fileName = "CustomerData.json";
            string path = Path.Combine(Environment.CurrentDirectory, @"InputData\", fileName);
            string jsonString = File.ReadAllText(path);
            CustomerList customerData = JsonConvert.DeserializeObject<CustomerList>(jsonString);
            return customerData;

        }
        private static TellerList ReadTellerData()
        {
            string fileName = "TellerData.json";
            string path = Path.Combine(Environment.CurrentDirectory, @"InputData\", fileName);
            string jsonString = File.ReadAllText(path);
            TellerList tellerData = JsonConvert.DeserializeObject<TellerList>(jsonString);
            return tellerData;

        }
        static void Calculation(CustomerList customers, TellerList tellers)
        {
            //Least common type
            //Console.WriteLine(LeastCommonCustomer(customers));
            //sort Customers by length
            customers.Customer.OrderBy(x => x.duration);
            foreach(Customer customer in customers.Customer)
            {
                var appointment = new Appointment(customer, findBestTeller(customer, tellers));
                appointmentList.Add(appointment);
            }
            

            
            // What is less common customer
            // Your code goes here .....
            // Re-write this method to be more efficient instead of a random assignment
            //foreach(Customer customer in customers.Customer)
            //{
            //    var appointment = new Appointment(customer, tellers.Teller[random.Next(150)]);
            //    appointmentList.Add(appointment);
            //}
        }
        static Teller findBestTeller(Customer customer, TellerList tellers)
        {
            Teller best = tellers.Teller[0];
            var typeSort = tellers.Teller.Where(x => x.specialtyType.Equals(customer.type));
            var multipleSort = typeSort.OrderBy(x => x.multiplier);
            var lowestNumber = 999999;
            foreach(Teller teller in multipleSort)
            {
                if (TotalforTeller(teller) < lowestNumber)
                {
                    best = teller;
                };
            }
            if(best == tellers.Teller[0])
            {
               best = tellers.Teller[random.Next(150)];
            }
            //tellers.Teller.OrderBy(x => x.)
            return best;
        }
        static string LeastCommonCustomer(CustomerList customers)
        {
            int least = 999999999;
            string leastType = "";
            var groups = customers.Customer.GroupBy(i => i.type);
            foreach(var grp in groups)
            {
                var type = grp.Key;
                var total = grp.Count();
                if(total < least)
                {
                    least = total;
                    leastType = type;
                }
            }
            return leastType;
        }
        static int TotalforTeller(Teller teller)
        {
            if (appointmentList.Count > 0)
            {
                var tellerAppointments =
                from appointment in appointmentList
                group appointment by appointment.teller into tellerGroup
                select new
                {
                    teller = tellerGroup.Key,
                    totalDuration = tellerGroup.Sum(x => x.duration),
                };
                var total = tellerAppointments.Where(i => i.teller.Equals(teller)).LastOrDefault();
                if (total != null)
                {
                    if (Double.IsNaN(total.totalDuration))
                    {
                        return 0;
                    }
                    else
                    {
                        return (int)total.totalDuration;
                    }

                }
                
            }
            return 0;
            

        }
        static void OutputTotalLengthToConsole()
        {
            var tellerAppointments =
                from appointment in appointmentList
                group appointment by appointment.teller into tellerGroup
                select new
                {
                    teller = tellerGroup.Key,
                    totalDuration = tellerGroup.Sum(x => x.duration),
                };
            var max = tellerAppointments.OrderBy(i => i.totalDuration).LastOrDefault();
            Console.WriteLine("Teller " + max.teller.id + " will work for " + max.totalDuration + " minutes!");
        }

    }
}
