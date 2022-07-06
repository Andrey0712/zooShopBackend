﻿using System.ComponentModel.DataAnnotations;

namespace WebZooShop.Model
{
    /// <summary>
    /// Модель для створення товару
    /// </summary>
    public class ProductAddViewModel
    {
        [Display(Name = "Назва"), Required(ErrorMessage = "Поле 'Назва' не може бути пустим!")]
        public string Name { get; set; }
        [Display(Name = "Ціна"), Required(ErrorMessage = "Поле 'Ціна' не може бути пустим!")]
        public int Price { get; set; }
        [Display(Name = "Опис товару"), Required(ErrorMessage = "Поле 'Опис товару' не може бути пустим!")]
        public string Description { get; set; }
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        [Display(Name = "Титульна фотографія")]
        public IFormFile StartPhoto { get; set; }
        //[Display(Name = "Дата створення")]
        //public DateTime DateCreate { get; set; }

       /* [Display(Name = "Фотографії")]
        public List<IFormFile> Images { get; set; }*/
    }

    /*public class ProductItemViewModel
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string StartPhoto { get; set; }

    }*/

    public class ProductItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        //public int? CategoryId { get; set; }
        public string Category { get; set; }
        public string InventoryStatus { get; set; }
        public int Rating { get; set; }

    }

    public class ProductDelViewModel
    {
        public int Id { get; set; }
        //public string Name { get; set; }

    }

    public class ProductViewModelImages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string StartPhoto { get; set; }
        public string DateCreate { get; set; }
        //public List<ProductImageItemViewModel> Images { get; set; }
    }

   /* public class ProductImageItemViewModel
    {
        public string Path { get; set; }
    }*/



    /// <summary>
    /// Модель для зміни товару
    /// </summary>
    public class ProductImageToEdit
    {
        [Display(Name = "Id"), Required(ErrorMessage = "Поле 'Опис товару' не може бути пустим!")]
        public int Id { get; set; }
        [Display(Name = "Назва")]
        public string Name { get; set; }
        [Display(Name = "Ціна")]
        public int Price { get; set; }
        [Display(Name = "Опис товару")]
        public string Description { get; set; }
        [Display(Name = "Титульна фотографія")]
        public IFormFile StartPhoto { get; set; }

       /* //public List<ProductImage> Images { get; set; }
        public List<IFormFile> Images { get; set; }//це для фото,які ми будемо додавати на вьюшці едіта.
        public List<string> deletedImages { get; set; }//це для фото,які ми будемо видаляти на вьюшці едіта.*/
    }
}
