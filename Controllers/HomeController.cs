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

    public ActionResult Index()
    {
        List<Hashtag> hashtags = _context.Hashtags.ToList();

        List<Review> reviews = _context.Reviews
        .Include(reviews => reviews.Hashtag)
        .ToList();

        HashtagsReviewsDTO hashtagsReviews = new HashtagsReviewsDTO
        {
            Hashtags = hashtags,
            Reviews = reviews
        };

        Console.WriteLine(hashtagsReviews);


        return View(hashtagsReviews);


    }
    [HttpPost]
    public ActionResult Index(HashtagsReviewsDTO hashtagsReviews)
    {
        // pattern to check hashtag
        string pattern = @"#(\w+) ";
        // original message
        string? message = hashtagsReviews.Review?.Message.ToLower();
        // extracted hashtag
        string hashtagWithHash = Regex.Match(hashtagsReviews.Review.Message, pattern).Value;
        string hashtag = Regex.Replace(hashtagWithHash, "#", "");

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
        return RedirectToAction("Index");
    }
}
