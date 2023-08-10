using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.ViewModels {
	public class ComsumerInfoVM
    {
		public ApplicationUser ApplicationUser { get; set; }
        public PurchasePoint PurchasePoint { get; set; }
    }
}
