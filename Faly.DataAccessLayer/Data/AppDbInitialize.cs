using Faly.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Faly.DataAccessLayer.Data;

public class AppDbInitialize
{
    public static async Task InitializeRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roles = { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    public static async Task InitializeUser(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string adminEmail = "dev@ssttek.com";
        string adminUserName = "ssttek";
        string adminPassword = "Ssttek123";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = adminUserName,
                Email = adminEmail,
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                // Admin rolünü atama
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }

    public static async Task InitializeCourse(AppDbContext context)
    {
        if (!await context.Category.AnyAsync())
        {
            // Seed Kategoriler
            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Technology",
                    Description = "Tech-related courses",
                    IsActive = true,
                },
                new Category
                {
                    Name = "Business",
                    Description = "Courses for business professionals",
                    IsActive = true,
                },
                new Category
                {
                    Name = "Art & Design",
                    Description = "Creative art and design courses",
                    IsActive = true,
                },
            };

            await context.Category.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }

        if (!await context.Courses.AnyAsync())
        {
            // Seed Kurslar
            var courses = new List<Course>
            {
                new Course
                {
                    Name = "Introduction to Python",
                    Description = "Learn Python from scratch",
                    Price = 99.99m,
                    IsActive = true,
                },
                new Course
                {
                    Name = "Advanced Excel",
                    Description = "Excel for professionals",
                    Price = 79.99m,
                    IsActive = true,
                },
                new Course
                {
                    Name = "Graphic Design Basics",
                    Description = "Learn the basics of graphic design",
                    Price = 59.99m,
                    IsActive = true,
                },
                new Course
                {
                    Name = "Machine Learning 101",
                    Description = "Introduction to Machine Learning",
                    Price = 129.99m,
                    IsActive = true,
                },
                new Course
                {
                    Name = "Marketing Strategies",
                    Description = "Strategies to grow your business",
                    Price = 89.99m,
                    IsActive = true,
                },
                new Course
                {
                    Name = "Video Editing Mastery",
                    Description = "Master the art of video editing",
                    Price = 69.99m,
                    IsActive = true,
                },
            };

            await context.Courses.AddRangeAsync(courses);
            await context.SaveChangesAsync();

            // Seed CourseCategory
            var courseCategories = new List<CourseCategory>
            {
                new CourseCategory { CourseId = courses[0].Id, CategoryId = 1 },
                new CourseCategory { CourseId = courses[1].Id, CategoryId = 2 },
                new CourseCategory { CourseId = courses[2].Id, CategoryId = 3 },
                new CourseCategory { CourseId = courses[3].Id, CategoryId = 1 },
                new CourseCategory { CourseId = courses[4].Id, CategoryId = 2 },
                new CourseCategory { CourseId = courses[5].Id, CategoryId = 3 },
            };

            await context.CourseCategory.AddRangeAsync(courseCategories);
            await context.SaveChangesAsync();

            // Seed Course Sections
            foreach (var course in courses)
            {
                var sections = new List<CourseSection>
                {
                    new CourseSection
                    {
                        CourseId = course.Id,
                        Title = "Introduction",
                        Description = "Overview of the course",
                    },
                    new CourseSection
                    {
                        CourseId = course.Id,
                        Title = "Core Concepts",
                        Description = "Deep dive into the core topics",
                    },
                    new CourseSection
                    {
                        CourseId = course.Id,
                        Title = "Practical Examples",
                        Description = "Real-world examples and projects",
                    },
                    new CourseSection
                    {
                        CourseId = course.Id,
                        Title = "Conclusion",
                        Description = "Summary and next steps",
                    },
                };

                await context.CourseSection.AddRangeAsync(sections);
                await context.SaveChangesAsync();

                // Seed Videos
                foreach (var section in sections)
                {
                    var videos = new List<Video>
                    {
                        new Video
                        {
                            CourseSectionId = section.Id,
                            Title = $"{section.Title} - Part 1",
                            Url = "https://example.com/video1.mp4",
                            Description = "First part of the section",
                            DurationInSeconds = 300,
                        },
                        new Video
                        {
                            CourseSectionId = section.Id,
                            Title = $"{section.Title} - Part 2",
                            Url = "https://example.com/video2.mp4",
                            Description = "Second part of the section",
                            DurationInSeconds = 400,
                        },
                        new Video
                        {
                            CourseSectionId = section.Id,
                            Title = $"{section.Title} - Part 3",
                            Url = "https://example.com/video3.mp4",
                            Description = "Third part of the section",
                            DurationInSeconds = 500,
                        },
                    };

                    await context.Video.AddRangeAsync(videos);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
