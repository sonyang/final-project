using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace BookReview.Models.Google.Books {

  public enum BookSearchType : int { ISBN = 0, Title = 1 }
  public sealed class BookSearch {
    private static string BaseUrl = "https://www.googleapis.com/books/v1/volumes";

    public string Kind { get; set; }
    public List<Item> Items { get; set; }
    public int TotalItems { get; set; }

    public static async Task<BookSearch> SearchAsync(BookSearchType SearchType, string Query) {
      using (var client = new HttpClient()) {
        try {
          var results = string.Empty;

          if (SearchType == BookSearchType.ISBN) {
            results = await client.GetStringAsync(string.Format("{0}?q=isbn{1}", BaseUrl, Query));
          }
          else {
            results = await client.GetStringAsync(string.Format("{0}?q=intitle:{1}", BaseUrl, Query));
          }

          return JsonConvert.DeserializeObject<BookSearch>(results);
        }
        catch {
          return new BookSearch { Kind = "books#volumes", TotalItems = 0, Items = new List<Item>() };
        }
      }
    }
  }

  public sealed class Item {
    public string Kind { get; set; }
    public string ID { get; set; }
    public string ETag { get; set; }
    public string SelfLink { get; set; }
    public VolumeInformation VolumeInfo { get; set; }

  }

  public sealed class VolumeInformation {
    public string Title { get; set; }
    public string Publisher { get; set; }
    public string PublishedDate { get; set; }
    public List<string> Authors { get; set; }
    public List<IndustryIdentifier> IndustryIdentifiers { get; set; }
    public ImageLink ImageLinks { get; set; }
    public int PageCount { get; set; }
    public Size Dimensions { get; set; }
  }

  public sealed class IndustryIdentifier {
    public string Type { get; set; }
    public string Identifier { get; set; }
  }

  public sealed class ImageLink {
    public string SmallThumbnail { get; set; }
    public string Thumbnail { get; set; }
    public string Small { get; set; }
    public string Medium { get; set; }
    public string Large { get; set; }
    public string ExtraLarge { get; set; }
  }

  public sealed class Size {
    public string Height { get; set; }
    public string Width { get; set; }
    public string Thickness { get; set; }
  }
}