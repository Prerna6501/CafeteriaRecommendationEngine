using ServerSide.Entity;

namespace ServerSide.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CafeteriaDbContext context)
        {
            context.Database.EnsureCreated();
            if (!context.UserTypes.Any())
            {
                context.AddRange(new UserType[]
                {
                    new UserType
                    {
                        Name = "Employee"
                    },
                    new UserType
                    {
                        Name ="Chef"
                    },
                    new UserType
                    {
                        Name ="Admin"
                    }
                });
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.AddRange(new[]
                {
                    new User
                    {
                        Name = "Prerna",
                        UserTypeId = 1,
                        PhoneNo = "9265369377",
                        Email = "prerna.mehta@intimetec.com",
                        Age = 23,
                        Password = "Test123"
                    },
                    new User
                    {
                        Name = "Chef",
                        UserTypeId = 2,
                        PhoneNo = "9263444377",
                        Email = "chef.one@intimetec.com",
                        Age = 30,
                        Password = "Test123"
                    },
                     new User
                    {
                        Name = "Admin",
                        UserTypeId = 3,
                        PhoneNo = "9244444377",
                        Email = "admin.one@intimetec.com",
                        Age = 28,
                        Password = "Test123"
                    },

                });
                context.SaveChanges();
            }

            if (!context.MealTypes.Any())
            {
                context.AddRange(new[]
                {
                    new MealType
                    {
                       Name = "Breakfast"
                    },
                    new MealType
                    {
                        Name = "Lunch"
                    },
                    new MealType
                    {
                        Name = "Dinner"
                    }
                });
                context.SaveChanges();
            }

            if (!context.MenuItemTypes.Any())
            {
                context.AddRange(new[]
                {
                    new MenuItemType
                    {
                       Name = "Beaverages"
                    },
                    new MenuItemType
                    {
                        Name = "Sandwiches"
                    },
                    new MenuItemType
                    {
                        Name = "Wraps"
                    },
                    new MenuItemType
                    {
                        Name = "Healthy Snacks"
                    },
                    new MenuItemType
                    {
                        Name = "Burgers"
                    },
                    new MenuItemType
                    {
                        Name = "Meals"
                    },
                    new MenuItemType
                    {
                        Name = "Breakfast"
                    }
                });
                context.SaveChanges();
            }

            if (!context.MenuItems.Any())
            {
                context.AddRange(new[]
                {
                new MenuItem
                {
                    Name = "Chai",
                    Price = 15,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 1
                },
                new MenuItem
                {
                    Name = "Boiled Eggs",
                    Price = 40,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 4
                },
                new MenuItem
                {
                    Name = "Peanut Salad",
                    Price = 60,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 4
                },
                new MenuItem
                {
                    Name = "Grilled Veg Sandwich",
                    Price = 70,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 2
                },
                new MenuItem
                {
                    Name = "Grilled Paneer Sandwich",
                    Price = 80,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 2
                },
                new MenuItem
                {
                    Name = "Cheese burger",
                    Price = 7,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 5
                },
                new MenuItem
                {
                    Name = "Fruit Salad",
                    Price = 5,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 4
                },
                new MenuItem
                {
                    Name = "Paneer Sabji",
                    Price = 2,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 6
                },
                new MenuItem
                {
                    Name = "Orange Juice",
                    Price = 3,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 1
                },
                new MenuItem
                {
                    Name = "Tandoori Wraps",
                    Price = 100,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 3
                },
                new MenuItem
                {
                    Name = "Mexican wrap",
                    Price = 60,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 3
                },
                new MenuItem
                {
                    Name = "Pizza",
                    Price = 1000,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 3
                },
                new MenuItem
                {
                    Name = "Ice cream",
                    Price = 25,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 5
                },
                new MenuItem
                {
                    Name = "Pina colada",
                    Price = 120,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 3
                },
                new MenuItem
                {
                    Name = "upma",
                    Price = 40,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 7
                },
                new MenuItem
                {
                    Name = "poha",
                    Price = 30,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 7
                },
                new MenuItem
                {
                    Name = "Ice tea",
                    Price = 25,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 3
                },
                new MenuItem
                {
                    Name = "Dosa",
                    Price = 50,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 7
                },
                new MenuItem
                {
                    Name = "Gatte sabji",
                    Price = 70,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 6
                },
                new MenuItem
                {
                    Name = "Mix Veg",
                    Price = 30,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 6
                },
                new MenuItem
                {
                    Name = "Kadhi pakoda",
                    Price = 45,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 6
                },
                new MenuItem
                {
                    Name = "Idli",
                    Price = 50,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 7
                },
                new MenuItem
                {
                    Name = "Chole sabji",
                    Price = 45,
                    IsAvailable = true,
                    IsDeleted = false,
                    MenuItemTypeId = 6
                }
            });
                context.SaveChanges();
            }

            if (!context.FixedMeals.Any())
            {
                context.AddRange(new[]
                {
                    new FixedMeal
                    {
                        MealTypeId = 1,
                        MenuItemId = 8,
                        PreparedDate = DateTime.Now
                    }
                });
                context.SaveChanges();
            }

            if (!context.NotificationTypes.Any())
            {
                context.AddRange(new[]
                {
                    new NotificationType
                    {
                        Name = "MenuItem Updates"
                    },
                    new NotificationType
                    {
                        Name = "Final Preparation"
                    },
                    new NotificationType
                    {
                        Name = "Choose the roll out Item"
                    },
                    new NotificationType
                    {
                        Name = "Unavailability"
                    },
                    new NotificationType
                    {
                        Name = "Deleted"
                    }
                });
                context.SaveChanges();
            }

            if (!context.Notifications.Any())
            {
                context.AddRange(new[]
                {
                    new Notification
                    {
                        NotificationTypeId = 1,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        IsRead = false,
                        Message = "Orange Juice was added in the menu"
                    }
                });
                context.SaveChanges();
            }

            if (!context.Feedbacks.Any())
            {
                context.AddRange(new[]
                {
                    new Feedback
                    {
                        MenuItemId = 9,
                        Comment = "Very Tasty",
                        CreatedDate = DateTime.Now,
                        UserId = 1
                    }
                });
                context.SaveChanges();
            }

            if (!context.VotingResults.Any())
            {
                context.AddRange(new[]
                {
                    new VotingResult
                    {
                        MealtypeId = 1,
                        MenuItemId = 23,
                        NoOfVotes = 2,
                        CreatedDate = DateTime.Now
                    },
                    new VotingResult
                    {
                        MealtypeId = 1,
                        MenuItemId = 16,
                        NoOfVotes = 12,
                        CreatedDate = DateTime.Now
                    },
                    new VotingResult
                    {
                        MealtypeId = 1,
                        MenuItemId = 17,
                        NoOfVotes = 7,
                        CreatedDate = DateTime.Now
                    },
                    new VotingResult
                    {
                        MealtypeId = 2,
                        MenuItemId = 20,
                        NoOfVotes = 2,
                        CreatedDate = DateTime.Now
                    },
                    new VotingResult
                    {
                        MealtypeId = 2,
                        MenuItemId = 21,
                        NoOfVotes = 12,
                        CreatedDate = DateTime.Now
                    },
                    new VotingResult
                    {
                        MealtypeId = 2,
                        MenuItemId = 22,
                        NoOfVotes = 7,
                        CreatedDate = DateTime.Now
                    },
                    new VotingResult
                    {
                        MealtypeId = 3,
                        MenuItemId = 25,
                        NoOfVotes = 4,
                        CreatedDate = DateTime.Now
                    },
                    new VotingResult
                    {
                        MealtypeId = 3,
                        MenuItemId = 16,
                        NoOfVotes = 3,
                        CreatedDate = DateTime.Now
                    },
                    new VotingResult
                    {
                        MealtypeId = 3,
                        MenuItemId = 8,
                        NoOfVotes = 7,
                        CreatedDate = DateTime.Now
                    }
                });
                context.SaveChanges();
            }

            if (!context.Feedbacks.Any())
            {
                context.AddRange(new[]
                {
                    new Feedback
                    {
                        MenuItemId = 9,
                        UserId = 1,
                        Rating = 4,
                        Comment = "Very tasty",
                        CreatedDate = DateTime.Now
                    }
                });
                context.SaveChanges();
            }
            return;
        }
    }
}
