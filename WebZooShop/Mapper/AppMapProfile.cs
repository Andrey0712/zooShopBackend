﻿using AutoMapper;
using System.Globalization;
using WebZooShop.Data.Entities;
using WebZooShop.Data.Entities.Identity;
using WebZooShop.Model;

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
            CreateMap<CreateCategoryViewModel, ProductCategory>();
            CreateMap<ProductCategory, CategoryItemViewModel>();

            //мепери для продукт
            CreateMap<ProductAddViewModel, Product>()
                .ForMember(x => x.ProductImages, opt => opt.Ignore())
                .ForMember(x => x.DateCreate, opt => opt.MapFrom(x =>
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)))
                
                .ForMember(x => x.StartPhoto, opt => opt.Ignore())
                .ForMember(x => x.ProductCategoryId, opt => opt.MapFrom(opt => opt.ProductCategoryId))
                .ForMember(x => x.Name, opt => opt.MapFrom(opt => opt.Name))
                .ForMember(x => x.Price, opt => opt.MapFrom(opt => opt.Price))
                .ForMember(x => x.Description, opt => opt.MapFrom(opt => opt.Description));



            CreateMap<Product, ProductItemViewModel>()
               
               .ForMember(x => x.Price, opt => opt.MapFrom(x => x.Price.ToString(cultureInfo)))
               .ForMember(x => x.StartPhoto, opt => opt.MapFrom(x => $"/uploads/{x.StartPhoto}"));



        }


    }
}
