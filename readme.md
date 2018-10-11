# Certify the web - TransIP

This command line utility works with [CertifyTheWeb](https://docs.certifytheweb.com/docs/dns-validation.html) dns validation and can be used to automatically update the TransIP dns 
records for the domain you want to request an ssl certificate for

## Getting Started

Download the source and compile to a regular exe console application

### Prerequisites

* Visual studio (community edition is free of charge), downloading exe's from the internet is dangerous you know?
* [CertifyTheWeb](https://www.certifytheweb.com)

### Installing

Configure CertifyTheWeb as follows

1. On the authorization tab
2. Set challenge type to "dns-01"
3. Set DNS Update Method to "(Use custom script)"
4. Set the script paths as follows (point it to the path of your compiled files): ![CertifyTheWeb configuration](https://i.imgur.com/K7EdHZS.png)
5. Update the paths in the .bat files to their appropriate locations
6. I'd suggest using a propagation delay of 300 seconds, as this will give the nameservers enough time to update
7. Update the CertifyTheWebTransIP.dll.config file with the applicable username and private key (you can generate this on the TransIP user panel)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details