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
            bool isDelete = args[0] == "delete";
            // Remove *. from domain when validating a wildcard certificate
            var domain = args[1].Replace("*.", string.Empty);
            // Remove the domain part from the entry name
            var dnsEntryName = args[2].Replace($".{domain}", string.Empty);
            var dnsEntryContent = !isDelete ? args[3] : null;

            Console.WriteLine($"{(isDelete ? "Deleting" : "Updating")} TransIP DNS entry for:");
            Console.WriteLine($"Domain: {domain}");
            Console.WriteLine($"DNSName: {dnsEntryName}");
            if(!isDelete)
                Console.WriteLine($"Content: {dnsEntryContent}");

            var domainService = new DomainService(ConfigurationManager.AppSettings["UserName"], ClientMode.ReadWrite, ConfigurationManager.AppSettings["PrivateKey"]);

            var domainData = await domainService.GetInfoAsync(domain);

            // Get the existing dns entries
            var entries = domainData.DnsEntries.ToList();

            if (isDelete)
            {
                var toRemoveEntry = entries.FirstOrDefault(x => x.Name == dnsEntryName && x.Type == DnsEntryType.TXT);
                if (toRemoveEntry != null)
                {
                    entries.Remove(toRemoveEntry);
                    Console.WriteLine($"Removed DNS entry {toRemoveEntry.Name}, with content {toRemoveEntry.Content} and type {toRemoveEntry.Type}");
                }
            }
            else
            {
                // Create a new entry
                entries.Add(new DnsEntry
                {
                    Name = dnsEntryName,
                    Type = DnsEntryType.TXT,
                    Expire = 60, // 1 minute
                    Content = dnsEntryContent
                });
            }

            // Save
            await domainService.SetDnsEntriesAsync(domain, entries.ToArray());
        }
    }
}
