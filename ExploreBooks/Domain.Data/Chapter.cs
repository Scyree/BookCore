using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class Chapter
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid BookId { get; set; }

        public string Chapters { get; set; }

        public static Chapter CreateChapter(Guid userId, Guid bookId)
        {
            var instance = new Chapter
            {
                Id = Guid.NewGuid()
            };

            instance.UpdateChapter(userId, bookId);

            return instance;
        }

        private void UpdateChapter(Guid userId, Guid bookId)
        {
            UserId = userId;
            BookId = bookId;
        }
    }
}
