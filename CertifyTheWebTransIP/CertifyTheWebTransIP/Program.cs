using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using TransIp.Api;
using TransIp.Api.Dto;

namespace CertifyTheWebTransIP
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            // Remove *. from domain when validating a wildcard certificate
            var domain = args[0].Replace("*.", "");

            Console.WriteLine($"Domain: {domain}");
            Console.WriteLine($"DNSName: {args[1]}");
            Console.WriteLine($"Content: {args[2]}");


            var domainService = new DomainService(ConfigurationManager.AppSettings["UserName"], ClientMode.ReadWrite, ConfigurationManager.AppSettings["PrivateKey"]);

            var domainData = await domainService.GetInfoAsync(domain);

            // Get the existing dns entries
            var entries = domainData.DnsEntries.ToList();

            // Create a new entry
            entries.Add(new DnsEntry
            {
                Name = args[1],
                Type = DnsEntryType.TXT,
                Expire = 3600, // 1 hour
                Content = args[2]
            });

            // Save
            await domainService.SetDnsEntriesAsync(domain, entries.ToArray());
        }
    }
}
