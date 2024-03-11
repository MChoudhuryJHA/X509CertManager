using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using X509CertManager.Models;

namespace X509CertManager.Operations
{
		public class CertOperation
		{
				public void GetCerts()
				{
						// Collect X509 certs from localmachine storage
						X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
						store.Open(OpenFlags.ReadOnly | OpenFlags.MaxAllowed);
						X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
						X509Certificate2Collection rdpX509Certs = (X509Certificate2Collection)collection.Find(X509FindType.FindBySubjectName, "RemoteDepositPlus", false);

						if (rdpX509Certs.Count == 0)
						{
								AnsiConsole.MarkupLine("[yellow]Certificate not found[/], press [b]ENTER[/] to return to menu");
								Console.ReadLine();
								return;
						}

						var table = new Table()
								.RoundedBorder()
								.AddColumn("[b]Subject[/]")
								.AddColumn("[b]Issuer[/]")
								.AddColumn("[b]CreateDate[/]")
								.AddColumn("[b]ExpiryDate[/]")
								.AddColumn("[b]Thumbprint[/]")
								.AddColumn("[b]Storage[/]")
								.Alignment(Justify.Center)
								.BorderColor(Color.LightSlateGrey)
								.Title("[LightGreen]List of Certificates[/]");

						foreach (var cert in rdpX509Certs)
						{
								if (cert.Subject == "Return")
								{
										continue;
								}

								table.AddRow(
										cert.Subject,
										cert.Issuer,
										cert.NotBefore.ToShortDateString(),
										cert.NotAfter.ToShortDateString(),
										cert.Thumbprint,
										StorageType.LocalMachine.ToString()
										);
						}
						
						// Print cert
						AnsiConsole.Write(table);
						AnsiConsole.MarkupLine("Press [b]ENTER[/] to return to menu");
						Console.ReadLine();
				}

				public void GenerateEncodedValue()
				{
						var x509 = new X509Certificate2(File.ReadAllBytes("C:\\X509Certificates\\RemoteDepositPlusClient.cer"));
						string encodedValue = ExportToPEM(x509);
						AnsiConsole.MarkupLine(encodedValue);
						AnsiConsole.MarkupLine("Press [b]ENTER[/] to return to menu");
						Console.ReadKey();
				}

				private string ExportToPEM(X509Certificate2 x509)
				{
						StringBuilder builder = new StringBuilder();

						builder.AppendLine("-----BEGIN CERTIFICATE-----");
						builder.AppendLine(Convert.ToBase64String(x509.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks));
						builder.AppendLine("-----END CERTIFICATE-----");

						return builder.ToString();
				}

				public void RemoveCert()
				{
						try
						{
								X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
								store.Open(OpenFlags.ReadOnly | OpenFlags.MaxAllowed);
								X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
								X509Certificate2Collection rdpCerts = (X509Certificate2Collection)collection.Find(X509FindType.FindBySubjectName, "RemoteDepositPlus", false);

								AnsiConsole.MarkupLine("Number of RemoteDepositPlus X.509 Certificates: {0}{1}", rdpCerts.Count, Environment.NewLine);
							
								if (rdpCerts.Count > 0)
								{
										foreach (X509Certificate2 certificate in rdpCerts)
										{
												byte[] bytes = certificate.RawData;
												AnsiConsole.MarkupLine("Friendly Name: {0}{1}", certificate.FriendlyName, Environment.NewLine);
												AnsiConsole.MarkupLine("Simple Name: {0}{1}", certificate.GetNameInfo(X509NameType.SimpleName, true), Environment.NewLine);
												store.Remove(certificate);
										}

										AnsiConsole.MarkupLine("Deleted Successfully. Press [b]ENTER[/] to return to menu");
								}
								else
								{
										AnsiConsole.MarkupLine("X.509 Certificate is not found. Press [b]ENTER[/] to return to menu");
								}

								store.Close();

								Console.ReadLine();
								
						}
						catch (CryptographicException ex)
						{
								AnsiConsole.MarkupLine(ex.Message);
						}

				}

				public void CreateCert()
				{
						
						// Create X509 store 
						X509Store store = new X509Store(StoreLocation.LocalMachine);
						store.Open(OpenFlags.ReadWrite);

						// Create certificate from certificate file
						X509Certificate2 x509 = new X509Certificate2("C:\\X509Certificates\\RemoteDepositPlusClient.cer");

						// Add certificate to the store
						store.Add(x509);
											
						AnsiConsole.MarkupLine("Created Successfully. Press [b]ENTER[/] to return to menu");
						Console.ReadLine();
				}
		}
}
