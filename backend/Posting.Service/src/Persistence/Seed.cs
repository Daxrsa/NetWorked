using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            try
            {
                if (context.Posts.Any()) return;

                var posts = new List<Post>
                {
                new Post
                {
                    Id = Guid.Parse("9484bf72-9988-4a66-962d-9176f0f18b4d"),
                    Title = "Today I had a great day!",
                    Description = "I had a lovely day outside with my friends!",
                    DateCreated = DateTime.Parse("12/07/2022 23:00")
                },

                new Post
                {
                    Id = Guid.Parse("1302349e-2b57-47f6-8e4a-18147e49b5ba"),
                    Title = "I have worked on this project...",
                    Description = "blah blah blah...",
                    DateCreated = DateTime.Parse("12/07/2023 23:00")
                },

                new Post
                {
                    Id = Guid.Parse("57147200-36cc-49e6-9ed4-286e8d2b6ac3"),
                    Title = "I am looking to further develop my skills in AI",
                    Description = "blah blah blah...",
                    DateCreated = DateTime.Parse("12/07/2024 23:00")
                }
             };
                await context.Posts.AddRangeAsync(posts);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while seeding the database.", ex);
            }
        }
    }
}