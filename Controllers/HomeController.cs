using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NashGrub.Models;
using NashGrub.Models.DTOs;
using NashGrub.Data;
using Microsoft.EntityFrameworkCore;




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

}
