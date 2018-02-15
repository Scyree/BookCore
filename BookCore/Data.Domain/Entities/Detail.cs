using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Domain.Entities
{
    public class Detail
    {
        [Key]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public static Detail CreateDetail(string text)
        {
            var instance = new Detail
            {
                Id = Guid.NewGuid()
            };

            instance.UpdateDetail(text);

            return instance;
        }

        private void UpdateDetail(string text)
        {
            Text = text;
        }
    }
}
