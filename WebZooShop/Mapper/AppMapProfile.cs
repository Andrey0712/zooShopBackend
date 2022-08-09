using AutoMapper;
using System.Globalization;
using WebZooShop.Data.Entities;
using WebZooShop.Data.Entities.Identity;
using WebZooShop.Model;
using static WebZooShop.Model.CartViewModels;


namespace WebZooShop.Mapper
{
    public class AppMapProfile : Profile
    {
        public AppMapProfile()
        {
            var cultureInfo = new CultureInfo("uk-UA");

            CreateMap<RegisterViewModel, AppUser>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(opt => opt.FirstName))
                .ForMember(x => x.SecondName, opt => opt.MapFrom(opt => opt.SecondName))
                .ForMember(x => x.Email, opt => opt.MapFrom(opt => opt.Email))
                .ForMember(x => x.Phone, opt => opt.MapFrom(opt => opt.Phone));

            //мепер для вівода юзера
            CreateMap<AppUser, UserItemViewModel>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(opt => opt.FirstName))
                .ForMember(x => x.Email, opt => opt.MapFrom(opt => opt.Email));
                

            CreateMap<UserEditViewModel, AppUser>()
                
                .ForMember(x => x.UserName, opt => opt.Ignore())
                .ForMember(x => x.FirstName, opt => opt.MapFrom(opt => opt.FirstName))
                .ForMember(x => x.SecondName, opt => opt.MapFrom(opt => opt.SecondName))
                .ForMember(x => x.Email, opt => opt.Ignore())
                .ForMember(x => x.Phone, opt => opt.MapFrom(opt => opt.Phone));


            //мепери для категорії
            CreateMap<CreateCategoryViewModel, CategoryEntity>();
            CreateMap<CategoryEntity, CategoryItemViewModel>();

            //мепери для продукт
            CreateMap<ProductAddViewModel, ProductEntity>()
                //.ForMember(x => x.ProductImages, opt => opt.Ignore())
                .ForMember(x => x.DateCreated, opt => opt.MapFrom(x =>
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)))

               .ForMember(x => x.StartPhoto, opt => opt.Ignore())
               .ForMember(x => x.CategoryId, opt => opt.MapFrom(opt => opt.CategoryId))
               .ForMember(x => x.Name, opt => opt.MapFrom(opt => opt.Name))
               .ForMember(x => x.Price, opt => opt.MapFrom(opt => opt.Price))
               .ForMember(x => x.Description, opt => opt.MapFrom(opt => opt.Description))
            .ForMember(x => x.InventoryStatusId, opt => opt.MapFrom(opt => 1));



            /*CreateMap<ProductEntity, ProductItemViewModel>()

               .ForMember(x => x.Price, opt => opt.MapFrom(x => x.Price.ToString(cultureInfo)))
               .ForMember(x => x.StartPhoto, opt => opt.MapFrom(x => $"/uploads/{x.StartPhoto}"));*/

            CreateMap<ProductEntity, ProductItemViewModel>()
               .ForMember(x => x.Image, opt => opt.MapFrom(x => $"uploads/{x.StartPhoto}"))
               .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name))
            .ForMember(x => x.InventoryStatus, opt => opt.MapFrom(x => x.InventoryStatus.Name));
            //.ForMember(x => x.InventoryStatus, opt => opt.MapFrom(x => "Очікуєм"))
            //.ForMember(x => x.InventoryStatus, opt => opt.MapFrom(x => "У наявності"))
            //.ForMember(x => x.Rating, opt => opt.MapFrom(x => new Random().Next(1, 5)));

            CreateMap<CartAddViewModel, CartEntity>();

            CreateMap<CartEntity, CartItemViewModel>()
               .ForMember(x => x.ProductName, opt => opt.MapFrom(x => x.Product.Name))
               .ForMember(x => x.ProductImage, opt => opt.MapFrom(x => $"uploads/{x.Product.StartPhoto}"))
               .ForMember(x => x.ProductPrice, opt => opt.MapFrom(x => x.Product.Price));



            CreateMap<OrderStatusEntity, OrderStatusItemViewModel>();

            CreateMap<OrderAddViewModel, OrderEntity>()
                .ForMember(x => x.DateCreated, opt => opt.MapFrom(x =>
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)))
                .ForMember(x => x.OrderItems, opt => opt.Ignore());
            //.ForMember(x => x.OrderStatus, opt => opt.MapFrom(x => "Нове замовленя"));

            CreateMap<OrderItemAddViewModel, OrderItemEntity>();

            CreateMap<OrderItemEntity, OrderItemViewModel>()
                .ForMember(x => x.ProductName, opt => opt.MapFrom(x => x.Product.Name));
                //.ForMember(x => x.ProductImage, opt => opt.MapFrom(x => x.Product.ProductImages.Select(x => x.Name)));

            CreateMap<OrderEntity, OrderViewModel>()
                .ForMember(x => x.DateCreated, opt => opt.MapFrom(x => x.DateCreated.ToString("dd.MM.yyyy HH:mm:ss")))
                .ForMember(x => x.StatusName, opt => opt.MapFrom(x => x.OrderStatus.Name))
                .ForMember(x => x.Items, opt => opt.MapFrom(x => x.OrderItems));
        }


    }
}
