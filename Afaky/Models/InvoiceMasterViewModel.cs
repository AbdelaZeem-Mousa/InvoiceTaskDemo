using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Afaky.Models
{
    public class InvoiceMasterViewModel
    {
        public int Id { get; set; }
        [Display (Name ="العميل")]
        [Required(ErrorMessage ="الرجاء اختيار العميل")]
        public int CustomerId { get; set; }
        [Display(Name = "المخزن")]
        [Required(ErrorMessage = "الرجاء اختيار المخزن")]
        public int StoreId { get; set; }
        [Display(Name = "التاريخ")]
        [Required(ErrorMessage = "الرجاء تحديد التاريخ")]
        public DateTime DateInvoice { get; set; }
        [Display(Name = "الوصف")]
        [Required(ErrorMessage = "الرجاء اضافة الوصف")]
        public string Notes { get; set; }
        [Display(Name = "الاجمالى")]
        public decimal SumPrice { get; set; }
        [Display(Name = "نسبة الخصم")]
        public decimal DiscountPrecent { get; set; }
        [Display(Name = "قيمة الخصم")]
        public decimal DiscountVal { get; set; }
        [Display(Name = "الصافى")]
        public decimal Net { get; set; }
        [Display(Name = "العميل")]
        public string CustomerName { get; set; }
        [Display(Name = "المخزن")]
        public string StoreName { get; set; }

        [Display(Name = "المنتج")]
        [Required(ErrorMessage = "Please Enter Product")]
        public int ProductId { get; set; }
        [Display(Name = "السعر")]
        public decimal Price { get; set; }
        [Display(Name = "الكمية")]
        public decimal Qty { get; set; }
        [Display(Name = "الاجمالى")]
        public decimal Total { get; set; }

        public List<InvoiceDetailsViewModel> Details { get; set; }
    }
}
