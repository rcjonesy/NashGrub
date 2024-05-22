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
        List<Hashtag> hashtags = [.. _context.Hashtags];
        List<Review> reviews = [];

        if (hashtag != null)
        {
            foundHashtag = _context.Hashtags.FirstOrDefault((h) => h.BusinessName == hashtag);

            if (foundHashtag != null)
            {
                reviews = _context.Reviews.Include((review) => review.Hashtag).Where((review) => review.HashtagId == foundHashtag.Id).OrderByDescending((review) => review.DateCreated).ToList();
            }
        }

        if (hashtag == null)
        {
            reviews = _context.Reviews
            .Include(reviews => reviews.Hashtag)
            .OrderByDescending((review) => review.DateCreated)
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
    public ActionResult Index(HashtagsReviewsDTO hashtagsReview)
    {
        // need to change this because it is not doing anything
        if (hashtagsReview.Review.Message == null)
        {
            return RedirectToAction();
        }
        // original message
        string? message = hashtagsReview.Review?.Message?.ToLower();

        // pattern to check hashtag
        string pattern = @"#(\w+)";


        // extracted hashtag
        string hashtagWithHash = Regex.Match(message, pattern).Value;
        string hashtag = Regex.Replace(hashtagWithHash, "#", "").Trim().ToLower();

        Hashtag? findHashtag = _context.Hashtags.FirstOrDefault((h) => h.BusinessName == hashtag);

        if (findHashtag != null)
        {
            Review newReviewObj = new()
            {
                DateCreated = DateTime.Now,
                Message = message,
                Hashtag = findHashtag
            };

            _context.Add(newReviewObj);
            _context.SaveChanges();
        }

        if (findHashtag == null)
        {
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
        }

        return RedirectToAction();
    }
}
