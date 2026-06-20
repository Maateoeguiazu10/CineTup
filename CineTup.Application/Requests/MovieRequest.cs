using CineTup.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CineTup.Application.Requests
{
    public class MovieRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string BannerUrl { get; set; } = string.Empty;

        public int Duration { get; set; }

        public string Language { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }
    }
}
