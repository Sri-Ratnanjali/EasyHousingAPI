using System;
using System.ComponentModel;

namespace EasyHousingAPI.Models
{
    public class Seller
    {
        /// <summary>
        /// Seller ID
        /// </summary>
        [DisplayName("Seller ID")]
        public string SellerID { get; set; } = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Trim('=').ToUpper();
        /// <summary>
        /// Name of the Seller
        /// </summary>
        [DisplayName("Seller Name")]
        public string SellerName { get; set; }
        /// <summary>
        /// Seller MobileNo
        /// </summary>
        [DisplayName("Seller MobileNo")]
        public string SellerMobileNo { get; set; }
        /// <summary>
        /// Property Type
        /// </summary>
        [DisplayName("Property Type")]
        public string PropertyType { get; set; }

        /// <summary>
        /// Name of the Property
        /// </summary>
        [DisplayName("Property Name")]
        public string PropertyName { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [DisplayName("Description")]
        public string Description { get; set; }


        /// <summary>
        /// Address 
        /// </summary>
        [DisplayName("Address")]
        public string Address { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        [DisplayName("Price")]
        public int Price { get; set; }
        /// <summary>
        /// Region
        /// </summary>
        [DisplayName("Region")]
        public string Region { get; set; }

        /// <summary>
        /// Property Option
        /// </summary>
        [DisplayName("Property Option")]
        public string PropertyOption { get; set; }


    }
}
