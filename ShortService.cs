public record UrlRecord(string Key, string Url);
public class ShortService
{

  private static Dictionary<string, UrlRecord> Database = new Dictionary<string, UrlRecord>();

  private static string urlSafeChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
  private static string ShortenUrl(string path)
  {
    var result = "";
    var maxLength = 6;
    var retryCount = 0;
    while (result == "")
    {
      for (int i = 0; i < maxLength; i++)
      {
        result += urlSafeChars[new Random().Next(0, urlSafeChars.Length)];
      }
      if (Database.ContainsKey(result))
      {
        result = "";
      }
      retryCount++;
      if (retryCount % 10 == 0)
      {
        maxLength++;
      }
    }
    return result;
  }

  public string Shorten(string urlString)
  {
    var url = new Uri(urlString);
    var path = url.AbsolutePath;
    var key = ShortenUrl(path);
    Database[key] = new UrlRecord(key, urlString);
    return key;
  }

  public string? Expand(string key)
  {
    if (Database.ContainsKey(key))
    {
      return Database[key].Url;
    }
    return null;
  }
}