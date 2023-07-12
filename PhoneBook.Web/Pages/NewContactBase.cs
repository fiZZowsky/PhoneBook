using Microsoft.AspNetCore.Components;
using PhoneBook.Models.Dtos;
using PhoneBook.Web.Services.Contracts;

namespace PhoneBook.Web.Pages
{
    public class NewContactBase : ComponentBase
    {
        //        [Inject]
        //        public ICategoriesService CategoriesService { get; set; }

        [Inject]
        public IContactService ContactService { get; set; }

        //        [Inject]
        //        public ISubcategoriesService SubcategoriesService { get; set; }

        [Parameter]
        public IEnumerable<CategoriesDto> Categories { get; set; }

        [Parameter]
        public IEnumerable<SubcategoriesDto> Subcategories { get; set; }

        //        //protected override async Task OnInitializedAsync()
        //        //{
        //        //    Categories = await CategoriesService.GetCategories();
        //        //    Subcategories = await SubcategoriesService.GetSubcategories();
        //        //}
    }
}
