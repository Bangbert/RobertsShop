using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobertsShop.Core.ViewModels
{
    public class BasketSummaryViewModel
    {
        public int BasketCount { get; set; }
        public decimal BasketTotal { get; set; }

        public BasketSummaryViewModel()
        { }

        public BasketSummaryViewModel( int _basketCount, decimal _basketTotal)
        {
            BasketCount = _basketCount;
            BasketTotal = _basketTotal;
        }
    }
}
