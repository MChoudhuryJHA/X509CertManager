using Spectre.Console;
using System.Security.Cryptography.X509Certificates;
using X509CertManager.Models;
using X509CertManager.Operations;

MenuItem menuItem = new MenuItem();

while (menuItem.Id > -1)
{
		AnsiConsole.Clear();
		AnsiConsole.Write(
				new Panel(new Text("X-509 Certificate Maintenance Utility Tool").Centered())
				.Expand()
				.SquareBorder()
				.BorderStyle(new Style(Color.Cornsilk1))
				.Header("[LightGreen]X509 Utility Tool[/]")
				.HeaderAlignment(Justify.Center)			
				);

		menuItem = AnsiConsole.Prompt(MenuOperation.MainMenu());
		MenuSelection selection = new MenuSelection();
		selection.Option(menuItem);
}

