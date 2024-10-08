﻿using MovieReviewer.Shared.Core.Enums;

namespace MovieReviewer.Shared.Core.Domain
{
    public class Movie : BaseEntity
    {
        public required string Title { get; set; }
        public required string Plot { get; set; }
        public RatingSystem MovieRating { get; set; }
        public Language MovieLanguage { get; set; }
        public required string ImdbId { get; set; }
        public required double ImdbRating { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public bool IsDisabled { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
