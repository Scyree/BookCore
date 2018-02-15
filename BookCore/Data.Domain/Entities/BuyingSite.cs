using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Domain.Entities
{
    public class BuyingSite
    {
        [Key]
        public Guid Id { get; set; }

        public Guid BookId { get; set; }

        public string Site { get; set; }

        public static BuyingSite CreateBuyingSite(Guid bookId, string site)
        {
            var instance = new BuyingSite
            {
                Id = Guid.NewGuid()
            };

            instance.UpdateBuyingSite(bookId, site);

            return instance;
        }

        private void UpdateBuyingSite(Guid bookId, string site)
        {
            BookId = bookId;
            Site = site;
        }
    }
}
