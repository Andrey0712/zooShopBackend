using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebZooShop.Constants;
using WebZooShop.Data.Entities.Identity;

namespace WebZooShop.Data.Entities
{
    public static class Seeder
    {
        public static void SeederData(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                try
                {
                    logger.LogInformation("Seeding Databases");//в консоль сообщает что сидится DB
                    var context = scope.ServiceProvider.GetRequiredService<AppEFContext>();//создаем контекст и дальще накатіваем миграцию
                    context.Database.Migrate();

                    SeedRole(services);//сидим роли
                    SeedCateory(services);
                    SeedInventoryStatus(services);
                    SeedProduct(services);
                    SeedOrderStatuses(services);

                }
                catch (Exception)
                {

                    throw;
                }

            }
        }


        private static void SeedRole(IServiceProvider service)
        {
            var roleManeger = service.GetRequiredService<RoleManager<AppRole>>();
            var userManeger = service.GetRequiredService<UserManager<AppUser>>();

            if (!roleManeger.Roles.Any())
            {
                var rez = roleManeger.CreateAsync(new AppRole
                {
                    Name = Roles.Admin
                }).Result;
                rez = roleManeger.CreateAsync(new AppRole
                {
                    Name = Roles.User
                }).Result;
            }

            if (!userManeger.Users.Any())
            {
                var user = new AppUser
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    FirstName = "Василь",
                    SecondName = "Василько",
                    Phone = "+38098839384",
                    
                };
                var result = userManeger.CreateAsync(user, "qwerty1").Result;
                if (result.Succeeded)
                {
                    result = userManeger.AddToRoleAsync(user, Roles.Admin).Result;
                }
            }

        }

        private static void SeedOrderStatuses(IServiceProvider service)
        {
            var context = service.GetRequiredService<AppEFContext>();
            if (!context.OrderStatuses.Any())
            {
                context.OrderStatuses
                        .Add(new OrderStatusEntity
                        {
                            Name = "Нове замовленя"
                        });
               
                context.OrderStatuses
                    .Add(new OrderStatusEntity
                    {
                        Name = "Відправлено"
                    });

                context.OrderStatuses
                    .Add(new OrderStatusEntity
                    {
                        Name = "Відхилено"
                    });

                context.SaveChanges();
            }

        }

        private static void SeedCateory(IServiceProvider service)
        {
            var context = service.GetRequiredService<AppEFContext>();
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(new List<CategoryEntity>
                {
                    new CategoryEntity
                    {
                        Name="Корм"
                    },
                    new CategoryEntity
                    {
                        Name="Вітаміни"
                    },
                    new CategoryEntity
                    {
                        Name="Іграшки"
                    },
                    new CategoryEntity
                    {
                        Name="Ветеринарні препарати"
                    }
                });
                context.SaveChanges();
            }

        }

        private static void SeedInventoryStatus(IServiceProvider service)
        {
            var context = service.GetRequiredService<AppEFContext>();
            if (!context.InventoryStatus.Any())
            {
                context.InventoryStatus.AddRange(new List<InventoryStatusEntity>
                {
                    new InventoryStatusEntity
                    {
                        Name="У наявності"
                    },
                    new InventoryStatusEntity
                    {
                        Name="Очікуєм"
                    },
                    
                });
                context.SaveChanges();
            }

        }

        /*private static void SeedUserProd(IServiceProvider service)
        {
            var context = service.GetRequiredService<AppEFContext>();
            if (!context.UserProduct.Any())
            {
                var user = context.Users.FirstOrDefault();
                var product = context.Products.FirstOrDefault();
                UserProduct userProduct = new UserProduct
                {
                    UserId = user.Id,
                    ProductId = product.Id
                };
                context.UserProduct.Add(userProduct);
                context.SaveChanges();
            }

        }*/


        private static void SeedProduct(IServiceProvider service)
        {

            var context = service.GetRequiredService<AppEFContext>();

            if (!context.Products.Any())
            {
                var productCategory = context.Categories.FirstOrDefault();
                var productInventoryStatus = context.InventoryStatus.FirstOrDefault();

                ProductEntity product = new ProductEntity
                {
                   CategoryId = productCategory.Id,
                    InventoryStatusId = productInventoryStatus.Id,
                    Name = "Royal Canin",
                    Description = "Корм для дорослих собак середніх порід (чия доросла вага становить від 11 до 25 кг) у віці від 12 місяців до 7 років",
                    //DateCreate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    Price = 500,
                    StartPhoto = "1xxn0a2u.a1p.jpg",
                    //InventoryStatus= "У наявності",
                    Rating=3
                    /*ProductImages = new List<ProductImage>
                     {

                         new ProductImage
                         {
                             ProductId= 1,
                             Name ="https://zoolove.com.ua/components/com_jshopping/files/img_products/full_royal_canin_medium_adult_sostav_4_kg_zoolove_com_ua.jpg"
                         },
                         new ProductImage
                         {
                             ProductId=1,
                             Name="https://images.paws.com/image/upload/b_rgb:FFFFFF,c_pad,dpr_3.0,f_auto,h_400,q_auto,w_400/c_pad,h_400,w_400/v1/product/I0045443_en_06?pgw=1"
                         }
                     }*/

                };

                context.Products.Add(product);

                context.SaveChanges();

            }

        }
    }
}

