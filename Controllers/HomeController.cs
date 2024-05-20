using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NashGrub.Models;
using NashGrub.Models.DTOs;
using NashGrub.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace NashGrub.Controllers;

public class HomeController : Controller
{
    private readonly NashGrubDbContext _context;

    public HomeController(NashGrubDbContext context)
    {
        _context = context;
    }

    public ActionResult Index(string? hashtag)
    {
        Hashtag? foundHashtag;
        List<Hashtag> hashtags = _context.Hashtags.ToList();
        List<Review> reviews = new();

        if (hashtag != null)
        {
            foundHashtag = _context.Hashtags.FirstOrDefault((h) => h.BusinessName == hashtag);

            if (foundHashtag != null)
            {
                reviews = _context.Reviews.Include((review) => review.Hashtag).Where((review) => review.HashtagId == foundHashtag.Id).ToList();
            }
        }

        if (hashtag == null)
        {
            reviews = _context.Reviews
            .Include(reviews => reviews.Hashtag)
            .ToList();
        }

        HashtagsReviewsDTO hashtagsReviews = new HashtagsReviewsDTO
        {
            Hashtags = hashtags,
            Reviews = reviews
        };

        return View(hashtagsReviews);
    }

    [HttpPost]
    public ActionResult Index(HashtagsReviewsDTO hashtagsReviews)
    {
        // original message
        string? message = hashtagsReviews.Review?.Message?.ToLower();

        // pattern to check hashtag
        string pattern = @"#(\w+)";

        // need to change this because it is not doing anything
        if (message == null)
        {
            return View("Index");
        }

        // extracted hashtag
        string hashtagWithHash = Regex.Match(message, pattern).Value;
        string hashtag = Regex.Replace(hashtagWithHash, "#", "").Trim().ToLower();

        Review newReviewObj = new()
        {
            DateCreated = DateTime.Now,
            Message = message,
            Hashtag = new()
            {
                BusinessName = hashtag.ToLower()
            }
        };

        _context.Add(newReviewObj);
        _context.SaveChanges();
        return RedirectToAction();
    }
}
