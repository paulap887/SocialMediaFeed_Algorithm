public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int> Following { get; set; }
    public string Interest { get; set; }
}

public class Post
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int AuthorId { get; set; }
    public DateTime PostedAt { get; set; }
    public List<int> Likes { get; set; }
}

public class Like
{
    public int UserId { get; set; }
    public int PostId { get; set; }
}

public class SocialNetworkService
{
    public List<User> GetUsers() { /* implementation */ return new List<User>(); }
    public List<Post> GetPosts() { /* implementation */  return new List<Post>(); }
    public List<Like> GetLikes() { /* implementation */ return new List<Like>(); }
}

public class ContentFilter
{
    public List<Post> FilterByInterests(List<Post> posts, User user)
    {
        var filteredPosts = new List<Post>();
        foreach (var post in posts)
        {
            if (post.Text.Contains(user.Interest))
            {
                filteredPosts.Add(post);
            }
        }
        return filteredPosts;
    }
}

public class ContentRanker
{
    public List<Post> RankByLikes(List<Post> posts)
    {
        var rankedPosts = posts.OrderByDescending(p => p.Likes.Count).ToList();
        return rankedPosts;
    }
}


public class FeedPresenter
{
    private readonly SocialNetworkService _service;
    private readonly ContentFilter _filter;
    private readonly ContentRanker _ranker;

    public FeedPresenter()
    {
        _service = new SocialNetworkService();
        _filter = new ContentFilter();
        _ranker = new ContentRanker();
    }

    public void DisplayFeed(int userId)
    {
        var user = _service.GetUsers().Single(u => u.Id == userId);
        var posts = _service.GetPosts();
        var likes = _service.GetLikes();

        var filteredPosts = _filter.FilterByInterests(posts, user);
        var rankedPosts = _ranker.RankByLikes(filteredPosts);

        Console.WriteLine($"Welcome, {user.Name}!");
        Console.WriteLine("Here are the top posts in your feed:");
        foreach (var post in rankedPosts)
        {
            Console.WriteLine($"- {post.Text} (likes: {post.Likes.Count})");
        }
    }
}


