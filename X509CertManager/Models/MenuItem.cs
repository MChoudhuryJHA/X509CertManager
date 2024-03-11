using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X509CertManager.Models
{
		public class MenuItem
		{
				public int Id { get; set; }
				public string Text { get; set; }
				public override string ToString() => Text;
				
		}
}
