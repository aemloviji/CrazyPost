using CrazyPost.Contexts;
using CrazyPost.Models;
using System;
using System.Linq;

namespace CrazyPost.Context
{
    public static class DbInitializer
    {

        public static void Initialize(ApiDbContext context)
        {
            // Look for any posts.
            if (context.Post.Any())
            {
                return;   // DB has been seeded
            }


            var posts = new Post[]
            {
                new Post {
                    Text ="Microsoft Azure. Turn your ideas into solutions faster using a trusted cloud that is designed for you",
                    CreatedBy ="aemloviji",
                    InsertDate = DateTime.Now.AddDays(-5)
                },
                new Post {
                     Text ="Spring Framework. The right stack for the right job",
                     CreatedBy ="aemloviji",
                    InsertDate = DateTime.Now.AddDays(-2)
                },
                new Post {
                    Text ="Ruby on Rails. Imagine what you could build if you learned ",
                    CreatedBy ="aemloviji",
                    InsertDate = DateTime.Now,

                }
            };

            foreach (Post post in posts)
            {
                context.Post.Add(post);
            }
            context.SaveChanges();


            var comments = new Comment[]
            {
                new Comment {
                    Text ="I'm glad to introduce you new MS Azure",
                    InsertDate = DateTime.Now,
                    PostId=1
                },
                new Comment {
                    Text ="Cloud services is very trend now",
                    InsertDate = DateTime.Now,
                    PostId=1
                },
                new Comment {
                    Text ="I'm glad to introduce you new features of Spring Framework",
                    InsertDate = DateTime.Now,
                    PostId=2
                },
                new Comment {
                    Text ="I'm glad to introduce you new features in ROR development",
                    InsertDate = DateTime.Now,
                    PostId=3
                }
            };

            foreach (Comment comment in comments)
            {
                context.Comment.Add(comment);
            }
            context.SaveChanges();

        }

    }
}
