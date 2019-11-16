﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace bikevision.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            this.FeatureValueOfItems = new HashSet<FeatureValueOfItem>();
            this.Opinions = new HashSet<Opinion>();
            this.SaleDetails = new HashSet<SaleDetail>();
            this.DiscountCodeForItems = new HashSet<DiscountCodeForItem>();
            this.AspNetUserFavorites = new HashSet<AspNetUserFavorite>();
        }
    
        public int idItem { get; set; }
        [Display(Name = "Nazwa:")]
        [Required(ErrorMessage = "Nazwa jest wymagana.")]
        public string name { get; set; }
        [Display(Name = "Opis:")]
        [Required(ErrorMessage = "Opis jest wymagany.")]
        public string description { get; set; }
        [Display(Name = "Dostępność:")]
        [Required(ErrorMessage = "Dostępność jest wymagana.")]
        public byte availability { get; set; }
        [Display(Name = "Cena:")]
        [Required(ErrorMessage = "Cena jest wymagana.")]
        public decimal price { get; set; }
        [Display(Name = "Promocja (%):")]
        [Required(ErrorMessage = "Promocja jest wymagana.")]
        public Nullable<int> discount { get; set; }
        [Display(Name = "Towar z ekspozycji? (0 - Nie/ 1 - Tak):")]
        [Required(ErrorMessage = "Wartość jest wymagana.")]
        public Nullable<short> outlet { get; set; }
        [Display(Name = "Waga:")]
        [Required(ErrorMessage = "Waga jest wymagana.")]
        public Nullable<double> weight { get; set; }
        [Display(Name = "Wymiary:")]
        [Required(ErrorMessage = "Wymiary są wymagane.")]
        public string dimensions { get; set; }
        public int ItemType_idItemType { get; set; }
        public int Category_idCategory { get; set; }
        public int Brand_idBrand { get; set; }
    
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeatureValueOfItem> FeatureValueOfItems { get; set; }
        public virtual ItemType ItemType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Opinion> Opinions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiscountCodeForItem> DiscountCodeForItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserFavorite> AspNetUserFavorites { get; set; }
    }
}
