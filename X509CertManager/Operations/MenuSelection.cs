using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using X509CertManager.Models;
using W = ConsoleHelperLibrary.Classes.WindowUtility;

namespace X509CertManager.Operations
{
		public class MenuSelection
		{
				[ModuleInitializer]
				public static void Init()
				{
						Console.Title = "X509 Utility Tool";
						Console.SetWindowSize(100, 20);
						W.SetConsoleWindowPosition(W.AnchorWindow.Center);
				}
				public void Option(MenuItem menuItem)
				{
						CertOperation operation = new CertOperation();

						switch (menuItem.Id)
						{
								case 0:
										operation.GetCerts();
										break;
								case 1:
										operation.CreateCert();
										break;
								case 2:
										operation.RemoveCert();
										break;
								case 3:
										operation.GenerateEncodedValue();
										break;
								case 4:
										Environment.Exit(0);
										break;
									
						}
				}
		}
}
