﻿using PhoneBook.Api.Entities;
using PhoneBook.Models.Dtos;

namespace PhoneBook.Api.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<ContactDto> ConvertToDto(this IEnumerable<Contact> contacts,
                                                            IEnumerable<Category> categories,
                                                            IEnumerable<Subcategory> subcategories)
        {
            return (from contact in contacts
                    join category in categories
                    on contact.CategoryId equals category.Id
                    join subcategory in subcategories
                    on contact.SubcategoryId equals subcategory.Id
                    select new ContactDto
                    {
                        Id = contact.Id,
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        Email = contact.Email,
                        Password = contact.Password,
                        PhoneNumber = contact.PhoneNumber,
                        BirthDate = contact.BirthDate,
                        CategoryId = contact.CategoryId,
                        CategoryName = category.Name,
                        SubcategoryId = contact.SubcategoryId,
                        SubcategoryName = subcategory == null ? "" : subcategory.Name
                    }).ToList();
        }
        public static ContactDto ConvertToDto(this Contact contact,
                                                   Category category,
                                                   Subcategory subcategory)
        {
            return new ContactDto
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Password = contact.Password,
                PhoneNumber = contact.PhoneNumber,
                BirthDate = contact.BirthDate,
                CategoryId = contact.CategoryId,
                CategoryName = category.Name,
                SubcategoryId = contact.SubcategoryId,
                SubcategoryName = subcategory == null ? "" : subcategory.Name
            };
        }
        //public static IEnumerable<CategoryDto> ConvertToDto(this IEnumerable<Category> categories)
        //{
        //    return (from category in categories
        //            select new CategoryDto
        //            {
        //                Id = category.Id,
        //                Name = category.Name
        //            }).ToList();
        //}
    }
}
