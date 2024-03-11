using Spectre.Console;
using X509CertManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X509CertManager.Operations
{
		public class MenuOperation
		{
				private static Style HighlightStyle => new Style(Color.LightGreen, Color.Black, Decoration.None);
				public static SelectionPrompt<MenuItem> MainMenu()
				{
						SelectionPrompt<MenuItem> menu = new()
						{
								HighlightStyle = HighlightStyle
						};

						menu.Title("Select an [B]option[/]");
						menu.AddChoices(new List<MenuItem>()
						{
								new() {Id = 0, Text = "Show X.509 Cert"},
								new() {Id = 1, Text = "Create X.509 Cert"},
								new() {Id = 2, Text = "Remove X.509 Cert"},
								new() {Id = 3, Text = "Generate Base64 Value"},
								new() {Id = 4, Text = "Exit"},
						});
							
						return menu;
				}
		}
}
