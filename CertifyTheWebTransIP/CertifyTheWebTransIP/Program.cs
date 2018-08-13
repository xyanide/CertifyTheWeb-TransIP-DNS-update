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
            var domainService = new DomainService(ConfigurationManager.AppSettings["UserName"], ClientMode.ReadWrite, ConfigurationManager.AppSettings["PrivateKey"]);

            var domain = await domainService.GetInfoAsync(args[0]);

            // Get the existing dns entries
            var entries = domain.DnsEntries.ToList();

            // Create a new entry
            entries.Add(new DnsEntry
            {
                Name = args[1],
                Type = DnsEntryType.TXT,
                Expire = 3600, // 1 hour
                Content = args[2]
            });

            // Save
            await domainService.SetDnsEntriesAsync(args[0], entries.ToArray());
        }
    }
}
